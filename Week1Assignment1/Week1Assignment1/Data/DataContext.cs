using Microsoft.EntityFrameworkCore;
using Week1Assignment1.Models;

namespace Week1Assignment1.Data
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// Data Context
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
