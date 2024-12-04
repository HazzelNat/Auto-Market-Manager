using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxData : MonoBehaviour{
    public string itemName;
    public Sprite itemSprite;
    public float cost;
    public int stockNumber;

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;

    private void Update() {
        if(itemName != null && itemSprite != null){
           UpdateUI();
        }
    }

    private void UpdateUI() {
        image.sprite = itemSprite;
        text.text = itemName;
    }
}