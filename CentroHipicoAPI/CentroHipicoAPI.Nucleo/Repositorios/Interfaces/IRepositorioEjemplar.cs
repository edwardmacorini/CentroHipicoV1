using CentroHipicoAPI.Nucleo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Repositorios
{
    public interface IRepositorioEjemplar
    {
        Task<List<DTOEjemplar>> ObtenerEjemplares();
        Task<DTOEjemplar> ObtenerEjemplarPorId(int idEjemplar);
        Task AgregarEjemplar(DTOEjemplar ejemplar);
        Task ActualizarEjemplar(DTOEjemplar ejemplar);
        Task EliminarEjemplar(int idEjemplar);
    }
}