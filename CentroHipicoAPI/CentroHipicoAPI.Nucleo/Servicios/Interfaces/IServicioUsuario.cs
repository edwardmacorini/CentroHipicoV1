using CentroHipicoAPI.Nucleo.DTOs;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Servicios
{
    public interface IServicioUsuario
    {
        Task<DTOUsuario> AutenticarUsuario(DTOUsuarioRequest usuario);
        Task<DTOUsuario> RegistrarUsuario(DTOUsuario usuario);
    }
}