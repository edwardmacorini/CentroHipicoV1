using CentroHipicoAPI.Datos.Modelos.CentroHipico;
using CentroHipicoAPI.Nucleo.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CentroHipicoAPI.Nucleo.Repositorios
{
    public class RepositorioUsuario : RepositorioBase, IRepositorioUsuario
    {
        public RepositorioUsuario(CentroHipicoContext context) : base(context)
        {

        }

        public async Task<DTOUsuario> AutenticarUsuarioAsync(DTOUsuarioRequest usuario)
        {
            DTOUsuario result = await (from u in _context.Users
                                                  where u.Username.Trim().ToLower() == usuario.Username.Trim().ToLower() &&
                                                  u.Password.Trim() == usuario.Password.Trim()
                                                  select new DTOUsuario
                                                  {
                                                      Id = u.Id,
                                                      Name = u.Name.Trim().ToLower(),
                                                      Username = u.Username.Trim().ToLower()
                                                  }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<DTOUsuario> RegistrarUsuarioAsync(DTOUsuario usuario)
        {
            var nuevoUsuario = new User
            {
                Name = usuario.Name.Trim().ToLower(),
                Username = usuario.Username.Trim().ToLower(),
                Password = usuario.Password.Trim(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var result = await _context.Users.AddAsync(nuevoUsuario);
            await _context.SaveChangesAsync();
            if (result != null) usuario.Id = result.Entity.Id;
            return usuario;
        }
    }
}
