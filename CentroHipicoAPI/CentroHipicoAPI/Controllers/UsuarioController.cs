using CentroHipicoAPI.Nucleo.DTOs;
using CentroHipicoAPI.Nucleo.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicioUsuario _servicioUsuario;

        public UsuarioController(IServicioUsuario servicioUsuario)
        {
            _servicioUsuario = servicioUsuario;
        }

        [HttpPost]
        [Route("autenticar")]
        public async Task<IActionResult> AgregarEjemplar(DTOUsuarioRequest usuario)
        {
            return Ok(await _servicioUsuario.AutenticarUsuario(usuario));
        }

        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> AgregarEjemplar(DTOUsuario usuario)
        {
            return Ok(await _servicioUsuario.RegistrarUsuario(usuario));
        }
    }
}
