using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShow : MonoBehaviour
{
   

    void Start()
    {
 
    }


    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            Inventory.instance.bossShow = true;
           GetComponent<FadeIn>().enabled = true;
            
            
            
            
        }
    }
}
