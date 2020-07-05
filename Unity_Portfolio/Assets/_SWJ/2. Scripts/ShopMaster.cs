﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMaster : MonoBehaviour
{
    public GameObject speak;
    public GameObject Ui;
    public GameObject KUi;
    public GameObject hpBar;
    private void Update()
    {
        if(Imotal.instance.isKeyBorad==true)
        {
            if (speak.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (GameObject.Find("Inventory Canvas").GetComponent<InventoryUI>().isStoreActive == false)
                    {
                        if (Imotal.instance.isKeyBorad == true)
                        {
                            KUi.SetActive(false);
                        }
                        hpBar.SetActive(false);
                        Time.timeScale = 0;
                        speak.SetActive(false);
                        GameObject.Find("Inventory Canvas").GetComponent<InventoryUI>().OpenStore();

                    }

                }
            }
        }
        
    }
    public void openShop()
    {
        if (speak.activeSelf == true)
        {
            
                if (GameObject.Find("Inventory Canvas").GetComponent<InventoryUI>().isStoreActive == false)
                {
                    if (Imotal.instance.isKeyBorad == false)
                    {
                        Ui.SetActive(false);
                    }
                    
                       

                    
                Time.timeScale = 0;
                hpBar.SetActive(false);
                    speak.SetActive(false);
                    GameObject.Find("Inventory Canvas").GetComponent<InventoryUI>().OpenStore();

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
