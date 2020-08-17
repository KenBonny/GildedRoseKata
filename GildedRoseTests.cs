using System.Collections.Generic;
using Xunit;

namespace KenBonny.GildedRoseKata
{
    public class GildedRoseTests
    {
        [Fact]
        public void ConjuredItemsDegradeTwiceAsFast()
        {
            IList<Item> items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal("fixme", items[0].Name);
        }
    }
}