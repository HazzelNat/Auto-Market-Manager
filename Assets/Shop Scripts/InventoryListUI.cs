using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryListUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform inventoryItemTemplate;
    [SerializeField] private GameObject inventoryPanel;

    private InventoryManager inventoryManager;
    private Dictionary<Item.ItemType, GameObject> inventoryItemObjects = new Dictionary<Item.ItemType, GameObject>();

    private void Awake()
    {
        // Find references if not set in inspector
        if (container == null)
        {
            container = transform.Find("Inventory_Container");
        }

        if (inventoryItemTemplate == null)
        {
            inventoryItemTemplate = container.Find("inventoryItemTemplate");
        }

        // Hide template and panel initially
        if (inventoryItemTemplate != null)
            inventoryItemTemplate.gameObject.SetActive(false);

        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        // Find InventoryManager
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void UpdateInventoryItem(Item.ItemType itemType, int count)
    {
        // If count is 0, remove the item from display
        if (count <= 0)
        {
            RemoveInventoryItem(itemType);
            return;
        }

        // If item exists, update its count
        if (inventoryItemObjects.ContainsKey(itemType))
        {
            TextMeshProUGUI countText = inventoryItemObjects[itemType].transform.Find("countText").GetComponent<TextMeshProUGUI>();
            countText.text = count.ToString();
            return;
        }

        // If item doesn't exist, create a new entry
        CreateInventoryItemEntry(itemType, count);
    }

    private void CreateInventoryItemEntry(Item.ItemType itemType, int count)
    {
        Transform inventoryItemTransform = Instantiate(inventoryItemTemplate, container);
        RectTransform inventoryItemRectTransform = inventoryItemTransform.GetComponent<RectTransform>();

        float inventoryItemHeight = 100f;
        inventoryItemRectTransform.anchoredPosition = new Vector2(0, -inventoryItemHeight * (inventoryItemObjects.Count));

        // Set item name
        string itemName = itemType.ToString().Replace('_', ' ');
        inventoryItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);

        // Set item count
        inventoryItemTransform.Find("countText").GetComponent<TextMeshProUGUI>().SetText(count.ToString());

        // Set item image
        inventoryItemTransform.Find("itemImage").GetComponent<Image>().sprite = Item.GetSprite(itemType);

        inventoryItemTransform.gameObject.SetActive(true);

        // Store reference to the created object
        inventoryItemObjects[itemType] = inventoryItemTransform.gameObject;
    }

    private void RemoveInventoryItem(Item.ItemType itemType)
    {
        if (inventoryItemObjects.ContainsKey(itemType))
        {
            Destroy(inventoryItemObjects[itemType]);
            inventoryItemObjects.Remove(itemType);

            // Reposition remaining items
            ReorganizeInventoryDisplay();
        }
    }

    private void ReorganizeInventoryDisplay()
    {
        float inventoryItemHeight = 100f;
        int index = 0;
        foreach (var itemObject in inventoryItemObjects.Values)
        {
            RectTransform rectTransform = itemObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -inventoryItemHeight * index);
            index++;
        }
    }

    public void ToggleInventory()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System.Collections.Generic;

// public class InventoryListUI : MonoBehaviour
// {
//     [SerializeField] private Transform container;
//     [SerializeField] private Transform inventoryItemTemplate;
//     [SerializeField] private GameObject inventoryPanel;
//     [SerializeField] private CanvasGroup inventoryCanvasGroup;

//     private InventoryManager inventoryManager;
//     private Dictionary<Item.ItemType, GameObject> inventoryItemObjects = new Dictionary<Item.ItemType, GameObject>();

//     private void Awake()
//     {
//         // Find references if not set in inspector
//         if (container == null)
//         {
//             container = transform.Find("Inventory_Container");
//         }

//         if (inventoryItemTemplate == null)
//         {
//             inventoryItemTemplate = container.Find("inventoryItemTemplate");
//         }

//         // If no CanvasGroup is assigned, try to get or add one
//         if (inventoryCanvasGroup == null)
//         {
//             inventoryCanvasGroup = GetComponent<CanvasGroup>();
//             if (inventoryCanvasGroup == null)
//             {
//                 inventoryCanvasGroup = gameObject.AddComponent<CanvasGroup>();
//             }
//         }

//         // Hide template initially
//         if (inventoryItemTemplate != null)
//             inventoryItemTemplate.gameObject.SetActive(false);

//         // Initially hide the inventory but keep it interactable
//         HideInventory();

//         // Find InventoryManager
//         inventoryManager = FindObjectOfType<InventoryManager>();
//     }

//     public void UpdateInventoryItem(Item.ItemType itemType, int count)
//     {
//         // If count is 0, remove the item from display
//         if (count <= 0)
//         {
//             RemoveInventoryItem(itemType);
//             return;
//         }

//         // If item exists, update its count
//         if (inventoryItemObjects.ContainsKey(itemType))
//         {
//             TextMeshProUGUI countText = inventoryItemObjects[itemType].transform.Find("countText").GetComponent<TextMeshProUGUI>();
//             countText.text = count.ToString();
//             return;
//         }

//         // If item doesn't exist, create a new entry
//         CreateInventoryItemEntry(itemType, count);
//     }

//     private void CreateInventoryItemEntry(Item.ItemType itemType, int count)
//     {
//         Transform inventoryItemTransform = Instantiate(inventoryItemTemplate, container);
//         RectTransform inventoryItemRectTransform = inventoryItemTransform.GetComponent<RectTransform>();

//         float inventoryItemHeight = 100f;
//         inventoryItemRectTransform.anchoredPosition = new Vector2(0, -inventoryItemHeight * (inventoryItemObjects.Count));

//         // Set item name
//         string itemName = itemType.ToString().Replace('_', ' ');
//         inventoryItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);

//         // Set item count
//         inventoryItemTransform.Find("countText").GetComponent<TextMeshProUGUI>().SetText(count.ToString());

//         // Set item image
//         inventoryItemTransform.Find("itemImage").GetComponent<Image>().sprite = Item.GetSprite(itemType);

//         inventoryItemTransform.gameObject.SetActive(true);

//         // Store reference to the created object
//         inventoryItemObjects[itemType] = inventoryItemTransform.gameObject;
//     }

//     private void RemoveInventoryItem(Item.ItemType itemType)
//     {
//         if (inventoryItemObjects.ContainsKey(itemType))
//         {
//             Destroy(inventoryItemObjects[itemType]);
//             inventoryItemObjects.Remove(itemType);

//             // Reposition remaining items
//             ReorganizeInventoryDisplay();
//         }
//     }

//     private void ReorganizeInventoryDisplay()
//     {
//         float inventoryItemHeight = 100f;
//         int index = 0;
//         foreach (var itemObject in inventoryItemObjects.Values)
//         {
//             RectTransform rectTransform = itemObject.GetComponent<RectTransform>();
//             rectTransform.anchoredPosition = new Vector2(0, -inventoryItemHeight * index);
//             index++;
//         }
//     }

//     public void ShowInventory()
//     {
//         if (inventoryCanvasGroup != null)
//         {
//             inventoryCanvasGroup.alpha = 1f;
//             inventoryCanvasGroup.interactable = true;
//             inventoryCanvasGroup.blocksRaycasts = true;
//         }
//     }

//     public void HideInventory()
//     {
//         if (inventoryCanvasGroup != null)
//         {
//             inventoryCanvasGroup.alpha = 0f;
//             inventoryCanvasGroup.interactable = false;
//             inventoryCanvasGroup.blocksRaycasts = false;
//         }
//     }

//     public void ToggleInventory()
//     {
//         if (inventoryCanvasGroup.alpha == 0f)
//         {
//             ShowInventory();
//         }
//         else
//         {
//             HideInventory();
//         }
//     }
// }