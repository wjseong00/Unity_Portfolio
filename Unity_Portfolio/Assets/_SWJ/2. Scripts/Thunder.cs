using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    Transform player;
    float att = 15;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject,1.5f);
        if (other.tag == "Player")
        {
            
            player.GetComponent<PlayerDamage>().StunPlayer(att);


        }
    }



}
