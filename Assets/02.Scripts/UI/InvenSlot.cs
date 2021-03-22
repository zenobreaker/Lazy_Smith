using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenSlot : Slot
{
    [SerializeField] Text txt_ItemCount; 

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if(item != null)
        {
            // UI 호출 
            Inventory.instance.OpenInvenAlert(this.item);

        }
    }

    public override void AddItem(Item _item, int _count = 0)
    {
        base.AddItem(_item, _count);
        txt_ItemCount.text = itemCount.ToString();
    }

}
