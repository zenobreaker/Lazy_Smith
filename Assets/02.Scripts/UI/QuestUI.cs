using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] GameObject go_BaseUI = null;
    [SerializeField] GameObject go_Context = null;

    [SerializeField] Text txt_QuestTitle = null;

    [SerializeField] GameObject go_QuestContext = null;
    [SerializeField] GameObject go_AlertScreen = null;


    bool isQuest = false; 

    void SetTilte(string p_str)
    {
        txt_QuestTitle.text = p_str;
    }

    // 퀘스트 내용 작성
    public void SetQuestContext(QuestData p_questData)
    {
        SetTilte(p_questData.questName);
        if (!isQuest)
        {
            for (int i = 0; i < p_questData.weaponID.Length; i++)
            {
                Item t_weponName = ItemDatabase.instance.GetWeaponItemByID(p_questData.weaponID[i]);
                int currentItemCount = Inventory.instance.GetWeaponItemByID(t_weponName.itemID);

                var clone = Instantiate(go_QuestContext, go_Context.transform);
                clone.GetComponent<QuestContext>().SetText(t_weponName.itemName, currentItemCount, p_questData.each[i]);

            }
            isQuest = true;
        }else
        {
            for (int i = 0; i < p_questData.weaponID.Length; i++)
            {
                Item t_weponName = ItemDatabase.instance.GetWeaponItemByID(p_questData.weaponID[i]);
                int currentItemCount = Inventory.instance.GetWeaponItemByID(t_weponName.itemID);

                go_Context.transform.GetChild(i).GetComponent<QuestContext>().SetText(t_weponName.itemName,
                    currentItemCount, p_questData.each[i]);
            }
        }

    }
    public void ClearList()
    {
        isQuest = false;

        SetTilte("");

        int count = go_Context.transform.childCount;

        for (int i = 0; i < count ; i++)
        {
            Destroy(go_Context.transform.GetChild(i).gameObject);
        }
    }


    public void QuestClearAlert(bool p_Flag)
    {
        go_AlertScreen.SetActive(p_Flag);
    }

    public void SettingUI(bool p_Flag)
    {
        go_BaseUI.SetActive(p_Flag);
    }
}
