using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.LootboxOpening
{
    [CreateAssetMenu(fileName = "ItemName", menuName = "ScriptableObjects/Item")]
    public class Item : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Color IconColor { get; private set; } = Color.red;
    }
}