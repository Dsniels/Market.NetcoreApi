using BusinessLogic.Data.Configuration;
using Core.Entities;
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
        private readonly IGenericRepository<OrdenCompras> _ordenCompraRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly ICarritoCompraRepository _carritoCompraRepository;
        private readonly IGenericRepository<TipoEnvio> _tipoEnvioRepository;

        public OrdenCompraService(IGenericRepository<OrdenCompras> ordenCompraRepository, IGenericRepository<Producto> productoRepository, ICarritoCompraRepository carritoCompraRepository, IGenericRepository<TipoEnvio> tipoEnvioRepository)
        {
            _ordenCompraRepository = ordenCompraRepository;
            _productoRepository = productoRepository;
            _carritoCompraRepository = carritoCompraRepository;
            _tipoEnvioRepository = tipoEnvioRepository;
        }

        public async Task<OrdenCompras> AddOrdenCompraAsync(string CompradorEmail, int TipoEnvio, string CarritoId, Core.Entities.OrdenCompra.Direccion direccion)
        {
            var carritoCompra = await _carritoCompraRepository.GetCarritoCompraAsync(CarritoId);
            var items = new List<OrderItem>();
            foreach (var item in carritoCompra.Items)
            {
                var productoItem = await _productoRepository.GetByIdAsync(item.Id);
                var itemOrdenado = new ProductoItemOrdenado(productoItem.Id, productoItem.Nombre, productoItem.Imagen);
                var ordenItem = new OrderItem(itemOrdenado, productoItem.Precio, item.Cantidad);
                items.Add(ordenItem);

            }

            var tipoEnvioEntity = await _tipoEnvioRepository.GetByIdAsync(TipoEnvio);
            var sbTotal = items.Sum(item => item.Precio * item.Cantidad);

            var ordenCompra = new OrdenCompras(CompradorEmail, direccion, tipoEnvioEntity, items, sbTotal);

            return ordenCompra;

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
