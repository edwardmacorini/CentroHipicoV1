using System;
using System.Collections.Generic;

#nullable disable

namespace CentroHipicoAPI.Datos.Modelos.CentroHipico
{
    public partial class Ejemplar
    {
        public Ejemplar()
        {
            CarreraDetalles = new HashSet<CarreraDetalle>();
            CarreraEjemplares = new HashSet<CarreraEjemplar>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual ICollection<CarreraDetalle> CarreraDetalles { get; set; }
        public virtual ICollection<CarreraEjemplar> CarreraEjemplares { get; set; }
    }
}
