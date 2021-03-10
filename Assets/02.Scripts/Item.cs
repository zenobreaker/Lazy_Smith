using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    //게임 오브젝트에 붙일 필요가 없는  스크립트 생성 


    public string itemID;           // 아이템의 고유 ID
    public Sprite itemImage;     
    public string itemName;     // 아이템 이름
    public int itemValue;       // 아이템 가격 
    public bool isSale;
    
  
  

    public Item(Item _item)
    {
    
 
        itemName = _item.itemName;
        itemValue = _item.itemValue;
     

    }
  

  
}
