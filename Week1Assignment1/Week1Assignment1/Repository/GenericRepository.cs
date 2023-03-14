using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Week1Assignment1.Data;
using Week1Assignment1.Models;

namespace Week1Assignment1.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<T> table;

        /// <summary>
        /// Injecting DbContext
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(DataContext context)
        {
            _dbContext = context;
            table = _dbContext.Set<T>();
        }

        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="item"></param>
        public void Delete(T item)
        {
            table.Remove(item);
        }

        /// <summary>
        /// Get All Employees
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return table.ToList();
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetSingle(int id)
        {
            return table.Find(id);
        }

        /// <summary>
        /// Adding Data
        /// </summary>
        /// <param name="item"></param>
        public void Insert(T item)
        {
            table.Add(item);
        }

        /// <summary>
        /// Saving Changes
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Updating
        /// </summary>
        /// <param name="item"></param>
        public void Update(T item)
        {
            table.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Searching/Filtering
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> filter)
        {
            return await table.Where(filter).ToListAsync();
        }

        /// <summary>
        /// Sorting
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll<TKey>(Expression<Func<T, TKey>> orderBy, string sortBy)
        {
            var data = await table.ToListAsync();
            switch (sortBy)
            {
                case "name_desc":
                    data = await table.OrderByDescending(orderBy).ToListAsync();
                    break;
                case "name_asc":
                    data = await table.OrderBy(orderBy).ToListAsync();
                    break;
                default:
                    await table.ToListAsync();
                    break;
            }
            return data;
        }
    }
}
