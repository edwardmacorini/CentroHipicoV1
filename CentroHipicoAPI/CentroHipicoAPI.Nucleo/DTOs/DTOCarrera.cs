using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.DTOs
{
    public class DTOCarrera
    {
        public int Id { get; set; }
        public DateTime FechaCarrera { get; set; }
        public bool EstaActiva { get; set; } = false;
        public bool EstaCerrada { get; set; } = false;
        public int? Ganador { get; set; }
        public decimal? MontoGanancia { get; set; }
        public decimal? MontoSubTotal { get; set; }
        public decimal? MontoTotal { get; set; }
        public int NumeroCarrera { get; set; }
        public string Ubicacion { get; set; } = "";
    }

    public class DTOCarreraResponse
    {
        public DTOCarrera Carrera { get; set; }
        public List<DTOCarreraEjemplar> Ejemplares { get; set; }
        public List<DTOCarreraDetalleResponse> Detalles { get; set; } = new List<DTOCarreraDetalleResponse>();
    }
    public class DTOCarreraEjemplar
    {
        public int Id { get; set; }
        public int IdCarrera { get; set; }
        public DTOEjemplar Ejemplar { get; set; }
        public bool EsFavorito { get; set; }
    }

    public class DTOCarreraDetalle
    {
        public int Id { get; set; }
        public int IdCarrera { get; set; }
        public int IdCliente { get; set; }
        public int IdEjemplar { get; set; }
        public decimal MontoApuesta { get; set; }
    }

    public class DTOCarreraDetalleResponse
    {
        public int IdCarrera { get; set; }
        public DTOCarreraEjemplar Ejemplar { get; set; } = null;
        public DTOCliente Cliente { get; set; } = null;
        public decimal MontoApuesta { get; set; } = 0;
    }

    public class DTOCarreraGanador
    {
        public int IdCarrera { get; set; }
        public int IdCliente { get; set; }
    }

    public class DTOCarreraGanadorResponse
    {
        public int IdCarrera { get; set; }
        public DTOCliente Cliente { get; set; }
    }
}
