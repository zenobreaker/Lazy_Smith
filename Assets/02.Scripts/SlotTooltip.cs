using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotTooltip : MonoBehaviour
{
    [SerializeField] protected GameObject go_Base = null;

    [SerializeField] protected Image img_item = null;
    [SerializeField] protected Text txt_ItemName = null;
    [SerializeField] protected Text txt_ItemAbility = null;
    [SerializeField] protected Text txt_ItemDesc = null;
    
    [SerializeField] protected Text txt_ItemHowtoUsed = null;
  
    [SerializeField] Button btn_Equip = null;
    [SerializeField] Button btn_Change = null;
    [SerializeField] Button btn_TakeOff = null;
   

    //[SerializeField] ItemEffectDatabase theDatabase = null;
 //   [SerializeField] EnchantManual theEnchant = null;

    public Item selectedItem;
   // public InvenSlot selectedSlot;
    protected int itemCount;

    // 아이템의 이미지를 보여주는 메소드
    public void ShowToolTip(Item p_item)
    {
        selectedItem = p_item;

        if (!go_Base.activeSelf)
            go_Base.SetActive(true);

        img_item.sprite = p_item.itemImage;
        txt_ItemName.text =  " " + p_item.itemName;
      
        
    }


    public void ShowToolTip(Item p_item, int _count = 1)
    {
        if (selectedItem != null)
            selectedItem = null;

        selectedItem = p_item;
        itemCount = _count; 

        if (!go_Base.activeSelf)
         //   LobbyManager.MyInstance.OpenClose(go_Base);

        img_item.sprite = p_item.itemImage;

    }


    //public void ShowToolTip(InvenSlot _invenSlot)
    //{
    //    selectedItem = _invenSlot.GetItem();
    //    selectedSlot = _invenSlot;

    //    if (!go_Base.activeSelf)
    //        LobbyManager.MyInstance.OpenClose(go_Base);

    //    img_item.sprite = _invenSlot.GetItem().itemImage;
    //    txt_ItemName.text = "+" + _invenSlot.GetItem().itemEnchantRank + " " + _invenSlot.GetItem().itemName;
    //    WritingItemAbility(_invenSlot);
    //    txt_ItemDesc.text = _invenSlot.GetItem().itemDesc;
    //}

    //public void WritingItemAbility(InvenSlot _invenSlot)
    //{
    //    Item _item = _invenSlot.GetItem();

    //    if(_item.itemType == Item.ItemType.Equipment)
    //    {
    //        if(_item.equipType == Item.EquipType.Weapon)
    //            txt_ItemAbility.text = "공격력 + "+ _invenSlot.GetItem().itemMainAbility.ToString();
    //        if (_item.equipType == Item.EquipType.Armor)
    //            txt_ItemAbility.text = "방어력 + " + _invenSlot.GetItem().itemMainAbility.ToString();
    //        if (_item.equipType == Item.EquipType.Accessory)
    //            txt_ItemAbility.text = "특정 옵션 + " + _invenSlot.GetItem().itemMainAbility.ToString();
    //    }
    //}

    public void HideToolTip()
    {
        if (go_Base.activeSelf)
        {
            go_Base.SetActive(false);
            this.selectedItem = null;
        }
    }

 
    // 아이템 변경 
    public void ChangeItem()
    {
    //    EquipMenu.instance.TabSetting((int)selectedItem.itemType);
        HideToolTip();
     //   LobbyManager.MyInstance.OpenClose(Inventory.instance.go_InventoryBase);
       

    }

    // 아이템 해제
    public void TakeOffItem()
    {
    //    InventoryManager.instance.TakeOffEquipment(selectedSlot.GetItem());
    }

    // 호출형태에 따른 버튼 변경
    public void ChangeBtn(int p_num)
    {
        if(p_num == 1)
        {
            btn_Equip.gameObject.SetActive(true);
            btn_Change.gameObject.SetActive(false);
            btn_TakeOff.gameObject.SetActive(false);
        }
        else if(p_num == 2)
        {
            btn_Equip.gameObject.SetActive(false);
            btn_Change.gameObject.SetActive(true);
            btn_TakeOff.gameObject.SetActive(false);
        }
        else if(p_num == 3)
        {
            btn_Equip.gameObject.SetActive(false);
            btn_Change.gameObject.SetActive(false);
            btn_TakeOff.gameObject.SetActive(true);
        }
    }

    public void EnchantBtn()
    {
 //       LobbyManager.MyInstance.OpenClose(theEnchant.go_BaseUI);
      //  theEnchant.SetEquip(selectedItem);
        //HideToolTip();
    }


    public void CancelUI()
    {
        selectedItem = null;
   //     selectedSlot = null;
        HideToolTip();
    }
}
