using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class CarritoCompraRepository : ICarritoCompraRepository
    {
        public Task<bool> DeleteCarritoCompraAsync(string carritoId)
        {
            throw new NotImplementedException();
        }

        public Task<CarritoCompra> GetCarritoCompraAsync(string carritoId)
        {
            throw new NotImplementedException();
        }

        public Task<CarritoCompra> UpdateCarritoCompraAsync(CarritoCompra carritocompra)
        {
            throw new NotImplementedException();
        }
    }
}
