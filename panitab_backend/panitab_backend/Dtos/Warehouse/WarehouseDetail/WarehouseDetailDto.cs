namespace panitab_backend.Dtos.Warehouse.WarehouseDetail
{
    //para devolver la informacion de un detalle de bodega
    public class WarehouseDetailDto
    {
        public Guid Id { get; set; }
        public string BreadClass { get; set; }
        public int InitialStock { get; set; }
        public int IncomingStock { get; set; }
        public int OutgoingStock { get; set; }
        public int Adjustments { get; set; }
        public int DamagedStock { get; set; }
        public int FinalStock { get; set; }
        public int RealStock { get; set; }
        public int Difference { get; set; }
        public int? Shortage { get; set; }
        public int? Excess { get; set; }
        public string OrderType { get; set; }
    }
}