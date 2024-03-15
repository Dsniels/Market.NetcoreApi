using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Errors;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IGenericSeguridadRepository<Usuario> _seguridadRepository;
        private readonly RoleManager<IdentityRole> _roleManager;






        public UsuarioController(RoleManager<IdentityRole> roleManager, IPasswordHasher<Usuario> passwordHasher, IGenericSeguridadRepository<Usuario> seguridadRepository, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _seguridadRepository = seguridadRepository;
            _roleManager = roleManager;
        }

        [HttpPost("login")]

        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDtio)
        {
            var usuario = await _userManager.FindByEmailAsync(loginDtio.Email);

            if (usuario == null)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, loginDtio.Password, false);

            if (!resultado.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            return new UsuarioDto
            {
                Email = usuario.Email,
                Username = usuario.UserName,
                Token = _tokenService.CreateToken(usuario),
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido
            };
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(RegistrarDto registrarDto)
        {

            var usuario = new Usuario
            {
                Email = registrarDto.Email,
                UserName = registrarDto.UserName,
                Nombre = registrarDto.Nombre,
                Apellido = registrarDto.Apellido
            };

            var resultado = await _userManager.CreateAsync(usuario, registrarDto.Password);

            if (!resultado.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400));
            }

            return new UsuarioDto
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Token = _tokenService.CreateToken(usuario),
                Email = usuario.Email,
                Username = usuario.UserName
            };
        }

        [HttpPut("actualizar/{id}")]
        public async Task<ActionResult<UsuarioDto>> Actualizar(string id, RegistrarDto registrarDto)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe"));
            }

            usuario.Nombre = registrarDto.Nombre;
            usuario.Apellido = registrarDto.Apellido;
            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, registrarDto.Password);

            var result = await _userManager.UpdateAsync(usuario);

            if (!result.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(404, "No se pudo encontrar el Usuario"));
            }


            return new UsuarioDto
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Username = usuario.UserName,
                Token = _tokenService.CreateToken(usuario),
                Imagen = usuario.Imagen
            };



        }

        [HttpGet("pagination")]

        public async Task<ActionResult<Pagination<UsuarioDto>>> GetUsuarios([FromQuery] UsuarioSpecificationParams usuarioparams)
        {
            var spec = new UsuarioSpecification(usuarioparams);
            var usuarios = await _seguridadRepository.GetAllWithSpec(spec);

            var specCount = new UsuarioForCountingSpecification(usuarioparams);
            var totalUsuarios = await _seguridadRepository.CountAsync(specCount);

            var rounded = Math.Ceiling( Convert.ToDecimal(totalUsuarios) /Convert.ToDecimal(usuarioparams.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<Usuario>, IReadOnlyList< UsuarioDto >> (usuarios);
            return Ok(new Pagination<UsuarioDto>
            {
                Count = totalUsuarios,
                Data = data,
                pageCount = totalPages,
                PageIndex = usuarioparams.PageIndex,
                PageSize = usuarioparams.PageSize
            }


            ) ;
            
        }

        [HttpPut("role/{id}")]
        public async Task<ActionResult<UsuarioDto>> UpdateRole(string id, RoleDto roleParam)
        {

            var role = await _roleManager.FindByNameAsync(roleParam.Nombre);

            if (role == null)
            {
                return NotFound(new CodeErrorResponse(404, "El role no existe"));

            }
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe"));

            }
            var usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);
            
            if (roleParam.Status)
            {
                var result = await _userManager.AddToRoleAsync(usuario, roleParam.Nombre);
                if (result.Succeeded)
                {
                    usuarioDto.Admin = true;
                }

                if (result.Errors.Any())
                {
                    if(result.Errors.Where(x => x.Code == "UserAlreadyInRole").Any())
                    {
                        usuarioDto.Admin = true;
                    }
                }
            }
            else
            {
                var result = await _userManager.RemoveFromRoleAsync(usuario, roleParam.Nombre);
                if (result.Succeeded)
                {
                    usuarioDto.Admin = false;
                }

            }

            return usuarioDto;


        }






        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> GetUsuario()
        {
            var usuario = await _userManager.BuscarUsuarioAsync(HttpContext.User);

            return new UsuarioDto
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Username = usuario.UserName,
                Token = _tokenService.CreateToken(usuario)
            };

        }

        [HttpGet("emailvalido")]

        public async Task<ActionResult<bool>> ValidarEmail([FromQuery] string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario == null) return false;

            return true;

        }

        [Authorize]
        [HttpGet("direccion")]

        public async Task<ActionResult<DireccionDto>> GetDireccion()
        {

            var usuario = await _userManager.BuscarUsuarioConDireccionAsync(HttpContext.User);

            return _mapper.Map<Direccion, DireccionDto>(usuario.Direccion);
        }


        [Authorize]
        [HttpPut("direccion")]
        public async Task<ActionResult<DireccionDto>> UpdateDireccion(DireccionDto direccion)
        {
            var usuario = await _userManager.BuscarUsuarioConDireccionAsync(HttpContext.User);
            usuario.Direccion = _mapper.Map<DireccionDto, Direccion>(direccion);
            var resultado = await _userManager.UpdateAsync(usuario);
            if (resultado.Succeeded) return Ok(_mapper.Map<Direccion, DireccionDto>(usuario.Direccion));

            return BadRequest("No se pudo actualizar la direccion del usuario");
        }



    }
}
