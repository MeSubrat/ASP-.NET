using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DATA
{
    public class FirstAPIContext : DbContext
    {
        public FirstAPIContext(DbContextOptions<FirstAPIContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().HasData(
                new Student
                { 
                    Id = 1, 
                    Name = "Subrat", 
                    Age = 22,
                    Email = "subrat@gmail.com"
                }
                );
        }
        
        public DbSet<Student> Students { get; set; }

    }
}
