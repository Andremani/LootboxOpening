using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.LootboxOpening
{
    public class ItemStack
    {
        public Item Item { get; }
        public int Amount { get; } = 1;

        public ItemStack(Item item, int amount)
        {
            Item = item;
            Amount = amount;
        }
    }
}