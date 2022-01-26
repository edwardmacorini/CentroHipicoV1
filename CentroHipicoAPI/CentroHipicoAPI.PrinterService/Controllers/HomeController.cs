using CentroHipicoAPI.Nucleo.DTOs;
using CentroHipicoAPI.PrinterService.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace CentroHipicoAPI.PrinterService.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IPrinterService _printerService;

        public HomeController(IPrinterService printerService)
        {
            _printerService = printerService;
        }

        [HttpPost]
        [Route("print")]
        public IActionResult PrintTicket([FromBody] DTOTicket ticket)
        {
            return Ok(_printerService.PrintTicket(ticket));
        }
    }
}
