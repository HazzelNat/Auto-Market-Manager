using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemPriceText;
    [SerializeField] Sprite itemImage;
    [SerializeField] int itemPrice;
    [SerializeField] string itemName;
    [SerializeField] GameObject box;
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] GameObject boxes;

    private void Start() {
        image.sprite = itemImage;
        itemNameText.text = itemName;
        itemPriceText.text = $"${itemPrice}";
    }

    public void BuyItem()
    {
        if(moneyManager.CheckMoney() >= itemPrice){
            moneyManager.SpentMoney(itemPrice);

            
            GameObject instance = Instantiate(box, new Vector3(Random.Range(2, 13), Random.Range(-1, -7), 0), Quaternion.identity);
            instance.transform.parent = boxes.transform;

            instance.GetComponent<BoxData>().itemName = itemName;
            instance.GetComponent<BoxData>().itemSprite = itemImage;
            instance.GetComponent<BoxData>().stockNumber = 10;
        }

    }
}
