using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBar : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            player.GetComponent<PlayerMove>().isJumpBar = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<PlayerMove>().isJumpBar = false;
        }
    }
}
