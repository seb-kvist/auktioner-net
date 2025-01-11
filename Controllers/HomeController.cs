using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AuktionApp.Models;
using Microsoft.EntityFrameworkCore;
using AuktionApp.Areas.Identity.Data;

namespace AuktionApp.Controllers;

public class HomeController : Controller
{
    private readonly AuktionAppIdentityDbContext _context;

    public HomeController(ILogger<HomeController> logger, AuktionAppIdentityDbContext context) 
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Inventory()
        {
            var items = _context.AuctionItems.Include(a => a.CreatedBy).ToList();
            return View(items);
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
