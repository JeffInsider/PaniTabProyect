using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using panitab_backend.Constants;
using panitab_backend.Dtos;
using panitab_backend.Dtos.Administration.Customer;
using panitab_backend.Services.Interfaces;

namespace panitab_backend.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            this._customersService = customersService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseDto<List<CustomerDto>>>> GetAllCustomers()
        {
            var response = await _customersService.GetAllCustomerAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.OFFICE}")]
        public async Task<ActionResult<ResponseDto<CustomerDto>>> GetCustomerById(Guid id)
        {
            var response = await _customersService.GetCustomerByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.OFFICE}")]
        public async Task<ActionResult<ResponseDto<CustomerDto>>> CreateCustomer([FromBody] CreateCustomerDto dto)
        {
            var response = await _customersService.CreateCustomerAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.OFFICE}")]
        public async Task<ActionResult<ResponseDto<CustomerDto>>> UpdateCustomer(Guid id, [FromBody] UpdateCustomerDto dto)
        {
            var response = await _customersService.UpdateCustomerAsync(id, dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("disable/{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.OFFICE}")]
        public async Task<ActionResult<ResponseDto<CustomerDto>>> DisableCustomer(Guid id)
        {
            var response = await _customersService.DisableCustomerAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("enable/{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.OFFICE}")]
        public async Task<ActionResult<ResponseDto<CustomerDto>>> EnableCustomer(Guid id)
        {
            var response = await _customersService.EnableCustomerAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
