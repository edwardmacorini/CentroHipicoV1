using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.DTOs
{
    public class DTOUsuario
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } = "";

    }

    public class DTOUsuarioRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DTOUsuarioResponse : DTOUsuario
    {
        public string Token { get; set; } = "";

        public DTOUsuarioResponse(DTOUsuario usuario, string token)
        {
            Id = usuario.Id;
            Name = usuario.Name;
            Username = usuario.Username;
            Token = token;
        }
    }
}
