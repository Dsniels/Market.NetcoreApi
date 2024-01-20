using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{

    public class CarritoCompraController : BaseApiController
    {
        private readonly ICarritoCompraRepository _carritoCompra;
        private string id;

        public CarritoCompraController(ICarritoCompraRepository carritoCompra)
        {
            _carritoCompra = carritoCompra;
        }


        [HttpGet]
        public async Task<ActionResult<CarritoCompra>> GetCarritoById(string ID)
        {
            var carrito = await _carritoCompra.GetCarritoCompraAsync(ID);

            return Ok(carrito ?? new CarritoCompra(ID));
        }

        [HttpPost]
        public async Task<ActionResult<CarritoCompra>> UpdateCarritoCompra(CarritoCompra CarritoParametro)
        {
            var carrito = await _carritoCompra.UpdateCarritoCompraAsync(CarritoParametro);

            return Ok(carrito);
        }

        [HttpDelete]

        public async Task DeleteCarritoCompra(string id)
        {
            await _carritoCompra.DeleteCarritoCompraAsync(id);
        }

    }
}
