using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeAlert : MonoBehaviour
{
    [SerializeField] Image img_ItemImage = null;
    [SerializeField] GameObject go_BaseUI = null;
    [SerializeField] Button btn_Confirm = null;
    [SerializeField] Button btn_Cancel = null;


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
    }

    public void HideUI()
    {
        go_BaseUI.SetActive(false);
    }

    public void SetItem(RecipeSlot _recipeSlot)
    {
        currentItem = _recipeSlot.MyItem;
        img_ItemImage.sprite = _recipeSlot.itemImage.sprite;
    }

    public void ClickConfirm()
    {
        RecipePage.instance.GotoCraft(currentCount);
        HideUI();
    }

    public void Cancel()
    {
        HideUI();
    }

}
