using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NpcQuestion : MonoBehaviour
{
    public GameObject speak;
    public Transform camPos;
    BoxCollider coll;
    void Start()
    {
        
    }
    private void Update()
    {
        if(speak.activeSelf==true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            speak.SetActive(true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            speak.SetActive(false);
        }
    }
}
