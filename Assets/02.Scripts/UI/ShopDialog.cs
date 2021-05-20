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
    [SerializeField] Text txt_ItemCount = null;

    [SerializeField] GameObject go_Warning = null;
    [SerializeField] Text txt_Alert = null;

    [SerializeField] Text txt_Explain = null; 

    public Item selectedItem;
    private ShopSlot selectedSlot;
    protected int itemCount;
    public int maxCount = 99;

    public Color successColor;
    public Color FailureColor;

    [SerializeField] InteractionController theIC = null;

    public void ShowToolTip(ShopSlot _shopSlot, int _count = 1)
    {
        selectedSlot = _shopSlot;
        selectedItem = _shopSlot.MyItem;
        itemCount = _count;
        

        if (!go_Base.activeSelf)
            go_Base.SetActive(true);

        img_item.sprite = _shopSlot.MyItem.itemImage;
        txt_ItemName.text = _shopSlot.MyItem.itemName;
        txt_ItemCount.text = itemCount.ToString();
        txt_Explain.text = "위 아이템을 구매하시겠습니까? " + "현재 가격 : " + selectedItem.itemValue * itemCount + "코인";
    }

    public void CancelUI()
    {
        selectedItem = null;
        go_Base.SetActive(false);
     //   LobbyManager.MyInstance.Cancel(go_Base);
    }

    public void BuyItem()
    {


        if (GameManager.money >= selectedItem.itemValue * itemCount)
        {
            //Item buyedItem = new Item(selectedItem);
            GameManager.money -= selectedItem.itemValue * itemCount;
            UIManager.instance.SetMoney(GameManager.money);
            Inventory.instance.IncreaseItemCount(selectedItem, itemCount);
            Debug.Log("구입 완료" + selectedItem.itemName);

            txt_Alert.color = successColor;
            txt_Alert.text = "구입이 성공적으로 이루어졌습니다.";
            StartCoroutine(FadeInOut());

            if (selectedItem.itemID.Equals("006"))
                theIC.ShowDialogue(6);

            CancelUI();

        }
        else
            AppearFailedBuyItem();
    }

    public void IncreaseCount()
    {
        if (itemCount < maxCount)
        {
            itemCount++;
            txt_ItemCount.text = itemCount.ToString();
            txt_Explain.text = "위 아이템을 구매하시겠습니까? " + "현재 가격 : " + selectedItem.itemValue * itemCount + "코인";
        }
    }

    public void DecreaseCount()
    {
        if(itemCount > 1 )
        {
            itemCount--;
            txt_ItemCount.text = itemCount.ToString();
            txt_Explain.text = "위 아이템을 구매하시겠습니까? " + "현재 가격 : " + selectedItem.itemValue * itemCount + "코인";
        }
    }

    public void AppearFailedBuyItem()
    {
        txt_Alert.color = FailureColor;
        txt_Alert.text = "잔액이 부족합니다.";

        StartCoroutine(FadeInOut());
    }


    IEnumerator FadeInOut()
    {
        go_Warning.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        go_Warning.SetActive(false);
    }

}
