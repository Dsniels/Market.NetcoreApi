using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;   

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

            if (!resultado.Succeeded) {
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
