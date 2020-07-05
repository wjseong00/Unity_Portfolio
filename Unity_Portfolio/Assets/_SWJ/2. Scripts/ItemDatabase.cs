using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    public int money = 0;

    private void Awake()
    {
        instance = this;
    }
    public List<Item>itemDB = new List<Item>();
    public GameObject key;
    public GameObject healkit;

    private void Start()
    {
        money = 0;
        key.GetComponent<FieldItems>().SetItem(itemDB[0]);
        healkit.GetComponent<FieldItems>().SetItem(itemDB[1]);
    }
    //[Space(20)]
    //public GameObject fieldItemPrefab;
    //public Vector3[] pos;
    //private void Start()
    //{
    //    for (int i = 0; i < 6; i++)
    //    {
    //        GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
    //        go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0,2)]);
    //
    //    }
    //}
}
