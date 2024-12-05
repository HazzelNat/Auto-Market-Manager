using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private float startingMoney = 1000;
    [SerializeField] private TextMeshProUGUI moneyText; // Reference to UI 
    
    private float currentMoney;

    private void Start()
    {
        currentMoney = startingMoney;
        UpdateMoneyDisplay();
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
            moneyText.text = $"${currentMoney}";
        }
    }
}