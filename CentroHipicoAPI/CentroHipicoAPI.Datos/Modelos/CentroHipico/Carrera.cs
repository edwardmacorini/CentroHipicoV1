using System;
using System.Collections.Generic;

#nullable disable

namespace CentroHipicoAPI.Datos.Modelos.CentroHipico
{
    public partial class Carrera
    {
        public Carrera()
        {
            CarreraDetalles = new HashSet<CarreraDetalle>();
            CarreraEjemplares = new HashSet<CarreraEjemplar>();
        }

        public int Id { get; set; }
        public int NumeroCarrera { get; set; }
        public DateTime FechaCarrera { get; set; }
        public string Ubicacion { get; set; }
        public bool EstaActiva { get; set; }
        public bool EstaCerrada { get; set; }
        public int? Ganador { get; set; }
        public decimal? MontoGanancia { get; set; }
        public decimal? MontoSubTotal { get; set; }
        public decimal? MontoTotal { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual ICollection<CarreraDetalle> CarreraDetalles { get; set; }
        public virtual ICollection<CarreraEjemplar> CarreraEjemplares { get; set; }
    }
}
