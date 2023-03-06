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

        public DbSet<MyUser> Users { get; set; }

        public DbSet<Weapon> Weapons { get; set; }

        public DbSet<Skill> Skills { get; set; }
        
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeSkill>()
                .HasKey(cs => new { cs.EmployeeId, cs.SkillId });
        }
    }
}
