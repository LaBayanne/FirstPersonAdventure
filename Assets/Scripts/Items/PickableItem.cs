using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField]
    protected ItemGroup m_itemGroup;

    public ItemGroup GetItemGroup() { return m_itemGroup; }
}
