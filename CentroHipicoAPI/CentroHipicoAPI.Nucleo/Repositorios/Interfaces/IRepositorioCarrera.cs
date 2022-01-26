using CentroHipicoAPI.Nucleo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Repositorios
{
    public interface IRepositorioCarrera
    {
        Task<List<DTOCarrera>> ObtenerCarreras();
        Task<DTOCarrera> ObtenerCarreraPorId(int idCarrera);
        Task<List<DTOCarreraDetalle>> ObtenerDetalles(int idCarrera);
        Task<List<DTOCarreraEjemplar>> ObtenerEjemplares(int idCarrera);
        Task<DTOCarreraResponse> ObtenerCarreraResponse(int idCarrera);
        Task AgregarCarrera(DTOCarrera carrera);
        Task ActualizarCarrera(DTOCarrera carrera);
        Task EliminarCarrera(int idCarrera);
        Task AgregarEjemplar(DTOCarreraEjemplar carreraEjemplar);
        Task EliminarEjemplar(int idCarreraEjemplar);
        Task AgregarApuesta(DTOCarreraDetalle carreraDetalle);
        Task ModificarApuesta(DTOCarreraDetalle carreraDetalle);
        Task EliminarApuesta(int idCarreraDetalle);
        Task AbrirCarrera(int idCarrera);
        Task CerrarCarrera(int idCarrera);
        Task AgregarGanador(int carrera, int idGanador);

    }
}