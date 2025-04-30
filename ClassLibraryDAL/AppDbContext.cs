using ClassLibraryEntities;
using Microsoft.EntityFrameworkCore;

namespace ClassLibraryDAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define DbSets (Tables)
        public DbSet<BusinessCategory> BusinessCategories { get; set; }
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

            modelBuilder.Entity<BusinessCategory>()
                    .ToTable("BusinessCategories");

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


            
        }
    }
}
