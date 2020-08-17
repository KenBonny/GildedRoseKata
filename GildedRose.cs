using System.Collections.Generic;

namespace KenBonny.GildedRoseKata
{
    public class GildedRose
    {
        private const string AgedBrie = "Aged Brie";
        private const string BackstageConcertPasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const int TenDays = 10;
        private const int FiveDays = 5;
        private const int MaxQuality = 50;
        private const int MinQuality = 0;
        private const int DoubleQualityIncrease = 2;
        private const int TripleQualityIncrease = 3;
        private readonly IList<Item> _items;
        public GildedRose(IList<Item> items)
        {
            _items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in _items)
            {
                if (item.Is(Sulfuras))
                {
                    continue;
                }

                item.SellIn--;

                if (item.Is(AgedBrie))
                {
                    if (item.IsExpired())
                    {
                        item.Quality += DoubleQualityIncrease;
                    }
                    else
                    {
                        item.Quality++;
                    }
                }
                else if (item.Is(BackstageConcertPasses))
                {
                    if (item.SellInLessThan(FiveDays))
                    {
                        item.Quality += TripleQualityIncrease;
                    }
                    else if (item.SellInLessThan(TenDays))
                    {
                        item.Quality += DoubleQualityIncrease;
                    }
                    else
                    {
                        item.Quality++;
                    }

                    if (item.IsExpired())
                    {
                        item.Quality = 0;
                    }
                }
                else
                {
                    if (item.IsExpired())
                    {
                        item.Quality -= DoubleQualityIncrease;
                    }
                    else
                    {
                        item.Quality--;
                    }
                }

                if (item.Quality < MinQuality)
                {
                    item.Quality = MinQuality;
                }

                if (item.Quality > MaxQuality)
                {
                    item.Quality = MaxQuality;
                }
            }
        }
    }

    internal static class ItemExtensions
    {
        public static bool HasQuality(this Item item) => item.Quality > 0;

        public static bool Is(this Item item, string name) => item.Name == name;

        public static bool IsNot(this Item item, string name) => !Is(item, name);

        public static bool IsExpired(this Item item) => item.SellIn < 0;

        public static bool SellInLessThan(this Item item, int days) => item.SellIn < days;
    }
}