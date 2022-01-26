using CentroHipicoAPI.Nucleo.DTOs;
using CentroHipicoAPI.Nucleo.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Servicios
{
    public class ServicioCliente : IServicioCliente
    {
        private readonly IRepositorioCliente _repositorioCliente;

        public ServicioCliente(IRepositorioCliente repositorioCliente)
        {
            _repositorioCliente = repositorioCliente;
        }

        public async Task<List<DTOCliente>> ObtenerClientes()
        {
            return await _repositorioCliente.ObtenerClientes();
        }

        public async Task<DTOCliente> ObtenerClientePorId(int idCliente)
        {
            return await _repositorioCliente.ObtenerClientePorId(idCliente);
        }

        public async Task AgregarCliente(DTOCliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException();

            if (cliente.Nombre.Length <= 3)
                throw new Exception("Nombre inválido..!");

            var clientes = await _repositorioCliente.ObtenerClientes();
            if (clientes != null)
            {
                var clienteEncontrado = clientes.Find(x => x.Nombre.Trim().ToLower() == cliente.Nombre.Trim().ToLower());
                if (clienteEncontrado != null)
                {
                    throw new Exception("El cliente ya existe..!");
                }
            }

            await _repositorioCliente.AgregarCliente(cliente);
        }

        public async Task ActualizarCliente(DTOCliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException();

            if (cliente.Nombre.Length <= 3)
                throw new Exception("Nombre inválido..!");

            await _repositorioCliente.ActualizarCliente(cliente);
        }

        public async Task EliminarCliente(int idCliente)
        {
            if (idCliente < 1)
                throw new ArgumentException();

            await _repositorioCliente.EliminarCliente(idCliente);
        }
    }
}
