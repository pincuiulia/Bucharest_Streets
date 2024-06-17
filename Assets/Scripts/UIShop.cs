using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Item;

public class UIShop : MonoBehaviour
{
    private Transform Container;
    private Transform ShopItemTemplate;


    private void Awake()
    {
        Container = transform.Find("Container");
        ShopItemTemplate = Container.Find("ShopItemTemplate");
        ShopItemTemplate.gameObject.SetActive(false);

    }

    private void Start()
    {
        CreateItemButton(Item.GetSprite(Item.ItemType.Armor), "Armor", Item.GetCost(Item.ItemType.Armor), 0);
    }

    private void CreateItemButton (Sprite itemSprite, string itemName, int itemCost, int positionIdx)
    {
        Transform shopItemTransform = Instantiate(ShopItemTemplate, Container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 90f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIdx);

        shopItemTransform.Find("imageName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("price").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemTransform.Find("imageIcon").GetComponent<Image>().sprite = itemSprite;

        // TREBUIE IMPLEMENTATA
        // shopItemTransform.GetComponent<Button>().onClick.AddListener(() => { TryBuyItem(itemType); });
    }

    private void TryBuyItem(Item.ItemType itemType)
    {
        int itemCost = Item.GetCost(itemType);
        // Verificam daca jucatorul are destui bani (aici trebuie adaugat un sistem de valute)
            
        // Ex: if (playerCurrency >= itemCost) {
        // playerCurrency -= itemCost;
        //inventory.AddItem(itemType);
        Debug.Log("Bought: " + itemType);
        // }
        // else {
        // Debug.Log("Not enough money!");
        // }
    }
}
