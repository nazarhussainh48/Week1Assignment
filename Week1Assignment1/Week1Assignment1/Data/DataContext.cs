using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Week1Assignment1.Models;

namespace Week1Assignment1.Data
{
    public class DataContext : IdentityDbContext
    {
        /// <summary>
        /// Data Context
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<MyUser> Users { get; set; }

        
    }
}
