namespace panitab_backend.Dtos.Warehouse.WarehouseDetail
{
    //para recibir la informacion de un detalle de bodega
    public class CreateWarehouseDetailDto
    {
        // de donde se obtiene el id de la clase de pan
        public Guid BreadClassId { get; set; }
        public int InitialStock { get; set; }
        public int IncomingStock { get; set; }
        public string OrderType { get; set; }
        public Guid? OrderId { get; set; }
    }
}
