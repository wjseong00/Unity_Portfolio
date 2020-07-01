using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumables/Scroll")]
public class ScrollEffect : ItemEffect
{
    


    public override bool ExecuteRole()
    {

        GameObject.Find("Player").transform.position = GameObject.Find("Player").GetComponent<PlayerMove>().startPoint.transform.position;
        return true;
    }
}
