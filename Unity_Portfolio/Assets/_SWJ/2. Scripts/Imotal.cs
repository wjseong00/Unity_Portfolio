using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imotal : MonoBehaviour
{
    public static Imotal instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public bool isKeyBorad = false;
    private void Start()
    {
        isKeyBorad = false;
    }
}
