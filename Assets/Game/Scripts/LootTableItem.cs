using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Andremani.LootboxOpening
{
    [Serializable]
    public class LootTableItem
    {
        [field: SerializeField] public Item Item { get; private set; }
        [field: SerializeField] public int Amount { get; private set; } = 1;
        [field: SerializeField] public float DropChanceWeight { get; private set; } = 10f;
    }
}