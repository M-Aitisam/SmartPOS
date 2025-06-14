﻿using ClassLibraryDAL;
using ClassLibraryEntities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return await _context.BusinessCategories
                .Include(c => c.SubItems)
                    .ThenInclude(s => s.NestedSubItems)
                .AsNoTracking()
                .ToListAsync();
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

        // Single category update
        public async Task UpdateCategoryAsync(BusinessCategory category)
        {
            var existingCategory = await _context.BusinessCategories
                .Include(c => c.SubItems)
                    .ThenInclude(si => si.NestedSubItems)
                .FirstOrDefaultAsync(c => c.CategoryID == category.CategoryID);

            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                existingCategory.UrduName = category.UrduName;
                existingCategory.ImageUrl = category.ImageUrl;
                existingCategory.Description = category.Description;
                await _context.SaveChangesAsync();
            }
        }

        // Multiple categories update
        public async Task UpdateCategoriesAsync(List<BusinessCategory> categories)
        {
            foreach (var category in categories)
            {
                var existingCategory = await _context.BusinessCategories
                    .Include(c => c.SubItems)
                        .ThenInclude(si => si.NestedSubItems)
                    .FirstOrDefaultAsync(c => c.CategoryID == category.CategoryID);

                if (existingCategory != null)
                {
                    existingCategory.CategoryName = category.CategoryName;
                    existingCategory.UrduName = category.UrduName;
                    existingCategory.ImageUrl = category.ImageUrl;
                    existingCategory.Description = category.Description;

                    foreach (var updatedSub in category.SubItems)
                    {
                        var existingSub = existingCategory.SubItems
                            .FirstOrDefault(s => s.SubItemID == updatedSub.SubItemID);

                        if (existingSub != null)
                        {
                            existingSub.SubItemName = updatedSub.SubItemName;
                            existingSub.Price = updatedSub.Price;

                            foreach (var updatedNested in updatedSub.NestedSubItems)
                            {
                                var existingNested = existingSub.NestedSubItems
                                    .FirstOrDefault(n => n.SubItemID == updatedNested.SubItemID);

                                if (existingNested != null)
                                {
                                    existingNested.SubItemName = updatedNested.SubItemName;
                                    existingNested.Price = updatedNested.Price;
                                }
                            }
                        }
                    }
                }
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