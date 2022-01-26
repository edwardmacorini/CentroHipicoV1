using CentroHipicoAPI.Nucleo.DTOs;

namespace CentroHipicoAPI.PrinterService.Servicies
{
    public interface IPrinterService
    {
        bool PrintTicket(DTOTicket ticket);
    }
}
