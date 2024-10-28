using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebAppMvc.Models;

namespace WebAppMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            ChangeTracker.StateChanged += UpdateTimestamps;
            ChangeTracker.Tracked += UpdateTimestamps;
        }

        //https://learn.microsoft.com/ja-jp/ef/core/logging-events-diagnostics/events
        private static void UpdateTimestamps(object? sender, EntityEntryEventArgs e)
        {
            if (e.Entry.Entity is IHasTimestamps entityWithTimestamps)
            {
                switch (e.Entry.State)
                {
                    case EntityState.Modified:
                        entityWithTimestamps.UpdatedAt = DateTime.Now;//UtcNow
                        break;
                    case EntityState.Added:
                        entityWithTimestamps.CreatedAt = DateTime.Now;//UtcNow
                        entityWithTimestamps.UpdatedAt = DateTime.Now;//UtcNow
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //https://learn.microsoft.com/ja-jp/ef/core/modeling/relationships
            //https://learn.microsoft.com/ja-jp/ef/core/modeling/keys?tabs=fluent-api
            //リレーション
            modelBuilder.Entity<Student>()
                .HasOne(student => student.Department1)
                .WithMany(dept => dept.Students1)
                .HasForeignKey(student => student.Department1Id)
                .HasPrincipalKey(dept => dept.Id);
            modelBuilder.Entity<Student>()
                .HasOne(student => student.Department2)
                .WithMany(dept => dept.Students2)
                .HasForeignKey(student => student.Department2Id)
                .HasPrincipalKey(dept => dept.Id);
        }

        public DbSet<WebAppMvc.Models.Lesson> Lesson { get; set; } = default!;
        public DbSet<WebAppMvc.Models.Department> Department { get; set; } = default!;
        public DbSet<WebAppMvc.Models.Student> Student { get; set; } = default!;
    }
}
