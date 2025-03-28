using panitab_backend.Dtos;
using panitab_backend.Dtos.Users;

namespace panitab_backend.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResponseDto<UserDto>> CreateUserAsync(CreateUserDto dto);
        Task<ResponseDto<UserDto>> DisableUserAsync(string id);
        Task<ResponseDto<UserDto>> EnableUserAsync(string id);
        Task<ResponseDto<List<UserDto>>> GetAllUsersAsync();
        Task<ResponseDto<UserDto>> GetUserByIdAsync(string id);
        Task<ResponseDto<UserDto>> UpdateUserAsync(string id, UpdateUserDto dto);
    }
}
