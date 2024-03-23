using Core.Entities.OrdenCompra;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class OrdenCompraService : IOrdenCompraServices
    {
        public Task<OrdenCompras> AddOrdenCompraAsync(string CompradorEmail, int TipoEnvio, string CarritoId, Direccion direccion)
        {
            throw new NotImplementedException();
        }

        public Task<OrdenCompras> GetOrdenCompraByIdAsync(int id, string email)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<OrdenCompras>> GetOrdenComprasByUserEmailAsync(string Email)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TipoEnvio>> GetTiposEnvios()
        {
            throw new NotImplementedException();
        }
    }
}
