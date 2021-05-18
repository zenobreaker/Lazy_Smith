using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenAlert : MonoBehaviour
{
    [SerializeField] GameObject go_BaseUI = null;
    [SerializeField] Image img_ItemImage = null;
    [SerializeField] Text txt_ItemCount = null;
    [SerializeField] Text txt_ItemName = null;
    [SerializeField] Text txt_Explain = null;

    [SerializeField] Button btn_Confirm = null;
 //   [SerializeField] Button btn_Cancel = null;

    Item currentItem;
    private int currentCount;
    private int maxCount; 
    public void SettingUI(Item p_Item)
    {
        currentItem = p_Item;
        currentCount = maxCount = currentItem.itemCount;
        img_ItemImage.sprite = p_Item.itemIcon;
        txt_ItemCount.text = p_Item.itemCount.ToString();
        txt_ItemName.text = p_Item.itemName;
       

    }

    public void OpenAlertUI(Item p_Item)
    {
        go_BaseUI.SetActive(true);
        SettingUI(p_Item);
        if (p_Item.itemType == ItemType.WEAPON && p_Item.itemCount > 0)
        {
            Debug.Log(p_Item.itemType);
            txt_Explain.gameObject.SetActive(true);
            btn_Confirm.interactable = true;
        }
        else if (p_Item.itemType == ItemType.MATERIAL)
        {
            Debug.Log(p_Item.itemType);
            btn_Confirm.interactable = false;
            txt_Explain.gameObject.SetActive(false);
        }


    }

    public void Confirm()
    {
        go_BaseUI.SetActive(false);
        Inventory.instance.SellWeaponItem(currentItem, currentCount);
    }

    public void Cancel()
    {
        go_BaseUI.SetActive(false);
    }

    public void IncreaseCount()
    {
        if (currentCount < maxCount && maxCount > 0)
        {
            currentCount++;
            txt_ItemCount.text = currentCount.ToString();
        }
    }

    public void DecreaseCount()
    {
        if (currentCount > 0)
        {
            currentCount--;
            txt_ItemCount.text = currentCount.ToString();
        }
    }
}
