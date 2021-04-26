using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestContext : MonoBehaviour
{
    QuestData currentData; 

    [SerializeField] Text txt_itemName = null;
    [SerializeField] Text txt_ObjectEach = null;

    public void SetText(string p_ItemName,int p_current, int target)
    {
        txt_itemName.text = p_ItemName;

        txt_ObjectEach.text = p_current.ToString() + " / " + target.ToString();
    }

    public void SetText(QuestData p_questdata)
    {
    }
}
