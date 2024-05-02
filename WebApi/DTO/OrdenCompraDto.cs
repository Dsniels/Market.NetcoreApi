namespace WebApi.DTO
{
    public class OrdenCompraDto
    {
        public string CarritoCompraId { get; set; }
        public int TipoEnvio { get; set; }
        public DireccionDto DireccionEnvio { get; set; }

    }
}
