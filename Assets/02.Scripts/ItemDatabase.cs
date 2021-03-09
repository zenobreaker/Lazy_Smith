using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public TextAsset items;
    public TextAsset subOptions;
    public List<Item> weaponList;
    public List<Item> armorList;
    public List<Item> accessroyList;

    void Start()
    {
        string[] line = items.text.Substring(0, items.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            string itemUID = row[0];
            Item.ItemType itemType = DiscernToItemType(row[1]);
            Item.EquipType equipType = DiscernToEquipType(row[2]);
            Item.ItemRank itemRank = DiscernToItemRank(row[3]);
            string itemImgID = row[4];
            string itemName = row[5];
            string itemDesc = row[6].Replace("\\n","\n");
            int itemValue = int.Parse(row[7]);
            int itemEnch = int.Parse(row[8]);
            bool isEquip = row[9] == "TRUE";
            int itemMainAbil = int.Parse(row[10]);


            if (itemType == Item.ItemType.Equipment)
            {
                Item _item = new Item(itemUID, itemType, equipType, itemRank,
                                    itemImgID, itemName, itemDesc, itemMainAbil, itemValue,
                                    itemEnch, isEquip);

                _item.SetItemPowers(SplitSubItemOption(_item.itemUID));

                switch (equipType)
                {
                    case Item.EquipType.Weapon:
                        weaponList.Add(_item);
                        break;
                    case Item.EquipType.Armor:
                        armorList.Add(_item);
                        break;
                    case Item.EquipType.Accessory:
                        accessroyList.Add(_item);
                        break;
                }
            }
            
        }
    }


    ItemAbility[] SplitSubItemOption(string _uid)
    {
        string[] line = subOptions.text.Substring(0, subOptions.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            string itemUID = row[0];
            
            if(_uid == itemUID)
            {
                ItemAbility[] _itemAbility = new ItemAbility[3];
                
                for (int j = 0; j < _itemAbility.Length; j++)
                {
                    _itemAbility[j].type = row[j * 3 + 1];
                    _itemAbility[j].isPercent = row[j * 3 + 2] == "TRUE";
                    _itemAbility[j].power = int.Parse(row[j * 3 + 3]);
                }

                return _itemAbility;
            }
        }
       
        return null;
    }

    Item.ItemType DiscernToItemType(string _itemType)
    {
        switch (_itemType)
        {
            case "Equipment":
                return Item.ItemType.Equipment;
            case "Used":
                return Item.ItemType.Used;
            case "Ingredient":
                return Item.ItemType.Ingredient;
            case "ETC":
                return Item.ItemType.ETC;
            default:
                return Item.ItemType.Coin;
        }
    }

    Item.EquipType DiscernToEquipType(string _equipType)
    {
        switch (_equipType)
        {
            case "Weapon":
                return Item.EquipType.Weapon;
            case "Armor":
                return Item.EquipType.Armor;
            case "Accessory":
                return Item.EquipType.Accessory;
            default:
                return Item.EquipType.None;
        }
    }

    Item.ItemRank DiscernToItemRank(string _itemRank)
    {
        switch (_itemRank)
        {
            case "Common":
                return Item.ItemRank.Common;
            case "Magic":
                return Item.ItemRank.Magic;
            case "Rare":
                return Item.ItemRank.Rare;
            case "Unique":
                return Item.ItemRank.Unique;
            case "Legendary":
                return Item.ItemRank.Legendary;
            default:
                return Item.ItemRank.NONE;
        }
    }
}
