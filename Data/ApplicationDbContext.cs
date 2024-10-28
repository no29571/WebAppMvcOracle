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
        public DbSet<WebAppMvc.Models.Lesson> Lesson { get; set; } = default!;
    }
}
