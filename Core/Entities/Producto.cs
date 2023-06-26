using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Producto
    {
         public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int Marca { get; set;}

        [Column(TypeName ="decimal(18,4)")]
        public decimal Precio { get; set; } 
        public string Imagen { get; set; }  



    }
}
