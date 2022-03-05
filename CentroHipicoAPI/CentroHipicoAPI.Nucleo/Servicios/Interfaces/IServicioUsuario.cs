using CentroHipicoAPI.Nucleo.DTOs;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Servicios
{
    public interface IServicioUsuario
    {
        Task<DTOUsuario> ObtenerUsuarioPorId(int id);
        Task<DTOUsuarioResponse> AutenticarUsuario(DTOUsuarioRequest usuario);
        Task<DTOUsuarioResponse> RegistrarUsuario(DTOUsuario usuario);
    }
}