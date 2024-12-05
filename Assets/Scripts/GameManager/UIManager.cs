using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button shopButton;
    [SerializeField] GameObject shopPanel;

    private void Start() {
        shopPanel.SetActive(false);
    }
    
    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
}
