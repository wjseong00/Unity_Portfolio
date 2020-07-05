using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlast : MonoBehaviour
{
    Transform player;
    
    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<PlayerDamage>().fire(true);
            


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<PlayerDamage>().fire(false);           
        }
    }
}
