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
    }
}
