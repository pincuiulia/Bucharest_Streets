using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        ArmorNone,
        Armor,
        HelmetNone,
        Helmet,
        Weapons,
        HealthPotion
    }


    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.ArmorNone: return 0;
            case ItemType.Armor: return 200;
            case ItemType.HelmetNone: return 0;
            case ItemType.Helmet: return 95;
            case ItemType.Weapons: return 220;
            case ItemType.HealthPotion: return 60;

        }
    }


    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.ArmorNone: return null;
            case ItemType.Armor: return Resources.Load<Sprite>("Assets/Shopping Map/Grafica/Shopping Icons/Armor.png");
            case ItemType.HelmetNone: return null;
            case ItemType.Helmet: return Resources.Load<Sprite>("Assets/Shopping Map/Grafica/Shopping Icons/1px All.png");
            case ItemType.Weapons: return Resources.Load<Sprite>("Assets / Shopping Map / Grafica / Shopping Icons / Weapon Spritesheet.png");
            case ItemType.HealthPotion: return Resources.Load<Sprite>("Assets / Shopping Map / Grafica / Shopping Icons / potion bottle still.png");
        }
    }
}
