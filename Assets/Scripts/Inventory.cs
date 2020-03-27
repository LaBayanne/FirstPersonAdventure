using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This class represents an inventory, which can be attached to anything in the game.
/// Obviously at the player, but at the enemies too, or at some containers the player can
/// find during his exploration.
/// </summary>
public class Inventory : MonoBehaviour
{
    //We use both a List and a Dictionary cause dictionaries cannot be displayed
    //in unity's inspector.
    [SerializeField]
    protected List<ItemCount> items;
    protected Dictionary<string, ItemCount> m_items;

    [SerializeField]
    protected int m_size;
    protected int m_count;

    protected void Start()
    {
        //Copy the List in the dictionary
        m_items = new Dictionary<string, ItemCount>();

        foreach(ItemCount item in items)
        {
            if(item != null && item.GetData() != null && item.GetCount() > 0)
            {
                m_items.Add(item.GetData().GetName(), item);

                m_count += item.GetCount();
            }

        }

        //resize the inventory if needing
        if(m_count > m_size)
        {
            m_size = m_count;
        }
    }

    protected void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            PrintInventory();
        }
    }

    /// <summary>
    /// This method allows to add an item to the inventory.
    /// return true if we can add the item, else false.
    /// </summary>
    /// <param name="itemData">The item to add</param>
    /// <returns></returns>

    public bool AddItem(ItemData itemData)
    {
        if (m_count >= m_size)
            return false;

        if(m_items.ContainsKey(itemData.GetName()))
        {
            m_items[itemData.GetName()].IncreaseCount();
        }
        else
        {
            m_items.Add(itemData.GetName(), new ItemCount(itemData, 1));
        }

        return true;
    }

    /// <summary>
    /// This method allows to remove an item from the inventory.
    /// return true if the item was removed, else false.
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public bool RemoveItem(ItemData itemData)
    {
        if (m_items.ContainsKey(itemData.GetName()))
        {
            if(m_items[itemData.GetName()].DecreaseCount() == 0)
            {
                m_items.Remove(itemData.GetName());
            }

            return true;
        }

        return false;
    }

    public void PrintInventory()
    {
        foreach (KeyValuePair<string, ItemCount> entry in m_items)
        {
            Debug.Log(entry.Value.GetData().GetName() + " : " + entry.Value.GetCount().ToString());
        }
    }
}

/// <summary>
/// This class represents a certain number of a particular item.
/// </summary>
[Serializable]
public class ItemCount
{
    [SerializeField]
    protected ItemData m_data;
    [SerializeField]
    protected int m_count;

    [SerializeField]
    public ItemCount(ItemData data, int count)
    {
        m_data = data;
        m_count = count;
    }

    public int IncreaseCount()
    {
        return m_count ++;
    }

    public int DecreaseCount()
    {
        return m_count--;
    }
    
    public ItemData GetData() { return m_data; }
    public int GetCount() { return m_count; }
}
