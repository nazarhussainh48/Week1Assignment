using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using MailKit.Search;
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
        public List<T> GetAll(string? orderBy, int pageSize = 3, int currentPage = 1, string? filter = null)
        {
            IQueryable<T> query = from p in table select p;

            // Apply filters
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(filter);

            // Apply sorting
            if (!string.IsNullOrEmpty(orderBy))
                query = query.OrderBy(p => EF.Property<object>(p, orderBy));

            // Apply pagination
            var skip = (currentPage - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            return query.ToList();
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
    }
}
