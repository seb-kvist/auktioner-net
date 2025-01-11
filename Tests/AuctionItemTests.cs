using System;
using System.Text.RegularExpressions;
using Xunit;
using AuktionApp.Models;

namespace AuktionApp.Tests
{
    public class AuctionItemTests
    {
        //TEST 1 - Kollar om slutpriset är större än startpriset
        [Fact] 
        public void FinalPriceIsMoreStartingPricee()
        {
            // Arrange
            var auctionItem = new AuctionItem
            {
                StartingPrice = 50,
                FinalPrice = 100
            };

            // Act
            var isValid = auctionItem.FinalPrice > auctionItem.StartingPrice;

            // Assert
            Assert.True(isValid, "Slutpriset måste vara större än startpriset");
        }

        //TEST 2 - Kollar om ID är i rätt format
        [Fact]
        public void AuctionItem_Id_Format()
        {
            // Arrange
            var auctionItem = new AuctionItem
            {
                Id = "ABC123456"
            };

            // Act
            var isValid = Regex.IsMatch(auctionItem.Id, @"^[A-Z]{3}\d{6}$");

            // Assert
            Assert.True(isValid, "Auktions-id måste vara 3 bokstäver följt av 6 siffror..");
        }
    }
}