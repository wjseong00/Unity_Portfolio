using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool bossShow = false;
    public bool bossEnd = false;
    public AudioSource sound;
    public static Inventory instance;
    
    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);

        }
    }
    private void Update()
    {
        sound.volume = SoundManager.instance.efVolume;
    }

    void Start()
    {
        
        
        SlotCnt = 20;
    }

    public bool AddItem(Item _item)
    {
        if(items.Count<SlotCnt)
        {
            items.Add(_item);
            if(_item.itemType == ItemType.Etc)
            {
                _item.Use();
            }
            
            if(onChangeItem!=null)
            onChangeItem.Invoke();
            return true;
        }
        return false;
    }

    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FieldItem"))
        {
            sound.Play();
            if (other.gameObject.name.Contains("Coin"))
            {
                ItemDatabase.instance.money += 100;
                Destroy(other.gameObject);
            }
            else
            {
                FieldItems fieldItems = other.GetComponent<FieldItems>();
                if (AddItem(fieldItems.GetItem()))
                {
                    fieldItems.DestroyItem();

                }
            }
            
            
        }
        
    }
}
