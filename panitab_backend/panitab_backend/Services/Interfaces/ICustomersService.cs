using panitab_backend.Dtos;
using panitab_backend.Dtos.Administration.Customer;

namespace panitab_backend.Services.Interfaces
{
    public interface ICustomersService
    {
        Task<ResponseDto<CustomerDto>> EnableCustomerAsync(Guid id);
        Task<ResponseDto<CustomerDto>> CreateCustomerAsync(CreateCustomerDto dto);
        Task<ResponseDto<CustomerDto>> DisableCustomerAsync(Guid id);
        Task<ResponseDto<List<CustomerDto>>> GetAllCustomerAsync();
        Task<ResponseDto<CustomerDto>> GetCustomerByIdAsync(Guid id);
        Task<ResponseDto<CustomerDto>> UpdateCustomerAsync(Guid id, UpdateCustomerDto dto);
    }
}
