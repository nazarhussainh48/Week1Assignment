using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Week1Assignment1.Data;

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
        public async Task<T> Delete(int id)
        {
            var entity = GetSingle(id);
            table.Remove(entity);
            SaveChanges();
            return entity;
        }

        /// <summary>
        /// Get All Employees
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll(int page, int pageSize)
        {
            var query = table.AsQueryable();
            var totalRecords = query.Count();

            if (totalRecords <= 0)
                return null;

            var items = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            return items;
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
        /// Adding Employee
        /// </summary>
        /// <param name="item"></param>
        public async Task<T> Insert(T item)
        {
            await table.AddAsync(item);
            SaveChanges();
            return item;
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
        public async Task Update(T item)
        {
            table.Attach(item);
            foreach(var property in _dbContext.Entry(item).Properties)
            {
                var currentValue = property.CurrentValue;
                var isModified = property.IsModified;
                if(currentValue == null && isModified )
                {
                    property.IsModified = false;
                }
            }
            SaveChanges();
        }

        /// <summary>
        /// Searching/Filtering
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> predicate)
        {
            return await table.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Sorting
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        /// 
        public async Task<IEnumerable<T>> Sorting(string sortBy, string order)
        {
            IQueryable<T> q = from p in table select p;
            var field = char.ToUpper(sortBy[0]) + (sortBy.Substring(1).ToLower());
            q = q.OrderBy(p => EF.Property<object>(p, field));

            switch (order)
            {
                case "asc":
                    q = q.OrderBy(p => EF.Property<object>(p, field));
                    break;
                case "desc":
                    q = q.OrderByDescending(p => EF.Property<object>(p, field));
                    break;
                default:
                    await q.ToListAsync();
                    break;
            }
            return q;
        }
    }
}
