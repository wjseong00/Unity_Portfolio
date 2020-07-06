using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePort : MonoBehaviour
{
    public GameObject isEnemy;
    public GameObject player;
    public GameObject otherTelePort;
    int enemyCount = 0;
    bool isTelePort = false;
    bool ready = false;
    
    private void OnEnable()
    {
        
    }

    void Update()
    {
        Collider[] cols = Physics.OverlapSphere(isEnemy.transform.position, 20f);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == "Enemy")
            {
                enemyCount++;
            }
            

            
        }
        if (enemyCount != 0)
        {
            enemyCount = 0;
        }
        else
        {
            isTelePort = true;
        }
        if (isTelePort && ready)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                player.transform.position = otherTelePort.transform.position;
                isTelePort = false;
            }
           
        }
        
    }
    public void onTelePort()
    {
        if (isTelePort && ready)
        {
            
            player.transform.position = otherTelePort.transform.position;
            isTelePort = false;
            

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            ready = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ready = false;
        }
    }
}
