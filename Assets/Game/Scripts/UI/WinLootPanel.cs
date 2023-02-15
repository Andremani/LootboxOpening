using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Andremani.LootboxOpening.UI
{
    public class WinLootPanel : MonoBehaviour
    {
        [SerializeField] private GameObject visuals;
        [SerializeField] private LootContainer winningLootContainer;
        [SerializeField] private TextMeshProUGUI winningText;

        [SerializeField] private LootboxOpener lootboxOpener;

        private void Start()
        {
            visuals.SetActive(false);
            lootboxOpener.OnLootWin += OnLootWin;
        }

        private void OnLootWin(ItemStack itemStack)
        {
            winningLootContainer.ItemStack = itemStack;
            winningText.text = $"You got {itemStack.Item.Name}!";
            visuals.SetActive(true);
        }
    }
}