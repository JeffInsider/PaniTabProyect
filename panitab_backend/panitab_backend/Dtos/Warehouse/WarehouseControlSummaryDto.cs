namespace panitab_backend.Dtos.Warehouse
{
    //para mostrar un resumen de un control de bodega
    public class WarehouseControlSummaryDto
    {
        public DateTime ClosingDate { get; set; }
        public int TotalIncoming { get; set; }
        public int TotalOutgoing { get; set; }
        public int TotalAdjustments { get; set; }
        public int TotalDamaged { get; set; }
        public int FinalStock { get; set; }
        public int RealStock { get; set; }
        public int Difference { get; set; }
    }
}
