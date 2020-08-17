using System.Collections.Generic;

namespace KenBonny.GildedRoseKata
{
    public class GildedRose
    {
        private const string AgedBrie = "Aged Brie";
        private const string BackstageConcertPasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private readonly IList<Item> _items;
        public GildedRose(IList<Item> items)
        {
            _items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in _items)
            {
                if (item.IsNot(AgedBrie) && item.IsNot(BackstageConcertPasses))
                {
                    if (item.HasQuality())
                    {
                        if (item.IsNot(Sulfuras))
                        {
                            item.Quality--;
                        }
                    }
                }
                else
                {
                    if (item.QualityIsBelowMax())
                    {
                        item.Quality++;

                        if (item.Is(BackstageConcertPasses))
                        {
                            if (item.SellIn < 11)
                            {
                                if (item.QualityIsBelowMax())
                                {
                                    item.Quality++;
                                }
                            }

                            if (item.SellIn < 6)
                            {
                                if (item.QualityIsBelowMax())
                                {
                                    item.Quality++;
                                }
                            }
                        }
                    }
                }

                if (item.IsNot(Sulfuras))
                {
                    item.SellIn--;
                }

                if (item.SellIn < 0)
                {
                    if (item.Is(AgedBrie))
                    {
                        if (item.QualityIsBelowMax())
                        {
                            item.Quality++;
                        }
                    }
                    else
                    {
                        if (item.Is(BackstageConcertPasses))
                        {
                            item.Quality -= item.Quality;
                        }
                        else
                        {
                            if (item.HasQuality())
                            {
                                if (item.IsNot(Sulfuras))
                                {
                                    item.Quality--;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    internal static class ItemExtensions
    {
        public static bool QualityIsBelowMax(this Item item) => item.Quality < 50;

        public static bool HasQuality(this Item item) => item.Quality > 0;

        public static bool Is(this Item item, string name) => item.Name == name;

        public static bool IsNot(this Item item, string name) => !Is(item, name);
    }
}