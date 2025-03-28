using AutoMapper;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Constants;
using panitab_backend.Database;
using panitab_backend.Database.Entities.Warehouse;
using panitab_backend.Dtos;
using panitab_backend.Dtos.Warehouse;
using panitab_backend.Dtos.Warehouse.WarehouseDetail;
using panitab_backend.Services.Interfaces;

namespace panitab_backend.Services
{
    public class WarehouseControlService : IWarehouseControlService
    {
        private readonly PaniTabContext _context;
        private readonly IMapper _mapper;
        public WarehouseControlService(PaniTabContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //obtener todos los controles de bodega
        public async Task<ResponseDto<List<WarehouseControlDto>>> GetAllControlAsync()
        {
            try
            {
                // aqui se obtienen todos los controles de bodega
                var controls = await _context.WarehouseControls
                    .Include(c => c.WarehouseControlDetails)
                    .ToListAsync();

                if (!controls.Any())
                {
                    return new ResponseDto<List<WarehouseControlDto>>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No se encontraron controles de bodega",
                        Data = new List<WarehouseControlDto>()
                    };
                }

                var controlsDto = _mapper.Map<List<WarehouseControlDto>>(controls);

                return new ResponseDto<List<WarehouseControlDto>>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Controles de bodega encontrados",
                    Data = controlsDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<WarehouseControlDto>>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = new List<WarehouseControlDto>()
                };
            }
        }

        //buscar un control de bodega por el numero de control
        public async Task<ResponseDto<WarehouseControlDto>> GetControlByIdAsync(string number)
        {
            try
            {
                // aqui se busca un control de bodega por control number
                var control = await _context.WarehouseControls
                    .Include(c => c.WarehouseControlDetails)
                    .FirstOrDefaultAsync(c => c.ControlNumber == number);

                if (control == null)
                {
                    return new ResponseDto<WarehouseControlDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Control de bodega no encontrado",
                        Data = null
                    };
                }

                var controlDto = _mapper.Map<WarehouseControlDto>(control);

                return new ResponseDto<WarehouseControlDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Control de bodega encontrado",
                    Data = controlDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<WarehouseControlDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        //crear un control de bodega
        public async Task<ResponseDto<WarehouseControlDto>> CreateControlAsync(CreateWarehouseControlDto dto)
        {
            try
            {
                // Obtener el último control de bodega para generar el número secuencial
                var lastControl = await _context.WarehouseControls
                    .Include(c => c.WarehouseControlDetails) // Incluir detalles para evitar problemas de carga diferida
                    .OrderByDescending(c => c.CreatedDate)
                    .FirstOrDefaultAsync();

                int nextNumber = lastControl != null
                    ? int.Parse(lastControl.ControlNumber.Substring(3)) + 1 // Extrae el número y suma 1
                    : 1;

                string controlNumber = $"CB-{nextNumber:D4}"; //el d4 es para que siempre tenga 4 digitos

                // Verificar si ya existe un control abierto para la fecha
                var existingControl = await _context.WarehouseControls
                    .Where(c => !c.IsCompleted && c.ClosingDate.Date == dto.ClosingDate.Date)
                    .FirstOrDefaultAsync();

                if (existingControl != null)
                {
                    return new ResponseDto<WarehouseControlDto>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "Ya existe un control de bodega abierto para esta fecha",
                        Data = null
                    };
                }

                var newControl = new WarehouseControlEntity
                {
                    ControlNumber = controlNumber, //usar el numero secuencial
                    ClosingDate = dto.ClosingDate,
                    Observations = dto.Observations,
                    IsCompleted = false,
                    CreatedDate = DateTime.Now,
                    WarehouseControlDetails = new List<WarehouseControlDetailEntity>()
                };

                //procesar los detalles del control de bodega
                foreach (var detail in dto.Details)
                {
                    // Buscar el stock final del último control
                    var previousStock = lastControl?.WarehouseControlDetails
                        .FirstOrDefault(d => d.BreadClassId == detail.BreadClassId)
                        ?.FinalStock ?? 0;

                    var detailEntity = new WarehouseControlDetailEntity
                    {
                        BreadClassId = detail.BreadClassId,
                        InitialStock = previousStock,
                        IncomingStock = detail.IncomingStock,
                        OrderType = detail.OrderType, // Es importante registrar si es Recarga o Pedido
                        OrderId = detail.OrderId ?? Guid.Empty,
                        FinalStock = previousStock + detail.IncomingStock, // Ajustar stock
                        RealStock = previousStock + detail.IncomingStock // Inicialmente igual al stock teórico
                    };

                    newControl.WarehouseControlDetails.Add(detailEntity);
                }

                _context.WarehouseControls.Add(newControl);
                await _context.SaveChangesAsync();

                var controlDto = _mapper.Map<WarehouseControlDto>(newControl);

                return new ResponseDto<WarehouseControlDto>
                {
                    Status = true,
                    StatusCode = 201,
                    Message = "Control de bodega creado",
                    Data = controlDto
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<WarehouseControlDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        //registrar ajustes de inventario
        public async Task<ResponseDto<WarehouseControlDto>> RegisterInventoryAdjustmentAsync(string number, List<UpdateWarehouseDetailDto> adjustments)
        {
            try
            {
                var control = await _context.WarehouseControls
                    .Include(c => c.WarehouseControlDetails)
                    .FirstOrDefaultAsync(c => c.ControlNumber == number);

                if (control == null)
                {
                    return new ResponseDto<WarehouseControlDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Control de bodega no encontrado",
                        Data = null
                    };
                }

                // Actualizar los detalles de bodega con los ajustes proporcionados
                foreach (var adjustment in adjustments)
                {
                    var detail = control.WarehouseControlDetails.FirstOrDefault(d => d.BreadClassId == adjustment.Id);
                    if (detail != null)
                    {
                        // Aplicar ajustes (evitando valores nulos)
                        detail.Adjustments += adjustment.Adjustments;
                        detail.DamagedStock += adjustment.DamagedStock;
                        detail.Shortage = adjustment.Shortage ?? 0;
                        detail.Excess = adjustment.Excess ?? 0;

                        // Recalcular RealStock (sin empaques ni ventas, ya que van en otros servicios)
                        detail.RealStock = detail.InitialStock + detail.IncomingStock - detail.OutgoingStock - detail.DamagedStock;

                        // Diferencia entre Real y Final Stock
                        detail.Difference = detail.RealStock - detail.FinalStock;
                    }
                }

                await _context.SaveChangesAsync();

                return new ResponseDto<WarehouseControlDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Ajuste de inventario registrado",
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<WarehouseControlDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        //cerrar un control de bodega
        public async Task<ResponseDto<WarehouseControlDto>> CloseWarehouseControlAsync(string controlNumber)
        {
            var control = await _context.WarehouseControls
                .Include(c => c.WarehouseControlDetails)
                .FirstOrDefaultAsync(c => c.ControlNumber == controlNumber);

            if (control == null)
            {
                return new ResponseDto<WarehouseControlDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Control de bodega no encontrado"
                };
            }
            //verificar que el control no este cerrado
            if (control.IsCompleted)
            {
                return new ResponseDto<WarehouseControlDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "El control de bodega ya está cerrado"
                };
            }

            //evitar que se cierre el control si hay diferencias
            bool hasDifferences = control.WarehouseControlDetails.Any(d => d.Difference != 0);
            if (hasDifferences)
            {
                return new ResponseDto<WarehouseControlDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "No se puede cerrar el control de bodega con diferencias"
                };
            }

            foreach (var detail in control.WarehouseControlDetails)
            {
                // Ajustar stock final al real
                detail.FinalStock = detail.RealStock; // El stock final se ajusta al stock real
                detail.Shortage = 0; // Se reinician los valores de faltantes
                detail.Excess = 0; // Se reinician los valores de sobrantes
                detail.Difference = 0; // Se reinician las diferencias
            }

            control.IsCompleted = true;
            control.LastClosingDate = DateTime.UtcNow; // Fecha de cierre

            await _context.SaveChangesAsync();

            //mapear el control de bodega
            var controlDto = _mapper.Map<WarehouseControlDto>(control);

            return new ResponseDto<WarehouseControlDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Control de bodega cerrado exitosamente"
            };
        }

        //actualizar un control de bodega solo para los administradores
        public async Task<ResponseDto<WarehouseControlDto>> UpdateWarehouseControlAsync(string controlNumber, UpdateWarehouseControlDto dto, string userRole)
        {
            var control = await _context.WarehouseControls
                .Include(c => c.WarehouseControlDetails)
                .FirstOrDefaultAsync(c => c.ControlNumber == controlNumber);

            if (control == null)
            {
                return new ResponseDto<WarehouseControlDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Control de bodega no encontrado"
                };
            }

            // si el control esta cerrado y el usuario no es ADMIN, bloquear la actualización
            if (control.IsCompleted && userRole != RolesConstant.ADMIN)
            {
                return new ResponseDto<WarehouseControlDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "No tienes permisos para actualizar un control de bodega cerrado"
                };
            }

            //actualizar las observaciones
            control.Observations = dto.Observations;
            control.LastClosingDate = dto.LastClosingDate;

            // Validar que haya detalles antes de iterar
            if (dto.Details != null)
            {
                foreach (var detailDto in dto.Details)
                {
                    var detail = control.WarehouseControlDetails.FirstOrDefault(d => d.BreadClassId == detailDto.Id);
                    if (detail != null)
                    {
                        detail.Adjustments = detailDto.Adjustments;
                        detail.DamagedStock = detailDto.DamagedStock;
                        detail.RealStock = detailDto.RealStock;
                        detail.Shortage = detailDto.Shortage ?? 0;
                        detail.Excess = detailDto.Excess ?? 0;

                        // Recalcular la diferencia entre RealStock y FinalStock
                        detail.Difference = detail.RealStock - detail.FinalStock;
                    }
                }
            }

            await _context.SaveChangesAsync();

            //mapear el control de bodega
            var controlDto = _mapper.Map<WarehouseControlDto>(control);

            return new ResponseDto<WarehouseControlDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Control de bodega actualizado"
            };
        }
    }
}
