using System;

namespace AuktionApp.Models;

public class AuctionItem
{
    public string Id { get; set; } // Format: Tre bokstäver och sex siffror
    public string Name { get; set; } //Namn på item
    public string Decade { get; set; } //Årtionde för intern revision
    public string Description { get; set; } //Produktbeskr
    public string Category { get; set; } //Kategori

    public decimal StartingPrice { get; set; } //Startpris
    public decimal? FinalPrice { get; set; } //Slutpris
    
    public string Status { get; set; } // "Tillgänglig" eller "Såld"
    public string CreatedById { get; set; } //Id på användare som skapat auktionen
    public IdentityUser CreatedBy { get; set; } //Användare som skapat auktionen
}
