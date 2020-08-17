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
        private const int NormalCoefficient = 1;
        private const int DoubleCoefficient = 2;
        private const int TripleCoefficient = 3;
        private const int SumCoefficient = -1;
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

                if (item.Is(BackstageConcertPasses) &&
                    item.IsExpired())
                {
                    item.EmptyQuality();
                    continue;
                }

                var coefficient = item.IsExpired() ? DoubleCoefficient : NormalCoefficient;
                if (item.Is(AgedBrie))
                {
                    coefficient *= SumCoefficient;
                }
                else if (item.Is(BackstageConcertPasses))
                {
                    if (item.SellInLessThan(FiveDays))
                    {
                        coefficient = TripleCoefficient;
                    }
                    else if (item.SellInLessThan(TenDays))
                    {
                        coefficient = DoubleCoefficient;
                    }
                    else
                    {
                        coefficient = NormalCoefficient;
                    }

                    coefficient *= SumCoefficient;
                }

                item.Quality -= 1 * coefficient;

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

        public static void EmptyQuality(this Item item) => item.Quality = 0;

        public static bool Is(this Item item, string name) => item.Name == name;

        public static bool IsNot(this Item item, string name) => !Is(item, name);

        public static bool IsExpired(this Item item) => item.SellIn < 0;

        public static bool SellInLessThan(this Item item, int days) => item.SellIn < days;
    }
}