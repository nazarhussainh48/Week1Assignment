using System.Linq.Expressions;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Models;

namespace Week1Assignment1.Repository
{
    public interface IGenericRepository<T>
    {
        List<T> GetAll(int page, int pageSize);
        T GetSingle(int id);
        Task<T> Insert(T item);
        Task Update(T item);
        Task<T> Delete(int id);
        int SaveChanges();
        Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> Sorting(string sortBy, string order);



    }

}
