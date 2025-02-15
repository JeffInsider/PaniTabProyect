using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using panitab_backend.Database.Entities;
using panitab_backend.Dtos;
using panitab_backend.Dtos.Auth;
using panitab_backend.Services.Interfaces;

namespace panitab_backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService 
            )
        {
            this._authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]

        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login(LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        //[HttpPost("register")]
        //[AllowAnonymous]
        //public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Register(CreateUserDto dto)
        //{
        //    var response = await _authService.RegisterAsync(dto);

        //    return StatusCode(response.StatusCode, response);
        //}

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> GetToken(RefreshTokenDto dto)
        {
            var response = await _authService.RefreshTokenAsync(dto);

            return StatusCode(response.StatusCode, response);
        }
    }
}
