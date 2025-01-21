using Microsoft.AspNetCore.Identity;
using panitab_backend.Dtos;
using panitab_backend.Dtos.Auth;

namespace panitab_backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<IdentityResult>> CreateUserAsync(CreateUserDto userDto);
        Task<ResponseDto<IdentityResult>> DeleteUserAsync(string userId);
        Task<ResponseDto<LoginResponseDto>> LoginUserAsync(LoginDto dto);
        Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync(RefreshTokenDto dto);
        Task<ResponseDto<IdentityResult>> UpdateUserAsync(UpdateUserDto userDto);
    }
}
