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

            _stateContainer.Products = products;
            return products;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            var category = await _context.BusinessCategories
                .FirstOrDefaultAsync(c => c.CategoryID == product.CategoryID);

            if (category == null)
                throw new Exception("Invalid category");

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            await GetAllProductsAsync();

            return product; // Return the added product
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductID == product.ProductID);

            if (existingProduct == null)
                throw new Exception("Product not found");

            existingProduct.ProductTitle = product.ProductTitle;
            existingProduct.ProductPrice = product.ProductPrice;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.IsActive = product.IsActive;
            existingProduct.CategoryID = product.CategoryID;

            await _context.SaveChangesAsync();
            await GetAllProductsAsync();

            return existingProduct; // Return the updated product
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            await GetAllProductsAsync();
            return true;
        }

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
