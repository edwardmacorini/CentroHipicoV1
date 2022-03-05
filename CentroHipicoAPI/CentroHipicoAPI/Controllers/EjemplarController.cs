using CentroHipicoAPI.Nucleo.DTOs;
using CentroHipicoAPI.Nucleo.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CentroHipicoAPI.Controllers
{
    [Authorize]
    [Route("api/ejemplares")]
    [ApiController]
    public class EjemplarController : ControllerBase
    {
        private readonly IServicioEjemplar _servicioEjemplar;

        public EjemplarController(IServicioEjemplar servicioEjemplar)
        {
            _servicioEjemplar = servicioEjemplar;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEjemplares()
        {
            return Ok(await _servicioEjemplar.ObtenerEjemplares());
        }

        [HttpGet("{idEjemplar}")]
        public async Task<IActionResult> ObtenerEjemplar(int idEjemplar)
        {
            return Ok(await _servicioEjemplar.ObtenerEjemplarPorId(idEjemplar));
        }

        [HttpPost]
        public async Task<IActionResult> AgregarEjemplar(DTOEjemplar ejemplar)
        {
            await _servicioEjemplar.AgregarEjemplar(ejemplar);
            return Ok(new DTOBase());
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarEjemplar(DTOEjemplar ejemplar)
        {
            await _servicioEjemplar.ActualizarEjemplar(ejemplar);
            return Ok(new DTOBase());
        }

        [HttpDelete("{idEjemplar}")]
        public async Task<IActionResult> EliminarEjemplar(int idEjemplar)
        {
            await _servicioEjemplar.EliminarEjemplar(idEjemplar);
            return Ok(new DTOBase());
        }
    }
}
