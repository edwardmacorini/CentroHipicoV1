using System;
using System.Collections.Generic;

#nullable disable

namespace CentroHipicoAPI.Datos.Modelos.CentroHipico
{
    public partial class CarreraDetalle
    {
        public int Id { get; set; }
        public int IdCarrera { get; set; }
        public int IdCliente { get; set; }
        public int IdEjemplar { get; set; }
        public decimal MontoApuesta { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual Carrera IdCarreraNavigation { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Ejemplar IdEjemplarNavigation { get; set; }
    }
}
