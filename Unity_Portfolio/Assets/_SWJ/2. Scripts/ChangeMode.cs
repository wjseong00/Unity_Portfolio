using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeMode : MonoBehaviour
{
    
    public Sprite game;
    public Sprite key;
    public GameObject player;
    public GameObject Canvas;
    public GameObject KUiCanvas;
    
    Image main;
   

    
    private void Start()
    {
        main = GetComponent<Image>();
    }
    public void Changed()
    {
        Imotal.instance.isKeyBorad  = !Imotal.instance.isKeyBorad;
        
    }
    public void Update()
    {
        if(GameObject.Find("Inventory Canvas").GetComponent<InventoryUI>().isStoreActive == false && GameObject.Find("Npc").GetComponent<NpcQuestion>().isUse==false)
        {
            if (Imotal.instance.isKeyBorad == true)
            {
                main.sprite = key;
                Canvas.SetActive(false);
                KUiCanvas.SetActive(true);
            }
            else
            {
                main.sprite = game;
                Canvas.SetActive(true);
                KUiCanvas.SetActive(false);

            }
        }
        
        
    }
}
