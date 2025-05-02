using System.Text.Json;
using ClassLibraryEntities;

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
            rateItemsFilePath = Path.Combine(AppContext.BaseDirectory, "Dal", "RateItems.json");
            EnsureDirectoryExists();
            LoadRateItemsFromFile();
        }

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
                    // Copy all other properties
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

        public async Task AddRateItemAsync(RateItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (RateItems.Any(r =>
                r.Name?.Equals(item.Name, StringComparison.OrdinalIgnoreCase) ?? false))
            {
                throw new InvalidOperationException($"Product '{item.Name}' already exists");
            }

            RateItems.Add(item);
            SaveRateItemsToFile();
            await NotifyStateChangedAsync();
        }

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

        public void UpdateRateItems(List<RateItem> items)
        {
            RateItems = items ?? throw new ArgumentNullException(nameof(items));
            SaveRateItemsToFile();
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
                // Log error here
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
                await NotifyStateChangedAsync(); // Use this instead of directly calling OnChange
            }
        }
    }
}