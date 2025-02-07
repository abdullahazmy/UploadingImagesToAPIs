using Microsoft.EntityFrameworkCore;

namespace Day4API.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        //    optionsBuilder.UseSqlServer("Data Source=ABDULLAHAZMY\\SQLEXPRESS;Initial Catalog=School;Integrated Security=True;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project_Department>(e =>
            {
                e.HasKey(e => new { e.ProjectId, e.DepartmentId });
            });

        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project_Department> Project_Departments { get; set; }
    }
}
