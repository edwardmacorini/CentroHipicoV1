using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroHipicoAPI.Nucleo.DTOs;
using CentroHipicoAPI.Nucleo.Repositorios;

namespace CentroHipicoAPI.Nucleo.Servicios
{
    public class ServicioCarrera : IServicioCarrera
    {
        private readonly IRepositorioCarrera _repositorioCarrera;
        private readonly IRepositorioEjemplar _repositorioEjemplar;

        public ServicioCarrera(IRepositorioCarrera repositorioCarrera,
                               IRepositorioEjemplar repositorioEjemplar)
        {
            _repositorioCarrera = repositorioCarrera;
            _repositorioEjemplar = repositorioEjemplar;
        }

        public async Task<List<DTOCarrera>> ObtenerCarreras()
            => await _repositorioCarrera.ObtenerCarreras();

        public async Task<DTOCarreraResponse> ObtenerCarrera(int idCarrera)
            => await _repositorioCarrera.ObtenerCarreraResponse(idCarrera);

        public async Task AgregarCarrera(DTOCarrera carrera)
        {
            if (carrera == null)
                throw new ArgumentException();

            carrera.FechaCarrera = carrera.FechaCarrera.AddHours(-4);

            await _repositorioCarrera.AgregarCarrera(carrera);
        }

        public async Task EliminarCarrera(DTOCarrera carrera)
        {
            if (carrera == null && carrera.Id <= 0)
                throw new ArgumentException();

            var carreraEncontrada = await _repositorioCarrera.ObtenerCarreraPorId(carrera.Id);
            if (carreraEncontrada == null)
                throw new NullReferenceException("La carrera no existe..!");

            var detalles = await _repositorioCarrera.ObtenerDetalles(carrera.Id);
            if (detalles != null && detalles.Count > 0)
                throw new Exception("La carrera tiene apuestas abiertas..!");

            var ejemplares = await _repositorioCarrera.ObtenerEjemplares(carrera.Id);
            if (ejemplares != null && ejemplares.Count > 0)
            {
                foreach (var ejemplar in ejemplares)
                {
                    await _repositorioCarrera.EliminarEjemplar(ejemplar.Id);
                }
            }

            await _repositorioCarrera.EliminarCarrera(carrera.Id);
        }

        public async Task AgregarEjemplar(DTOCarreraEjemplar ejemplar)
        {
            if (ejemplar == null ||
                ejemplar.Ejemplar == null ||
                ejemplar.Ejemplar.Id == 0 ||
                ejemplar.IdCarrera == 0)
                throw new ArgumentNullException("Información invalidad..!");

            var ejemplarEncontrado = await _repositorioEjemplar.ObtenerEjemplarPorId(ejemplar.Ejemplar.Id);
            if (ejemplarEncontrado == null)
                throw new NullReferenceException("Ejemplar no encontrado..!");

            var ejemplaresRegistrados = await _repositorioCarrera.ObtenerEjemplares(ejemplar.IdCarrera);
            if (ejemplaresRegistrados != null && ejemplaresRegistrados.Count > 0)
            {
                var validacion = ejemplaresRegistrados.Where(x => x.Ejemplar.Id == ejemplarEncontrado.Id).Any();
                if (validacion == true)
                    throw new Exception("El ejemplar ya se encuentra registrado en la carrera");
            }

            await _repositorioCarrera.AgregarEjemplar(ejemplar);
        }

        public async Task EliminarEjemplar(DTOCarreraEjemplar ejemplar)
        {
            if (ejemplar == null)
                throw new ArgumentNullException();

            var carrera = await _repositorioCarrera.ObtenerCarreraPorId(ejemplar.IdCarrera);
            if (carrera == null)
                throw new NullReferenceException();

            if (carrera.EstaCerrada == true)
                throw new Exception("Carrera Cerrada");

            var detalles = await _repositorioCarrera.ObtenerDetalles(ejemplar.IdCarrera);
            if (detalles != null)
            {
                var validacion = detalles.Where(x => x.IdEjemplar == ejemplar.Ejemplar.Id).Any();
                if (validacion == true)
                    throw new Exception("El ejemplar se encuentra asignado a una apuesta");
            }

            await _repositorioCarrera.EliminarEjemplar(ejemplar.Id);
        }

        public async Task AgregarApuesta(DTOCarreraDetalle carreraDetalle)
        {
            if (carreraDetalle == null)
                throw new ArgumentNullException();

            var carrera = await _repositorioCarrera.ObtenerCarreraPorId(carreraDetalle.IdCarrera);
            if (carrera == null)
                throw new NullReferenceException();

            if (carrera.MontoGanancia == null ||
                carrera.MontoSubTotal == null ||
                carrera.MontoTotal == null)
            {
                carrera.MontoGanancia = 0;
                carrera.MontoSubTotal = 0;
                carrera.MontoTotal = 0;
            }

            var detalles = await _repositorioCarrera.ObtenerDetalles(carreraDetalle.IdCarrera);
            if (detalles == null)
            {
                carrera.MontoGanancia += carreraDetalle.MontoApuesta * 0.2M;
                carrera.MontoSubTotal += carreraDetalle.MontoApuesta * 0.8M;
                carrera.MontoTotal += carreraDetalle.MontoApuesta;
                await _repositorioCarrera.AgregarApuesta(carreraDetalle);
                await _repositorioCarrera.ActualizarCarrera(carrera);
            }
            else
            {
                var detalleEncontrado = detalles.Where(x => x.IdEjemplar == carreraDetalle.IdEjemplar).SingleOrDefault();
                if (detalleEncontrado != null)
                {
                    if (detalleEncontrado.MontoApuesta < carreraDetalle.MontoApuesta)
                    {
                        carreraDetalle.Id = detalleEncontrado.Id;

                        carrera.MontoGanancia -= detalleEncontrado.MontoApuesta * 0.2M;
                        carrera.MontoSubTotal -= detalleEncontrado.MontoApuesta * 0.8M;
                        carrera.MontoTotal -= detalleEncontrado.MontoApuesta;

                        carrera.MontoGanancia += carreraDetalle.MontoApuesta * 0.2M;
                        carrera.MontoSubTotal += carreraDetalle.MontoApuesta * 0.8M;
                        carrera.MontoTotal += carreraDetalle.MontoApuesta;

                        await _repositorioCarrera.ModificarApuesta(carreraDetalle);
                        await _repositorioCarrera.ActualizarCarrera(carrera);
                    }
                }
                else
                {
                    carrera.MontoGanancia += carreraDetalle.MontoApuesta * 0.2M;
                    carrera.MontoSubTotal += carreraDetalle.MontoApuesta * 0.8M;
                    carrera.MontoTotal += carreraDetalle.MontoApuesta;
                    await _repositorioCarrera.AgregarApuesta(carreraDetalle);
                    await _repositorioCarrera.ActualizarCarrera(carrera);
                }
            }
        }

        public async Task EliminarApuesta(DTOCarreraDetalle carreraDetalle)
        {
            if (carreraDetalle == null)
                throw new ArgumentNullException();

            var carrera = await _repositorioCarrera.ObtenerCarreraPorId(carreraDetalle.IdCarrera);
            if (carrera == null)
                throw new NullReferenceException();

            if (carrera.EstaCerrada == true)
                throw new Exception("Carrera Cerrada");

            var detalles = await _repositorioCarrera.ObtenerDetalles(carreraDetalle.IdCarrera);
            if (detalles == null)
                throw new NullReferenceException();

            var detalle = detalles.Where(x => x.Id == carreraDetalle.Id).SingleOrDefault();
            if (detalle == null)
                throw new NullReferenceException();

            await _repositorioCarrera.EliminarApuesta(detalle.Id);
        }

        public async Task AbrirCarrera(int idCarrera)
        {
            if (idCarrera <= 0)
                throw new ArgumentNullException();

            await _repositorioCarrera.AbrirCarrera(idCarrera);
        }

        public async Task CerrarCarrera(int idCarrera)
        {
            if (idCarrera <= 0)
                throw new ArgumentNullException();

            await _repositorioCarrera.CerrarCarrera(idCarrera);
        }

        public async Task AgregarGanador(DTOCarreraGanador ganador)
        {
            if (ganador == null)
                throw new ArgumentNullException();

            await _repositorioCarrera.AgregarGanador(ganador.IdCarrera, ganador.IdCliente);
        }
    }
}
