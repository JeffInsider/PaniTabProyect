using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using panitab_backend.Constants;
using panitab_backend.Dtos;
using panitab_backend.Dtos.Warehouse;
using panitab_backend.Dtos.Warehouse.WarehouseDetail;
using panitab_backend.Services.Interfaces;

namespace panitab_backend.Controllers
{
    [Route("api/warehouse-control")]
    [ApiController]
    public class WarehouseControlController : Controller
    {
        private readonly IWarehouseControlService _warehouseControlService;
        public WarehouseControlController(IWarehouseControlService warehouseControlService) 
        {
            this._warehouseControlService = warehouseControlService;
        }


        //obtener todos los controles de inventario
        [HttpGet]
        [Authorize(Roles = $"{RolesConstant.ADMIN},{RolesConstant.STORE}")]
        public async Task<ActionResult<ResponseDto<List<WarehouseControlDto>>>> GetAllWarehouseControls()
        {
            var response = await _warehouseControlService.GetAllControlAsync();

            return StatusCode(response.StatusCode, response);
        }

        //obtener control de inventario por el numero de control
        [HttpGet("{number}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN},{RolesConstant.STORE}")]
        public async Task<ActionResult<ResponseDto<WarehouseControlDto>>> GetWarehouseControlByNumber(string number)
        {
            var response = await _warehouseControlService.GetControlByIdAsync(number);

            return StatusCode(response.StatusCode, response);
        }

        //crear un control de inventario
        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.ADMIN},{RolesConstant.STORE}")]
        public async Task<ActionResult<ResponseDto<WarehouseControlDto>>> CreateWarehouseControl([FromBody] CreateWarehouseControlDto dto)
        {
            var response = await _warehouseControlService.CreateControlAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        //registar un ajuste de inventario
        [HttpPost("adjustments/{number}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN},{RolesConstant.STORE}")]
        public async Task<ActionResult<ResponseDto<WarehouseControlDto>>> RegisterInventoryAdjustment(string number, [FromBody] List<UpdateWarehouseDetailDto> adjustments)
        {
            var response = await _warehouseControlService.RegisterInventoryAdjustmentAsync(number, adjustments);

            return StatusCode(response.StatusCode, response);
        }

        //cerrar un control de inventario
        [HttpPut("close/{number}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN},{RolesConstant.STORE}")]
        public async Task<ActionResult<ResponseDto<WarehouseControlDto>>> CloseWarehouseControl(string number)
        {
            var response = await _warehouseControlService.CloseWarehouseControlAsync(number);

            return StatusCode(response.StatusCode, response);
        }

        //actualizar un control de inventario
        [HttpPut("{number}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<WarehouseControlDto>>> UpdateWarehouseControl(string number, [FromBody] UpdateWarehouseControlDto dto, [FromQuery] string userRole)
        {
            var response = await _warehouseControlService.UpdateWarehouseControlAsync(number, dto, userRole);
            return StatusCode(response.StatusCode, response);
        }
    }
}
