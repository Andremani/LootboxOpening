using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Andremani.LootboxOpening.UI
{
    public class HiderPanel : MonoBehaviour
    {
        [SerializeField] private Button hideButton;

        private void Start()
        {
            hideButton.onClick.AddListener(OnHideButtonClick);
        }

        private void OnHideButtonClick()
        {
            gameObject.SetActive(false);
            hideButton.onClick.RemoveListener(OnHideButtonClick);
        }
    }
}