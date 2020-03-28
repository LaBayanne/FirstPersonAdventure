using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;

/// <summary>
/// This class represents an inventory, which can be attached to anything in the game.
/// Obviously at the player, but at the enemies too, or at some containers the player can
/// find during his exploration.
/// </summary>
public class Inventory : MonoBehaviour
{
    [SerializeField]
    protected ItemGroup[] m_items;

    [SerializeField]
    protected GameObject m_inventoryUI;
    [SerializeField]
    protected Text m_textInventoryContent;

    [SerializeField]
    protected GameObject m_itemsGO;

    protected bool m_isInventoryOpen = false;


    protected void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            if(m_isInventoryOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    //This method allows to add an item to the inventory.
    //return the number of items we were unable to add.
    public int AddItem(ItemGroup itemGroup)
    {
        Debug.Log(itemGroup.GetItemData().GetName() + " added to inventory.");
        return 0;
    }

    
    //This method allows to remove an item from the inventory.
    //return the number of items we were unable to add.
    public int RemoveItem(ItemGroup itemGroup)
    {
        return 0;
    }


    //Open the inventory and display it at the screen
    public void OpenInventory()
    {
        m_isInventoryOpen = true;
        m_inventoryUI.SetActive(true);

        string textInventoryContent = "";

        foreach(ItemGroup item in m_items)
        {
            if(item == null || item.GetItemData() == null)
            {
                textInventoryContent += "/";
            }
            else
            {
                textInventoryContent += item.GetItemData().GetName() + " (" + item.GetNumber() + ")";
            }
            textInventoryContent += "\n";
        }

        m_textInventoryContent.text = textInventoryContent;
    }

    //Close the inventory
    public void CloseInventory()
    {
        m_isInventoryOpen = false;
        m_inventoryUI.SetActive(false);
    }

}


/// <summary>
/// Represent a group of items, useful for items wich come in multiple copies
/// </summary>
[Serializable]
public class ItemGroup
{
    [SerializeField]
    protected ItemData m_itemData;
    [SerializeField]
    protected int m_number = 1;

    public ItemGroup(ItemData itemData, int number = 1)
    {
        m_itemData = itemData;
        m_number = number;
    }

    //Add a certain number of items to this group
    //Return the number of items we were unable to add cause to the size max of the group
    public int AddItems(int number)
    {
        m_number += number;

        int max = m_itemData.GetMaxNumberPerGroup();

        if (m_number <= max)
        {
            return 0;
        }

        int rest = m_number - max;

        m_number = max;

        return rest;

    }

    //Remove a certain number of items from this group.
    //Return the number of items we were unable to remove cause the current size of the current reach 0
    public int RemoveItems(int number)
    {
        m_number -= number;

        if(m_number <= 0)
        {
            int rest = -m_number;

            m_number = 0;

            return m_number;
        }

        return 0;
    }

    public ItemData GetItemData() { return m_itemData; }
    public int GetNumber() { return m_number; }

}