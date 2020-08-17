using System.Collections.Generic;
using Xunit;

namespace KenBonny.GildedRoseKata
{
    public class GildedRoseTests
    {
        [Fact]
        public void NonExpiredConjuredItemsDegradeTwiceAsFast()
        {
            var conjuredItem = new Item { Name = "Conjured cake", SellIn = 5, Quality = 10 };
            UpdateQuality(conjuredItem);
            Assert.Equal(8, conjuredItem.Quality);
        }

        [Fact]
        public void ExpiredConjuredItemsDegradeTwiceAsFast()
        {
            var expiredConjuredItem = new Item { Name = "Conjured cake", SellIn = -1, Quality = 10 };
            UpdateQuality(expiredConjuredItem);
            Assert.Equal(6, expiredConjuredItem.Quality);
        }

        private static void UpdateQuality(Item item)
        {
            var app = new GildedRose(new List<Item>{item});
            app.UpdateQuality();
        }
    }
}