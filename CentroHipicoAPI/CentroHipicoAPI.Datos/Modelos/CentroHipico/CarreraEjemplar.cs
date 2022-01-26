using System;
using System.Collections.Generic;

#nullable disable

namespace CentroHipicoAPI.Datos.Modelos.CentroHipico
{
    public partial class CarreraEjemplar
    {
        public int Id { get; set; }
        public int IdEjemplar { get; set; }
        public int IdCarrera { get; set; }
        public bool EsFavorito { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual Carrera IdCarreraNavigation { get; set; }
        public virtual Ejemplar IdEjemplarNavigation { get; set; }
    }
}
