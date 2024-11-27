using UnityEngine;
using UnityEngine.UI;

public class UIManagerController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button toggleUIButton;

    private void Start()
    {
        // Hide panels initially
        if (shopPanel != null)
            shopPanel.SetActive(false);
        
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        // Set up button listener
        if (toggleUIButton != null)
            toggleUIButton.onClick.AddListener(ToggleUI);
    }

    public void ToggleUI()
    {
        // Toggle both panels
        if (shopPanel != null)
            shopPanel.SetActive(!shopPanel.activeSelf);
        
        if (inventoryPanel != null)
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
