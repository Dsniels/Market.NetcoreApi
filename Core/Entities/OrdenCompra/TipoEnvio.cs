using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrdenCompra
{
    public class TipoEnvio : ClaseBase
    {
        public string Nombre { get; set; }
        public string DeliveryTime { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
