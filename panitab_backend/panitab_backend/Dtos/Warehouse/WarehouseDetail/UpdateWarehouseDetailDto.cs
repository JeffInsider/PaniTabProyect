namespace panitab_backend.Dtos.Warehouse.WarehouseDetail
{
    //para actualizar la informacion de un detalle de bodega
    public class UpdateWarehouseDetailDto
    {
        public Guid Id { get; set; }
        public int Packed { get; set; }
        public int Sold { get; set; }
        public int Adjustments { get; set; }
        public int DamagedStock { get; set; }
        public int RealStock { get; set; }
        public int? Shortage { get; set; }
        public int? Excess { get; set; }
        public Guid? OrderId { get; set; }
    }
}
