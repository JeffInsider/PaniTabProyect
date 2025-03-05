namespace panitab_backend.Dtos.Warehouse
{
    public class WarehouseControlAdjustmentDto
    {
        public Guid BreadClassId { get; set; } // Clase de pan
        public int TotalDamaged { get; set; }  // Pan dañado
        public int TotalShortage { get; set; } // Faltante
        public int TotalExcess { get; set; }   // Sobrante
    }
}
