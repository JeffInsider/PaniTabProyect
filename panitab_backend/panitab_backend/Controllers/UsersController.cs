using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using panitab_backend.Constants;
using panitab_backend.Dtos;
using panitab_backend.Dtos.Users;
using panitab_backend.Services.Interfaces;

namespace panitab_backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseDto<List<UserDto>>>> GetAllUsers()
        {
            var response = await _usersService.GetAllUsersAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> GetUserById(string id)
        {
            var response = await _usersService.GetUserByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> CreateUser([FromBody] CreateUserDto dto)
        {
            var response = await _usersService.CreateUserAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> UpdateUser(string id, [FromBody] UpdateUserDto dto)
        {
            dto.Id = id;
            var response = await _usersService.UpdateUserAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("disable/{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> DisableUser(string id)
        {
            var response = await _usersService.DisableUserAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("enable/{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> EnableUser(string id)
        {
            var response = await _usersService.EnableUserAsync(id);

            return StatusCode(response.StatusCode, response);
        }

    }
}
