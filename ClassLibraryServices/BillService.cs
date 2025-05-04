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

        // Add this method to notify components of state changes
        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
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
            NotifyStateChanged();
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
            ArgumentNullException.ThrowIfNull(item);

            // Check if item already exists (case-insensitive comparison)
            var existingItem = RateItems.FirstOrDefault(r =>
                r.Name?.Equals(item.Name, StringComparison.OrdinalIgnoreCase) ?? false);

            if (existingItem != null)
            {
                // Update existing item
                existingItem.BasePrice = item.BasePrice;
                existingItem.ImageUrl = item.ImageUrl;
                existingItem.IsActive = item.IsActive;
                existingItem.Category = item.Category;
            }
            else
            {
                // Add new item
                RateItems.Add(item);
            }

            SaveRateItemsToFile();
            await NotifyStateChangedAsync();
        }

        // Modified version of your existing RemoveRateItemAsync
        public async Task RemoveRateItemAsync(string productName)
        {
            var rateItem = RateItems.FirstOrDefault(x =>
                x.Name?.Equals(productName, StringComparison.OrdinalIgnoreCase) ?? false);

            if (rateItem != null)
            {
                RateItems.Remove(rateItem);
                SaveRateItemsToFile();
                await NotifyStateChangedAsync();
            }
        }

        // Modified version of your existing UpdateRateItems
        public void UpdateRateItems(List<RateItem> items)
        {
            RateItems = items ?? throw new ArgumentNullException(nameof(items));
            SaveRateItemsToFile();
            NotifyStateChanged();
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
            ArgumentNullException.ThrowIfNull(item);

            var existingItem = SelectedItems.FirstOrDefault(i =>
                i.Name?.Equals(item.Name, StringComparison.OrdinalIgnoreCase) ?? false);

            if (existingItem != null)
            {
                existingItem.Quantity++;
                existingItem.Price = existingItem.BasePrice * existingItem.Quantity;
            }
            else
            {
                var newItem = new RateItem
                {
                    Id = item.Id,
                    Name = item.Name,
                    BasePrice = item.BasePrice,
                    Price = item.BasePrice,
                    Quantity = 1,
                    Category = item.Category,
                    IsActive = item.IsActive
                };
                SelectedItems.Add(newItem);
            }
            await NotifyStateChangedAsync();
        }

        public async Task RemoveItemAsync(RateItem item)
        {
            var existingItem = SelectedItems.FirstOrDefault(i =>
                i.Name?.Equals(item.Name, StringComparison.OrdinalIgnoreCase) ?? false);

            if (existingItem == null) return;

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
            var item = SelectedItems.FirstOrDefault(i =>
                i.Name?.Equals(updatedItem.Name, StringComparison.OrdinalIgnoreCase) ?? false);

            if (item != null)
            {
                item.Quantity = updatedItem.Quantity;
                item.Price = item.BasePrice * item.Quantity;
                await NotifyStateChangedAsync();
            }
        }

        private async Task NotifyStateChangedAsync()
        {
            if (OnChange != null)
            {
                await OnChange.Invoke();
            }
        }

        private void SaveRateItemsToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(RateItems.Where(r => r.IsActive), options);
            File.WriteAllText(rateItemsFilePath, json);
        }

        private void LoadRateItemsFromFile()
        {
            if (!File.Exists(rateItemsFilePath)) return;

            try
            {
                var json = File.ReadAllText(rateItemsFilePath);
                var items = JsonSerializer.Deserialize<List<RateItem>>(json) ?? new List<RateItem>();
                RateItems = items.Where(i => i.IsActive).ToList();
            }
            catch (Exception)
            {
                RateItems = new List<RateItem>();
            }
        }

        private void EnsureDirectoryExists()
        {
            var directory = Path.GetDirectoryName(rateItemsFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
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