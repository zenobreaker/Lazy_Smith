using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    [SerializeField] GameObject go_BaseUI = null;
    [SerializeField] Image img_ItemImage = null;
    [SerializeField] Text txt_ItemName = null;
    [SerializeField] Button btn_Confirm = null;
    
    Item currentItem;

    // UI에 등재된 내용 리셋 
    void ResetInfo()
    {
        currentItem = null;
        img_ItemImage.sprite = null;
        txt_ItemName.text = "";
    }

    public void CofirmClear()
    {
        SoundManager.instance.PlaySE("ButtonClick");
        ResetInfo();
        go_BaseUI.SetActive(false);
        GameManager.instance.ReturnLobby();
    }

    public void SettingItem(string p_ID)
    {
        Debug.Log("아이템 아이디 :" + p_ID);
        Item t_item = ItemDatabase.instance.GetWeaponItemByID(p_ID);

        if (t_item != null)
        {
            currentItem = t_item;
            img_ItemImage.sprite = t_item.itemImage;
            txt_ItemName.text = t_item.itemName;
        }
        
    }


    public void OpenUI()
    {
        go_BaseUI.SetActive(true);
    }
}
