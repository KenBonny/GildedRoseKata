using System.Collections.Generic;
using Xunit;

namespace KenBonny.GildedRoseKata
{
    public class GildedRoseTests
    {
        [Fact]
        public void NonExpiredConjuredItemsDegradeTwiceAsFast()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Conjured cake", SellIn = 5, Quality = 10 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(8, items[0].Quality);
        }

        [Fact]
        public void ExpiredConjuredItemsDegradeTwiceAsFast()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Conjured cake", SellIn = -1, Quality = 10 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(6, items[0].Quality);
        }
    }
}