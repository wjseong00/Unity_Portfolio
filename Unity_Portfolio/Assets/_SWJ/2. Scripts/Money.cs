using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Money : MonoBehaviour
{
    Text money;
    private void Start()
    {
        money = GetComponent<Text>();
    }
    private void Update()
    {
        money.text ="X " + ItemDatabase.instance.money.ToString();
    }
}
