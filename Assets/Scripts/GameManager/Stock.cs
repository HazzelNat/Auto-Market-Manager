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
    [SerializeField] int maxStock;
    CanvasGroup canvasGroup;

    public string itemName;
    public Sprite itemSprite;
    public int stockNumber;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public bool CheckItem(GameObject newBox)
    {
        box = newBox;
        boxData = box.GetComponent<BoxData>();

        if(boxData.itemName == gameObject.name){
            if(maxStock - stockNumber <= maxStock){
                AddItem(box);
                return true;
            } else {
                boxData.CantPutThere("Full!");
            }
           
        } else {
            boxData.CantPutThere("Wrong item to put");
        }

        return false;
    }

    public void AddItem(GameObject box)
    {
        boxData = box.GetComponent<BoxData>();

        itemName = boxData.itemName;
        itemSprite = boxData.itemSprite;
        stockNumber += boxData.stockNumber;

        UpdateUIOn();
    }

    private void UpdateUIOn()
    {
        canvasGroup = (gameObject.transform.GetChild(0).gameObject).GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        itemStock.text = stockNumber.ToString();
        image.sprite = itemSprite;
    }

    public bool DecreaseStock()
    {
        if(stockNumber > 0){
            stockNumber -= 1;
            UpdateUIOn();
            CheckForStock();
            return true;
        }

        UpdateUIOn();
        CheckForStock();

        return false;
    }

    private void CheckForStock()
    {
        if(stockNumber == 0){
            canvasGroup = gameObject.transform.GetChild(0).gameObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            itemStock.text = null;
            image.sprite = null;
        }
    }
}