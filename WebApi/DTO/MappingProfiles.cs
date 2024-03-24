using AutoMapper;
using Core.Entities;
using Core.Entities.OrdenCompra;

namespace WebApi.DTO
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Producto, ProductoDto>()
                .ForMember(p => p.CategoriaNombre, x => x.MapFrom(a => a.Categoria.Nombre))
                .ForMember(p => p.MarcaNombre, x => x.MapFrom(a => a.Marca.Nombre));

            CreateMap<Core.Entities.Direccion, DireccionDto>().ReverseMap();
            CreateMap<DireccionDto, Core.Entities.OrdenCompra.Direccion>();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();

        }
    }
}
