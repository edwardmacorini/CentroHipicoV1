using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentroHipicoAPI.Nucleo.Repositorios
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

    }
}