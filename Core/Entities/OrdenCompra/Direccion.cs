using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrdenCompra
{
    public class Direccion
    {
        public Direccion()
        {

        }

        public Direccion(int id, string calle, string ciudad, string departamento, string codigoPostal)
        {
            Id = id;
            Calle = calle;
            Ciudad = ciudad;
            Departamento = departamento;
            CodigoPostal = codigoPostal;
        }

        public int Id { get; set; }
        public string Calle { get; set; }

        public string Ciudad { get; set; }

        public string Departamento { get; set; }

        public string CodigoPostal { get; set; }


    }
}
