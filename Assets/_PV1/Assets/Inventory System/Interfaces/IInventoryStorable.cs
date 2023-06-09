namespace PV.InventorySystem
{
    public interface IInventoryStorable
    {
        public void OnStored(Inventory.InventoryEntry entry);
        public void OnRemoved(Inventory.InventoryEntry entry);
        public InventoryItem InventoryItem { get; }
    }
}