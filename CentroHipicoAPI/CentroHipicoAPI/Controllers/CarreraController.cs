using CentroHipicoAPI.Nucleo.DTOs;
using CentroHipicoAPI.Nucleo.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Controllers
{
    [Route("api/carreras")]
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly IServicioCarrera _servicioCarrera;

        public CarreraController(IServicioCarrera servicioCarrera)
        {
            _servicioCarrera = servicioCarrera;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCarreras()
        {
            return Ok(await _servicioCarrera.ObtenerCarreras());
        }

        [HttpGet]
        [Route("{idCarrera}")]
        public async Task<IActionResult> ObtenerCarrera(int idCarrera)
        {
            return Ok(await _servicioCarrera.ObtenerCarrera(idCarrera));
        }

        [HttpPost]
        [Route("agregar")]
        public async Task<IActionResult> AgregarCarrera(DTOCarrera carrera)
        {
            await _servicioCarrera.AgregarCarrera(carrera);
            return Ok(new DTOBase());
        }

        [HttpPost]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarCarrera(DTOCarrera carrera)
        {
            await _servicioCarrera.EliminarCarrera(carrera);
            return Ok(new DTOBase());
        }

        [HttpPost]
        [Route("ejemplar/agregar")]
        public async Task<IActionResult> AgregarEjemplar(DTOCarreraEjemplar ejemplar)
        {
            await _servicioCarrera.AgregarEjemplar(ejemplar);
            return Ok(new DTOBase());
        }

        [HttpPost]
        [Route("ejemplar/eliminar")]
        public async Task<IActionResult> EliminarEjemplar(DTOCarreraEjemplar ejemplar)
        {
            await _servicioCarrera.EliminarEjemplar(ejemplar);
            return Ok(new DTOBase());
        }

        [HttpPost]
        [Route("apuesta/agregar")]
        public async Task<IActionResult> AgregarApuesta(DTOCarreraDetalle carreraDetalle)
        {
            await _servicioCarrera.AgregarApuesta(carreraDetalle);
            return Ok(new DTOBase());
        }

        [HttpPost]
        [Route("apuesta/eliminar")]
        public async Task<IActionResult> EliminarApuesta(DTOCarreraDetalle carreraDetalle)
        {
            await _servicioCarrera.EliminarApuesta(carreraDetalle);
            return Ok(new DTOBase());
        }

        [HttpPost]
        [Route("abrir")]
        public async Task<IActionResult> AbrirCarrera(int idCarrera)
        {
            await _servicioCarrera.AbrirCarrera(idCarrera);
            return Ok(new DTOBase());
        }

        [HttpPost]
        [Route("cerrar")]
        public async Task<IActionResult> CerrarCarrera(int idCarrera)
        {
            await _servicioCarrera.CerrarCarrera(idCarrera);
            return Ok(new DTOBase());
        }

        [HttpPost]
        [Route("ganador")]
        public async Task<IActionResult> AgregarGanador(DTOCarreraGanador ganador)
        {
            await _servicioCarrera.AgregarGanador(ganador);
            return Ok(new DTOBase());
        }
    }
}
