using ClassLibraryDAL;
using ClassLibraryEntities;
using Microsoft.EntityFrameworkCore;

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
            return await _context.BusinessCategories.ToListAsync();
        }


        public async Task<bool> AddCategoryAsync(BusinessCategory category)
        {
            _context.BusinessCategories.Add(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddSubItemAsync(BusinessSubItem subItem)
        {
            _context.SubCategories.Add(subItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateCategoriesAsync(List<BusinessCategory> categories)
        {
            foreach (var category in categories)
            {
                _context.Entry(category).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }



        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.BusinessCategories.FindAsync(categoryId);
            if (category != null)
            {
                _context.BusinessCategories.Remove(category);
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
            return await _context.BusinessCategories
                .AsNoTracking()
                .Include(c => c.SubItems)
                    .ThenInclude(s => s.NestedSubItems)
                .Include(c => c.Products)
                .ToListAsync();
        }
    }
}