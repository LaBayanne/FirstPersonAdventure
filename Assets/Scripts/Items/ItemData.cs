using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [SerializeField]
    protected string m_name;
    [SerializeField]
    protected Sprite m_icon;
    [SerializeField]
    protected int m_maxNumberPerGroup;
    [SerializeField]
    protected GameObject m_physicalItem;//Representation of this item in the world

    public abstract bool Use(GameObject player);

    public string GetName() { return m_name; }
    public Sprite GetSprite() { return m_icon; }
    public int GetMaxNumberPerGroup() { return m_maxNumberPerGroup; }
    public GameObject GetPhysicalItem() { return m_physicalItem; }

}
