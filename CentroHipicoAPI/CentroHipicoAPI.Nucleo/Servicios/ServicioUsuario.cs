using CentroHipicoAPI.Nucleo.DTOs;
using CentroHipicoAPI.Nucleo.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Servicios
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ServicioUsuario(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public async Task<DTOUsuario> AutenticarUsuario(DTOUsuarioRequest usuario)
        {
            if (usuario == null || usuario.Username.Length < 3 || usuario.Password.Length < 3)
                throw new ArgumentException("Datos invalidos..!");

            var result = await _repositorioUsuario.AutenticarUsuarioAsync(usuario);

            if (result == null) throw new Exception("Usuario o Contraseña invalidos..!");

            return result;
        }

        public async Task<DTOUsuario> RegistrarUsuario(DTOUsuario usuario)
        {
            if (usuario == null
             || usuario.Name.Length < 3
             || usuario.Username.Length < 3 
             || usuario.Password.Length < 3)
                throw new ArgumentException("Datos invalidos..!");

            var result = await _repositorioUsuario.RegistrarUsuarioAsync(usuario);

            if (result == null) throw new Exception("Usuario o Contraseña invalidos..!");

            return result;
        }
    }
}
