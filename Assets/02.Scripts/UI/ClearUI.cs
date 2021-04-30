using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    [SerializeField] GameObject go_BaseUI = null;
    [SerializeField] Image img_BackGround = null;

    [Header("일반 UI")]
    [SerializeField] GameObject go_NormalUI = null;
    [SerializeField] Image img_ItemImage = null;
    [SerializeField] Text txt_ItemName = null;

    [Header("타임어택 UI")]
    [SerializeField] GameObject go_TAUI = null;
    [SerializeField] Text txt_Score     = null;
    [SerializeField] Text txt_MaxCombo  = null;

    [SerializeField] Button btn_Confirm = null;
    [SerializeField] Animator theAnim = null;

   

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
        img_BackGround.gameObject.SetActive(false);
        UIManager.instance.ReturnLobby();
    }

    public void SettingItem(string p_ID)
    {
        Debug.Log("아이템 아이디 :" + p_ID);
        Item t_item = ItemDatabase.instance.GetWeaponItemByID(p_ID);

        if (t_item != null)
        {
            currentItem = t_item;
            img_ItemImage.sprite = t_item.itemIcon;
            txt_ItemName.text = t_item.itemName;
            Inventory.instance.IncreaseItemCount(currentItem);
        }
        
    }


    public void OpenUI()
    {
        img_BackGround.gameObject.SetActive(true);
        go_NormalUI.SetActive(true);
        go_TAUI.SetActive(false);
        go_BaseUI.SetActive(true);
        theAnim.SetTrigger("Appear");
    }

    public void OpenFailureUI()
    {

        txt_ItemName.text = "제작 실패!";

        img_BackGround.gameObject.SetActive(true);
        go_NormalUI.SetActive(true);
        go_TAUI.SetActive(false);

        go_BaseUI.SetActive(true);
        theAnim.SetTrigger("Appear");
    }

    public void OpenTimeAttackUI()
    {
        SettingText();

        go_NormalUI.SetActive(false);
        go_TAUI.SetActive(true);

        go_BaseUI.SetActive(true);
        theAnim.SetTrigger("Appear");
    }

    public void SettingText()
    {
        ComboManager theCombo = FindObjectOfType<ComboManager>();

        txt_MaxCombo.text = theCombo.maxCombo.ToString();
        txt_Score.text = theCombo.score.ToString();
    }
}
