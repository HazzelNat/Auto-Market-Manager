using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryListUI inventoryListUI;
    private Dictionary<Item.ItemType, int> inventoryItems = new Dictionary<Item.ItemType, int>();

    public bool AddItem(Item.ItemType itemType)
    {
        // Update item count
        if (inventoryItems.ContainsKey(itemType))
        {
            inventoryItems[itemType]++;
        }
        else
        {
            inventoryItems[itemType] = 1;
        }

        // Update display
        UpdateInventoryDisplay(itemType);

        return true;
    }

    public void RemoveItem(Item.ItemType itemType)
    {
        if (!inventoryItems.ContainsKey(itemType) || inventoryItems[itemType] <= 0)
        {
            Debug.Log("Item not in inventory!");
            return;
        }

        inventoryItems[itemType]--;

        // Update display
        UpdateInventoryDisplay(itemType);
    }

    private void UpdateInventoryDisplay(Item.ItemType itemType)
    {
        // Get current count
        int currentCount = inventoryItems.ContainsKey(itemType) ? inventoryItems[itemType] : 0;

        // Update UI
        if (inventoryListUI != null)
        {
            inventoryListUI.UpdateInventoryItem(itemType, currentCount);
        }
    }

    public int GetItemCount(Item.ItemType itemType)
    {
        return inventoryItems.ContainsKey(itemType) ? inventoryItems[itemType] : 0;
    }
}