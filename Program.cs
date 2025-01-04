using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuktionApp.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuktionAppIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuktionAppIdentityDbContextConnection' not found.");;

builder.Services.AddDbContext<AuktionAppIdentityDbContext>(options =>  //IN MEMORY-DATABASEN
     options.UseInMemoryDatabase("AuktionerDb"));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
     options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuktionAppIdentityDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
