using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<WebApplication3.Models.Instrument> Instrument { get; set; }
        public DbSet<WebApplication3.Models.Purchase> Purchase { get; set; }
        public DbSet<WebApplication3.Models.Customer> Customer { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");

            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Instrument>().ToTable("Instrument");
            modelBuilder.Entity<Purchase>().ToTable("Purchase");
        }
        
    }
}