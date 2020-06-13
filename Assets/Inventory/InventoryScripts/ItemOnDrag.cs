using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform orginalParent;
    public void OnBeginDrag(PointerEventData eventData)
    {
        orginalParent = this.transform.parent;
        this.transform.SetParent(transform.parent.parent);
        this.transform.position = eventData.position;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);// in  slot
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;// same in slot
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(orginalParent);// item to slot
            eventData.pointerCurrentRaycast.gameObject.transform.parent.position = orginalParent.position;// item to slot
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
        transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
