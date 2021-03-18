using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialQuest : MonoBehaviour
{
    [SerializeField] Image img_ItemImage = null;
    [SerializeField] Text txt_ItemName = null;
    [SerializeField] Text txt_EachCount = null; 


    public void SettingUI(Item p_item,int p_haveCount, int p_MaxCount)
    {
        img_ItemImage.sprite = p_item.itemImage;
        txt_ItemName.text = p_item.itemName;
        txt_EachCount.text = p_haveCount + "/" + p_MaxCount;
    }
}
