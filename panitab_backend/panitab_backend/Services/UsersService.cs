using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Constants;
using panitab_backend.Database;
using panitab_backend.Database.Entities;
using panitab_backend.Dtos;
using panitab_backend.Dtos.Users;
using panitab_backend.Services.Interfaces;

namespace panitab_backend.Services
{
    public class UsersService : IUsersService
    {
        private readonly PaniTabContext _context;
        private readonly IMapper _mapper;
        //necesario para traer los roles de los usuarios
        private readonly UserManager<UserEntity> _userManager;

        public UsersService(PaniTabContext context, IMapper mapper, UserManager<UserEntity> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        //obtener todos los usuarios
        public async Task<ResponseDto<List<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                var usersEntity = await _context.Users.ToListAsync();

                if (usersEntity == null || !usersEntity.Any())
                {
                    return new ResponseDto<List<UserDto>>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No hay usuarios registrados",
                        Data = new List<UserDto>()
                    };
                }

                //aqui obtenemos los roles de los usuarios

                var usersDto = new List<UserDto>();

                foreach (var user in usersEntity)
                {
                    var userDto = _mapper.Map<UserDto>(user);

                    //obtener los roles
                    var roles = await _userManager.GetRolesAsync(user);

                    //Agregar los roles al DTO, puedes decidir cómo agregarlo en tu DTO (como una lista de roles)
                    userDto.Roles = roles.ToList();

                    usersDto.Add(userDto);
                }

                //se comento el mapper porque no se esta utilizando
                //var usersDto = _mapper.Map<List<UserDto>>(usersEntity);

                return new ResponseDto<List<UserDto>>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Lista de usuarios",
                    Data = usersDto
                };
            }
            catch (Exception e)
            {
                return new ResponseDto<List<UserDto>>()
                {
                    Data = new List<UserDto>(),
                    Message = e.Message,
                    Status = false,
                    StatusCode = 500
                };
            }
        }

        //consultar un usuario por id
        public async Task<ResponseDto<UserDto>> GetUserByIdAsync(string id)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userEntity == null)
            {
                return new ResponseDto<UserDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Usuario no encontrado",
                    Data = null
                };
            }

            //obtener los roles
            var roles = await _userManager.GetRolesAsync(userEntity);

            var userDto = _mapper.Map<UserDto>(userEntity);

            //Agregar los roles al DTO, puedes decidir cómo agregarlo en tu DTO (como una lista de roles)
            userDto.Roles = roles.ToList();

            return new ResponseDto<UserDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Usuario encontrado",
                Data = userDto
            };
        }

        //crear un usuario

        public async Task<ResponseDto<UserDto>> CreateUserAsync(CreateUserDto dto)
        {
            try
            {
                var validRoles = new List<string>
                {
                    RolesConstant.ADMIN,
                    RolesConstant.STORE,
                    RolesConstant.CHECKER,
                    RolesConstant.OFFICE,
                    RolesConstant.REPORTS
                };

                if (!validRoles.Contains(dto.Role))
                {
                    return new ResponseDto<UserDto>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "Rol no válido",
                        Data = null
                    };
                }

                //crear al usuario:
                var user = new UserEntity
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    UserName = dto.Email
                };

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (result.Succeeded)
                {
                    //buscar usuario por correo
                    var userEntity = await _userManager.FindByEmailAsync(dto.Email);

                    //asignar el rol
                    await _userManager.AddToRoleAsync(userEntity, dto.Role);

                    //obtener los roles
                    var roles = await _userManager.GetRolesAsync(userEntity);

                    //mapear al usuario
                    var userDto = _mapper.Map<UserDto>(userEntity);
                    //asignar los roles al DTO
                    userDto.Roles = roles.ToList();

                    return new ResponseDto<UserDto>
                    {
                        Status = true,
                        StatusCode = 200,
                        Message = "Usuario creado",
                        Data = userDto
                    };

                }

                return new ResponseDto<UserDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Error al crear el usuario",
                    Data = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDto<UserDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
            }
        }

        //actualizar un usuario
        public async Task<ResponseDto<UserDto>> UpdateUserAsync(UpdateUserDto dto)
        {
            try
            {
                var userEntity = await _userManager.FindByIdAsync(dto.Id);

                if (userEntity == null)
                {
                    return new ResponseDto<UserDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Usuario no encontrado",
                        Data = null
                    };
                }

                //actualizar los datos
                userEntity.FirstName = dto.FirstName;
                userEntity.LastName = dto.LastName;
                userEntity.Email = dto.Email;
                userEntity.UserName = dto.Email;

                var result = await _userManager.UpdateAsync(userEntity);
                if (!result.Succeeded)
                {
                    return new ResponseDto<UserDto>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "Error al actualizar el usuario",
                        Data = null
                    };
                }

                //manejo de roles
                var currentRoles = await _userManager.GetRolesAsync(userEntity);
                if (!currentRoles.Contains(dto.Role))
                {
                    //remover roles anteriores y asignar el nuevo
                    await _userManager.RemoveFromRolesAsync(userEntity, currentRoles);
                    await _userManager.AddToRoleAsync(userEntity, dto.Role);
                }

                //obtener los roles actualizados
                var updatedRoles = await _userManager.GetRolesAsync(userEntity);

                var userDto = _mapper.Map<UserDto>(userEntity);
                //asignar los roles
                userDto.Roles = updatedRoles.ToList();

                return new ResponseDto<UserDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Usuario actualizado",
                    Data = userDto
                };
            }
            catch (Exception e)
            {
                return new ResponseDto<UserDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
            }
        }
        //desactivar un usuario
        public async Task<ResponseDto<UserDto>> DisableUserAsync(string id)
        {
            try
            {
                var userEntity = await _userManager.FindByIdAsync(id);

                if (userEntity == null)
                {
                    return new ResponseDto<UserDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Usuario no encontrado",
                        Data = null
                    };
                }

                //si ya esta desactivado evitar hacer la operacion
                if (!userEntity.IsActive)
                {
                    return new ResponseDto<UserDto>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "El usuario ya esta desactivado",
                        Data = null
                    };
                }

                userEntity.IsActive = false;
                await _userManager.UpdateAsync(userEntity);

                var userDto = _mapper.Map<UserDto>(userEntity);

                return new ResponseDto<UserDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Usuario desactivado correctamente",
                    Data = userDto
                };
            }
            catch (Exception e)
            {
                return new ResponseDto<UserDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
            }
        }

        //activar un usuario
        public async Task<ResponseDto<UserDto>> EnableUserAsync(string id)
        {
            try
            {
                var userEntity = await _userManager.FindByIdAsync(id);

                if (userEntity == null)
                {
                    return new ResponseDto<UserDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Usuario no encontrado",
                        Data = null
                    };
                }

                //si ya esta activado evitar hacer la operacion
                if (userEntity.IsActive)
                {
                    return new ResponseDto<UserDto>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "El usuario ya esta activado",
                        Data = null
                    };
                }

                userEntity.IsActive = true;
                await _userManager.UpdateAsync(userEntity);

                var userDto = _mapper.Map<UserDto>(userEntity);

                return new ResponseDto<UserDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Usuario activado correctamente",
                    Data = userDto
                };
            }
            catch (Exception e)
            {
                return new ResponseDto<UserDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
            }
        }
    }
}

