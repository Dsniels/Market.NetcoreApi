
using Core.Entities.OrdenCompra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrdenCompraServices
    {
        Task<OrdenCompras> AddOrdenCompraAsync(string CompradorEmail, int TipoEnvio, string CarritoId, Direccion direccion);
        Task<IReadOnlyList<OrdenCompras>> GetOrdenComprasByUserEmailAsync(string Email);

        Task<OrdenCompras> GetOrdenCompraByIdAsync(int id, string email);
        Task<IReadOnlyList<TipoEnvio>> GetTiposEnvios();
    }
}
