using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Button closeButton;
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;

    private void Start()
    {
        // Make sure shop is hidden on start
        if (shopPanel != null)
            shopPanel.SetActive(false);

        // Set up close button
        if (closeButton != null)
            closeButton.onClick.AddListener(() => ToggleShop());
    }

    private void Update()
    {
        // Allow toggling with key press
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleShop();
        }
    }

    public void ToggleShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(!shopPanel.activeSelf);
        }
    }
}
