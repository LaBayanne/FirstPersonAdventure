using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New WineData", menuName = "Items/WineData", order = 51)]
public class WineData : ItemData
{
    
    public override bool Use(GameObject player)
    {
        Debug.Log("Wine bottle drunk. Hips !");

        return true;
    }
}
