using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private float money = 1000;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start() {
        UpdateMoney();
    }

    public void SpentMoney(float amount)
    {
        money -= amount;
        UpdateMoney();
    }

    public void ProfitMoney(float amount)
    {
        money += amount;
        UpdateMoney();
    }

    public float CheckMoney()
    {
        return money;
    }

    private void UpdateMoney()
    {
        moneyText.text = $"${money}";
    }
}
