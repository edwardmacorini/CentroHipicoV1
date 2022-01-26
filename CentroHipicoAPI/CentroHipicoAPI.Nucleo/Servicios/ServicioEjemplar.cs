using CentroHipicoAPI.Nucleo.DTOs;
using CentroHipicoAPI.Nucleo.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Servicios
{
    public class ServicioEjemplar : IServicioEjemplar
    {
        private readonly IRepositorioEjemplar _repositorioEjemplar;

        public ServicioEjemplar(IRepositorioEjemplar repositorioEjemplar)
        {
            _repositorioEjemplar = repositorioEjemplar;
        }

        public async Task<List<DTOEjemplar>> ObtenerEjemplares()
        {
            return await _repositorioEjemplar.ObtenerEjemplares();
        }

        public async Task<DTOEjemplar> ObtenerEjemplarPorId(int idEjemplar)
        {
            return await _repositorioEjemplar.ObtenerEjemplarPorId(idEjemplar);
        }

        public async Task AgregarEjemplar(DTOEjemplar ejemplar)
        {
            if (ejemplar == null)
                throw new ArgumentNullException();

            if (ejemplar.Nombre.Length <= 3)
                throw new Exception("Nombre inválido..!");

            var ejemplares = await _repositorioEjemplar.ObtenerEjemplares();
            if (ejemplares != null)
            {
                var ejemplarEncontrado = ejemplares.Find(x => x.Nombre.Trim().ToLower() == ejemplar.Nombre.Trim().ToLower());
                if (ejemplarEncontrado != null)
                {
                    throw new Exception("El ejemplar ya existe..!");
                }
            }

            await _repositorioEjemplar.AgregarEjemplar(ejemplar);
        }

        public async Task ActualizarEjemplar(DTOEjemplar ejemplar)
        {
            if (ejemplar == null)
                throw new ArgumentNullException();

            if (ejemplar.Nombre.Length <= 3)
                throw new Exception("Nombre inválido..!");

            await _repositorioEjemplar.ActualizarEjemplar(ejemplar);
        }

        public async Task EliminarEjemplar(int idEjemplar)
        {
            if (idEjemplar < 1)
                throw new ArgumentException();

            await _repositorioEjemplar.EliminarEjemplar(idEjemplar);
        }
    }
}
