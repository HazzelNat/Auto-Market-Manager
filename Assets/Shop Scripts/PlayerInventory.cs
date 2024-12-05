using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxInventorySize = 20;
    private List<Item.ItemType> inventory = new List<Item.ItemType>();

    public bool AddItem(Item.ItemType item)
    {
        if (inventory.Count >= maxInventorySize)
            return false;

        inventory.Add(item);
        OnInventoryChanged();
        return true;
    }

    public bool RemoveItem(Item.ItemType item)
    {
        bool removed = inventory.Remove(item);
        if (removed)
            OnInventoryChanged();
        return removed;
    }

    public int GetItemCount(Item.ItemType item)
    {
        return inventory.FindAll(x => x == item).Count;
    }

    private void OnInventoryChanged()
    {
        Debug.Log("Inventory updated!");
    }
}