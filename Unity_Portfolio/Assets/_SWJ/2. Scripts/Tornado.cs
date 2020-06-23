using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    GameObject player;
    float speed =1.0f;
    Vector3 origin;
    
    void Start()
    {
        player = GameObject.Find("Player");
        origin = transform.position;
        speed = Random.Range(0.2f, 1.1f);
    }

    void Update()
    {
       
        transform.position = Vector3.Slerp(transform.position, player.transform.position, speed * Time.deltaTime);
        
        Destroy(gameObject,7f);
        
    }
}
