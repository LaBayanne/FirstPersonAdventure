using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents the datas of an item, necessary to manage it in inventories
/// </summary>
public abstract class ItemData : MonoBehaviour
{
    [SerializeField]
    protected string m_name;
    [SerializeField]
    protected Sprite m_icon;

    protected GameObject m_itemInWorld;//Representation of this item in the world

    protected void Awake()
    {
        m_itemInWorld = gameObject;
    }

    public abstract bool Use(PlayerStats playerStats);

    public string GetName() { return m_name; }
}
