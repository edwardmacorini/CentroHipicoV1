using CentroHipicoAPI.Datos.Modelos.CentroHipico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Repositorios
{
    public class RepositorioBase
    {
        protected readonly CentroHipicoContext _context;

        public RepositorioBase(CentroHipicoContext context)
        {
            _context = context;
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }
    }
}
