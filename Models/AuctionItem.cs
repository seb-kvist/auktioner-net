using System;

namespace AuktionApp.Models;

public class AuctionItem
{
    public string Id { get; set; } // Format: Tre bokstäver och sex siffror
    public string Name { get; set; }
    public string Decade { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }

    public decimal CostPrice { get; set; }
    public decimal StartingPrice { get; set; }
    public decimal? FinalPrice { get; set; }
    
    public string Status { get; set; } // "Tillgänglig" eller "Såld"
    public string CreatedById { get; set; }
    public IdentityUser CreatedBy { get; set; }
}
