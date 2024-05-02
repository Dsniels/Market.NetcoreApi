using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<ActionResult<Pagination<ProductoDto>>> GetProductos([FromQuery] ProductoSpecificationsParamts productoParams)
        {
            var spec = new ProductoWithCategoriaAndMarcaSpecification(productoParams);

            var productos = await _productoRepository.GetAllWithSpec(spec);

            var specCount = new ProductoForCountingSpecification(productoParams);
            var totalProductos = await _productoRepository.CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalProductos / productoParams.PageSize));
            var totalpage = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<Producto>, IReadOnlyList<ProductoDto>>(productos);

            return Ok(
                new Pagination<ProductoDto>
                {
                    Count = totalProductos,
                    Data = data,
                    pageCount = totalpage,
                    PageIndex = productoParams.PageIndex,
                    PageSize = productoParams.PageSize
                });


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


        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Producto>> post(Producto producto)
        {
            var result = await _productoRepository.add(producto);

            if (result == 0)
            {
                throw new Exception("No se inserto el producto");
            }

            return Ok(producto);
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Producto>> put(int id, Producto producto)
        {
            producto.Id = id;
            var resultado = await _productoRepository.update(producto);

            if (resultado == 0)
            {
                throw new Exception("No se pudo actualizar el producto");

            }

            return Ok(producto);


        }

    }
}
