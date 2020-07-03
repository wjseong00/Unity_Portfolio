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
        if (Imotal.instance.isKeyBorad == true)
        {
            main.sprite = key;
            Canvas.SetActive(false);
            
        }
        else
        {
            main.sprite = game;
            Canvas.SetActive(true);
            
        }
        
    }
}
