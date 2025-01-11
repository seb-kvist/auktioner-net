using AuktionApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuktionApp.Areas.Identity.Data;

public class AuktionAppIdentityDbContext : IdentityDbContext<IdentityUser> // InMemoryDatabas som används för att lagra användare och roller
{
    public AuktionAppIdentityDbContext(DbContextOptions<AuktionAppIdentityDbContext> options) //Konstruktor som tar emot DbContextOptions
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

    }

    public DbSet<AuctionItem> AuctionItems { get; set; } = null!;  // Detta är en DbSet som används för att skapa en tabell i databasen
}
