using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> stoneList;
    public List<Item> weaponList;

    void Start()
    {
      
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

  
}
