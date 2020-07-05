using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShow : MonoBehaviour
{
    public static BossShow instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public bool bossStart = false;


    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            bossStart = true;
            Inventory.instance.bossShow = true;
           GetComponent<FadeIn>().enabled = true;
            
            
            
            
        }
    }
}
