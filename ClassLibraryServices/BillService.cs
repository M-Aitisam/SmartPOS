using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

        public async Task AddRateItemAsync(RateItem item)
        {
            RateItems.Add(item);
            SaveRateItemsToFile();
            await NotifyStateChangedAsync();
        }

        public async Task ClearAllItemsAsync()
        {
            SelectedItems.Clear();
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

        public async Task ClearTotalAmountAsync()
        {
            foreach (var item in SelectedItems)
            {
                item.Price = item.BasePrice;
                item.Quantity = 1;
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

        private async Task NotifyStateChangedAsync()
        {
            if (OnChange != null)
            {
                await OnChange.Invoke();
            }
        }

        private void SaveRateItemsToFile()
        {
            EnsureDirectoryExists();
            var options = new JsonSerializerOptions { WriteIndented = true };
            var rateItemsJson = JsonSerializer.Serialize(RateItems, options);
            File.WriteAllText(rateItemsFilePath, rateItemsJson);
        }

        private void LoadRateItemsFromFile()
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
    }
}
