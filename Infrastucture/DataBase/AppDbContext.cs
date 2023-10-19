using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastucture.DataBase
{
    public class AppDbContext : DbContext
    {
        //Проверерить связи
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<CustomTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomTask>()
                .HasOne(t => t.Author)
                .WithMany(e => e.AuthoredTasks)
                .HasForeignKey(t => t.AuthorId);

            modelBuilder.Entity<CustomTask>()
                .HasOne(t => t.Executor)
                .WithMany(e => e.AssignedTasks)
                .HasForeignKey(t => t.ExecutorId)
                .IsRequired(false);

            modelBuilder.Entity<Employee>()
         .HasMany(e => e.MemberProjects)
         .WithMany(p => p.ProjectEmployees)
         .UsingEntity(j => j.ToTable("EmployeeProject"));

            modelBuilder.Entity<Project>()
         .HasOne(p => p.ProjectManager)
         .WithMany(e => e.ManagedProjects)
         .HasForeignKey(p => p.ProjectManagerId)
         .IsRequired(false);
        }

    }
}
