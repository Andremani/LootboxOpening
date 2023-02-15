using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using Andremani.LootboxOpening.UI;

namespace Andremani.LootboxOpening
{
    public class LootboxOpener : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform scrollTransform;
        [SerializeField] private HorizontalOrVerticalLayoutGroup scrollLayoutGroup;
        [SerializeField] private LootContainer lootContainerPrefab;
        [SerializeField] private List<LootContainer> lootContainers;
        [SerializeField] private Button startScrollButton;
        [Header("Preferencies")]
        [SerializeField] private LootTable lootTable;
        [SerializeField] private int minAmountOfLootContainersToScroll;
        [SerializeField] private int maxAmountOfLootContainersToScroll;
        [SerializeField] private float scrollingDuration = 5f;
        [SerializeField] private AnimationCurve speedChange;

        private float elementWidth;
        private float gapBetweenElements;

        public event Action<ItemStack> OnLootWin; //parameter is winning loot

        private void Start()
        {
            elementWidth = (lootContainerPrefab.transform as RectTransform).rect.width;
            gapBetweenElements = scrollLayoutGroup.spacing;

            //make center of elements to be in center of screen
            scrollTransform.anchoredPosition -= new Vector2(elementWidth / 2, 0);

            if (startScrollButton == null)
            {
                Debug.LogWarning("startScrollButton is null");
            }
            InitScroll(startScrollButton?.onClick);
        }

        private void OnValidate()
        {
            if (scrollingDuration < 0)
            {
                scrollingDuration = 0;
            }
            if (minAmountOfLootContainersToScroll < 0)
            {
                minAmountOfLootContainersToScroll = 0;
            }
            if (maxAmountOfLootContainersToScroll < 0)
            {
                maxAmountOfLootContainersToScroll = 0;
            }

            if (maxAmountOfLootContainersToScroll < minAmountOfLootContainersToScroll)
            {
                maxAmountOfLootContainersToScroll = minAmountOfLootContainersToScroll;
            }
        }

        public void InitScroll(UnityEvent startScrollingEvent)
        {
            int amountOfCointainersToMove = UnityEngine.Random.Range(minAmountOfLootContainersToScroll, maxAmountOfLootContainersToScroll + 1);
            ItemStack winningLoot = lootTable.PickRandomItem();

            ClearScroll();
            FillScroll(amountOfCointainersToMove, winningLoot);

            startScrollingEvent?.AddListener(() => MoveScroll(amountOfCointainersToMove, scrollingDuration, winningLoot));
        }

        private void ClearScroll()
        {
            foreach (LootContainer container in lootContainers)
            {
                Destroy(container.gameObject);
            }
            lootContainers.Clear();
        }

        private void FillScroll(int amountOfContainersToMove, ItemStack winningElement)
        {
            //extra elements to fill left/right side with (instead of empty space)
            int amountOfExtraElements = Mathf.CeilToInt((Screen.width / 2) / (elementWidth + gapBetweenElements));

            //adding extra elements on left side
            for (int i = 0; i < amountOfExtraElements; i++)
            {
                AddRandomElementToScroll();
            }
            scrollTransform.anchoredPosition -= new Vector2((elementWidth + gapBetweenElements) * amountOfExtraElements, 0);

            //adding elements that will be scrolled through
            for (int i = 0; i < amountOfContainersToMove; i++)
            {
                AddRandomElementToScroll();
            }

            AddElementToScroll(winningElement);

            //adding extra elements on right side
            for (int i = 0; i < amountOfExtraElements; i++)
            {
                AddRandomElementToScroll();
            }
        }

        private void AddRandomElementToScroll()
        {
            ItemStack itemStack = lootTable.PickRandomItem();
            AddElementToScroll(itemStack);
        }

        private void AddElementToScroll(ItemStack itemStack)
        {
            LootContainer newContainer = Instantiate(lootContainerPrefab, scrollTransform);
            newContainer.ItemStack = itemStack;
            lootContainers.Add(newContainer);
        }

        private void MoveScroll(int amountOfContainersToMove, float movementDuration, ItemStack winningLoot)
        {
            scrollTransform.DOAnchorPosX(scrollTransform.anchoredPosition.x - amountOfContainersToMove * (elementWidth + gapBetweenElements), movementDuration)
                .SetEase(speedChange)
                .OnComplete(()=>
                {
                    OnLootWin?.Invoke(winningLoot);
                }
            );
        }
    }
}