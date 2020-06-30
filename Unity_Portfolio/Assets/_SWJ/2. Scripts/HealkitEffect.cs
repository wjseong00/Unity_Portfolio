using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Etc/Healkit")]
public class HealkitEffect : ItemEffect
{
    //public int healingPoint = 0;


    public override bool ExecuteRole()
    {
        GameObject.Find("Player").GetComponent<PlayerMoney>().isItem = true;
        //GameObject.Find("Player").GetComponent<PlayerDamage>().hp += healingPoint;
        return true;
    }
}