using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { WEAPON, MATERIAL}


[System.Serializable]
public class Item
{
    //게임 오브젝트에 붙일 필요가 없는  스크립트 생성 


    public string itemID;           // 아이템의 고유 ID
    public Sprite itemImage;     
    public string itemName;     // 아이템 이름
    public int itemValue;       // 아이템 가격 
    public int itemCount;       // 아이템 개수 
    public bool isSale;
    public ItemType itemType;   // 아이템 타입     
  
  

    public Item(Item _item)
    {
    
 
        itemName = _item.itemName;
        itemValue = _item.itemValue;
        

    }
  

  
}
