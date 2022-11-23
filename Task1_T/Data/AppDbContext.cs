using Microsoft.EntityFrameworkCore;
using Task1_T.Models.Entities;
using Task1_T.Models.Shared;
namespace Task1_T.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<CommonEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .HasOne(p => p.Position) 
            .WithMany(e => e.Employees);

            modelBuilder.Entity<Employee>().HasMany(e => e.Children)
                .WithOne(c => c.EmployeeParent)
                .HasForeignKey(p=>p.EmployeeParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeDepartment>().HasKey(ed => ed.Id);

            modelBuilder.Entity<EmployeeDepartment>().HasOne(d => d.Department)
            .WithMany(ed => ed.EmployeeDepartments).HasForeignKey(d => d.DepartmentId);

            modelBuilder.Entity<EmployeeDepartment>().HasOne(e=> e.Employee)
            .WithMany(ed => ed.EmployeeDepartments).HasForeignKey(e => e.EmployeeId);


            modelBuilder.Entity<UserPermission>().HasKey(up => up.Id);

            modelBuilder.Entity<UserPermission>().HasOne(u => u.User)
            .WithMany(up => up.UserPermissions).HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserPermission>().HasOne(p => p.Permission)
            .WithMany(up => up.UserPermissions).HasForeignKey(p=>p.PermissionId);
        }
    }
}
