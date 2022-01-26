using CentroHipicoAPI.Nucleo.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Repositorios
{
    public interface IRepositorioCliente
    {
        Task<List<DTOCliente>> ObtenerClientes();
        Task<DTOCliente> ObtenerClientePorId(int idCliente);
        Task AgregarCliente(DTOCliente cliente);
        Task ActualizarCliente(DTOCliente cliente);
        Task EliminarCliente(int idCliente);

    }
}
