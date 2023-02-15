using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Andremani.LootboxOpening.UI
{
    public class LootContainer : MonoBehaviour
    {
        [SerializeField] private ItemStack itemStack;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;

        public ItemStack ItemStack
        {
            get
            {
                return itemStack;
            }
            set
            {
                itemStack = value;
                icon.color = itemStack.Item.IconColor;
                nameText.text = itemStack.Item.Name;
            }
        }
    }
}