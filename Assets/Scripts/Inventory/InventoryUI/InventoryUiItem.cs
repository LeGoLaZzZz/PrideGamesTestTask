using Grenades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.InventoryUI
{
    public class InventoryUiItem : MonoBehaviour
    {
        public GameObject selectedPanel;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI countText;
        public Image icon;
        public string countFormat = "x{0}";

        public void SetUp(GrenadeConfig grenadeConfig, int count)
        {
            icon.color = grenadeConfig.icon;
            countText.text = string.Format(countFormat, count);
            titleText.text = grenadeConfig.title;
        }

        public void SetSelected()
        {
            selectedPanel.SetActive(true);
        }

        public void UnSetSelected()
        {
            selectedPanel.SetActive(false);
        }

        private void Awake()
        {
            UnSetSelected();
        }
    }
}