using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemAbility
{
    public string type;
    public bool isPercent;
    public int power;
}

[System.Serializable]
public class Item
{
    //게임 오브젝트에 붙일 필요가 없는  스크립트 생성 

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC,
        Coin
    }

    public enum ItemRank
    {
        NONE,
        Common,
        Magic,
        Rare,
        Unique,
        Legendary
    }

    public enum EquipType
    {
        None,
        Weapon,
        Armor,
        Accessory,
    }


    public string itemUID;      // 아이템의 고유 ID
    public string itemName;     // 아이템 이름
    [TextArea]
    public string itemDesc;     // 아이템의 설명
    public ItemType itemType;   // 아이템의 유형
    public ItemRank itemRank;   // 아이템의 등급
    public string itemImgID;    // 아이템 이미지 아이디 
    public Sprite itemImage;    // 아이템 이미지
    public string itemSound;    // 아이템 획득 시, 사운드 

    public int itemValue;       // 아이템 가격 
    public EquipType equipType; // 장비 타입
    public int itemEnchantRank; // 아이템 강화 등급
    public bool isEquiped;      // 아이템 장착 여부 
    public bool isSale;         // 아이템 판매 여부 (상점 한)
    // 아이템 능력 순서 
    public const int MAIN = 0, ADD1 = 0, ADD2 = 1, ADD3 = 2;
    public int itemMainAbility; // 아이템 능력 수치 
    public ItemAbility[] itemAbilities = new ItemAbility[3];

    public Item(Item _item)
    {
        itemUID = _item.itemUID;
        itemType = _item.itemType;
        equipType = _item.equipType;
        itemRank = _item.itemRank;
        itemImgID = _item.itemImgID;
        itemName = _item.itemName;
        itemDesc = _item.itemDesc;
        itemValue = _item.itemValue;
        itemEnchantRank = _item.itemEnchantRank;
        isEquiped = _item.isEquiped;
        isSale = _item.isSale;
        itemMainAbility = _item.itemMainAbility;
        itemAbilities[ADD1] = _item.itemAbilities[ADD1];
        itemAbilities[ADD2] = _item.itemAbilities[ADD2];
        itemAbilities[ADD3] = _item.itemAbilities[ADD3];

        itemImage = Resources.Load("ItemImage/" + _item.itemImgID.ToString(), typeof(Sprite)) as Sprite;
    }
    public Item(string _itemUID, ItemType _itemTpye, EquipType _equipType, ItemRank _itemRank, string _itemIMG, string _itemName, string _itemDesc,
         int _mainAbil, int _itemValue = 0, int _echantRank = 0, bool _isEquiped = false, bool _isSale = false)
    {
        itemUID = _itemUID;
        itemType = _itemTpye;
        equipType = _equipType;
        itemRank = _itemRank;
        itemName = _itemName;
        itemDesc = _itemDesc;
        itemImgID = _itemIMG;

        itemMainAbility = _mainAbil;

        itemValue = _itemValue;
        itemEnchantRank = _echantRank;
        isEquiped = _isEquiped;
        isSale = _isSale;


        //itemAbilities[ADD1] = _add1;
        //itemAbilities[ADD2] = _add2;
        //itemAbilities[ADD3] = _add3;

        itemImage = Resources.Load("ItemImage/" + _itemIMG.ToString(), typeof(Sprite)) as Sprite;
    }

    public void SetItemPower(ItemAbility _itemAbility, int _num)
    {
        this.itemAbilities[_num] = _itemAbility;
    }

    public void SetItemPowers(ItemAbility[] _itemAbilities)
    {
        this.itemAbilities = _itemAbilities;
    }
}
