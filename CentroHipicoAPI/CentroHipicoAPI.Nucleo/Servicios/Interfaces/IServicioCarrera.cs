using CentroHipicoAPI.Nucleo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Servicios
{
    public interface IServicioCarrera
    {
        Task<List<DTOCarrera>> ObtenerCarreras();
        Task<DTOCarreraResponse> ObtenerCarrera(int idCarrera);
        Task AgregarCarrera(DTOCarrera carrera);
        Task EliminarCarrera(DTOCarrera carrera);
        Task AgregarEjemplar(DTOCarreraEjemplar ejemplar);
        Task EliminarEjemplar(DTOCarreraEjemplar ejemplar);
        Task AgregarApuesta(DTOCarreraDetalle carreraDetalle);
        Task EliminarApuesta(DTOCarreraDetalle carreraDetalle);
        Task AbrirCarrera(int idCarrera);
        Task CerrarCarrera(int idCarrera);
        Task AgregarGanador(DTOCarreraGanador ganador);
    }
}