using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrdenCompra
{
    public class ProductoItemOrdenado
    {
        public ProductoItemOrdenado()
        {
        }

        public ProductoItemOrdenado(int productoItemId, string productoNombre, string imagenUrl)
        {
            ProductoItemId = productoItemId;
            ProductoNombre = productoNombre;
            ImagenUrl = imagenUrl;
        }

        public int ProductoItemId { get; set; }
        public string ProductoNombre { get; set; }

        public string ImagenUrl { get; set; }
    }
}
