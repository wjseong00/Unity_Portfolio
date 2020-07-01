using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumables/ManaPotion")]
public class ManaEffect : ItemEffect
{
    public int manaPoint = 0;


    public override bool ExecuteRole()
    {

        GameObject.Find("Player").GetComponent<PlayerAttack>().mp += manaPoint;
        return true;
    }
}
