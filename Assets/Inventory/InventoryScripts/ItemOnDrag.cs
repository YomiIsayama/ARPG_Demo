using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform orginalParent;
    public Inventory itemBag;
    private int currentItemID;

    public void OnBeginDrag(PointerEventData eventData)
    {
        orginalParent = this.transform.parent;
        currentItemID = orginalParent.GetComponent<Slot>().slotID;
        this.transform.SetParent(transform.parent.parent);
        this.transform.position = eventData.position;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null) // pointerCurrentRaycast is  UI 
        {

            if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);// in  slot
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;// same in slot

                var temp = itemBag.itemList[currentItemID];
                itemBag.itemList[currentItemID] = itemBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                itemBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(orginalParent);// item to slot
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = orginalParent.position;// item to slot
                this.GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }


            if(eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

                itemBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = itemBag.itemList[currentItemID];
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID)
                {
                    itemBag.itemList[currentItemID] = null;
                }

                this.GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }


        }

        //other state
        transform.SetParent(orginalParent);
        transform.position = orginalParent.position;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

}
