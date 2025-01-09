using System;
using Microsoft.AspNetCore.Identity;

namespace AuktionApp.Areas.Identity.Seeders;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[] { "Inköpare", "Auktionsansvarig" }; // Roller som ska skapas
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role)) // Kontrollera om rollen finns
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (result.Succeeded)
                {
                    Console.WriteLine($"Roll skapad: {role}");
                }
                else
                {
                    Console.WriteLine($"Ett fel inträffade vid skapandet av rollen {role}");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }
        }
    }
}
