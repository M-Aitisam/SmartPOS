using Microsoft.EntityFrameworkCore;
using ClassLibraryEntities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibraryDAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define DbSets (Tables)
        public DbSet<BusinessCategory> Categories { get; set; }
        public DbSet<BusinessSubItem> SubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure BusinessCategory relationships
            modelBuilder.Entity<BusinessCategory>()
                .HasMany(c => c.SubItems)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure BusinessSubItem self-referencing relationship
            modelBuilder.Entity<BusinessSubItem>()
                .HasOne(s => s.ParentSubItem)
                .WithMany(p => p.NestedSubItems)
                .HasForeignKey(s => s.ParentSubItemID)
                .OnDelete(DeleteBehavior.Restrict);

            // Ensure string properties are properly handled
            modelBuilder.Entity<BusinessCategory>()
                .Property(c => c.CategoryName)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<BusinessSubItem>()
                .Property(s => s.SubItemName)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
