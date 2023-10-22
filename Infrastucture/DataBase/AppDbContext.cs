using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastucture.DataBase
{
    public class AppDbContext : DbContext
    {
        //ToDo: Проверерить связи
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

            modelBuilder.Entity<Employee>()
                .Navigation(e => e.MemberProjects).AutoInclude(false);

            modelBuilder.Entity<Employee>()
                .Navigation(e => e.AssignedTasks).AutoInclude();
            modelBuilder.Entity<Employee>()
               .Navigation(e => e.AuthoredTasks).AutoInclude();

            modelBuilder.Entity<Project>()
                .Navigation(p => p.ProjectEmployees).AutoInclude();

            modelBuilder.Entity<Project>()
                .Navigation(p => p.Tasks).AutoInclude();



            var directorId = Guid.NewGuid();
            var director = new Employee
            {
                Id = directorId,
                Firstname = "John",
                Lastname = "Doe",
                Patronymic = "Director",
                Email = "admin@mail.com",
                PasswordHash = "hashedpassword",
                Role = Domain.Enums.Role.Director
            };

            var projectId = Guid.NewGuid();
            var project = new Project
            {
                Id = projectId,
                Name = "Test Project",
                CustomerCompanyName = "Test Company",
                PerformingCompanyName = "Test Company",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                Priority = 1,
                ProjectManagerId = directorId,
            };

            var taskId = Guid.NewGuid();
            var task = new CustomTask
            {
                Id = taskId,
                Name = "Test Task",
                Comment = "This is a test task",
                Status = Domain.Enums.StatusTask.InProgress,
                Priority = 1,
                ProjectId = projectId,
                AuthorId = directorId,
            };

            modelBuilder.Entity<Employee>().HasData(director);
            modelBuilder.Entity<Project>().HasData(project);
            modelBuilder.Entity<CustomTask>().HasData(task);


            base.OnModelCreating(modelBuilder);

        }
    }
}
