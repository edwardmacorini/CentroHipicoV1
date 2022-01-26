using CentroHipicoAPI.Datos.Modelos.CentroHipico;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Repositorios
{
    public class RepositorioGenerico<T> : RepositorioBase, IRepositorioGenerico<T> where T : class
    {
        #region Propiedades
        private DbSet<T> entities;
        #endregion

        #region Constructor
        public RepositorioGenerico(CentroHipicoContext context) : base(context)
        {
            entities = _context.Set<T>();
        }
        #endregion

        #region Metodos Sincronos
        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }
        public T Get(int id)
        {
            return entities.Find(id);
        }
        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException();

            entities.Add(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException();
            entities.Update(entity);
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException();
            entities.Remove(entity);
            _context.SaveChanges();
        }
        #endregion

        #region Metodos Asincronos
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }
        public async Task<T> GetAsync(int id)
        {
            return await entities.FindAsync(id);
        }
        public async Task InsertAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException();

            await entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException();
            entities.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException();
            entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
