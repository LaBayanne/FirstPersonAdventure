using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineData : ItemData
{
    
    public override bool Use(PlayerStats playerStats)
    {
        Debug.Log("Wine bottle drunk. Hips !");

        return true;
    }
}
