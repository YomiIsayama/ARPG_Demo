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
    public Slot slotPrefab;
    public Text itemInfomation;
    void Update()
    {
    }
    void Start()
    {
        slotGrid = GameObject.Find("Grid");
        itemInfomation = GameObject.Find("ItemDescription").GetComponent<Text>();
    }

    public static void CreatNewItem(Item item)
    {
        Slot newItem = Instantiate(GetIsntance().slotPrefab, GetIsntance().slotGrid.transform);  
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
        Debug.Log(item.itemHeld.ToString());

    }

    //private IEnumerator LoadInventory()
    //{
    //    string uri = @"F:\unity\work\ARPG_Demo\AssetBundles\ui\slot.ab";
    //    UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
    //    yield return request.SendWebRequest();

    //    AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
    //    //other way
    //    //AssetBundle ab =( request.downloadHandler as DownloadHandlerAssetBundle).assetBundle

    //    GameObject gameObj = ab.LoadAsset<GameObject>("slot.ab");
    //    Instantiate(gameObj);
    //    Debug.Log(gameObj.name);
    //    yield return null;
    //}
}
