using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.DTOs
{
    public class DTOTicket
    {
        public string RazonSocial { get; set; }
        public string RIF { get; set; } = string.Empty;
        public DateTime FechaDocumento { get; set; } = DateTime.Now;
        public int NumeroCarrera { get; set; }
        public string NombreCliente { get; set; }
        public List<DTOTicketDetalle> Detalles { get; set; }

    }

    public class DTOTicketDetalle
    {
        public int NumeroEjemplar { get; set; }
        public string NombreEjemplar { get; set; } = string.Empty;
        public decimal Monto { get; set; }
    }
}
