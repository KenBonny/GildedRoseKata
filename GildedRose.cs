﻿using System.Collections.Generic;

namespace KenBonny.GildedRoseKata
{
    public class GildedRose
    {
        private const string AgedBrie = "Aged Brie";
        private const string BackstageConcertPasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const int TenDays = 10;
        private const int FiveDays = 5;
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
                            if (item.SellInLessThan(TenDays))
                            {
                                if (item.QualityIsBelowMax())
                                {
                                    item.Quality++;
                                }
                            }

                            if (item.SellInLessThan(FiveDays))
                            {
                                if (item.QualityIsBelowMax())
                                {
                                    item.Quality++;
                                }
                            }
                        }
                    }
                }

                if (item.Is(Sulfuras))
                {
                    continue;
                }

                item.SellIn--;

                if (item.IsExpired() &&
                    item.Is(BackstageConcertPasses))
                {
                    item.Quality = 0;
                }

                if (item.IsExpired() &&
                    item.Is(AgedBrie) &&
                    item.QualityIsBelowMax())
                {
                    item.Quality++;
                }

                if (item.IsExpired() &&
                    item.IsNot(AgedBrie) &&
                    item.HasQuality())
                {
                    item.Quality--;
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

        public static bool IsExpired(this Item item) => item.SellIn < 0;

        public static bool SellInLessThan(this Item item, int days) => item.SellIn <= days;
    }
}