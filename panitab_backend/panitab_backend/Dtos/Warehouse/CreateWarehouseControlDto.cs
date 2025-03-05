using panitab_backend.Dtos.Warehouse.WarehouseDetail;

namespace panitab_backend.Dtos.Warehouse
{
    //para recibir la informacion de un control de bodega
    public class CreateWarehouseControlDto
    {
        public string ControlNumber { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Observations { get; set; }
        public bool IsCompleted { get; set; }
        public List<CreateWarehouseDetailDto> Details { get; set; }
    }
}
