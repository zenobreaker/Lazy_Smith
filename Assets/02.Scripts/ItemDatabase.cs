using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance; 

    public List<Item> stoneList;
    public List<Item> weaponList;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public List<Item> GetStones()
    {
        return stoneList;
    }
    public List<Item> GetWeapons()
    {
        return weaponList;
    }

    public void SetList()
    {

    }


    public Item GetMetrialItemByID(string p_ID)
    {
        Item t_item = stoneList.Find(x => x.itemID == p_ID);

        if (t_item != null)
            return t_item;
        else
            return null;
    }

    public Item GetWeaponItemByID(string p_ID)
    {
        Item t_item = weaponList.Find(x => x.itemID == p_ID);

        if (t_item != null)
            return t_item;
        else
            return null;
    }

}
