using Microsoft.AspNetCore.SignalR;
using ClassLibraryEntities;

namespace BlazorAppUserView.Hubs
{
    public class SalesHub : Hub
    {
        public async Task NotifyNewOrder(BusinessTransaction transaction)
        {
            await Clients.All.SendAsync("NewOrder", transaction);
        }
    }
}