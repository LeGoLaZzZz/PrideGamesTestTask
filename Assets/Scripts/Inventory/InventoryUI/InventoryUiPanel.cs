using System.Collections.Generic;
using Grenades;
using UnityEngine;

namespace Inventory.InventoryUI
{
    public class InventoryUiPanel : MonoBehaviour
    {
        [SerializeField] private InventoryUiItem itemPrefab;
        [SerializeField] private RectTransform itemsContainer;

        private Inventory _inventory;
        private Dictionary<GrenadeConfig, InventoryUiItem> _items = new Dictionary<GrenadeConfig, InventoryUiItem>();
        private InventoryUiItem _currentSelected;

        private bool _isSubscribed;

        public void SetUp(Inventory inventory)
        {
            
            var readOnlyDictionary = inventory.Grenades;
            foreach (var keyValuePair in readOnlyDictionary)
            {
                SetUpItem(keyValuePair.Key, keyValuePair.Value);
            }
            Subscribe();
            UpdateSelected();
        }

        private void OnEnable()
        {
            if (_inventory) Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void Start()
        {
            _inventory = FindObjectOfType<Inventory>();

            SetUp(_inventory);
        }

        private void Subscribe()
        {
            if (_isSubscribed) return;
            _isSubscribed = true;
            _inventory.contentChanged.AddListener(OnContentChanged);
            _inventory.selectedChanged.AddListener(OnSelectedChanged);
        }

        private void UnSubscribe()
        {
            _isSubscribed = false;
            _inventory.selectedChanged.RemoveListener(OnSelectedChanged);
            _inventory.contentChanged.RemoveListener(OnContentChanged);
        }

        private void OnContentChanged(InventoryContentChangedEventArgs arg0)
        {
            if (!_items.ContainsKey(arg0.config)) SetUpItem(arg0.config, arg0.currentValue);
            else UpdateItem(arg0.config, arg0.currentValue);
            
        }

        private void OnSelectedChanged(InventorySelectedChangedEventArgs arg0)
        {
            UpdateSelected();
        }

        private void UpdateSelected()
        {
            if (_currentSelected) _currentSelected.UnSetSelected();
            if (_inventory.Selected == null) return;
            _currentSelected = _items[_inventory.Selected];
            _currentSelected.SetSelected();
        }

        private void SetUpItem(GrenadeConfig config, int count)
        {
            var inventoryUiItem = Instantiate(itemPrefab, itemsContainer, true);
            _items.Add(config, inventoryUiItem);
            UpdateItem(config, count);
        }

        private void UpdateItem(GrenadeConfig config, int count)
        {
            if (count <= 0)
            {
                Destroy(_items[config].gameObject);
                _items.Remove(config);
                return;
            }


            _items[config].SetUp(config, count);
        }
    }
}