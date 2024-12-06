using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    MoneyManager moneyManager;
    private float money;
    [SerializeField] private float moneyRequirement;
    [SerializeField] private GameObject levelWinPanel; 
    [SerializeField] private GameObject levelLosePanel; 

    private void Start() {
        moneyManager = GetComponent<MoneyManager>();
    }

    private void Update() {
        money = moneyManager.CheckMoney();

        if(money >= moneyRequirement){
            FinishLevel();
        }
    }

    private void FinishLevel()
    {
        // levelWinPanel.SetActive(true);
    }
}
