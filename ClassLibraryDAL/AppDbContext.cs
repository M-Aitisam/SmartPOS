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
        public DbSet<Product> Products { get; set; }
        public DbSet<BusinessTransaction> Transactions { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure BusinessCategory relationships
            modelBuilder.Entity<BusinessCategory>()
                    .HasMany(c => c.SubItems)
                    .WithOne(s => s.Category)
                    .HasForeignKey(s => s.CategoryID)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BusinessSubItem>()
                .HasOne(s => s.ParentSubItem)
                .WithMany(p => p.NestedSubItems)
                .HasForeignKey(s => s.ParentSubItemID)
                .OnDelete(DeleteBehavior.Restrict);

            // Add new product relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.SubCategory)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SubCategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Existing transaction configurations (keep these)
            modelBuilder.Entity<BusinessTransaction>()
                .HasKey(b => b.TransactionID);

            modelBuilder.Entity<TransactionItem>()
                .HasKey(t => t.TransactionItemID);

            modelBuilder.Entity<BusinessTransaction>()
                .HasMany(b => b.Items)
                .WithOne()
                .HasForeignKey(t => t.TransactionID)
                .OnDelete(DeleteBehavior.Cascade);

            // String property configurations (keep these)
            modelBuilder.Entity<BusinessCategory>()
                .Property(c => c.CategoryName)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<BusinessSubItem>()
                .Property(s => s.SubItemName)
                .IsRequired()
                .HasMaxLength(255); ;
        }
    }
}
