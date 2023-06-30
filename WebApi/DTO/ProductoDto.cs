using Core.Entities;
using StackExchange.Redis;

namespace WebApi.DTO
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int MarcaId { get; set; }
        public string MarcaNombre { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }


    }
}
