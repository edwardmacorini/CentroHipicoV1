using CentroHipicoAPI.Nucleo.DTOs;
using CentroHipicoAPI.Nucleo.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Controllers
{
    [Authorize]
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicioUsuario _servicioUsuario;

        public UsuarioController(IServicioUsuario servicioUsuario)
        {
            _servicioUsuario = servicioUsuario;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("autenticar")]
        public async Task<IActionResult> AutenticarUsuario(DTOUsuarioRequest usuario)
        {
            try
            {
                return Ok(await _servicioUsuario.AutenticarUsuario(usuario));
            }
            catch(Exception ex)
            {
                if(ex.Message == "Usuario o Contraseña invalidos..!")
                    return Forbid();

                throw;
            } 
        }

        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> RegistrarUsuario(DTOUsuario usuario)
        {
            return Ok(await _servicioUsuario.RegistrarUsuario(usuario));
        }
    }
}
