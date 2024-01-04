﻿using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager; 

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
                Token = "Listo el token",
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido
            };
        }
    }
}