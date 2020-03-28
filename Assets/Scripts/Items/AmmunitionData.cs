using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammunition", menuName = "Items/Ammunitions", order = 51)]
public class AmmunitionData : ItemData
{

    public override bool Use(GameObject player)
    {
        Debug.Log("You eated an ammunition. Are you stupid ?");

        return true;
    }
}
