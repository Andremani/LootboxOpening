using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.LootboxOpening
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "ScriptableObjects/LootTable")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private List<LootTableItem> items;

        public ItemStack PickRandomItem()
        {
            if (items == null)
            {
                return null;
            }

            LootTableItem chosenItem;
            float totalWeight = 0;

            foreach (LootTableItem item in items)
            {
                totalWeight += item.DropChanceWeight;
            }

            float randomPoint = Random.value * totalWeight;

            for (int i = 0; i < items.Count; i++)
            {
                if (randomPoint < items[i].DropChanceWeight)
                {
                    chosenItem = items[i];
                    return new ItemStack(chosenItem.Item, chosenItem.Amount);
                }
                else
                {
                    randomPoint -= items[i].DropChanceWeight;
                }
            }
            chosenItem = items[items.Count - 1];
            return new ItemStack(chosenItem.Item, chosenItem.Amount);
        }
    }

}