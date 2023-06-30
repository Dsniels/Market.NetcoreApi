using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Errors;

namespace WebApi.Controllers
{
    
    public class ProductoController : BaseApiController
    {

        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;
        public ProductoController(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productoRepository = productoRepository;

        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDto>>> GetProductos()
        {
            var spec = new ProductoWithCategoriaAndMarcaSpecification();
            var productos = await _productoRepository.GetAllWithSpec(spec);


            return Ok(_mapper.Map<IReadOnlyList<Producto>, IReadOnlyList<ProductoDto>>(productos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            //spec debe incluir la logica de la condicion de la consulta y tambien las relaciones entre
            //las entidades, la relacion entre producto, marca y categoria.
            var spec = new ProductoWithCategoriaAndMarcaSpecification(id);
            var producto = await _productoRepository.GetByIdWithSpec(spec);
            if (producto == null)
            {
                return NotFound(new CodeErrorResponse(404));
            }
            return _mapper.Map<Producto, ProductoDto>(producto);
        }

    }
}
