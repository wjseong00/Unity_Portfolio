﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/Health")]
public class ItemHealingEffect : ItemEffect
{
    public int healingPoint = 0;
    
    public override bool ExecuteRole()
    {
        GameObject.Find("Player").GetComponent<PlayerDamage>().hp += healingPoint;
        return true;
    }
}
