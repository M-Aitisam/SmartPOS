
using BlazorAppUserView.Components;
using BlazorAppUserView.Hubs;
using ClassLibraryDAL;
using ClassLibraryServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Get the correct path for app.db dynamically
var dbFileName = "app.db";
var dbPath = Path.Combine(AppContext.BaseDirectory, dbFileName);
var connectionString = $"Data Source={dbPath}";

Console.WriteLine($"🔹 SQLite Database Path: {dbPath}");

// ✅ Register DbContext with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString,
    b => b.MigrationsAssembly("ClassLibraryDAL")
    )
    );

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddSingleton<BillService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IDBOperations, DBOperations>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<StateContainerService>();
builder.Services.AddSignalR();

// ✅ Add Antiforgery Services
builder.Services.AddAntiforgery();

// ✅ Add Razor Components (Fix)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("Default", client =>
{
    var baseUrl = builder.Configuration["BaseUrl"] ?? builder.Environment.WebRootPath;
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

// ✅ Ensure the database exists and is migrated properly
try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Console.WriteLine("🔹 Checking & Applying Migrations...");
        dbContext.Database.Migrate(); // Ensures schema updates
        Console.WriteLine("✅ Database Migration Complete!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Database Migration Error: {ex.Message}");
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery(); // ✅ Antiforgery Middleware
app.MapHub<SalesHub>("/salesHub");

// ✅ Map Razor Components (Fix)
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();