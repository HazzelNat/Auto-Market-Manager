using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button shopButton;
    [SerializeField] GameObject shopPanel;
    [SerializeField] Button SettingsButton;
    [SerializeField] GameObject SettingPanel;

    private void Start() {
        shopPanel.SetActive(false);
        SettingPanel.SetActive(false);

    }
    
    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
      public void ToggleSettings()
    {
        SettingPanel.SetActive(!SettingPanel.activeSelf);
    }
}
