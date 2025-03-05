using panitab_backend.Dtos.Warehouse.WarehouseDetail;

namespace panitab_backend.Dtos.Warehouse
{
    //para actualizar la informacion de un control de bodega
    public class UpdateWarehouseControlDto
    {
        public Guid Id { get; set; }
        public string Observations { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? LastClosingDate { get; set; }
        public List<UpdateWarehouseDetailDto> Details { get; set; }
    }
}
