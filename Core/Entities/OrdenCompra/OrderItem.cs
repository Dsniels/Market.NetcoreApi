using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrdenCompra
{
    public class OrderItem : ClaseBase
    {
        public OrderItem(ProductoItemOrdenado ItemOrdenado, decimal precio, int cantidad)
        {
            this.ItemOrdenado = ItemOrdenado;
            Precio = precio;
            Cantidad = cantidad;
        }

        public OrderItem()
        {
        }

        public ProductoItemOrdenado ItemOrdenado { get; set; } 
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

    }
}
