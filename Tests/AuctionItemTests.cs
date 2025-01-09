using System;
using System.Text.RegularExpressions;
using Xunit;
using AuktionApp.Models;

namespace AuktionApp.Tests
{
    public class AuctionItemTests
    {
        [Fact]
        public void StartingPrice_Less_Than_CostPrice()
        {
            // Arrange
            var auctionItem = new AuctionItem
            {
                StartingPrice = 100,
                FinalPrice = 50
            };

            // Act
            var isValid = auctionItem.StartingPrice >= auctionItem.FinalPrice;

            // Assert
            Assert.True(isValid, "Startpriset kan inte vara lägre än inköpspris.");
        }

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