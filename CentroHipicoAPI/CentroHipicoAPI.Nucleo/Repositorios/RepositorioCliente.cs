using CentroHipicoAPI.Datos.Modelos.CentroHipico;
using CentroHipicoAPI.Nucleo.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Repositorios
{
    public class RepositorioCliente : RepositorioBase, IRepositorioCliente
    {
        public RepositorioCliente(CentroHipicoContext context) : base(context)
        {
        }

        public async Task<List<DTOCliente>> ObtenerClientes()
            => await (from c in _context.Clientes
                      select new DTOCliente
                      {
                          Id = c.Id,
                          Nombre = c.Nombre.Trim(),
                          Telefono = c.Telefono.Trim()
                      }).ToListAsync();

        public async Task<DTOCliente> ObtenerClientePorId(int idCliente)
            => await (from c in _context.Clientes
                      where c.Id == idCliente
                      select new DTOCliente
                      {
                          Id = c.Id,
                          Nombre = c.Nombre.Trim(),
                          Telefono = c.Telefono.Trim()
                      }).SingleOrDefaultAsync();

        public async Task AgregarCliente(DTOCliente cliente)
        {
            Cliente nuevoCliente = new Cliente
            {
                Id = cliente.Id ?? 0,
                Nombre = cliente.Nombre.Trim().ToLower(),
                Telefono = cliente.Telefono.Trim(),
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };
            await _context.Clientes.AddAsync(nuevoCliente);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarCliente(DTOCliente cliente)
        {
            var clienteEncontrado = await _context.Clientes.FindAsync(cliente.Id);
            if (clienteEncontrado == null) throw new NullReferenceException();
            clienteEncontrado.Nombre = cliente.Nombre.Trim().ToLower();
            clienteEncontrado.Telefono = cliente.Telefono.Trim();
            clienteEncontrado.FechaActualizacion = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarCliente(int idCliente)
        {
            var clienteEncontrado = await _context.Clientes.FindAsync(idCliente);
            if (clienteEncontrado == null) throw new NullReferenceException();
            _context.Clientes.Remove(clienteEncontrado);
            await _context.SaveChangesAsync();
        }
    }
}
