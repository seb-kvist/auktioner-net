using System;
using Microsoft.AspNetCore.Identity;

namespace AuktionApp.Areas.Identity.Seeders;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[] { "Inköpare", "Auktionsansvarig"}; //Roller som ska skapas
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role)) // Kollar om rollen finns 
            {
                await roleManager.CreateAsync(new IdentityRole(role)); //Skapar rollen ifall den inte finns vid start för att säkra upp mot fel.
            }
        }
    }
}
