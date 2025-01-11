using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuktionApp.Models;
using Microsoft.AspNetCore.Authorization;
using AuktionApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace AuktionApp.Controllers
{
    public class AuctionItemsController : Controller
    {
        private readonly AuktionAppIdentityDbContext _context; 
        private readonly UserManager<IdentityUser> _userManager; 

        public AuctionItemsController(AuktionAppIdentityDbContext context, UserManager<IdentityUser> userManager) // Konstruktor tar emot två parametrar som är en instans av AuktionAppIdentityDbContext och UserManager<IdentityUser> från ovan
        {
            _context = context; //In i en privat variabel
            _userManager = userManager;
        }

        //Detta är en GET-metod som hämtar alla auktioner till vyn Inventory
        public async Task<IActionResult> Inventory()
        {
            var auctionItems = await _context.AuctionItems 
                .Include(a => a.CreatedBy) // Inkludera skaparen av auktionen
                .OrderByDescending(a => a.CreatedAt) //Specifik ordning på auktionerna
                .ToListAsync(); //Detta hämtar alla auktioner från databasen sist
        return View(auctionItems);
        }

        //ADD AUCTION STRUKTUREN
        // En GET-metod som skapar en ny auktion
        [Authorize]
        public IActionResult AddAuction() //Lägg till en ny auktion
        {
            return View(new AuctionItem());
        }

        //Detta är en POST metod för att lägga till en auktion. Post-metod skickar data till servern
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddAuction(AuctionItem auctionItem)
        {
            if (ModelState.IsValid)
            {
                // Generera ett Id i formatet "ABC123456"
                var random = new Random();
                string letters = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 3)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                string numbers = random.Next(100000, 999999).ToString();
                auctionItem.Id = $"{letters}{numbers}";
                
                auctionItem.CreatedById = _userManager.GetUserId(User);  // Hämta inloggad användares ID
                auctionItem.CreatedBy = await _userManager.GetUserAsync(User); // Hämta inloggad användares objekt
                auctionItem.CreatedAt = DateTime.Now; // Ange tidpunkt för skapande

                _context.AuctionItems.Add(auctionItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Inventory)); //Återgår till vyn igen med den nya auktionen
            }
            return View(auctionItem);  //Fallback om det inte lyckas
        }


        //EDIT AUCTION STRUKTUREN
        //En GET-metod som hämtar en specifik auktion för redigering
        [Authorize]
        public async Task<IActionResult> EditAuction(string id)
        {
            if (id == null) //Om ID är null så kan vi inte redigera auktionen
            {
                return NotFound();
            }

            var auctionItem = await _context.AuctionItems.FindAsync(id); //Detta hämtar auktionen beroende på ID
            if (auctionItem == null)
            {
                return NotFound();
            }
            return View(auctionItem);
        }

        // POST: AuctionItems/EditAuction //Detta är POST-metoden för redigeringen av auktionen
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // Bind gör att vi bara tar med de egenskaper som vi vill redigera
        public async Task<IActionResult> EditAuction(string id, [Bind("Id,Name,Decade,Description,Category,StartingPrice,FinalPrice,Status,CreatedById,CreatedBy")] AuctionItem auctionItem)
        {
            if (id != auctionItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingAuctionItem = await _context.AuctionItems.FindAsync(id);

                    if (existingAuctionItem == null)
                    {
                        return NotFound();
                    }

                    if (User.IsInRole("Auktionsansvarig"))
                    {
                        existingAuctionItem.FinalPrice = auctionItem.FinalPrice;
                        existingAuctionItem.Status = auctionItem.Status;
                    }

                    _context.Update(existingAuctionItem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Inventory));
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return View(auctionItem);
        }

        private bool AuctionItemExists(string id) //En privat metod som kollar om auktionen redan finns
        {
            return _context.AuctionItems.Any(e => e.Id == id);
        }
    }
}