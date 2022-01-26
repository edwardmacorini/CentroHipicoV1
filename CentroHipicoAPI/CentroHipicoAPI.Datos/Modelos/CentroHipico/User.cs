using System;
using System.Collections.Generic;

#nullable disable

namespace CentroHipicoAPI.Datos.Modelos.CentroHipico
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
