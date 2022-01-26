using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.DTOs
{
    public class DTOCliente
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; } = "";
    }
}
