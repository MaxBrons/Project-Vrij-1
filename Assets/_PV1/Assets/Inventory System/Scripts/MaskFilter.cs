using PV.Interaction;
using PV.InventorySystem;
using UnityEngine;

namespace PV
{
    public class MaskFilter : Interactable, IInventoryStorable
    {
        [SerializeField] private InventoryItem m_Item;

        public InventoryItem InventoryItem => m_Item;

        protected override void OnInteraction(bool success)
        {
            if (!success)
                return;

            Inventory.Instance.AddItem(this);
        }

        public void OnStored(Inventory.InventoryEntry entry)
        {
            Debug.Log("STORED");
            Destroy(gameObject);
        }

        public void OnRemoved(Inventory.InventoryEntry entry) { }
    }
}
