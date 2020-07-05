using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FieldItems : MonoBehaviour
{
    Rigidbody rig;
    public Item item;
    public Image image;
    //AudioSource sound;
    private void Start()
    {
        //sound = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody>();
        rig.AddForce(Vector3.up * 250f);
    }
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.itemCost = _item.itemCost;
        item.efts = _item.efts;

        image.sprite = _item.itemImage;
    }
    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        
        Destroy(gameObject);
    }
        
    
}
