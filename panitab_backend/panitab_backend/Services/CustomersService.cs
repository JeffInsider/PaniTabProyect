using AutoMapper;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Database;
using panitab_backend.Database.Entities.Administration;
using panitab_backend.Dtos;
using panitab_backend.Dtos.Customer;
using panitab_backend.Services.Interfaces;

namespace panitab_backend.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly PaniTabContext _context;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        public CustomersService(PaniTabContext context, IMapper mapper, IAuditService auditService)
        {
            _context = context;
            _mapper = mapper;
            _auditService = auditService;
        }

        //obtener todos los Customers
        public async Task<ResponseDto<List<CustomerDto>>> GetAllCustomerAsync()
        {
            try
            {
                var customerEntity = await _context.Customers.ToListAsync();

                //if (customerEntity == null || !customerEntity.Any())
                //{
                //    return new ResponseDto<List<CustomerDto>>
                //    {
                //        Status = false,
                //        StatusCode = 200,
                //        Message = "No se encontraron Customer registrados",
                //        Data = null
                //    };
                //}

                var customersDto = new List<CustomerDto>();

                foreach (var customer in customerEntity)
                {
                    var customerDto = _mapper.Map<CustomerDto>(customer);
                    customersDto.Add(customerDto);
                }

                return new ResponseDto<List<CustomerDto>>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = customersDto.Any() ? "Customers encontrados" : "No hay customers registrados aún",
                    Data = customersDto
                };

            }
            catch (Exception ex) {
                return new ResponseDto<List<CustomerDto>>
                {
                    Data = new List<CustomerDto>(),
                    Message = ex.Message,
                    Status = false,
                    StatusCode = 500
                };
            }
        }
        //obtener un Customer por id
        public async Task<ResponseDto<CustomerDto>> GetCustomerByIdAsync(Guid id)
        {
            try
            {
                var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

                if (customerEntity == null)
                {
                    return new ResponseDto<CustomerDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No se encontró el Customer",
                        Data = null
                    };
                }

                var customerDto = _mapper.Map<CustomerDto>(customerEntity);

                return new ResponseDto<CustomerDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Customer encontrado",
                    Data = customerDto
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<CustomerDto>
                {
                    Data = new CustomerDto(),
                    Message = ex.Message,
                    Status = false,
                    StatusCode = 500
                };
            }
        }

        // crear un Customer
        public async Task<ResponseDto<CustomerDto>> CreateCustomerAsync(CreateCustomerDto dto)
        {
            try
            {
                var customerExist = await _context.Customers.AnyAsync(x => x.IdentityNumber == dto.IdentityNumber);

                if (customerExist)
                {
                    return new ResponseDto<CustomerDto>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "El Customer ya existe",
                        Data = null
                    };
                }

                var customer = new CustomerEntity
                {
                    Id = Guid.NewGuid(),
                    IdentityNumber = dto.IdentityNumber,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PhoneNumber = dto.PhoneNumber,
                    Balance = 0,
                    IsActive = true,
                    CreatedBy = _auditService.GetUserId() ?? "system" /// aqui se asigna el usuario que crea el Customer por eso no se pasa en el header
                };

                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                var customerDto = _mapper.Map<CustomerDto>(customer);

                return new ResponseDto<CustomerDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Customer creado",
                    Data = customerDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<CustomerDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"Error completo: {ex.Message} - {ex.InnerException?.Message}",
                    Data = null
                };
            }
        }

        //actualizar un Customer
        public async Task<ResponseDto<CustomerDto>> UpdateCustomerAsync(Guid id, UpdateCustomerDto dto)
        {
            try
            {
                var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

                if (customerEntity == null)
                {
                    return new ResponseDto<CustomerDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No se encontró el Customer",
                        Data = null
                    };
                }

                customerEntity.IdentityNumber = dto.IdentityNumber;
                customerEntity.FirstName = dto.FirstName;
                customerEntity.LastName = dto.LastName;
                customerEntity.PhoneNumber = dto.PhoneNumber;
                customerEntity.Balance = dto.Balance;

                _context.Customers.Update(customerEntity);
                await _context.SaveChangesAsync();

                var customerDto = _mapper.Map<CustomerDto>(customerEntity);

                return new ResponseDto<CustomerDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Customer actualizado",
                    Data = customerDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<CustomerDto>
                {
                    Data = new CustomerDto(),
                    Message = ex.Message,
                    Status = false,
                    StatusCode = 500
                };
            }
        }

        //desactivar un Customer
        public async Task<ResponseDto<CustomerDto>> DisableCustomerAsync(Guid id)
        {
            try
            {
                var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

                if (customerEntity == null)
                {
                    return new ResponseDto<CustomerDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No se encontró el Customer",
                        Data = new CustomerDto()
                    };
                }

                //si ya esta desactivado no se puede volver a desactivar
                if (!customerEntity.IsActive)
                {
                    return new ResponseDto<CustomerDto>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "El Customer ya está desactivado",
                        Data = new CustomerDto()
                    };
                }

                customerEntity.IsActive = false;

                _context.Customers.Update(customerEntity);
                await _context.SaveChangesAsync();

                var customerDto = _mapper.Map<CustomerDto>(customerEntity);

                return new ResponseDto<CustomerDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Customer desactivado",
                    Data = customerDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<CustomerDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        //activar un Customer
        public async Task<ResponseDto<CustomerDto>> EnableCustomerAsync(Guid id)
        {
            try
            {
                var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

                if (customerEntity == null)
                {
                    return new ResponseDto<CustomerDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No se encontró el Customer",
                        Data = null
                    };
                }

                //si ya esta activado no se puede volver a activar
                if (customerEntity.IsActive)
                {
                    return new ResponseDto<CustomerDto>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "El Customer ya está activado",
                        Data = new CustomerDto()
                    };
                }

                customerEntity.IsActive = true;

                _context.Customers.Update(customerEntity);
                await _context.SaveChangesAsync();

                var customerDto = _mapper.Map<CustomerDto>(customerEntity);

                return new ResponseDto<CustomerDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Customer activado",
                    Data = customerDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<CustomerDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
