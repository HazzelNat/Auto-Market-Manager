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

    private void Start() {
        if(itemName != null && itemSprite != null){
           UpdateUI();
        }
    }

    private void Update() 
    {
        
    }

    private void UpdateUI() 
    {
        image.sprite = itemSprite;
        ChangeText(itemName);
    }

    public void CantPutThere(string text)
    {
        ChangeText(text);
        StartCoroutine(ChangeTextBack());
    }

    private void ChangeText(string newText)
    {
        text.text = newText;
    }

    IEnumerator ChangeTextBack()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        UpdateUI();
    }
}