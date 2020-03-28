using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New FoodData", menuName = "Items/FoodData", order = 51)]
public class FoodData : ItemData
{
    public override bool Use(GameObject player)
    {
        Debug.Log("Food eaten. Burps. ");

        return true;
    }
}
