using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryCountDisplay : MonoBehaviour
{
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private GameObject inventoryItemPrefab;
    
    // Dictionary to store UI elements for each item type
    private Dictionary<Item.ItemType, GameObject> inventoryItemUIs = new Dictionary<Item.ItemType, GameObject>();

    public void UpdateItemCount(Item.ItemType itemType, int count)
    {
        // If item UI doesn't exist, create it
        if (!inventoryItemUIs.ContainsKey(itemType))
        {
            CreateInventoryItemUI(itemType);
        }

        // Update the count text
        GameObject itemUI = inventoryItemUIs[itemType];
        TextMeshProUGUI itemCountText = itemUI.transform.Find("ItemCount").GetComponent<TextMeshProUGUI>();
        
        if (count > 0)
        {
            itemCountText.text = count.ToString();
            itemUI.SetActive(true);
        }
        else
        {
            itemUI.SetActive(false);
        }
    }

    private void CreateInventoryItemUI(Item.ItemType itemType)
    {
        // Instantiate new item UI
        GameObject inventoryItemObj = Instantiate(inventoryItemPrefab, inventoryContainer);
        
        // Set item image
        Image itemImage = inventoryItemObj.transform.Find("ItemImage").GetComponent<Image>();
        itemImage.sprite = Item.GetSprite(itemType);

        // Set item name
        TextMeshProUGUI itemNameText = inventoryItemObj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemNameText.text = itemType.ToString().Replace('_', ' ');

        // Store reference
        inventoryItemUIs[itemType] = inventoryItemObj;
    }
}
