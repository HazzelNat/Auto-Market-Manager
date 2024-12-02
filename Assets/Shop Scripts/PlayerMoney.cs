using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private int startingMoney = 100;
    [SerializeField] private TextMeshProUGUI moneyText; // Reference to UI 
    
    private int currentMoney;

    private void Start()
    {
        currentMoney = startingMoney;
        UpdateMoneyDisplay();
    }

    // New method to safely access current money
    public int GetCurrentMoney()
    {
        return currentMoney;
    }

    public bool CanAfford(int amount)
    {
        return currentMoney >= amount;
    }

    public bool SpendMoney(int amount)
    {
        if (!CanAfford(amount)) return false;
        
        currentMoney -= amount;
        UpdateMoneyDisplay();
        return true;
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyDisplay();
    }

    private void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = $"Current Balance: ${currentMoney}";
        }
    }
}