using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
   
    Rigidbody rig;
    GameObject player;
    float explosion = 250f;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.AddForce(Vector3.up * explosion);
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 0.3f);
        for (int i = 0; i < cols.Length; i++)
        {
            if(cols[i].tag=="Player")
            {
                ItemDatabase.instance.money += 100;
                Destroy(gameObject);
            }
        }
    }
    
}
