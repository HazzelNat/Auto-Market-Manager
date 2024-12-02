using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Shop : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform shopItemTemplate; // Refferences the shop item template
    [SerializeField] private GameObject shopPanel; // Reference to the entire shop panel

    private void Awake()
    {
        if (container == null)
        {
            GameObject containerObj = GameObject.Find("Shop_Container");
            if (containerObj == null)
            {
                Debug.LogError("Shop_Container not found");
                return;
            }
            container = containerObj.transform;
        }

        if (shopItemTemplate == null)
        {
            shopItemTemplate = container.Find("shopItemTemplate");
            if (shopItemTemplate == null)
            {
                Debug.LogError("shopItemTemplate not found in Shop_Container");
                return;
            }
        }

        shopItemTemplate.gameObject.SetActive(false);
        if (shopPanel != null)
            shopPanel.SetActive(false);
    }

    private void Start()
    {
        if (container == null || shopItemTemplate == null)
        {
            Debug.LogError("Required components not found. Shop initialization failed.");
            return;
        }
        // Ambil Enum dari GameAssets
        int index = 0;
        foreach (Item.ItemType itemType in System.Enum.GetValues(typeof(Item.ItemType)))
        {
            string itemName = itemType.ToString().Replace('_', ' ');
            CreateItemButton(Item.GetSprite(itemType), itemName, Item.GetCost(itemType), index, itemType);
            index++;
        }
    }

    private void CreateItemButton(Sprite itemSprite, string itemName, int itemCost, int posIndex, Item.ItemType itemType)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 175f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * posIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        // Button
        ShopItemButton buttonComponent = shopItemTransform.gameObject.AddComponent<ShopItemButton>();
        buttonComponent.Initialize(itemType);

        shopItemTransform.gameObject.SetActive(true);
    }

    public void ToggleShop()
    {
        if (shopPanel != null)
            shopPanel.SetActive(!shopPanel.activeSelf);
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class Player_Shop : MonoBehaviour
// {
//     [SerializeField] private Transform container;
//     [SerializeField] private Transform shopItemTemplate;
//     [SerializeField] private GameObject shopPanel;
//     [SerializeField] private CanvasGroup shopCanvasGroup; // Add this

//     private void Awake()
//     {
//         if (container == null)
//         {
//             GameObject containerObj = GameObject.Find("Shop_Container");
//             if (containerObj == null)
//             {
//                 Debug.LogError("Shop_Container not found");
//                 return;
//             }
//             container = containerObj.transform;
//         }

//         if (shopItemTemplate == null)
//         {
//             shopItemTemplate = container.Find("shopItemTemplate");
//             if (shopItemTemplate == null)
//             {
//                 Debug.LogError("shopItemTemplate not found in Shop_Container");
//                 return;
//             }
//         }

//         // If no CanvasGroup is assigned, try to get or add one
//         if (shopCanvasGroup == null && shopPanel != null)
//         {
//             shopCanvasGroup = shopPanel.GetComponent<CanvasGroup>();
//             if (shopCanvasGroup == null)
//             {
//                 shopCanvasGroup = shopPanel.AddComponent<CanvasGroup>();
//             }
//         }

//         shopItemTemplate.gameObject.SetActive(false);
//         HideShop();
//     }

//     // Existing methods remain the same...

//     public void ShowShop()
//     {
//         if (shopCanvasGroup != null)
//         {
//             shopCanvasGroup.alpha = 1f;
//             shopCanvasGroup.interactable = true;
//             shopCanvasGroup.blocksRaycasts = true;
//         }
//     }

//     public void HideShop()
//     {
//         if (shopCanvasGroup != null)
//         {
//             shopCanvasGroup.alpha = 0f;
//             shopCanvasGroup.interactable = false;
//             shopCanvasGroup.blocksRaycasts = false;
//         }
//     }

//     public void ToggleShop()
//     {
//         if (shopCanvasGroup.alpha == 0f)
//         {
//             ShowShop();
//         }
//         else
//         {
//             HideShop();
//         }
//     }
// }