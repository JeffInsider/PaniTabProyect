using Microsoft.AspNetCore.Identity;
using panitab_backend.Dtos;
using panitab_backend.Dtos.Auth;
using System.Security.Claims;

namespace panitab_backend.Services.Interfaces
{
    public interface IAuthService
    {
        ClaimsPrincipal GetTokenPrincipal(string token);

        //Task<ResponseDto<IdentityResult>> CreateUserAsync(CreateUserDto userDto);
        //Task<ResponseDto<IdentityResult>> DeleteUserAsync(string userId);
        //Task<ResponseDto<UserDto>> GetUserByIdAsync(string userId);
        //Task<ResponseDto<List<UserDto>>> GetUsersAsync();
        //Task<ResponseDto<LoginResponseDto>> LoginUserAsync(LoginDto dto);
        //Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync(RefreshTokenDto dto);
        //Task<ResponseDto<IdentityResult>> UpdateUserAsync(UpdateUserDto userDto);
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync(RefreshTokenDto dto);
        Task<ResponseDto<LoginResponseDto>> RegisterAsync(CreateUserDto dto);
    }
}
