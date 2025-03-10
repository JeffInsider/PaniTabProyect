﻿using AutoMapper;
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

namespace panitab_backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly PaniTabContext _context;

        public AuthService(
            SignInManager<UserEntity> signInManager,
            UserManager<UserEntity> userManager,
            IConfiguration configuration,
            ILogger<AuthService> logger,
            PaniTabContext context
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public string GetUserId()
        {
            return "e84cc741-c1d1-4f12-88a3-5a9dcef3754b";
        }

        //login de usuario
        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto)
        {
            //buscar el usuario antes de hacer el login
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

            //bloquear el usuario si esta inactivo
            if (!userEntity.IsActive)
            {
                return new ResponseDto<LoginResponseDto>
                {
                    Status = false,
                    StatusCode = 403,
                    Message = "Usuario inactivo"
                };
            }

            var result = await _signInManager
                .PasswordSignInAsync(dto.Email,
                                     dto.Password,
                                     isPersistent: false,
                                     lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //generar el token
                //var userEntity = await _userManager.FindByEmailAsync(dto.Email);

                //if (userEntity == null)
                //{
                //    return new ResponseDto<LoginResponseDto>
                //    {
                //        Status = false,
                //        StatusCode = 401,
                //        Message = "Usuario no encontrado"
                //    };
                //}

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
        //public async Task<ResponseDto<LoginResponseDto>> RegisterAsync(CreateUserDto dto)
        //{
        //    //verificar si el rol es valido
        //    var validRoles = new List<string> 
        //    { 
        //        RolesConstant.ADMIN, 
        //        RolesConstant.STORE, 
        //        RolesConstant.CHECKER, 
        //        RolesConstant.OFFICE, 
        //        RolesConstant.REPORTS };

        //    if (!validRoles.Contains(dto.Role))
        //    {
        //        return new ResponseDto<LoginResponseDto>
        //        {
        //            StatusCode = 400,
        //            Status = false,
        //            Message = "Rol invalido"
        //        };
        //    }

        //    var user = new UserEntity
        //    {
        //        FirstName = dto.FirstName,
        //        LastName = dto.LastName,
        //        UserName = dto.Email,
        //        Email = dto.Email,
        //    };

        //    var result = await _userManager.CreateAsync(user, dto.Password);

        //    if (result.Succeeded)
        //    {
        //        var userEntity = await _userManager.FindByEmailAsync(dto.Email);

        //        //asignar rol
        //        await _userManager.AddToRoleAsync(userEntity, dto.Role);

        //        var authClaims = await GetClaims(userEntity);

        //        var jwtToken = GetToken(authClaims);

        //        var refreshToken = GenerateRefreshTokenString();
        //        _logger.LogInformation($"RefreshToken generado: {refreshToken}");

        //        userEntity.RefreshToken = refreshToken;
        //        userEntity.RefreshTokenExpire = DateTime.Now.AddMinutes(
        //            int.Parse(_configuration["JWT:RefreshTokenExpire"] ?? "30"));

        //        _context.Entry(userEntity);
        //        await _context.SaveChangesAsync();

        //        return new ResponseDto<LoginResponseDto>
        //        {
        //            StatusCode = 200,
        //            Status = true,
        //            Message = "Usuario registrado correctamente.",
        //            Data = new LoginResponseDto
        //            {
        //                FullName = $"{userEntity.FirstName} {userEntity.LastName}",
        //                Email = userEntity.Email,
        //                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
        //                TokenExpiration = jwtToken.ValidTo,
        //                RefreshToken = refreshToken
        //            }
        //        };
        //    }

        //    return new ResponseDto<LoginResponseDto>
        //    {
        //        StatusCode = 400,
        //        Status = false,
        //        Message = "Error al registrar el usuario"
        //    };


        //}

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
