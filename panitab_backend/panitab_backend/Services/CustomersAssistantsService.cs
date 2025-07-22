using AutoMapper;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Database;
using panitab_backend.Database.Entities.Administration;
using panitab_backend.Dtos;
using panitab_backend.Dtos.Administration.Customer;
using panitab_backend.Dtos.Administration.CustomerAssistant;
using panitab_backend.Services.Interfaces;
using System.Drawing;

namespace panitab_backend.Services
{
    public class CustomersAssistantsService : ICustomersAssistantsService
    {
        private readonly PaniTabContext _context;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        public CustomersAssistantsService(PaniTabContext context, IMapper mapper, IAuditService auditService)
        {
            _context = context;
            _mapper = mapper;
            _auditService = auditService;
        }

        // obtener todos los asistentes de un cliente
        public async Task<ResponseDto<List<CustomerAssistantDto>>> GetAllCustomerAssistantAsync()
        {
            try
            {
                var customerAssistantsEntity = await _context.CustomerAssistants.ToListAsync();

                var customerAssistantsDto = new List<CustomerAssistantDto>();

                foreach (var customerAssistant in customerAssistantsEntity)
                {
                    var cusAssistantDto = _mapper.Map<CustomerAssistantDto>(customerAssistant);
                    customerAssistantsDto.Add(cusAssistantDto);
                }

                return new ResponseDto<List<CustomerAssistantDto>>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = customerAssistantsDto.Any() ? "Customers Assistants encontrados" : "No hay customers Assistants registrados aún",
                    Data = customerAssistantsDto
                };
            }

            catch (Exception ex)
            {
                return new ResponseDto<List<CustomerAssistantDto>>()
                {
                    Data = new List<CustomerAssistantDto>(),
                    Message = ex.Message,
                    Status = false,
                    StatusCode = 500
                };
            }
        }

        // obtener un asistente de cliente por id
        public async Task<ResponseDto<CustomerAssistantDto>> GetCustomerAssistantByIdAsync(Guid id)
        {
            try
            {
                var customerAssistantEntity = await _context.CustomerAssistants.FirstOrDefaultAsync(x => x.Id == id);

                if (customerAssistantEntity == null)
                {
                    return new ResponseDto<CustomerAssistantDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Customer Assistant no encontrado",
                        Data = null
                    };
                }
                var customerAssistantDto = _mapper.Map<CustomerAssistantDto>(customerAssistantEntity);

                return new ResponseDto<CustomerAssistantDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Customer Assistant encontrado",
                    Data = customerAssistantDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<CustomerAssistantDto>()
                {
                    Data = null,
                    Message = ex.Message,
                    Status = false,
                    StatusCode = 500
                };
            }
        }
        // crear un nuevo asistente de cliente
        public async Task<ResponseDto<CustomerAssistantDto>> CreateCustomerAssistantAsync(CreateCustomerAssistantDto dto)
        {
            try
            {
                var customerAssistant = new CustomerAssistantEntity
                {
                    CustomerId = dto.CustomerId,
                    AssistantName = dto.AssistantName,
                    CreatedBy = _auditService.GetUserId() ?? "system",
                };

                await _context.CustomerAssistants.AddAsync(customerAssistant);
                await _context.SaveChangesAsync();

                var customerAssistantDto = _mapper.Map<CustomerAssistantDto>(customerAssistant);

                return new ResponseDto<CustomerAssistantDto>
                {
                    Status = true,
                    StatusCode = 201,
                    Message = "Customer Assistant creado exitosamente",
                    Data = customerAssistantDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<CustomerAssistantDto>()
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"Error completo: {ex.Message} - {ex.InnerException?.Message}",
                    Data = null
                };
            }
        }
    }
}



