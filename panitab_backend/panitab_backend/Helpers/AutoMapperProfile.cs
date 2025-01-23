using AutoMapper;
using panitab_backend.Database.Entities;
using panitab_backend.Dtos.Auth;

namespace panitab_backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<ClaseOrigen, ClaseDestino>();

            //mapear de UserEntity a CreateUserDto
            CreateMap<CreateUserDto, UserEntity> ();
            CreateMap<UserEntity, CreateUserDto> ();

            //mapear de UserEntity a LoginDto
            CreateMap<LoginDto, UserEntity>();
            CreateMap<UserEntity, LoginDto>();

            //mapear de UserEntity a LoginResponseDto
            CreateMap<LoginResponseDto, UserEntity>();
            CreateMap<UserEntity, LoginResponseDto>();
            CreateMap<LoginDto, LoginResponseDto>();

            //mapear de UserEntity a UserDto
            CreateMap<UserDto, UserEntity>();
            CreateMap<UserEntity, UserDto>();
        }
    }
}
