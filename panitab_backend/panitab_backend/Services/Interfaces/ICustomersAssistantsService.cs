using panitab_backend.Dtos;
using panitab_backend.Dtos.Administration.CustomerAssistant;

namespace panitab_backend.Services.Interfaces
{
    public interface ICustomersAssistantsService
    {
        Task<ResponseDto<List<CustomerAssistantDto>>> GetAllCustomerAssistantAsync();
    }
}
