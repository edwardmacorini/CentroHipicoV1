using CentroHipicoAPI.Nucleo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Servicios
{
    public interface IServicioEjemplar
    {
        Task<List<DTOEjemplar>> ObtenerEjemplares();
        Task<DTOEjemplar> ObtenerEjemplarPorId(int idEjemplar);
        Task AgregarEjemplar(DTOEjemplar ejemplar);
        Task ActualizarEjemplar(DTOEjemplar ejemplar);
        Task EliminarEjemplar(int idEjemplar);
    }
}