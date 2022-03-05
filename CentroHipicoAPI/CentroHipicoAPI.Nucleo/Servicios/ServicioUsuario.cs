using CentroHipicoAPI.Nucleo.DTOs;
using CentroHipicoAPI.Nucleo.Repositorios;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CentroHipicoAPI.Nucleo.Utilitarios;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace CentroHipicoAPI.Nucleo.Servicios
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IConfiguration _configuration;

        public ServicioUsuario(IRepositorioUsuario repositorioUsuario, IConfiguration configuration)
        {
            _repositorioUsuario = repositorioUsuario;
            _configuration = configuration;
        }

        public async Task<DTOUsuarioResponse> AutenticarUsuario(DTOUsuarioRequest usuario)
        {
            if (usuario == null || usuario.Username.Length < 3 || usuario.Password.Length < 3)
                throw new ArgumentException("Datos invalidos..!");

            var usuarioAutenticado = await _repositorioUsuario.AutenticarUsuarioAsync(usuario);

            if (usuarioAutenticado == null) throw new Exception("Usuario o Contraseña invalidos..!");
            
            var token = GenerateJwtToken(usuarioAutenticado);

            return new DTOUsuarioResponse(usuarioAutenticado, token);
        }

        private string GenerateJwtToken(DTOUsuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration["SecretKey"]);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Username));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };

            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken);
        }

        public async Task<DTOUsuarioResponse> RegistrarUsuario(DTOUsuario usuario)
        {
            if (usuario == null
             || usuario.Name.Length < 3
             || usuario.Username.Length < 3 
             || usuario.Password.Length < 3)
                throw new ArgumentException("Datos invalidos..!");

            var result = await _repositorioUsuario.RegistrarUsuarioAsync(usuario);

            if (result == null) throw new Exception("Usuario o Contraseña invalidos..!");

            var usuarioAutenticado = await AutenticarUsuario(new DTOUsuarioRequest
            {
                Username = result.Username,
                Password = result.Password
            });

            return usuarioAutenticado;
        }

        public Task<DTOUsuario> ObtenerUsuarioPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();

            return _repositorioUsuario.ObtenerUsuarioPorIdAsync(id);
        }
    }
}
