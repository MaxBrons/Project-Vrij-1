using System;
using System.Collections.Generic;
using UnityEngine;

namespace PV.InventorySystem
{
    [System.Serializable]
    public struct InventoryItem
    {
        public string Name;
        public Sprite Sprite;
        public GameObject ItemPrefab;

        public InventoryItem(string name, Sprite sprite, GameObject itemPrefab)
        {
            Name = name;
            Sprite = sprite;
            ItemPrefab = itemPrefab;
        }

        public readonly bool Equals(InventoryItem Other) => Name == Other.Name && Sprite == Other.Sprite;
    }

    public class Inventory : Core.Behaviour
    {
        public class InventoryEntry
        {
            public InventoryItem Item;
            public int Count;

            public InventoryEntry(InventoryItem item, int count)
            {
                Item = item;
                Count = count;
            }
        }

        public static Inventory Instance {
            get => s_Instance;
            set
            {
                if (s_Instance != null)
                    return;
                s_Instance = value;
            }
        }
        private static Inventory s_Instance;

        public Action<InventoryEntry> OnItemAdded;
        public Action<InventoryEntry> OnItemRemoved;

        private List<InventoryEntry> m_Items = new List<InventoryEntry>();

        public override void OnStart()
        {
            Instance = this;
            if (Instance != this)
                return;
        }

        public override void OnDestroy()
        {
            if (s_Instance == this)
                s_Instance = null;
        }

        public void AddItem(IInventoryStorable item)
        {
            if (item.Equals(null))
                return;

            var entry = m_Items.Find(e => e.Item.Equals(item.InventoryItem)) ?? new InventoryEntry(item.InventoryItem, 1);
            entry.Count++;

            item.OnStored(entry);
            OnItemAdded?.Invoke(entry);
        }

        public void RemoveItem(IInventoryStorable item)
        {
            if (item.Equals(null))
                return;

            var entry = m_Items.Find(e => e.Item.Equals(item.InventoryItem));
            if (entry == null)
                return;

            entry.Count--;

            item.OnRemoved(entry);
            OnItemRemoved?.Invoke(entry);
        }
    }
}
