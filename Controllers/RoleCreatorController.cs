using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuktionApp.Controllers
{
    public class RoleCreatorController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleCreatorController(RoleManager<IdentityRole> roleManager) // DI för att RoleManager ska användas
        {
            _roleManager = roleManager;
        }

        // Här skapar vi rollerna vid uppstart av applikationen
        public async Task CreateRoles() 
        {
            var roles = new[] { "Inköpare", "Auktionsansvarig" }; // Roller som ska skapas
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role)) // Kontrollerar om rollen redan finns för
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(role)); 
                    if (result.Succeeded)
                    {
                        Console.WriteLine($"Roll skapad: {role}");
                    }
                }
            }
        }
    }
}