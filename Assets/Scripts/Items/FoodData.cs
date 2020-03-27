using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodData : ItemData
{
    public override bool Use(PlayerStats playerStats)
    {
        Debug.Log("Food eaten. Burps. ");

        return true;
    }
}
