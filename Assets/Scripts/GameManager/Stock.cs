using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stock : MonoBehaviour
{
    public GameObject box;
    public BoxData boxData;

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI itemStock;
    CanvasGroup canvasGroup;

    public string itemName;
    public Sprite itemSprite;
    public float cost;
    public int stockNumber;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckItem(GameObject box)
    {
        boxData = box.GetComponent<BoxData>();

        if(boxData != null && stockNumber == 0){
            AddItem(box);
            return true;
        } else {
            return false;
        }
    }

    public void AddItem(GameObject box)
    {
        boxData = box.GetComponent<BoxData>();

        itemName = boxData.itemName;
        itemSprite = boxData.itemSprite;
        cost = boxData.cost;
        stockNumber = boxData.stockNumber;

        UpdateUI();
    }

    private void UpdateUI()
    {
        canvasGroup = (gameObject.transform.GetChild(0).gameObject).GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        itemStock.text = stockNumber.ToString();
        image.sprite = itemSprite;
    }
}