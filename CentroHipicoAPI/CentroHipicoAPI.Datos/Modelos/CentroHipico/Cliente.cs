using System;
using System.Collections.Generic;

#nullable disable

namespace CentroHipicoAPI.Datos.Modelos.CentroHipico
{
    public partial class Cliente
    {
        public Cliente()
        {
            CarreraDetalles = new HashSet<CarreraDetalle>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual ICollection<CarreraDetalle> CarreraDetalles { get; set; }
    }
}
