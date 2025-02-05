using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using panitab_backend.Constants;
using panitab_backend.Database;
using panitab_backend.Database.Entities;
using panitab_backend.Dtos;
using panitab_backend.Dtos.Auth;
using panitab_backend.Helpers;
using panitab_backend.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


//TODO: falta implementar ->
// verificacion de correo
// recuperacion de contraseña
// cerrar sesion

namespace panitab_backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly PaniTabContext _context;
        //private readonly IMapper _mapper;
        //private readonly JwtSettings

        //para saber si ya esta logeado
        //private readonly HttpContext _httpContext;
        //private readonly string _USER_ID;

        public AuthService(
            SignInManager<UserEntity> signInManager,
            UserManager<UserEntity> userManager,
            IConfiguration configuration,
            ILogger<AuthService> logger,
            PaniTabContext context
            )
        //IMapper mapper, 
        //IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _context = context;
            //_mapper = mapper;
            //_httpContext = httpContextAccessor.HttpContext;
            //var idClaim = _httpContext.User.Claims.Where(x => x.Type == "UserId")
            //    .FirstOrDefault();
            //_USER_ID = idClaim?.Value;
        }

        public string GetUserId()
        {
            return "e84cc741-c1d1-4f12-88a3-5a9dcef3754b";
        }

        //login de usuario
        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto)
        {
            var result = await _signInManager
                .PasswordSignInAsync(dto.Email,
                                     dto.Password,
                                     isPersistent: false,
                                     lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //generar el token
                var userEntity = await _userManager.FindByEmailAsync(dto.Email);

                if (userEntity == null)
                {
                    return new ResponseDto<LoginResponseDto>
                    {
                        Status = false,
                        StatusCode = 401,
                        Message = "Usuario no encontrado"
                    };
                }
                //claimlist para el usuario
                List<Claim> authClaims = await GetClaims(userEntity);

                var jwtToken = GetToken(authClaims);

                var refreshToken = GenerateRefreshTokenString();

                userEntity.RefreshToken = refreshToken;
                userEntity.RefreshTokenExpire = DateTime.Now.AddMinutes(
                    int.Parse(_configuration["JWT:RefreshTokenExpire"] ?? "30"));

                //el entry es para actualizar el usuario
                _context.Entry(userEntity);
                await _context.SaveChangesAsync();

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Inicio de sesión correcto.",
                    Data = new LoginResponseDto
                    {
                        FullName = $"{userEntity.FirstName} {userEntity.LastName}",
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken), //convertir el token a string
                        TokenExpiration = jwtToken.ValidTo,
                        RefreshToken = refreshToken
                    }
                };
            }

            return new ResponseDto<LoginResponseDto>
            {
                Status = false,
                StatusCode = 401,
                Message = "Fallo el inicio de sesion"
            };
        }

        //refresh token
        public async Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync(RefreshTokenDto dto)
        {
            string email = "";

            try
            {
                var principal = GetTokenPrincipal(dto.Token);

                var emailClaim = principal.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
                var userIdClaim = principal.Claims.Where(x => x.Type == "UserId").FirstOrDefault();
                _logger.LogInformation($"Correo del Usuario es: {emailClaim.Value}");
                //_logger.LogInformation($"Id del Usuario es: {userIdCLaim.Value}");

                if (emailClaim is null)
                {
                    return new ResponseDto<LoginResponseDto>
                    {
                        StatusCode = 401,
                        Status = false,
                        Message = "Acceso no autorizado: No se encontro un correo valido."
                    };
                }

                email = emailClaim.Value;

                var userEntity = await _userManager.FindByEmailAsync(email);

                if (userEntity is null)
                {
                    return new ResponseDto<LoginResponseDto>
                    {
                        StatusCode = 401,
                        Status = false,
                        Message = "Acceso no autorizado: Usuario no existe."
                    };
                }

                if (userEntity.RefreshToken != dto.RefreshToken)
                {
                    return new ResponseDto<LoginResponseDto>
                    {
                        StatusCode = 401,
                        Status = false,
                        Message = "Acceso no autorizado: La sesion no es valida"
                    };

                }

                if (userEntity.RefreshTokenExpire < DateTime.Now)
                {
                    return new ResponseDto<LoginResponseDto>
                    {
                        StatusCode = 401,
                        Status = false,
                        Message = "Acceso no autorizado: La sesion ha expirado"
                    };
                }

                List<Claim> authClaims = await GetClaims(userEntity);

                var jwtToken = GetToken(authClaims);

                var loginResponseDto = new LoginResponseDto
                {
                    Email = email,
                    FullName = $"{userEntity.FirstName} {userEntity.LastName}",
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    TokenExpiration = jwtToken.ValidTo,
                    RefreshToken = GenerateRefreshTokenString()
                };

                userEntity.RefreshToken = loginResponseDto.RefreshToken;
                userEntity.RefreshTokenExpire = DateTime.Now.AddMinutes(
                    int.Parse(_configuration["JWT:RefreshTokenExpire"] ?? "30"));

                _context.Entry(userEntity);
                await _context.SaveChangesAsync();

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Token refrescado correctamente.",
                    Data = loginResponseDto
                };

            }
            catch (Exception e)
            {
                _logger.LogError(exception: e, message: e.Message);
                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Ocurrio un error al renovar el token"
                };
            }
        }

        //Registrar usuario
        public async Task<ResponseDto<LoginResponseDto>> RegisterAsync(CreateUserDto dto)
        {
            //verificar si el rol es valido
            var validRoles = new List<string> { RolesConstant.ADMIN, RolesConstant.STORE, RolesConstant.CHECKER, RolesConstant.OFFICE };

            if (!validRoles.Contains(dto.Role))
            {
                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "Rol invalido"
                };
            }

            var user = new UserEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.Email,
                Email = dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                var userEntity = await _userManager.FindByEmailAsync(dto.Email);

                //asignar rol
                await _userManager.AddToRoleAsync(userEntity, dto.Role);

                var authClaims = await GetClaims(userEntity);

                var jwtToken = GetToken(authClaims);

                var refreshToken = GenerateRefreshTokenString();
                _logger.LogInformation($"RefreshToken generado: {refreshToken}");

                userEntity.RefreshToken = refreshToken;
                userEntity.RefreshTokenExpire = DateTime.Now.AddMinutes(
                    int.Parse(_configuration["JWT:RefreshTokenExpire"] ?? "30"));

                _context.Entry(userEntity);
                await _context.SaveChangesAsync();

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Usuario registrado correctamente.",
                    Data = new LoginResponseDto
                    {
                        FullName = $"{userEntity.FirstName} {userEntity.LastName}",
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        TokenExpiration = jwtToken.ValidTo,
                        RefreshToken = refreshToken
                    }
                };
            }

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 400,
                Status = false,
                Message = "Error al registrar el usuario"
            };


        }

        public ClaimsPrincipal GetTokenPrincipal(string token)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value));

            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        private string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[64];

            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(
                    //se le agrega ?? para evitar que sea nulo
                    int.Parse(_configuration["JWT:Expires"] ?? "15")),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }

        private async Task<List<Claim>> GetClaims(UserEntity userEntity)
        {
            //crear las claims
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userEntity.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", userEntity.Id),
            };

            //agregar los roles del usuario
            var userRoles = await _userManager.GetRolesAsync(userEntity);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            return authClaims;
        }

    }
}



//consultar usuario por id
//    public async Task<ResponseDto<UserDto>> GetUserByIdAsync(string userId)
//    {
//        var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

//        if (userEntity == null)
//        {
//            return new ResponseDto<UserDto>
//            {
//                StatusCode = 404,
//                Status = false,
//                Message = "Usuario no encontrado.",
//                Data = null
//            };
//        }

//        var userDto = _mapper.Map<UserDto>(userEntity);

//        return new ResponseDto<UserDto>
//        {
//            StatusCode = 200,
//            Status = true,
//            Message = "Usuario encontrado.",
//            Data = userDto
//        };
//    }

//    //consultar todos los usuarios
//    public async Task<ResponseDto<List<UserDto>>> GetUsersAsync()
//    {
//        var userEntities = await _context.Users.ToListAsync();

//        var userDtos = _mapper.Map<List<UserDto>>(userEntities);

//        return new ResponseDto<List<UserDto>>
//        {
//            StatusCode = 200,
//            Status = true,
//            Message = "Usuarios encontrados.",
//            Data = userDtos
//        };
//    }

//    //registrar usuario
//    public async Task<ResponseDto<IdentityResult>> CreateUserAsync(CreateUserDto userDto)
//    {
//        var user = new UserEntity
//        {
//            FirstName = userDto.FirstName,
//            LastName = userDto.LastName,
//            UserName = userDto.UserName,
//            Email = userDto.Email,
//            IdentityNumber = userDto.IdentityNumber,
//            CreatedDate = DateTime.UtcNow
//        };

//        //verificar si el correo ya esta registrado
//        if (await _userManager.FindByEmailAsync(user.Email) != null)
//        {
//            return new ResponseDto<IdentityResult>
//            {
//                StatusCode = 400,
//                Status = false,
//                Message = "El correo ya está registrado.",
//                Data = IdentityResult.Failed(new IdentityError { Description = "Email is already registered." })
//            };
//        }

//        var result = await _userManager.CreateAsync(user, userDto.Password);

//        if (!result.Succeeded)
//        {
//            return new ResponseDto<IdentityResult>
//            {
//                StatusCode = 500,
//                Status = false,
//                Message = "Error al registrar el usuario.",
//                Data = result
//            };
//        }

//        //verificar si se especifico un rol
//        if (!await _userManager.IsInRoleAsync(user, userDto.Role))
//        {
//            var roleExists = await _context.Roles.AnyAsync(x => x.Name == userDto.Role);
//            if (!roleExists) {
//                return new ResponseDto<IdentityResult>
//                {
//                    StatusCode = 400,
//                    Status = false,
//                    Message = "El rol especificado no existe.",
//                    Data = IdentityResult.Failed(new IdentityError { Description = "Role does not exist." })
//                };
//            }
//        }

//        //asignar rol
//        var roleResult = await _userManager.AddToRoleAsync(user, userDto.Role);
//        if (!roleResult.Succeeded)
//        {
//            return new ResponseDto<IdentityResult>
//            {
//                StatusCode = 500,
//                Status = false,
//                Message = "Error al asignar el rol al usuario.",
//                Data = roleResult
//            };
//        }

//        return new ResponseDto<IdentityResult>
//        {
//            StatusCode = 200,
//            Status = true,
//            Message = "Usuario registrado correctamente.",
//            Data = result
//        };
//    }



//    //generar token
//    private JwtSecurityToken GenerateJwtToken(List<Claim> authClaims) 
//    {
//        var authSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

//        var token = new JwtSecurityToken(
//            issuer: _configuration["JWT:ValidIssuer"],
//            audience: _configuration["JWT:ValidAudience"],
//            expires: DateTimeUtils.GetHondurasDateTime().AddMinutes(
//                int.Parse(_configuration["JWT:ExpiriryMinutes"]!)),

//            //expires: DateTime.Now.AddHours(1),
//            claims: authClaims,
//            signingCredentials: new SigningCredentials(
//                authSigningKey, SecurityAlgorithms.HmacSha256)
//        );
//        return token;
//    }

//    //generar refresh token cuando se inicia sesion
//    private string GenerateRefreshTokenString()
//    {
//        var randomNumber = new byte[64];

//        using (var numberGenerator = RandomNumberGenerator.Create())
//        {
//            numberGenerator.GetBytes(randomNumber);
//        }

//        return Convert.ToBase64String(randomNumber);
//    }

//    //refrescar token de usuario
//    public async Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync(RefreshTokenDto dto)
//    {
//        string email = "";

//        try
//        {
//            _logger.LogInformation($"Token recibido: {dto.Token}");
//            _logger.LogInformation($"RefreshToken recibido: {dto.RefreshToken}");
//            //obtener el token principal
//            var principal = GetTokenPrincipal(dto.Token!); //se niega para evitar que sea nulo

//            // Verificar si el principal es nulo
//            if (principal == null)
//            {
//                _logger.LogWarning("Principal obtenido del token es nulo.");
//                return new ResponseDto<LoginResponseDto>
//                {
//                    StatusCode = 400,
//                    Status = false,
//                    Message = "Acceso invalido: Token inválido."
//                };
//            }

//            //obtener la claim de name con url del token
//            //var emailClaim = principal.FindFirst(JwtRegisteredClaimNames.Email);
//            var emailClaim = principal.FindFirst(ClaimTypes.Email);

//            //verificar si el claim de email esta vacio
//            if (emailClaim is null)
//            {
//                _logger.LogWarning("El token no contiene la claim de nombre (ClaimTypes.Name).");
//                return new ResponseDto<LoginResponseDto>
//                {
//                    StatusCode = 400,
//                    Status = false,
//                    Message = "Acceso invalido: Token inválido."
//                };
//            }

//            email = emailClaim.Value;
//            _logger.LogInformation($"Email obtenido del token: {email}");

//            //buscar el usuario por el email
//            var userEntity = await _userManager.FindByEmailAsync(email);

//            //verificar si el usuario existe
//            if (userEntity is null)
//            {
//                _logger.LogWarning($"No se encontró un usuario con el email: {email}");
//                return new ResponseDto<LoginResponseDto>
//                {
//                    StatusCode = 401,
//                    Status = false,
//                    Message = "Acceso invalido: Usuario no encontrado."
//                };
//            }

//            _logger.LogInformation($"RefreshToken en la base de datos: {userEntity.RefreshToken}");
//            //verificar si el refresh token es igual al de la base de datos
//            if (userEntity.RefreshToken != dto.RefreshToken)
//            {
//                _logger.LogWarning("El RefreshToken no coincide con el de la base de datos.");
//                return new ResponseDto<LoginResponseDto>
//                {
//                    StatusCode = 401,
//                    Status = false,
//                    Message = "Acceso invalido: Refresh token inválido."
//                };
//            }

//            //verificar si el refresh token ha expirado
//            if (userEntity.RefreshTokenDate < DateTimeUtils.GetHondurasDateTime())
//            {
//                _logger.LogWarning("El RefreshToken ha expirado.");
//                return new ResponseDto<LoginResponseDto>
//                {
//                    StatusCode = 401,
//                    Status = false,
//                    Message = "Acceso invalido: Refresh token expirado."
//                };
//            }
//            _logger.LogInformation("Generando un nuevo token y refresh token.");

//            List<Claim> authClaims = await GetClaims(userEntity);

//            //generar nuevo token y refresh token
//            var jwtToken = GetToken(authClaims);


//            var userRoles = await _userManager.GetRolesAsync(userEntity);

//            //se crea la respuesta con el nuevo token y la informacion del usuario
//            var loginResponseDto = new LoginResponseDto
//            {
//                Email = userEntity.Email,
//                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
//                Username = userEntity.UserName,
//                TokenExpiration = jwtToken.ValidTo,
//                Roles = userRoles.ToList()
//            };

//            //Actualizar el token de actualizacion y la fecha de expiracion
//            loginResponseDto.RefreshToken = this.GenerateRefreshTokenString();

//            userEntity.RefreshToken = loginResponseDto.RefreshToken;
//            //se le suma el tiempo de expiracion del refresh token
//            userEntity.RefreshTokenDate = DateTimeUtils.GetHondurasDateTime().AddMinutes(
//                int.Parse(_configuration["JWT:RefreshTokenExpiriry"]!));

//            await _context.SaveChangesAsync();
//            _logger.LogInformation("Token refrescado correctamente.");

//            //devolver la respuesta con el nuevo token
//            return new ResponseDto<LoginResponseDto>
//            {
//                StatusCode = 200,
//                Status = true,
//                Message = "Token refrescado correctamente.",
//                Data = loginResponseDto
//            };

//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error al refrescar el token.");
//            return new ResponseDto<LoginResponseDto>
//            {
//                StatusCode = 500,
//                Status = false,
//                Message = "Error al refrescar el token.",
//                Data = null
//            };
//        }
//    }

//    //obtener el token principal
//    public ClaimsPrincipal GetTokenPrincipal(string token)
//    {
//        var securityKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value));

//        var validation = new TokenValidationParameters
//        {
//            IssuerSigningKey = securityKey,
//            ValidateLifetime = true, //se cambia a true para evitar procesar tokens expirados
//            ValidateActor = false,
//            ValidateAudience = false,
//            ValidateIssuer = false,
//        };
//        return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
//    }

//    //obtener las claims del usuario
//    public async Task<List<Claim>> GetClaims(UserEntity userEntity)
//    {
//        //crear las claims
//        var authClaims = new List<Claim>
//        {
//            new Claim(ClaimTypes.Name, userEntity.UserName),
//            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//            new Claim("UserId", userEntity.Id),

//        };

//        //agregar los roles del usuario
//        var userRoles = await _userManager.GetRolesAsync(userEntity);
//        foreach (var role in userRoles)
//        {
//            authClaims.Add(new Claim(ClaimTypes.Role, role));
//        }

//        return authClaims;
//    }

//    //obtener token despues de refrescar
//    public JwtSecurityToken GetToken(List<Claim> authClaims)
//    {
//        var authSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

//        var token = new JwtSecurityToken(
//            issuer: _configuration["JWT:ValidIssuer"],
//            audience: _configuration["JWT:ValidAudience"],
//            expires: DateTimeUtils.GetHondurasDateTime().AddMinutes(
//                int.Parse(_configuration["JWT:ExpiriryMinutes"]!)),
//            claims: authClaims,
//            signingCredentials: new SigningCredentials(
//                authSigningKey, SecurityAlgorithms.HmacSha256)
//        );

//        return token;
//    }

//    //generar string de token despues del refresh



//    //editar usuario
//    public async Task<ResponseDto<IdentityResult>> UpdateUserAsync(UpdateUserDto userDto)
//    {
//        var userEntity = await _userManager.FindByIdAsync(userDto.UserId);

//        if (userEntity == null)
//        {
//            return new ResponseDto<IdentityResult>
//            {
//                StatusCode = 404,
//                Status = false,
//                Message = "Usuario no encontrado.",
//                Data = IdentityResult.Failed(new IdentityError { Description = "User not found." })
//            };
//        }

//        //actualizar datos
//        userEntity.FirstName = userDto.FirstName;
//        userEntity.LastName = userDto.LastName;
//        userEntity.Email = userDto.Email;
//        userEntity.IdentityNumber = userDto.IdentityNumber;
//        userEntity.IsInactive = userDto.IsInactive;

//        var result = await _userManager.UpdateAsync(userEntity);

//        if (!result.Succeeded)
//        {
//            return new ResponseDto<IdentityResult>
//            {
//                StatusCode = 500,
//                Status = false,
//                Message = "Error al editar el usuario.",
//                Data = result
//            };
//        }

//        //cambiar rol
//        if (!await _userManager.IsInRoleAsync(userEntity, userDto.Role))
//        {
//            var roleExists = await _context.Roles.AnyAsync(x => x.Name == userDto.Role);
//            if (!roleExists)
//            {
//                return new ResponseDto<IdentityResult>
//                {
//                    StatusCode = 400,
//                    Status = false,
//                    Message = "El rol especificado no existe.",
//                    Data = IdentityResult.Failed(new IdentityError { Description = "Role does not exist." })
//                };
//            }

//            //quitar rol anterior
//            var userRoles = await _userManager.GetRolesAsync(userEntity);
//            var removeRoleResult = await _userManager.RemoveFromRolesAsync(userEntity, userRoles);
//            if (!removeRoleResult.Succeeded)
//            {
//                return new ResponseDto<IdentityResult>
//                {
//                    StatusCode = 500,
//                    Status = false,
//                    Message = "Error al quitar el rol anterior.",
//                    Data = removeRoleResult
//                };
//            }

//            //asignar nuevo rol
//            var roleResult = await _userManager.AddToRoleAsync(userEntity, userDto.Role);
//            if (!roleResult.Succeeded)
//            {
//                return new ResponseDto<IdentityResult>
//                {
//                    StatusCode = 500,
//                    Status = false,
//                    Message = "Error al asignar el nuevo rol al usuario.",
//                    Data = roleResult
//                };
//            }
//        }

//        return new ResponseDto<IdentityResult>
//        {
//            StatusCode = 200,
//            Status = true,
//            Message = "Usuario editado correctamente.",
//            Data = result
//        };
//    }

//    //eliminar usuario
//    public async Task<ResponseDto<IdentityResult>> DeleteUserAsync(string userId)
//    {
//        var userEntity = await _userManager.FindByIdAsync(userId);

//        if (userEntity == null)
//        {
//            return new ResponseDto<IdentityResult>
//            {
//                StatusCode = 404,
//                Status = false,
//                Message = "Usuario no encontrado.",
//                Data = IdentityResult.Failed(new IdentityError { Description = "User not found." })
//            };
//        }

//        var result = await _userManager.DeleteAsync(userEntity);

//        if (!result.Succeeded)
//        {
//            return new ResponseDto<IdentityResult>
//            {
//                StatusCode = 500,
//                Status = false,
//                Message = "Error al eliminar el usuario.",
//                Data = result
//            };
//        }

//        return new ResponseDto<IdentityResult>
//        {
//            StatusCode = 200,
//            Status = true,
//            Message = "Usuario eliminado correctamente.",
//            Data = result
//        };
//    }
//}
