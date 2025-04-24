using ClassLibraryDAL;
using ClassLibraryEntities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibraryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BusinessCategory>> GetCategoriesAsync()
        {
            return await _context.Categories
        .Include(c => c.SubItems)
            .ThenInclude(s => s.NestedSubItems)
        .Include(c => c.Products) // Add this line
        .AsNoTracking()
        .ToListAsync();
        }

        public async Task<bool> AddCategoryAsync(BusinessCategory category)
        {
            _context.Categories.Add(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddSubItemAsync(BusinessSubItem subItem)
        {
            _context.SubCategories.Add(subItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteSubItemAsync(int subItemId)
        {
            var subItem = await _context.SubCategories.FindAsync(subItemId);
            if (subItem != null)
            {
                _context.SubCategories.Remove(subItem);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<List<BusinessCategory>> GetCategoriesWithProductsAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .Include(c => c.SubItems)
                    .ThenInclude(s => s.NestedSubItems)
                .Include(c => c.Products) // Add this
                .ToListAsync();
        }
    }
}
