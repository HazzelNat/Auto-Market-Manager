using UnityEngine;
using UnityEngine.UI;

public class UIManagerController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button toggleUIButton;
    [SerializeField] private GameObject shopTint;

    private void Start()
    {
        shopTint.SetActive(false);
        // Hide panels initially
        if (shopPanel != null)
            shopTint.SetActive(false);
            shopPanel.SetActive(false);
        
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
            shopTint.SetActive(false);

        // Set up button listener
        if (toggleUIButton != null)
            toggleUIButton.onClick.AddListener(ToggleUI);
    }

    public void ToggleUI()
    {
        // Toggle both panels
        if (shopPanel != null)
            shopTint.SetActive(!shopTint.activeSelf);
            shopPanel.SetActive(!shopPanel.activeSelf);
        
        if (inventoryPanel != null)
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
