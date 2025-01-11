using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuktionApp.Areas.Identity.Data;
using AuktionApp.Controllers;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuktionAppIdentityDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'AuktionAppIdentityDbContextConnection' not found.");;

builder.Services.AddDbContext<AuktionAppIdentityDbContext>(options =>  //Skapar en InMemoryDatabase som heter AuktionerDb
     options.UseInMemoryDatabase("AuktionerDb"));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>  //Lägger till Identity som används för att hantera användare och roller
     options.SignIn.RequireConfirmedAccount = true) //Kräver att användaren bekräftar sitt konto
     .AddRoles<IdentityRole>() //Lägger till roller
     .AddEntityFrameworkStores<AuktionAppIdentityDbContext>(); //Lägger till en EntityFrameworkStore som används för att lagra användare och roller

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) //Skapar upp en ny scope med tjänster(DI) och kör initialiseringen
{
    var services = scope.ServiceProvider;

    try
    {   
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>(); //Skapar roller för användare
        var controller = new RoleCreatorController(roleManager);
        await controller.CreateRoles();

        var userManager = services.GetRequiredService<UserManager<IdentityUser>>(); //Hantering av mockdata, användare och roller
        var context = services.GetRequiredService<AuktionAppIdentityDbContext>();
        await MockdataSeeder.SeedMockDataAsync(context, userManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ett fel inträffade vid roll-skapandet: {ex.Message}"); //Loggning jag använde vid utveckling
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
