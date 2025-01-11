using AuktionApp.Areas.Identity.Data;
using AuktionApp.Models;
using Microsoft.AspNetCore.Identity;

public static class MockdataSeeder  // MockdataSeeder statisk klass för att lägga till mockdata till databasen. Använder InMemoryDatabase för att lagra datan varje uppstart.
{
    public static async Task SeedMockDataAsync(AuktionAppIdentityDbContext context, UserManager<IdentityUser> userManager) // Async för att kunnas använda await när vi anropar metoden
    {
        // Om det finns auktioner i databasen, avsluta
        if (context.AuctionItems.Any()) return;

        // Skapar en mockuser om den inte finns
        var user = await userManager.FindByEmailAsync("mockuser@example.com");
        if (user == null)
        {
            user = new IdentityUser //Sätter värden för mockuser
            {
                UserName = "mockuser@example.com",
                Email = "mockuser@example.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, "Password123!");
        }

        // Här lägger vi till mockdata för auktioner
        var items = new List<AuctionItem>
        {
            new AuctionItem //Skapar en lista med auktionerna nedan
            {
                Id = "ABC123456",
                Name = "Vintage Radio",
                Decade = "1960s",
                Description = "En klassisk radio från 60-talet i mycket bra skick.",
                Category = "Elektronik",
                StartingPrice = 500,
                FinalPrice = null,
                Status = AuctionStatus.Tillgänglig,
                CreatedById = user.Id,
                CreatedBy = user
            },
            new AuctionItem
            {
                Id = "DEF654321",
                Name = "Antik Skrivmaskin",
                Decade = "1930s",
                Description = "En charmig skrivmaskin från 30-talet med alla tangenter fungerande.",
                Category = "Kontorsmaterial",
                StartingPrice = 1200,
                FinalPrice = null,
                Status = AuctionStatus.Tillgänglig,
                CreatedById = user.Id,
                CreatedBy = user
            },
            new AuctionItem
            {
                Id = "ABC321654",
                Name = "Antik Vas",
                Decade = "1920-talet",
                Description = "En vacker antik vas från 1920-talet.",
                Category = "Porslin",
                StartingPrice = 500,
                FinalPrice = null,
                Status = AuctionStatus.Tillgänglig,
                CreatedById = user.Id,
                CreatedBy = user
            },
            new AuctionItem
            {
                Id = "DEF789101",
                Name = "Vintage Klocka",
                Decade = "1950-talet",
                Description = "En klassisk vintageklocka från 1950-talet.",
                Category = "Smycken",
                StartingPrice = 2000,
                FinalPrice = null,
                Status = AuctionStatus.Tillgänglig,
                CreatedById = user.Id,
                CreatedBy = user
            }
        };

        context.AuctionItems.AddRange(items); //Lägger till auktionerna i databasen och sparar
        await context.SaveChangesAsync(); 
    }
    
}