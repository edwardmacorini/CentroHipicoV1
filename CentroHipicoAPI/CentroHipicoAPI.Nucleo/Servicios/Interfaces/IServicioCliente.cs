using CentroHipicoAPI.Nucleo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Servicios
{
    public interface IServicioCliente
    {
        Task<List<DTOCliente>> ObtenerClientes();
        Task<DTOCliente> ObtenerClientePorId(int idCliente);
        Task AgregarCliente(DTOCliente cliente);
        Task ActualizarCliente(DTOCliente cliente);
        Task EliminarCliente(int idCliente);
    }
}