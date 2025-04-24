using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public interface ICategoryService
    {
        Task<List<BusinessCategory>> GetCategoriesAsync();
        Task<bool> AddCategoryAsync(BusinessCategory category);
        Task<bool> AddSubItemAsync(BusinessSubItem subItem);
        Task UpdateCategoriesAsync(List<BusinessCategory> categories);

        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<bool> DeleteSubItemAsync(int subItemId);
    }
}
