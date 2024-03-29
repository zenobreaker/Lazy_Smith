﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeAlert : MonoBehaviour
{
    [SerializeField] Image img_ItemImage = null;
    [SerializeField] GameObject go_BaseUI = null;
    [SerializeField] Text txt_ItemName = null;
   // [SerializeField] Button btn_Confirm = null;
   // [SerializeField] Button btn_Cancel = null;


    Item currentItem;
    int currentCount;

    public void SetCount(int p_num)
    {
        currentCount = p_num;
    }

    public void OpneUI(RecipeSlot _recipeSlot)
    {
        SetItem(_recipeSlot);
        go_BaseUI.SetActive(true);
        SoundManager.instance.PlaySE("ButtonClick");
    }

    public void HideUI()
    {
        SoundManager.instance.PlaySE("Cancel");
        go_BaseUI.SetActive(false);
    }

    public void SetItem(RecipeSlot _recipeSlot)
    {
        currentItem = _recipeSlot.MyItem;
        txt_ItemName.text = _recipeSlot.MyItem.itemName;
        img_ItemImage.sprite = _recipeSlot.itemImage.sprite;
    }

    public void ClickConfirm()
    {
        SoundManager.instance.PlaySE("ButtonClick");
        RecipePage.instance.GotoCraft(currentCount);
        HideUI();
    }

    public void Cancel()
    {
        HideUI();
    }

}
