using panitab_backend.Dtos;
using panitab_backend.Dtos.Warehouse;
using panitab_backend.Dtos.Warehouse.WarehouseDetail;

namespace panitab_backend.Services.Interfaces
{
    public interface IWarehouseControlService
    {
        Task<ResponseDto<WarehouseControlDto>> CloseWarehouseControlAsync(string controlNumber);
        Task<ResponseDto<WarehouseControlDto>> CreateControlAsync(CreateWarehouseControlDto dto);
        Task<ResponseDto<List<WarehouseControlDto>>> GetAllControlAsync();
        Task<ResponseDto<WarehouseControlDto>> GetControlByIdAsync(string number);
        Task<ResponseDto<WarehouseControlDto>> RegisterInventoryAdjustmentAsync(string number, List<UpdateWarehouseDetailDto> adjustments);
        Task<ResponseDto<WarehouseControlDto>> UpdateWarehouseControlAsync(string controlNumber, UpdateWarehouseControlDto dto, string userRole);
    }
}
