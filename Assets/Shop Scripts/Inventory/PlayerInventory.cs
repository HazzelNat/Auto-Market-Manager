// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerInventory : MonoBehaviour
// {
//     [SerializeField] private int maxInventorySize = 20;
//     [SerializeField] private InventoryItemSpawner itemSpawner; // New reference to the spawner
    
//     private List<Item.ItemType> inventory = new List<Item.ItemType>();
    
//     // Restore the InventoryWorldDisplay reference
//     private InventoryWorldDisplay inventoryWorldDisplay;
//     public event System.Action<Item.ItemType> OnItemAdded;

//     private void Awake()
//     {
//         // Restore the original inventoryWorldDisplay initialization
//         inventoryWorldDisplay = GetComponent<InventoryWorldDisplay>();

//         // If not set in inspector, try to find the spawner
//         if (itemSpawner == null)
//         {
//             itemSpawner = GetComponent<InventoryItemSpawner>();
            
//             // If still not found, search in the scene
//             if (itemSpawner == null)
//             {
//                 itemSpawner = FindObjectOfType<InventoryItemSpawner>();
//             }
//         }
//     }

//     public bool AddItem(Item.ItemType item)
//     {
//         if (inventory.Count >= maxInventorySize)
//             return false;

//         inventory.Add(item);
//         OnInventoryChanged(item);
        
//         // Invoke the event when an item is added
//         OnItemAdded?.Invoke(item);

//         // Spawn the item in the world when added to inventory
//         if (itemSpawner != null)
//         {
//             // Spawn in the first available grid cell
//             itemSpawner.SpawnItemInGrid(item);
//         }
//         else
//         {
//             Debug.LogWarning("No InventoryItemSpawner found to spawn item!");
//         }

//         return true;
//     }

//     public bool RemoveItem(Item.ItemType item)
//     {
//         bool removed = inventory.Remove(item);
//         if (removed)
//             OnInventoryChanged(item);
//         return removed;
//     }

//     public int GetItemCount(Item.ItemType item)
//     {
//         return inventory.FindAll(x => x == item).Count;
//     }

//     private void OnInventoryChanged(Item.ItemType item)
//     {
//         Debug.Log("Inventory updated!");
        
//         // Show world sprite when item is added
//         if (inventoryWorldDisplay != null)
//         {
//             inventoryWorldDisplay.ShowItemPickupWorldSprite(item);
//         }
//     }
// }
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxInventorySize = 20;
    [SerializeField] private InventoryItemSpawner itemSpawner;

    private List<Item.ItemType> inventory = new List<Item.ItemType>();

    private void Awake()
    {
        if (itemSpawner == null)
        {
            itemSpawner = GetComponent<InventoryItemSpawner>();

            if (itemSpawner == null)
            {
                itemSpawner = FindObjectOfType<InventoryItemSpawner>();
            }
        }

        if (itemSpawner == null)
        {
            Debug.LogError("No InventoryItemSpawner found in the scene!");
        }
    }

    public bool AddItem(Item.ItemType item)
    {
        if (inventory.Count >= maxInventorySize)
        {
            Debug.LogWarning("Inventory is full!");
            return false;
        }

        inventory.Add(item);
        Debug.Log($"{item} added to inventory. Current inventory size: {inventory.Count}");

        if (itemSpawner != null)
        {
            itemSpawner.SpawnItemInGrid(item);
        }
        else
        {
            Debug.LogError("ItemSpawner reference is null, unable to spawn item!");
        }

        return true;
    }
}
