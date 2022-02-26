using CentroHipicoAPI.Nucleo.DTOs;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Repositorios
{
    public interface IRepositorioUsuario
    {
        Task<DTOUsuario> AutenticarUsuarioAsync(DTOUsuarioRequest usuario);
        Task<DTOUsuario> RegistrarUsuarioAsync(DTOUsuario usuario);
    }
}