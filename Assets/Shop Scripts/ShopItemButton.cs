using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemButton : MonoBehaviour
{
    private Item.ItemType itemType;
    private Button button;
    private PlayerMoney playerMoney;
    private InventoryManager inventoryManager;

    [SerializeField] private GameObject insufficientFundsPanel;
    [SerializeField] private TextMeshProUGUI insufficientFundsText;

    private void Awake()
    {
        // Get the Button component attached to this GameObject
        button = GetComponent<Button>();
        
        // If no Button component exists, add one
        if (button == null)
        {
            button = gameObject.AddComponent<Button>();
        }

        // Find the PlayerMoney and InventoryManager components in the scene
        playerMoney = FindObjectOfType<PlayerMoney>();
        inventoryManager = FindObjectOfType<InventoryManager>();

        // Find or create the insufficient funds panel if not assigned
        if (insufficientFundsPanel == null)
        {
            insufficientFundsPanel = GameObject.Find("InsufficientFundsPanel");
        }

        // Find the text component for insufficient funds message
        if (insufficientFundsText == null && insufficientFundsPanel != null)
        {
            insufficientFundsText = insufficientFundsPanel.GetComponentInChildren<TextMeshProUGUI>();
        }

        // Ensure the insufficient funds panel is initially hidden
        if (insufficientFundsPanel != null)
        {
            insufficientFundsPanel.SetActive(false);
        }

        // Add click listener
        button.onClick.AddListener(OnItemButtonClicked);
    }

    public void Initialize(Item.ItemType type)
    {
        itemType = type;
    }

    private void OnItemButtonClicked()
    {
        // Get the cost of the item
        int itemCost = Item.GetCost(itemType);

        // Check if player can afford the item
        if (playerMoney != null && playerMoney.CanAfford(itemCost))
        {
            // Spend money
            if (playerMoney.SpendMoney(itemCost))
            {
                // Try to add item to inventory
                if (inventoryManager.AddItem(itemType))
                {
                    Debug.Log($"Purchased {itemType} for ${itemCost}");
                    HideInsufficientFundsMessage();
                }
                else
                {
                    // Inventory is full
                    ShowInsufficientFundsMessage($"Inventory is full! Cannot add {itemType}.");
                    // Refund the money since inventory addition failed
                    playerMoney.AddMoney(itemCost);
                }
            }
        }
        else
        {
            ShowInsufficientFundsMessage($"Not enough money to purchase {itemType}!");
        }
    }

    private void ShowInsufficientFundsMessage(string message)
    {
        if (insufficientFundsPanel != null && insufficientFundsText != null)
        {
            insufficientFundsText.text = message;
            insufficientFundsPanel.SetActive(true);

            // Optional: Auto-hide the message after a few seconds
            Invoke("HideInsufficientFundsMessage", 3f);
        }
    }

    private void HideInsufficientFundsMessage()
    {
        if (insufficientFundsPanel != null)
        {
            insufficientFundsPanel.SetActive(false);
        }
    }
}