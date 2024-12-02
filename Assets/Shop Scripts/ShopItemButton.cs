using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemButton : MonoBehaviour
{
    private Item.ItemType itemType;
    private Button button;
    private PlayerMoney playerMoney;
    private InventoryManager inventoryManager;
    
    // private CurrentMoney currentMoney;

    [SerializeField] private GameObject insufficientFundsPanel;
    [SerializeField] private TextMeshProUGUI insufficientFundsText;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            button = gameObject.AddComponent<Button>();
        }

        playerMoney = FindObjectOfType<PlayerMoney>();
        if (playerMoney == null)
        {
            Debug.LogError("PlayerMoney component not found in the scene!");
        }

        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager component not found in the scene!");
        }

        if (insufficientFundsPanel == null)
        {
            insufficientFundsPanel = GameObject.Find("InsufficientFundsPanel");
        }

        if (insufficientFundsText == null && insufficientFundsPanel != null)
        {
            insufficientFundsText = insufficientFundsPanel.GetComponentInChildren<TextMeshProUGUI>();
        }

        if (insufficientFundsPanel != null)
        {
            insufficientFundsPanel.SetActive(false);
        }

        button.onClick.AddListener(OnItemButtonClicked);
    }

    public void Initialize(Item.ItemType type)
    {
        itemType = type;
    }

    private void OnItemButtonClicked()
    {
        Debug.Log($"Attempting to purchase {itemType}...");
        int itemCost = Item.GetCost(itemType);

        if (playerMoney != null && playerMoney.CanAfford(itemCost))
        {
            if (playerMoney.SpendMoney(itemCost))
            {
                Debug.Log($"Spent ${itemCost} for {itemType}.");

                if (inventoryManager.AddItem(itemType))
                {
                    Debug.Log($"Successfully added {itemType} to inventory.");
                    HideInsufficientFundsMessage();
                }
                else
                {
                    ShowInsufficientFundsMessage($"Inventory full! Cannot add {itemType}.");
                    Debug.LogWarning($"Inventory full: Refund ${itemCost} for {itemType}.");
                    playerMoney.AddMoney(itemCost);
                }
            }
        }
        else
        {
            // Debug.LogWarning($"Not enough money to purchase {itemType}. Cost: ${itemCost}, Available: ${playerMoney.currentMoney}");
            // ShowInsufficientFundsMessage($"Not enough money to purchase {itemType}!");
        }
    }

    private void ShowInsufficientFundsMessage(string message)
    {
        if (insufficientFundsPanel != null && insufficientFundsText != null)
        {
            insufficientFundsText.text = message;
            insufficientFundsPanel.SetActive(true);
            Invoke("HideInsufficientFundsMessage", 3f);
        }
        else
        {
            Debug.LogWarning("Insufficient funds panel or text is not set!");
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