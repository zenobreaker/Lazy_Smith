using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : TabManual
{
    public static Inventory instance; 

    [SerializeField] InvenSlot invenSlot = null;
    [SerializeField] GameObject parentSlotObject = null;
    [SerializeField] InvenAlert invenAlert = null;

    List<InvenSlot> invenSlots = new List<InvenSlot>();
    List<Item> weaponitems = new List<Item>();
    List<Item> materialItems = new List<Item>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        SetList();
        SettingInvenSlot(weaponitems);
        SettingItems(weaponitems);
        TabSetting(0);
    }


    public override void OpenUI()
    {
        base.OpenUI();
        SettingInvenSlot(weaponitems);
        SettingItems(weaponitems);
        TabSetting(0);
    }

    public override void HideUI()
    {
        base.HideUI();
    }

    public void ClearPage()
    {
        for (int i = 0; i < invenSlots.Count; i++)
        {
            invenSlots[i].ClearSlot();
        }
    }

    void SettingInvenSlot(List<Item> _items)
    {
        if (_items.Count != invenSlots.Count)
        {
            if (_items.Count > invenSlots.Count)
            {
                int t_start = invenSlots.Count > 0 ? invenSlots.Count  : 0;

                for (int i = t_start; i < _items.Count; i++)
                {
                   
                    InvenSlot clone = Instantiate(invenSlot,
                                            Vector3.zero,
                                            Quaternion.identity);
                    clone.transform.SetParent(parentSlotObject.transform, false);
                    clone.name = "InvenSlot" + i;
                    invenSlots.Add(clone);
                }
            }
            else if (_items.Count < invenSlots.Count)
            {
                int t_start = _items.Count > 0 ? _items.Count - 1 : 0;

                for (int i = t_start; i < invenSlots.Count; i++)
                {
                    invenSlots[i].ClearSlot();
                    invenSlots[i].gameObject.SetActive(false);
                }
            }

        }

        for (int i = 0; i < _items.Count; i++)
        {
            invenSlots[i].gameObject.SetActive(true);
        }

    }

    void SettingItems(List<Item> _items)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            invenSlots[i].ClearSlot();
            invenSlots[i].AddItem(_items[i], _items[i].itemCount);
        }
    }

    void SettingItem(Item p_item, int p_idx)
    {
        invenSlots[p_idx].ClearSlot();
        invenSlots[p_idx].AddItem(p_item, p_item.itemCount);
    }

    public void TabSetting(int _tabNumber)
    {
        ClearPage();

        switch (_tabNumber)
        {

            case 0:
                SoundManager.instance.PlaySE("ButtonClick");
                SettingInvenSlot(weaponitems);
                SettingItems(weaponitems);
                TabSlotOpen(parentSlotObject);

                break;
            case 1:
                SoundManager.instance.PlaySE("ButtonClick");
                SettingInvenSlot(materialItems);
                SettingItems(materialItems);
                TabSlotOpen(parentSlotObject);
                break;
        }
    }

    void SetList()
    {
        weaponitems = ItemDatabase.instance.GetWeapons();
      
        materialItems = ItemDatabase.instance.GetStones();
       
    
    }

    public void OpenInvenAlert(Item p_item)
    {
        invenAlert.OpenAlertUI(p_item);
    }


    // 아이템 개수 증가 
    public void IncreaseItemCount(Item p_Item)
    {
        Debug.Log("item 추가 " + p_Item.itemType);
        //p_Item.itemCount += 1;

        switch (p_Item.itemType)
        {
            case ItemType.WEAPON:
                foreach (var item in weaponitems)
                {
                    if (item.Equals(p_Item))
                    {
                        item.itemCount += 1;
                        //SettingItem(p_Item, weaponitems.IndexOf(p_Item));
                        break;
                    }
                }
                break;
            case ItemType.MATERIAL:
                foreach (var item in materialItems)
                {
                    if (item.Equals(p_Item))
                    {
                        Debug.Log("item 추가 " + p_Item.itemName);
                        item.itemCount += 1;
                        //SettingItem(p_Item, materialItems.IndexOf(p_Item));
                        break;
                    }
                }
                break;
        }
    }

    // 아이템 개수 감소 
    public void DecreaseItemCount(string p_ItemID , int p_Count)
    {
        materialItems.Find(x => x.itemID.Equals(p_ItemID)).itemCount -= p_Count; 
    }

    public void SellWeaponItem(Item p_Item, int p_Count = 1)
    {
        GameManager.money += p_Item.itemValue;
        p_Item.itemCount -= p_Count;    // 얕은 복사를 이용함
        SettingItem(p_Item,weaponitems.IndexOf(p_Item));
        UIManager.instacne.SetMoney(GameManager.money);
        //weaponitems.Find(x => x == p_Item).itemCount -= p_Count;
    }

    // 레시피페이지에서만 작동 
    public int GetMaterialItemCount(Item p_Item)
    {
        foreach (var item in materialItems)
        {
            if(item.Equals(p_Item))
            {
                return item.itemCount;
            }
        }

        return 0;
    }

    // 레시피 매니저에서 호출 
    public int GetMaterialItemByID(string p_ID)
    {
        Item t_Item = materialItems.Find(x => x.itemID.Equals(p_ID));

        if (t_Item != null)
            return t_Item.itemCount;
        else
            return 0;
    }
}
