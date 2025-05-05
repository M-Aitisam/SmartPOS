using System.Net.NetworkInformation;
using System.Text.Json;
using ClassLibraryEntities;
using ClassLibraryServices;

namespace ClassLibraryServices
{
    public class BillService
    {
        private readonly string rateItemsFilePath;

        public List<RateItem> RateItems { get; private set; } = new();
        public List<RateItem> SelectedItems { get; private set; } = new();

        public decimal TotalAmount => SelectedItems.Sum(item => item.Price);

        public event Func<Task>? OnChange;

        public BillService()
        {
            rateItemsFilePath = Path.Combine(AppContext.BaseDirectory, "ClassLibraryDal", "RateItems.json");
            EnsureDirectoryExists();
            LoadRateItemsFromFile();
        }

       
        // Add this method to update rate items from Product page
        public void UpdateRateItemsFromProducts(List<Product> products, List<Category> categories)
        {
            var newRateItems = products
                .Where(p => p.IsActive)
                .Select(p => new RateItem
                {
                    Name = p.ProductTitle,
                    BasePrice = p.ProductPrice,
                    ImageUrl = p.ImageUrl,
                    IsActive = p.IsActive,
                    Category = categories.FirstOrDefault(c => c.CategoryID == p.CategoryID)?.CategoryName ?? "Uncategorized"
                })
                .ToList();

            RateItems = newRateItems;
            SaveRateItemsToFile();
            NotifyStateChangedAsync();
        }

        // Add this method to get all active rate items
        public List<RateItem> GetActiveRateItems()
        {
            return RateItems.Where(i => i.IsActive).ToList();
        }

        // Add this method to find rate items by category
        public List<RateItem> GetRateItemsByCategory(string category)
        {
            return RateItems
                .Where(i => i.IsActive &&
                           (category == "All" || (i.Category ?? "") == category))
                .ToList();
        }

        // Add this method to get all unique categories from rate items
        public List<string> GetAllCategories()
        {
            return new List<string> { "All" }
                .Concat(RateItems
                    .Where(i => !string.IsNullOrEmpty(i.Category))
                    .Select(i => i.Category!)
                    .Distinct())
                .ToList();
        }

        // Modified version of your existing AddRateItemAsync
        public async Task AddRateItemAsync(RateItem item)
        {
            RateItems.Add(item);
            await SaveRateItemsToFile();  // Ensure this is being called
            await NotifyStateChangedAsync();
        }

        public async Task RemoveRateItemAsync(string productName)
        {
            var rateItem = RateItems.FirstOrDefault(x => x.Name == productName);
            if (rateItem != null && !rateItem.IsActive)
            {
                RateItems.Remove(rateItem);
                SaveRateItemsToFile();
                await NotifyStateChangedAsync();
            }
        }

        // Modified version of your existing UpdateRateItems
        public async Task UpdateRateItems(List<RateItem> newItems)
        {
            RateItems = newItems;
            SaveRateItemsToFile();
            await NotifyStateChangedAsync();
        }
        // Rest of your existing methods remain the same...
        public RateItem? GetRateItemByName(string name) =>
            RateItems.FirstOrDefault(r => r.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) ?? false);

        public Task<List<BusinessCategory>> GetCategoriesAsync() =>
            Task.FromResult(
                RateItems
                    .Where(item => !string.IsNullOrWhiteSpace(item.Category))
                    .Select(item => new BusinessCategory { CategoryName = item.Category! })
                    .DistinctBy(c => c.CategoryName)
                    .ToList()
            );

        public async Task AddItemAsync(RateItem item)
        {
            var existingItem = SelectedItems.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem != null)
            {
                existingItem.Quantity++;
                existingItem.Price = existingItem.BasePrice * existingItem.Quantity;
            }
            else
            {
                item.Quantity = 1;
                if (item.BasePrice == 0) // Ensure BasePrice is always set
                {
                    item.BasePrice = item.Price;
                }
                SelectedItems.Add(item);
            }
            await NotifyStateChangedAsync();
        }

        public async Task RemoveItemAsync(RateItem item)
        {
            var existingItem = SelectedItems.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem != null)
            {
                if (existingItem.Quantity > 1)
                {
                    existingItem.Quantity--;
                    existingItem.Price = existingItem.BasePrice * existingItem.Quantity;
                }
                else
                {
                    SelectedItems.Remove(existingItem);
                }
                await NotifyStateChangedAsync();
            }
        }

        public async Task ClearAllItemsAsync()
        {
            SelectedItems.Clear();
            await NotifyStateChangedAsync();
        }

        public async Task ResetQuantitiesAsync()
        {
            foreach (var item in SelectedItems)
            {
                item.Quantity = 1;
                item.Price = item.BasePrice;
            }
            await NotifyStateChangedAsync();
        }

        public async Task UpdateItemAsync(RateItem updatedItem)
        {
            var item = SelectedItems.FirstOrDefault(i => i.Name == updatedItem.Name);
            if (item != null)
            {
                item.Quantity = updatedItem.Quantity;
                item.Price = item.BasePrice * item.Quantity;
                await NotifyStateChangedAsync();
            }
        }

        public async Task NotifyStateChangedAsync()
        {
            if (OnChange != null)
            {
                await OnChange.Invoke();
            }
        }

        public async Task SaveRateItemsToFile()
        {
            EnsureDirectoryExists();
            var options = new JsonSerializerOptions { WriteIndented = true };
            var rateItemsJson = JsonSerializer.Serialize(RateItems, options);
            // Use WriteAllTextAsync to save the file asynchronously
            await File.WriteAllTextAsync(rateItemsFilePath, rateItemsJson);
        }


        public void LoadRateItemsFromFile()
        {
            if (File.Exists(rateItemsFilePath))
            {
                var rateItemsJson = File.ReadAllText(rateItemsFilePath);
                RateItems = JsonSerializer.Deserialize<List<RateItem>>(rateItemsJson) ?? new List<RateItem>();
            }
        }

        private void EnsureDirectoryExists()
        {
            string? directoryPath = Path.GetDirectoryName(rateItemsFilePath);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public async Task ClearCart()
        {
            if (SelectedItems != null)
            {
                SelectedItems.Clear();
                await NotifyStateChangedAsync();
            }
        }

        public async Task RemoveItemCompletelyAsync(RateItem item)
        {
            var existingItem = SelectedItems.FirstOrDefault(i =>
                i.Name?.Equals(item.Name, StringComparison.OrdinalIgnoreCase) ?? false);

            if (existingItem != null)
            {
                SelectedItems.Remove(existingItem);
                await NotifyStateChangedAsync();
            }
        }
    }
}