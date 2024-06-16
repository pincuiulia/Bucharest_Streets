using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    public ItemSO[] itemSOs;
    void Start()
    {   
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && menuActivated)
        {
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if(Input.GetKeyDown(KeyCode.T) && !menuActivated)
        {
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public bool UseItem(string itemName)
    {
        for(int i = 0; i<itemSOs.Length; i++)
        {
            if(itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                return usable;
            }
           
        }
         return false;
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
       for (int i=0 ; i<itemSlot.Length; i++)
       {
        if(itemSlot[i].isFUll == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity==0)
        {
            int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
            if(leftOverItems>0)
            
                 leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
            return leftOverItems;
        }
        
       }
       return quantity;
    }
    public void DeselectAllSlots(){
        for(int i=0; i<itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

}

