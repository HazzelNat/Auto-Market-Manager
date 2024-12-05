using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        Eggs,
        Bottled_Water,
        Bread,
        Rice,
        Snacks,
        Instant_Noodles
        //Add more later
    }

    public static int GetCost(ItemType itemtype){
        switch (itemtype) {
            default:
            case ItemType.Eggs:                 return 10;
            case ItemType.Bottled_Water:        return 5;
            case ItemType.Bread:                return 20;
            case ItemType.Rice:                 return 15;
            case ItemType.Snacks:               return 5;
            case ItemType.Instant_Noodles:      return 5;
        }
    }

public static int CustomerCost(ItemType itemtype){
        switch (itemtype) {
            default:
            case ItemType.Eggs:                 return 15;
            case ItemType.Bottled_Water:        return 10;
            case ItemType.Bread:                return 25;
            case ItemType.Rice:                 return 20;
            case ItemType.Snacks:               return 10;
            case ItemType.Instant_Noodles:      return 10;
        }
    }

    public static Sprite GetSprite(ItemType itemtype){
        switch (itemtype) {
            default:
            case ItemType.Eggs:                 return GameAssetsHandler.instance.Egg_S;
            case ItemType.Bottled_Water:        return GameAssetsHandler.instance.Bottled_Water_S;
            case ItemType.Bread:                return GameAssetsHandler.instance.Bread_S;
            case ItemType.Rice:                 return GameAssetsHandler.instance.Rice_S;
            case ItemType.Snacks:               return GameAssetsHandler.instance.Snacks_S;
            case ItemType.Instant_Noodles:      return GameAssetsHandler.instance.Instant_Noodles_S;
        }
    }
}