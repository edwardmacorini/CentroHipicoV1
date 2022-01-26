using CentroHipicoAPI.Datos.Modelos.CentroHipico;
using CentroHipicoAPI.Nucleo.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Repositorios
{
    public class RepositorioEjemplar : RepositorioBase, IRepositorioEjemplar
    {
        public RepositorioEjemplar(CentroHipicoContext context) : base(context)
        {
        }

        public async Task<List<DTOEjemplar>> ObtenerEjemplares()
            => await (from e in _context.Ejemplares
                      select new DTOEjemplar
                      {
                          Id = e.Id,
                          Nombre = e.Nombre.Trim()
                      }).ToListAsync();

        public async Task<DTOEjemplar> ObtenerEjemplarPorId(int idEjemplar)
            => await (from e in _context.Ejemplares
                      where e.Id == idEjemplar
                      select new DTOEjemplar
                      {
                          Id = e.Id,
                          Nombre = e.Nombre.Trim()
                      }).SingleOrDefaultAsync();

        public async Task AgregarEjemplar(DTOEjemplar ejemplar)
        {
            Ejemplar nuevoEjemplar = new Ejemplar
            {
                Id = ejemplar.Id,
                Nombre = ejemplar.Nombre.Trim().ToLower(),
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };
            await _context.Ejemplares.AddAsync(nuevoEjemplar);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarEjemplar(DTOEjemplar ejemplar)
        {
            var ejemplarEncontrado = await _context.Ejemplares.FindAsync(ejemplar.Id);
            if (ejemplarEncontrado == null) throw new NullReferenceException();
            ejemplarEncontrado.Nombre = ejemplar.Nombre.Trim().ToLower();
            ejemplarEncontrado.FechaActualizacion = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarEjemplar(int idEjemplar)
        {
            var ejemplarEncontrado = await _context.Ejemplares.FindAsync(idEjemplar);
            if (ejemplarEncontrado == null) throw new NullReferenceException();
            _context.Ejemplares.Remove(ejemplarEncontrado);
            await _context.SaveChangesAsync();
        }
    }
}
