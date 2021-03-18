using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeSlot : Slot
{
    [SerializeField] Image img_Lock = null;   // 언락시킬 때 보일 이미지
    bool isLock; 

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (item != null && !isLock)
        {
            Debug.Log("클릭함");
            // 아이템 구매 창
            RecipePage.instance.OpenAlertUI(this);
        }
        else if (isLock)
        {
            Debug.Log("잠김" + item.itemID);
            RecipePage.instance.OpenRecipeUI(this);
        }
    }

    public void UnlockedSlot()
    {
        img_Lock.gameObject.SetActive(false);
        isLock = false;
    }

    public void LockedSlot()
    {
        img_Lock.gameObject.SetActive(true);
        isLock = true;
    }

    public void SetItem(Item _item)
    {
        item = _item;
        itemImage.sprite = _item.itemImage;
        this.transform.localScale = Vector3.one;
    }
}
