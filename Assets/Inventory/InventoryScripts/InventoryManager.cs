using SingleInstance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InventoryManager : SingleMono<InventoryManager>
{
    //public static InventoryManager instance;
    public Inventory itemBag;
    public GameObject slotGrid;
    //public Slot slotPrefab;
    public GameObject emptySlot;
    private string emptySlotPath = "InventoryPrefab/slot";
    public Text itemInfomation;
    public List<GameObject> slots = new List<GameObject>();


    void Update()
    {
    }
    void Start()
    {
        slotGrid = GameObject.Find("Grid");
        itemInfomation = GameObject.Find("ItemDescription").GetComponent<Text>();
        emptySlot = Resources.Load(emptySlotPath) as GameObject;
        RefreshItem();
        Instance().itemInfomation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        Instance().itemInfomation.text = itemDescription;
    }

    //public static void CreatNewItem(Item item)
    //{
    //    Slot newItem = Instantiate(Instance().slotPrefab, Instance().slotGrid.transform);
    //    newItem.slotItem = item;
    //    newItem.slotImage.sprite = item.itemImage;
    //    newItem.slotNum.text = item.itemHeld.ToString();
    //    Debug.Log(item.itemHeld.ToString());

    //}
    public static void RefreshItem()
    {
        for (int i = 0; i< Instance().slotGrid.transform.childCount; i++)
        {
            if(Instance().slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(Instance().slotGrid.transform.GetChild(i).gameObject);
            Instance().slots.Clear();
        }
        for(int i = 0; i < Instance().itemBag.itemList.Count; i++)
        {
            //CreatNewItem(Instance().itemBag.itemList[i]);
            Instance().slots.Add(Instantiate(Instance().emptySlot,Instance().slotGrid.transform));
            Instance().slots[i].GetComponent<Slot>().SetupSlot(Instance().itemBag.itemList[i]);
        }

    }

}
