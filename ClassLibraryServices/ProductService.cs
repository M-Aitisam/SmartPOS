using ClassLibraryDAL;
using ClassLibraryEntities;
using Microsoft.EntityFrameworkCore;

namespace ClassLibraryServices
{
    public class ProductService : IProductService
    {
        private readonly IDBOperations _dbOperations;
        private readonly AppDbContext _context;
        private readonly StateContainerService _stateContainer;

        // Add StateContainerService to constructor
        public ProductService(IDBOperations dbOperations,
                            AppDbContext context,
                            StateContainerService stateContainer)
        {
            _dbOperations = dbOperations;
            _context = context;
            _stateContainer = stateContainer;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .AsNoTracking()
                .ToListAsync();

            _stateContainer.Products = products; // Update state container
            return products;
        }

        public async Task AddProductAsync(Product product)
        {
            // Validate category exists
            var category = await _context.BusinessCategories
                .FirstOrDefaultAsync(c => c.CategoryID == product.CategoryID);

            if (category == null)
                throw new Exception("Invalid category");

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Refresh the product list
            await GetAllProductsAsync();
        }

        // Optional: Get products by category
        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryID == categoryId)
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}