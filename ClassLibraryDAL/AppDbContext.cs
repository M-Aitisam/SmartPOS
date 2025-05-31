using ClassLibraryEntities;
using Microsoft.EntityFrameworkCore;

namespace ClassLibraryDAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<BusinessDetails> BusinessDetails { get; set; }

        // Define DbSets (Tables)
        public DbSet<BusinessCategory> BusinessCategories { get; set; }
        public DbSet<BusinessModel> Businesses { get; set; }
        public DbSet<BusinessSubItem> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<BusinessTransaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
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

            // Configure self-referencing subcategories
            modelBuilder.Entity<BusinessSubItem>()
                .HasOne(s => s.ParentSubItem)
                .WithMany(p => p.NestedSubItems)
                .HasForeignKey(s => s.ParentSubItemID)
                .OnDelete(DeleteBehavior.Restrict);

            // Product relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Optional: You can apply constraints on ProductUrduName here
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductUrduName)
                .HasMaxLength(255); // Example


            modelBuilder.Entity<TransactionItem>()
          .HasOne(ti => ti.Transaction)
          .WithMany(bt => bt.Items)
          .HasForeignKey(ti => ti.TransactionId)
          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BusinessModel>()
               .HasKey(b => b.BusinessID);

            // Configure other entity properties if needed
            modelBuilder.Entity<BusinessDetails>()
                .Property(b => b.BusinessName)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<BusinessDetails>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ReceiptHeader).IsRequired();
                entity.Property(e => e.ReceiptFooter).IsRequired();
                entity.Property(e => e.DefaultTaxRate).HasDefaultValue(15.0m);
                entity.Property(e => e.IncludeBarcode).HasDefaultValue(true);
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(d => d.DiscountId);

                entity.HasOne(d => d.BusinessModel)
                      .WithMany(b => b.DefaultDiscounts)
                      .HasForeignKey(d => d.BusinessModelId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
