using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private int money = 1000;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start() {
        UpdateMoney();
    }

    public void SpentMoney(int amount)
    {
        money -= amount;
        UpdateMoney();
    }

    public void ProfitMoney(int amount)
    {
        money += amount;
        UpdateMoney();
    }

    public int CheckMoney()
    {
        return money;
    }

    private void UpdateMoney()
    {
        moneyText.text = $"${money}";
    }
}
