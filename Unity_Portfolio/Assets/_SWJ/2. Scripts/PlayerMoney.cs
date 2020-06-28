using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public int coinCount=0;
    public bool isKey = false;
    public bool isItem = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddCoin(int value)
    {
        coinCount+=value;
    }
    public void MinusCoin()
    {
        coinCount--;
    }
    
    
}
