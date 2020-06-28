using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medikit : MonoBehaviour
{
    Rigidbody rig;
    GameObject player;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.AddForce(Vector3.up * 250f);
        player = GameObject.Find("Player");
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            player.GetComponent<PlayerMoney>().isItem = true;
        }
    }
}
