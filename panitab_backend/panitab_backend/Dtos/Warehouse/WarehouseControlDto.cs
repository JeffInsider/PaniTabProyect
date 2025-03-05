using panitab_backend.Dtos.Warehouse.WarehouseDetail;

namespace panitab_backend.Dtos.Warehouse
{
    //para devolver la informacion de un control de bodega
    public class WarehouseControlDto
    {
        public Guid Id { get; set; }
        public string ControlNumber { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Observations { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? LastClosingDate { get; set; }
        public List<WarehouseDetailDto> Details { get; set; }
    }
}
