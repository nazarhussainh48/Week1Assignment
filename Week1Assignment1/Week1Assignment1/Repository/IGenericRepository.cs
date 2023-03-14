using System.Linq.Expressions;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Models;

namespace Week1Assignment1.Repository
{
    public interface IGenericRepository<T>
    {
        List<T> GetAll();
        T GetSingle(int id);
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
        int SaveChanges();
        Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAll<TKey>(Expression<Func<T, TKey>> orderBy, string sortBy);

    }

}
