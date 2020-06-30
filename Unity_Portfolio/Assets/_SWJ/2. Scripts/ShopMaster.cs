using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMaster : MonoBehaviour
{
    public GameObject speak;

    private void Update()
    {
        if(speak.activeSelf==true)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                if(GameObject.Find("Inventory Canvas").GetComponent<InventoryUI>().isStoreActive==false)
                {
                    GameObject.Find("Inventory Canvas").GetComponent<InventoryUI>().OpenStore();

                }

            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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
