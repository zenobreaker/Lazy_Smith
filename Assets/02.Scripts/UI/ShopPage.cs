using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPage : TabManual
{
    public static ShopPage instance;

    [SerializeField] ShopSlot shopSlot = null;
    [SerializeField] GameObject parentSlotPage = null;    // 슬롯의 부모 오브젝트
    [SerializeField] ShopDialog saleAlertUI = null;   // 구매 의사 UI
    [SerializeField] Scrollbar scrollbar = null;

    private List<ShopSlot> shopSlots = new List<ShopSlot>();

    [Header("일반 아이템 상점 목록")]
    [SerializeField] List<Item> stoneItems = new List<Item>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SetList();
        SettingShopSlot(stoneItems);
        TabSetting(0);
        //Debug.Log("탭세팅");
        //TabSlotOpen(selectedTab);
    }

    public void BuyItem()
    {
        saleAlertUI.BuyItem();
    }

    public override void OpenUI()
    {
        SoundManager.instance.PlaySE("ButtonClick");
        base.OpenUI(); TabSetting(0);
    }


    public override void HideUI()
    {
        base.HideUI();
        SoundManager.instance.PlaySE("ButtonClick");
    }

    // 구매의사 UI 켜기
    public void OpenSaleUI(ShopSlot _item)
    {
        saleAlertUI.ShowToolTip(_item);
    }

    // 아이템 상점 페이지를 초기화 
    public void ClearPage()
    {
        for (int i = 0; i < shopSlots.Count; i++)
        {
            shopSlots[i].ClearSlot();
        }
    }

    void SetList()
    {
        stoneItems = ItemDatabase.instance.GetStones();
    }

    // 정해진 아이템 리스트만큼 슬롯프리팹 생성 후 리스트 정렬 
    public void SettingShopSlot(List<Item> _items)
    {

        if (_items.Count != shopSlots.Count)
        {
            if (_items.Count > shopSlots.Count)
            {
                int t_start = shopSlots.Count > 0 ? shopSlots.Count - 1 : 0;

                for (int i = t_start; i < _items.Count; i++)
                {
                    ShopSlot clone = Instantiate(shopSlot, Vector3.zero, Quaternion.identity);
                    clone.transform.SetParent(parentSlotPage.transform, false);
                    shopSlots.Add(clone);
                }
            }
            else if (_items.Count < shopSlots.Count)
            {
                int t_start = _items.Count > 0 ? _items.Count - 1 : 0;

                for (int i = t_start; i < shopSlots.Count; i++)
                {
                    shopSlots[i].ClearSlot();
                    shopSlots[i].gameObject.SetActive(false);
                }
            }

        }

        for (int i = 0; i < _items.Count; i++)
        {
            shopSlots[i].gameObject.SetActive(true);
        }


    }

    // 초기 상점으로 복구 
    public void InitShopPage()
    {
        SettingShopSlot(stoneItems);
    }

    // 슬롯에 아이템 넣기 
    public void SettingItems(List<Item> _items)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            shopSlots[i].ClearSlot();
            //shopSlots[i].SetSize(parentSlotPage.GetComponent<GridLayoutGroup>().cellSize);
            shopSlots[i].AddItem(_items[i]);
            shopSlots[i].SetItemSale();
        }
    }

    public void TabSetting(int _tabNumber)
    {
        SoundManager.instance.PlaySE("ButtonClick");
        ClearPage();
        switch (_tabNumber)
        {

            case 0:
                SettingShopSlot(stoneItems);
                SettingItems(stoneItems);
                TabSlotOpen(parentSlotPage);

                break;
            case 1:
                SettingShopSlot(stoneItems);
                SettingItems(stoneItems);
                TabSlotOpen(parentSlotPage);
                scrollbar.value = 0;
                break;
        }


        scrollbar.value = 0;
        Debug.Log("스크롤 : " + scrollbar.value);
    }

}