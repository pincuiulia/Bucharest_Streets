using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //ITEM DATA//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFUll;
    public string itemDescription; // Corrected naming for consistency
    public Sprite emptySprite;

    [SerializeField]
    private int maxNumberOfItems;
  
    // ITEM SLOT //

    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    // ITEM DESCRIPTION SLOT//

    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected; // Fixed typo
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //verificam daca slotul este full//
        if(isFUll)
              return quantity;

        this.itemName = itemName;
       
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription; // Corrected naming for consistency
        this.quantity += quantity;
        if(this.quantity>=maxNumberOfItems)
        {
            quantityText.text = quantity.ToString();
            quantityText.enabled = true;
            isFUll = true;
            if (isFUll == true)
            {
                quantityText.text = maxNumberOfItems.ToString();
            }
    
        

        //return the LEFTOVERS
        int extraItems=this.quantity - maxNumberOfItems;
        this.quantity=maxNumberOfItems;
        return extraItems;
        }

        //Update QUANTITY TEXT

        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
  
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        if(thisItemSelected)
        { 
            bool usable = inventoryManager.UseItem(itemName);
            if(usable)
            {
            this.quantity-=1;
            quantityText.text=this.quantity.ToString();
            if(this.quantity <=0)
                   EmptySlot();
            }

        }

        else {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true; 
        ItemDescriptionNameText.text=itemName;
        ItemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite=itemSprite;
        if(itemDescriptionImage.sprite == null)
               itemDescriptionImage.sprite = emptySprite;
        }
       
    }

    private void EmptySlot()
    {
        quantityText.enabled=false;
        itemImage.sprite = emptySprite;
        ItemDescriptionNameText.text = "";
        ItemDescriptionText.text = ""; 
        itemDescriptionImage.sprite = emptySprite;
    }

    public void OnRightClick()
    {
       

    }
}
