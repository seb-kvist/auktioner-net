using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuktionApp.Areas.Identity.Data;
using AuktionApp.Controllers;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuktionAppIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuktionAppIdentityDbContextConnection' not found.");;

builder.Services.AddDbContext<AuktionAppIdentityDbContext>(options =>  //IN MEMORY-DATABASEN
     options.UseInMemoryDatabase("AuktionerDb"));

builder.Services.AddDefaultIdentity<IdentityUser>(options => //Identity
     options.SignIn.RequireConfirmedAccount = true)
     .AddRoles<IdentityRole>()
     .AddEntityFrameworkStores<AuktionAppIdentityDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {   
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var controller = new RoleCreatorController(roleManager);
        await controller.CreateRoles();

        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var context = services.GetRequiredService<AuktionAppIdentityDbContext>();
        await MockdataSeeder.SeedMockDataAsync(context, userManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ett fel inträffade vid roll-seedning: {ex.Message}");
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
