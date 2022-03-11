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
    public class RepositorioCarrera : RepositorioBase, IRepositorioCarrera
    {
        public RepositorioCarrera(CentroHipicoContext context) : base(context)
        {
        }

        public async Task<List<DTOCarrera>> ObtenerCarreras()
            => await (from c in _context.Carreras
                      select new DTOCarrera
                      {
                          Id = c.Id,
                          NumeroCarrera = c.NumeroCarrera,
                          FechaCarrera = c.FechaCarrera,
                          Ubicacion = c.Ubicacion,
                          EstaActiva = c.EstaActiva,
                          EstaCerrada = c.EstaCerrada
                      }).OrderByDescending(x => x.FechaCarrera)
                        .AsNoTracking()
                        .ToListAsync();

        public async Task<DTOCarrera> ObtenerCarreraPorId(int idCarrera)
            => await (from c in _context.Carreras
                      where c.Id == idCarrera
                      select new DTOCarrera
                      {
                          Id = c.Id,
                          FechaCarrera = c.FechaCarrera,
                          EstaActiva = c.EstaActiva,
                          EstaCerrada = c.EstaCerrada,
                          Ganador = c.Ganador,
                          MontoGanancia = c.MontoGanancia,
                          MontoSubTotal = c.MontoSubTotal,
                          MontoTotal = c.MontoTotal,
                          NumeroCarrera = c.NumeroCarrera,
                          Ubicacion = c.Ubicacion,
                      }).OrderByDescending(x => x.FechaCarrera)
                        .AsNoTracking()
                        .SingleOrDefaultAsync();

        public async Task<List<DTOCarreraDetalle>> ObtenerDetalles(int idCarrera)
            => await (from c in _context.Carreras
                      join cd in _context.CarreraDetalles on c.Id equals cd.IdCarrera
                      where c.Id == idCarrera
                      select new DTOCarreraDetalle
                      {
                          Id = cd.Id,
                          IdCarrera = cd.IdCarrera,
                          IdCliente = cd.IdCliente,
                          IdEjemplar = cd.IdEjemplar,
                          MontoApuesta = cd.MontoApuesta
                      }).AsNoTracking()
                        .ToListAsync();

        public async Task<List<DTOCarreraEjemplar>> ObtenerEjemplares(int idCarrera)
            => await (from c in _context.Carreras
                      join ce in _context.CarreraEjemplares on c.Id equals ce.IdCarrera
                      join e in _context.Ejemplares on ce.IdEjemplar equals e.Id
                      where c.Id == idCarrera
                      select new DTOCarreraEjemplar
                      {
                          Id = ce.Id,
                          IdCarrera = ce.IdCarrera,
                          Ejemplar = new DTOEjemplar
                          { Id = ce.Id, Nombre = e.Nombre.Trim() },
                          EsFavorito = ce.EsFavorito
                      }).AsNoTracking()
                        .ToListAsync();

        public async Task<DTOCarreraResponse> ObtenerCarreraResponse(int idCarrera)
        {
            DTOCarreraResponse carrera = new DTOCarreraResponse();
            carrera.Carrera = await (from c in _context.Carreras
                                     where c.Id == idCarrera
                                     select new DTOCarrera
                                     {
                                         Id = c.Id,
                                         NumeroCarrera = c.NumeroCarrera,
                                         FechaCarrera = c.FechaCarrera,
                                         Ubicacion = c.Ubicacion.Trim(),
                                         EstaActiva = c.EstaActiva,
                                         EstaCerrada = c.EstaCerrada,
                                         Ganador = c.Ganador,
                                         MontoGanancia = c.MontoGanancia,
                                         MontoSubTotal = c.MontoSubTotal,
                                         MontoTotal = c.MontoTotal
                                     }).AsNoTracking()
                                       .SingleOrDefaultAsync();

            if (carrera.Carrera.Ganador != null && carrera.Carrera.Ganador != 0)
                carrera.Ganador = await (from c in _context.Clientes
                                         where c.Id == carrera.Carrera.Ganador
                                         select new DTOCliente
                                         {
                                             Id = c.Id,
                                             Nombre = c.Nombre,
                                             Telefono = c.Telefono
                                         }).SingleOrDefaultAsync();

            carrera.Ejemplares = await (from ce in _context.CarreraEjemplares
                                        join e in _context.Ejemplares on ce.IdEjemplar equals e.Id
                                        where ce.IdCarrera == idCarrera
                                        select new DTOCarreraEjemplar
                                        {
                                            Id = ce.Id,
                                            IdCarrera = ce.IdCarrera,
                                            Ejemplar = new DTOEjemplar
                                            {
                                                Id = e.Id,
                                                Nombre = e.Nombre.Trim()
                                            },
                                            EsFavorito = ce.EsFavorito
                                        }).AsNoTracking().OrderBy(x => x.Id).ToListAsync();

            foreach (var ejemplar in carrera.Ejemplares)
            {
                var detalles = await (from cd in _context.CarreraDetalles
                                      join c in _context.Clientes
                                      on cd.IdCliente equals c.Id
                                      where cd.IdCarrera == idCarrera &&
                                            cd.IdEjemplar == ejemplar.Ejemplar.Id
                                      select new { cd, c }).AsNoTracking().FirstOrDefaultAsync();

                if (detalles != null)
                {
                    carrera.Detalles.Add(new DTOCarreraDetalleResponse
                    {
                        IdCarrera = idCarrera,
                        Ejemplar = ejemplar,
                        Cliente = new DTOCliente
                        {
                            Id = detalles.c.Id,
                            Nombre = detalles.c.Nombre.Trim(),
                            Telefono = detalles.c.Telefono.Trim()
                        },
                        MontoApuesta = detalles.cd.MontoApuesta
                    });
                }
                else
                {
                    carrera.Detalles.Add(new DTOCarreraDetalleResponse
                    {
                        IdCarrera = idCarrera,
                        Ejemplar = ejemplar
                    });
                }

            }

            return carrera;
        }

        public async Task AgregarCarrera(DTOCarrera carrera)
        {
            Carrera nuevaCarrera = new Carrera
            {
                NumeroCarrera = carrera.NumeroCarrera,
                FechaCarrera = carrera.FechaCarrera,
                Ubicacion = carrera.Ubicacion.Trim().ToLower(),
                EstaActiva = false,
                EstaCerrada = false,
                Ganador = null,
                MontoGanancia = null,
                MontoSubTotal = null,
                MontoTotal = null,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };

            await _context.Carreras.AddAsync(nuevaCarrera);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarCarrera(DTOCarrera carrera)
        {
            var carreraEncontrada = await _context.Carreras.FindAsync(carrera.Id);
            if (carreraEncontrada == null)
                throw new NullReferenceException();

            carreraEncontrada.EstaActiva = carrera.EstaActiva;
            carreraEncontrada.EstaCerrada = carrera.EstaCerrada;
            carreraEncontrada.Ganador = carrera.Ganador;
            carreraEncontrada.MontoGanancia = carrera.MontoGanancia;
            carreraEncontrada.MontoSubTotal = carrera.MontoSubTotal;
            carreraEncontrada.MontoTotal = carrera.MontoTotal;
            carreraEncontrada.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarCarrera(int idCarrera)
        {
            var carreraEncontrada = await _context.Carreras.FindAsync(idCarrera);
            if (carreraEncontrada == null)
                throw new NullReferenceException();

            _context.Carreras.Remove(carreraEncontrada);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarEjemplar(DTOCarreraEjemplar carreraEjemplar)
        {
            var ejemplarEncontrado = await _context.Ejemplares.FindAsync(carreraEjemplar.Ejemplar.Id);
            if (ejemplarEncontrado == null)
                throw new NullReferenceException("Ejemplar no encontrado..!");

            var existenciaEjemplar = await (from ce in _context.CarreraEjemplares
                                            where ce.IdEjemplar == carreraEjemplar.Ejemplar.Id &&
                                                  ce.IdCarrera == carreraEjemplar.IdCarrera
                                            select ce).ToListAsync();

            if (existenciaEjemplar != null && existenciaEjemplar.Count > 0)
                throw new Exception("El ejemplar ya se encuentra registrado en la carrera");

            CarreraEjemplar nuevoEjemplar = new CarreraEjemplar
            {
                IdEjemplar = ejemplarEncontrado.Id,
                IdCarrera = carreraEjemplar.IdCarrera,
                EsFavorito = carreraEjemplar.EsFavorito,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };

            await _context.CarreraEjemplares.AddAsync(nuevoEjemplar);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarEjemplar(int idCarreraEjemplar)
        {
            var carreraEjemplarEncontrado = await _context.CarreraEjemplares.FindAsync(idCarreraEjemplar);
            if (carreraEjemplarEncontrado == null)
                throw new NullReferenceException();

            _context.CarreraEjemplares.Remove(carreraEjemplarEncontrado);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarApuesta(DTOCarreraDetalle carreraDetalle)
        {
            CarreraDetalle nuevoCarreraDetalle = new CarreraDetalle
            {
                IdCarrera = carreraDetalle.IdCarrera,
                IdCliente = carreraDetalle.IdCliente,
                IdEjemplar = carreraDetalle.IdEjemplar,
                MontoApuesta = carreraDetalle.MontoApuesta,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };

            await _context.CarreraDetalles.AddAsync(nuevoCarreraDetalle);
            await _context.SaveChangesAsync();
        }

        public async Task ModificarApuesta(DTOCarreraDetalle carreraDetalle)
        {
            var carreraDetalleEncontrado = await _context.CarreraDetalles.FindAsync(carreraDetalle.Id);
            if (carreraDetalleEncontrado == null)
                throw new NullReferenceException();

            carreraDetalleEncontrado.IdCliente = carreraDetalle.IdCliente;
            carreraDetalleEncontrado.MontoApuesta = carreraDetalle.MontoApuesta;
            carreraDetalleEncontrado.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarApuesta(int idCarreraDetalle)
        {
            var carreraEncontrada = await _context.CarreraDetalles.FindAsync(idCarreraDetalle);
            if (carreraEncontrada == null)
                throw new NullReferenceException();

            _context.CarreraDetalles.Remove(carreraEncontrada);
            await _context.SaveChangesAsync();
        }

        public async Task AbrirCarrera(int idCarrera)
        {
            var carreraEncontrada = await _context.Carreras.FindAsync(idCarrera);
            if (carreraEncontrada == null)
                throw new NullReferenceException();

            carreraEncontrada.EstaActiva = true;
            carreraEncontrada.FechaActualizacion = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task CerrarCarrera(int idCarrera)
        {
            var carreraEncontrada = await _context.Carreras.FindAsync(idCarrera);
            if (carreraEncontrada == null)
                throw new NullReferenceException();

            carreraEncontrada.EstaCerrada = true;
            carreraEncontrada.FechaActualizacion = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task AgregarGanador(int carrera, int idCliente)
        {
            var carreraEncontrada = await _context.Carreras.FindAsync(carrera);
            if (carreraEncontrada == null)
                throw new NullReferenceException();

            carreraEncontrada.Ganador = idCliente;
            carreraEncontrada.FechaActualizacion = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
