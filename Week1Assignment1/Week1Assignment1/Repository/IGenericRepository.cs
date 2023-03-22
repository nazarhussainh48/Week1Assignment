using System.Linq.Expressions;
using Week1Assignment1.DTO.Employee;
using Week1Assignment1.Models;

namespace Week1Assignment1.Repository
{
    public interface IGenericRepository<T>
    {
        List<T> GetAll(string? orderBy, int pageSize = 3, int currentPage = 1, string? filter = null);
        T GetSingle(int id);
        Task<T> Insert(T item);
        Task Update(T item);
        Task<T> Delete(int id);
        int SaveChanges();
        

    }

}
