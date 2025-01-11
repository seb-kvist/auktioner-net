using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations; 

namespace AuktionApp.Models;


public class AuctionItem
{
    [RegularExpression(@"^[A-Z]{3}\d{6}$", ErrorMessage = "Id måste vara tre bokstäver följt av sex siffror.")]
        public string? Id { get; set; } 

    [Required(ErrorMessage = "Du måste fylla i ett namn på ditt objekt.")]
    [Display(Name = "Namn")]
        public string? Name { get; set; } 

    [Required(ErrorMessage = "Du måste ange ett årtioende.")]
    [Display(Name = "Årtionde")]
        public string? Decade { get; set; }

    [Required(ErrorMessage = "Du måste fylla i en beskrivning.")]
    [Display(Name = "Beskrivning")]
        public string? Description { get; set; } 
        
    [Required(ErrorMessage = "Du måste ange en kategori.")]
    [Display(Name = "Kategori")]
        public string? Category { get; set; } 

    [Required(ErrorMessage = "Du måste ange ett startpris.")]
    [Display(Name = "Startpris")]
    [Range(0, double.MaxValue, ErrorMessage = "Startpriset måste vara större än eller lika med 0.")]
        public decimal? StartingPrice { get; set; }  //Startpris, sätts av inköpare

    [Display(Name = "Slutpris")]
    public decimal? FinalPrice { get; set; } //Slutpris, sätts av Auktionsansvarig
    
    public AuctionStatus Status { get; set; } //Status på auktionen, antingen tillgänglig eller såld
    public string? CreatedById { get; set; } //Id på användare som skapat auktionen för att kunna koppla ihop med användare
    public IdentityUser? CreatedBy { get; set; } //Användare som skapat auktionen baserat

    public DateTime CreatedAt { get; set; }  // Varje auktion har ett skapande datum för att kunna sorteras efter detta
}

public enum AuctionStatus //Två statuslägen för auktionen
{
    Tillgänglig, Såld
}