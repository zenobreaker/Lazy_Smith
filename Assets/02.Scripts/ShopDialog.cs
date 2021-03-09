using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialog : MonoBehaviour
{
    [SerializeField] protected GameObject go_Base = null;

    [SerializeField] protected Image img_item = null;
    [SerializeField] protected Text txt_ItemName = null;
    [SerializeField] protected Text txt_ItemAbility = null;
    [SerializeField] protected Text txt_ItemDesc = null;

    public Item selectedItem;
    private ShopSlot selectedSlot;
    protected int itemCount;

    public void ShowToolTip(ShopSlot _shopSlot, int _count = 1)
    {
        selectedSlot = _shopSlot;
        selectedItem = _shopSlot.MyItem;
        itemCount = _count;

        if (!go_Base.activeSelf)
        //    LobbyManager.MyInstance.OpenClose(go_Base);

        img_item.sprite = _shopSlot.MyItem.itemImage;
        txt_ItemName.text = "+" + _shopSlot.MyItem.itemEnchantRank + " " + _shopSlot.MyItem.itemName;
        //txt_ItemAbility.text = p_item.itemAbility[0].ToString();
        WritingItemAbility();
        txt_ItemDesc.text = _shopSlot.MyItem.itemDesc;
        
    }

    public void WritingItemAbility()
    {

    }
    //{
    //    if (selectedItem.itemType == Item.ItemType.Equipment)
    //    {
    //        if (selectedItem.equipType == Item.EquipType.Weapon)
    //            txt_ItemAbility.text = "공격력 + " + selectedItem.itemMainAbility.ToString();
    //        if (selectedItem.equipType == Item.EquipType.Armor)
    //            txt_ItemAbility.text = "방어력 + " + selectedItem.itemMainAbility.ToString();
    //        if (selectedItem.equipType == Item.EquipType.Accessory)
    // /           txt_ItemAbility.text = ItemEffectDatabase.instance.GetAccesoryItemEffect(selectedItem);
    //    }
    //}

    public void CancelUI()
    {
        selectedItem = null;
     //   LobbyManager.MyInstance.Cancel(go_Base);
    }

    public void BuyItem()
    {
        //if (RLModeController.isRLMode)
        //{
        //    RLModeController.instance.DownBHPoint();
            
        //    if (RLModeController.instance.behaviourPoint > 0)
        //    {
        //        selectedItem.isSale = true;
        //        selectedSlot.SetItemSale();
        //        selectedSlot.GetComponent<Image>().enabled = false;
        //    }
        //    else
        //        return;
        //}

        //if (LobbyManager.coin >= selectedItem.itemValue)
        //{
        //    Item buyedItem = new Item(selectedItem);
        //    InventoryManager.instance.AddItemToInven(buyedItem, itemCount);
        //    LobbyManager.MyInstance.IncreseCoin(-selectedItem.itemValue);
        //    LobbyManager.MyInstance.Cancel(go_Base);
        //    Debug.Log("구입 완료" + buyedItem.itemName);
        //}
    }
}
