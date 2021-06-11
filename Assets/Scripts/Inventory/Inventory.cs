using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Grenades;
using PlayerInput;
using Throwing.Projectile;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    [Serializable]
    public class InventoryContentChangedEvent : UnityEvent<InventoryContentChangedEventArgs>
    {
    }

    [Serializable]
    public class InventoryContentChangedEventArgs
    {
        public GrenadeConfig config;
        public int changeValue;
        public int currentValue;

        public InventoryContentChangedEventArgs(GrenadeConfig config, int changeValue, int currentValue)
        {
            this.config = config;
            this.changeValue = changeValue;
            this.currentValue = currentValue;
        }
    }

    [Serializable]
    public class InventorySelectedChangedEvent : UnityEvent<InventorySelectedChangedEventArgs>
    {
    }

    [Serializable]
    public class InventorySelectedChangedEventArgs
    {
    }


    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Dictionary<GrenadeConfig, int> grenades = new Dictionary<GrenadeConfig, int>();
        [SerializeField] private GrenadeConfig selected;


        public InventorySelectedChangedEvent selectedChanged = new InventorySelectedChangedEvent();
        public InventoryContentChangedEvent contentChanged = new InventoryContentChangedEvent();

        public ReadOnlyDictionary<GrenadeConfig, int> Grenades => new ReadOnlyDictionary<GrenadeConfig, int>(grenades);
        public GrenadeConfig Selected => selected;
        public bool IsProjectilesEmpty => grenades.Values.All(grenadesValue => grenadesValue == 0);

        public void AddGrenade(GrenadeConfig grenadeConfig, int count)
        {
            if (!grenades.ContainsKey(grenadeConfig)) grenades.Add(grenadeConfig, count);
            else grenades[grenadeConfig] += count;

            contentChanged.Invoke(new InventoryContentChangedEventArgs(grenadeConfig, count, grenades[grenadeConfig]));

            UpdateSelected();
        }

        public bool TryTakeGrenade(GrenadeConfig grenadeConfig, int count)
        {
            if (!grenades.ContainsKey(grenadeConfig)) return false;

            var curCount = grenades[grenadeConfig];

            if (curCount - count < 0) return false;

            if (curCount - count == 0) grenades.Remove(grenadeConfig);
            else grenades[grenadeConfig] -= count;

            contentChanged.Invoke(new InventoryContentChangedEventArgs(grenadeConfig, -count, curCount - count));
            UpdateSelected();

            return true;
        }


        public bool TryTakeSelected(out IProjectileProvider provider, int count = 1)
        {
            provider = null;
            var currentSelected = selected;
            if (!currentSelected) return false;

            if (TryTakeGrenade(currentSelected, count))
            {
                provider = currentSelected;
                return true;
            }

            return false;
        }

        public IProjectileProvider GetSelected()
        {
            return Selected;
        }


        private void OnEnable()
        {
            InputReader.InventoryKeyPressedEvent.AddListener(OnInventoryKey);
        }

        private void OnDisable()
        {
            InputReader.InventoryKeyPressedEvent.RemoveListener(OnInventoryKey);
        }

        private void UpdateSelected()
        {
            if (selected && grenades.ContainsKey(selected) && grenades[selected] > 0) return;

            selected = null;
            if (grenades.Keys.Count > 0) selected = grenades.Keys.First();
            SelectedChangedInvoke(new InventorySelectedChangedEventArgs());
        }


        private void SwitchSelection(bool isLeft)
        {
            var configs = grenades.Keys.ToArray();
            var selectedId = -1;
            for (var i = 0; i < configs.Length; i++)
            {
                if (selected == configs[i])
                {
                    selectedId = i;
                }
            }


            if (isLeft) selectedId--;
            else selectedId++;

            // Debug.Log($"{selectedId} % {configs.Length} ={selectedId % configs.Length} ");
            selectedId = selectedId % configs.Length;
            if (selectedId < 0) selectedId += configs.Length;

            selected = configs[selectedId];
            SelectedChangedInvoke(new InventorySelectedChangedEventArgs());
        }

        private void OnInventoryKey(InventoryKeyEventArgs arg0)
        {
            switch (arg0.inventoryAction)
            {
                case InventoryAction.SwipeLeft:
                    SwitchSelection(true);
                    break;
                case InventoryAction.SwipeRight:
                    SwitchSelection(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SelectedChangedInvoke(InventorySelectedChangedEventArgs args)
        {
            selectedChanged.Invoke(args);
        }
    }
}