using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    Inventory inven;
    public GameObject inventoryPanel;
    bool activeInventory = false;
    public Slot[] slots;
    public Transform slotHolder;
    public ShopSlot[] shopSlots;
    public Transform shopHolder;
    public GameObject Ui;
    public GameObject KUi;

    GameObject player;
    GameObject cameraRig;
    public GameObject hpBar;
    public GameObject speak;
    private void Start()
    {
        player = GameObject.Find("Player");
        cameraRig = GameObject.Find("CameraRig");
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        shopSlots = shopHolder.GetComponentsInChildren<ShopSlot>();
        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].Init(this);
            shopSlots[i].slotnum = i;
        }
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        RedrawSlotUI();
        inventoryPanel.SetActive(activeInventory);
        closeShop.onClick.AddListener(DeActiveShop);
    }

    private void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();

        }
        for (int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    private void SlotChange(int val)
    {
        
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    private void Update()
    {
        if(Imotal.instance.isKeyBorad==true)
        {
            if (Input.GetKeyDown(KeyCode.I) && !isStoreActive)
            {
                Cursor.visible = !activeInventory;
                activeInventory = !activeInventory;
                inventoryPanel.SetActive(activeInventory);
            }
        }
        
    }
    public void openInven()
    {
        if (!isStoreActive)
        {
            
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }
    

    
    public GameObject shop;
    public Button closeShop;
    public bool isStoreActive;
    public ShopData shopData;

    public void OpenStore()
    {
        ActiveShop(true);
        shopData = GameObject.Find("ShopMaster").GetComponent<ShopData>();
        for (int i = 0; i < shopData.stocks.Count; i++)
        {
            shopSlots[i].item = shopData.stocks[i];
            shopSlots[i].UpdateSlotUI();
        }
    }
    public void Buy(int num)
    {
        shopData.soldOuts[num] = true;
    }
    public void ActiveShop(bool isOpen)
    {
        if(!activeInventory)
        {
            isStoreActive = isOpen;
            shop.SetActive(isOpen);
            inventoryPanel.SetActive(isOpen);
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].isShopMode = isOpen;
            }
        }
        
    }
    public void DeActiveShop()
    {
        if(Imotal.instance.isKeyBorad==false)
        {
            Ui.SetActive(true);
        }
        else
        {
            Cursor.visible = false;
            KUi.SetActive(true);
        }
        Time.timeScale = 1;
        
        speak.SetActive(true);
        hpBar.SetActive(true);
        player.GetComponent<PlayerMove>().enabled = true;
        player.GetComponent<PlayerAttack>().enabled = true;
        cameraRig.GetComponent<FollowCam>().enabled = true;
        ActiveShop(false);
        shopData = null;
        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].RemoveSlot();
        }
    }
    public void SellBtn()
    {
        for (int i = slots.Length; i >0 ; i--)
        {
            slots[i - 1].SellItem();
        }
    }
}
