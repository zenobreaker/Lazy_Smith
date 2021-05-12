using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialog : MonoBehaviour
{
    [SerializeField] protected GameObject go_Base = null;

    [SerializeField] protected Image img_item = null;
    [SerializeField] protected Text txt_ItemName = null;
    [SerializeField] protected Text txt_ItemDesc = null;

    public Item selectedItem;
    private ShopSlot selectedSlot;
    protected int itemCount;

    [SerializeField] InteractionController theIC = null;

    public void ShowToolTip(ShopSlot _shopSlot, int _count = 1)
    {
        selectedSlot = _shopSlot;
        selectedItem = _shopSlot.MyItem;
        itemCount = _count;

        if (!go_Base.activeSelf)
            go_Base.SetActive(true);

        img_item.sprite = _shopSlot.MyItem.itemImage;
        txt_ItemName.text = "+" + " " + _shopSlot.MyItem.itemName;
           
        
    }

    public void CancelUI()
    {
        selectedItem = null;
        go_Base.SetActive(false);
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

        if (GameManager.money >= selectedItem.itemValue)
        {
            //Item buyedItem = new Item(selectedItem);
            GameManager.money -= selectedItem.itemValue;
            UIManager.instance.SetMoney(GameManager.money);
            Inventory.instance.IncreaseItemCount(selectedItem);
            Debug.Log("구입 완료" + selectedItem.itemName);
            if (selectedItem.itemID.Equals("006"))
                theIC.ShowDialogue(6);
            CancelUI();
           
        }
    }
}
