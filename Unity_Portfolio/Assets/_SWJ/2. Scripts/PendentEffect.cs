using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Etc/Pendent")]
public class PendentEffect : ItemEffect
{
    public int attackPoint = 0;


    public override bool ExecuteRole()
    {

        GameObject.Find("Player").GetComponent<PlayerAttack>().att += attackPoint;
        return true;
    }
}
