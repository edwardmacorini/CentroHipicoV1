using CentroHipicoAPI.Nucleo.DTOs;
using CentroHipicoAPI.Nucleo.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Controllers
{
    [Authorize]
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IServicioCliente _servicioCliente;

        public ClienteController(IServicioCliente servicioCliente)
        {
            _servicioCliente = servicioCliente;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerClientes()
        {
            return Ok(await _servicioCliente.ObtenerClientes());
        }

        [HttpGet("{idCliente}")]
        public async Task<IActionResult> ObtenerCliente(int idCliente)
        {
            return Ok(await _servicioCliente.ObtenerClientePorId(idCliente));
        }

        [HttpPost]
        public async Task<IActionResult> AgregarCliente(DTOCliente cliente)
        {
            await _servicioCliente.AgregarCliente(cliente);
            return Ok(new DTOBase());
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarCliente(DTOCliente cliente)
        {
            await _servicioCliente.ActualizarCliente(cliente);
            return Ok(new DTOBase());
        }

        [HttpDelete("{idCliente}")]
        public async Task<IActionResult> EliminarCliente(int idCliente)
        {
            await _servicioCliente.EliminarCliente(idCliente);
            return Ok(new DTOBase());
        }
    }
}
