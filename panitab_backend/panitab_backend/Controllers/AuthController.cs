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
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthService authService, 
            IConfiguration configuration, 
            UserManager<UserEntity> userManager, 
            SignInManager<UserEntity> signInManager, 
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var response = await _authService.GetUsersAsync();

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            var response = await _authService.GetUserByIdAsync(userId);

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto<CreateUserDto>>> CreateUserAsync([FromBody] CreateUserDto model)
        {
            if (ModelState.IsValid) {
                var response = await _authService.CreateUserAsync(model);

                if (response.Status)
                {
                    return Ok(new { Message = response.Message });
                }

                return BadRequest(new { Message = response.Message, Errors = response.Data.Errors });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> LoginUserAsync([FromBody] LoginDto model)
        {
            var authResponse = await _authService.LoginUserAsync(model);
            return StatusCode(authResponse.StatusCode, authResponse);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> RefreshTokenAsync([FromBody] RefreshTokenDto dto)
        {
            var loginResponseDto = await _authService.RefreshTokenAsync(dto);

            return StatusCode(loginResponseDto.StatusCode, new
            {
                Status = true,
                loginResponseDto.Message,
                loginResponseDto.Data
            });
        }

        [HttpPut("update")]
        public async Task<ActionResult<ResponseDto<UpdateUserDto>>> UpdateUserAsync([FromBody] UpdateUserDto model)
        {
            var response = await _authService.UpdateUserAsync(model);

            if (response.Status)
            {
                return Ok(new { Message = response.Message });
            }

            return BadRequest(new { Message = response.Message, Errors = response.Data.Errors });
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteUserAsync(string userId)
        {
            var response = await _authService.DeleteUserAsync(userId);

            if (response.Status)
            {
                return Ok(new { Message = response.Message });
            }

            return BadRequest(new { Message = response.Message });
        }
    }
}
