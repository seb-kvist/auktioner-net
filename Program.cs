using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuktionApp.Areas.Identity.Data;
using AuktionApp.Areas.Identity.Seeders; 
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuktionAppIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuktionAppIdentityDbContextConnection' not found.");;

builder.Services.AddDbContext<AuktionAppIdentityDbContext>(options =>  //IN MEMORY-DATABASEN
     options.UseInMemoryDatabase("AuktionerDb"));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
     options.SignIn.RequireConfirmedAccount = true)
     .AddRoles<IdentityRole>()
     .AddEntityFrameworkStores<AuktionAppIdentityDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleSeeder.SeedRolesAsync(roleManager);
        var roles = await roleManager.Roles.ToListAsync();
        foreach (var role in roles)
        {
            Console.WriteLine($"Roll i databasen: {role.Name}");
        }
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
