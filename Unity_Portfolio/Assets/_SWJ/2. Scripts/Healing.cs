using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumables/Potion")]
public class HealingEffect : ItemEffect
{
    public int healingPoint = 0;


    public override bool ExecuteRole()
    {
        
        GameObject.Find("Player").GetComponent<PlayerDamage>().hp += healingPoint;
        return true;
    }
}
