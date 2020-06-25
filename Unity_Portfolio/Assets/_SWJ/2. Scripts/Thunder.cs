using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    Transform player;
    int att = 15;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject,2f);
        if (other.tag == "Player")
        {
            
            player.GetComponent<PlayerDamage>().StunPlayer(att);


        }
    }



}
