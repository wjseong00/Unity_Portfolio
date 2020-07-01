using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopData : MonoBehaviour
{
    public List<Item> stocks = new List<Item>();
    public bool[] soldOuts;
    private void Start()
    {
        stocks.Add(ItemDatabase.instance.itemDB[2]);
        stocks.Add(ItemDatabase.instance.itemDB[3]);
        stocks.Add(ItemDatabase.instance.itemDB[4]);
        stocks.Add(ItemDatabase.instance.itemDB[5]);
        soldOuts = new bool[stocks.Count];
        for (int i = 0; i < soldOuts.Length; i++)
        {
            soldOuts[i] = false;
        }
    }
}
