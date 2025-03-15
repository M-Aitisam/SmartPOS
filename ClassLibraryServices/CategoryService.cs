using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly List<BusinessCategory> categories = new();

        public async Task<List<BusinessCategory>> GetCategoriesAsync()
        {
            return await Task.FromResult(categories);
        }

        public async Task<bool> AddCategoryAsync(BusinessCategory category)
        {
            category.CategoryID = categories.Count + 1;
            categories.Add(category);
            return await Task.FromResult(true);
        }

        public async Task<bool> AddSubItemAsync(BusinessSubItem subItem)
        {
            var category = categories.FirstOrDefault(c => c.CategoryID == subItem.CategoryID);
            if (category != null)
            {
                subItem.SubItemID = category.SubItems.Count + 1;
                category.SubItems.Add(subItem);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = categories.FirstOrDefault(c => c.CategoryID == categoryId);
            if (category != null)
            {
                categories.Remove(category);
                return await Task.FromResult(true);
            }
            return false;
        }

        public async Task<bool> DeleteSubItemAsync(int subItemId)
        {
            foreach (var category in categories)
            {
                var subItem = category.SubItems.FirstOrDefault(s => s.SubItemID == subItemId);
                if (subItem != null)
                {
                    category.SubItems.Remove(subItem);
                    return await Task.FromResult(true);
                }
            }
            return false;
        }
    }
}
