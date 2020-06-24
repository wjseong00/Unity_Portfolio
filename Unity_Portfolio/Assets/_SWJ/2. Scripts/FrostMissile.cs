using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostMissile : MonoBehaviour
{
    GameObject player;
    float speed =2.0f;
    Vector3 origin;
    
    void Start()
    {
        player = GameObject.Find("Player");
        origin = transform.position;
        speed = Random.Range(0.4f, 1.6f);
    }

    void Update()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        dir.y = 0;
        transform.Translate(dir * speed * Time.deltaTime);
        //transform.position = Vector3.Slerp(transform.position, player.transform.position, speed * Time.deltaTime);
        
        Destroy(gameObject,13f);
        
    }
}
