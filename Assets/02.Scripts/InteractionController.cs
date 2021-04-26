using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{

    [SerializeField] Button btn_Dialogue = null;

    [SerializeField] InteractionEvent theIE = null;
    [SerializeField] DialogueManager theDM = null;
    [SerializeField] QuestManager theQuest = null; 
    [SerializeField] RecipePage recipePage = null;

    List<bool> viewList = new List<bool>();


    public void ShowDialogue(int p_EventNum)
    {
        switch (p_EventNum)
        {
            case 1:
                if (!viewList[0])
                {
                    ScriptManager.instance.SettingData("Prologue-Lazy", theIE);
                    theDM.ShowDialogue(theIE.GetDialogue());
                    viewList.Insert(0, true);
                    theQuest.SetQuest(1);
                }
                break;
            case 2:
                if (!viewList[1])
                {
                    ScriptManager.instance.SettingData("First-Lazy", theIE);
                    theDM.ShowDialogue(theIE.GetDialogue());
                    viewList.Insert(1, true);
                    theQuest.SetQuest(2);
                }
                break;
            case 3:
                if (!viewList[2])
                {
                    ScriptManager.instance.SettingData("Second-Lazy", theIE);
                    theDM.ShowDialogue(theIE.GetDialogue());
                    viewList.Insert(2, true);
                    theQuest.SetQuest(3);
                }
                break;
            case 4:
                if (!viewList[3])
                {
                    ScriptManager.instance.SettingData("Fourth-Lazy", theIE);
                    theDM.ShowDialogue(theIE.GetDialogue());
                    viewList.Insert(3, true);
                    theQuest.SetQuest(4);
                }
                break;
            case 5:
                if (!viewList[4])
                {
                    ScriptManager.instance.SettingData("Fiveth-Lazy", theIE);
                    theDM.ShowDialogue(theIE.GetDialogue());
                    viewList.Insert(4, true); 
                    theQuest.SetQuest(5);
                }
                break;
            case 6:
                if (!viewList[5])
                {
                    ScriptManager.instance.SettingData("Last-Lazy", theIE);
                    theDM.ShowDialogue(theIE.GetDialogue());
                    viewList.Insert(5, true);
                    theQuest.SetQuest(6);
                }
                break;
        }

    }

    public void SettingIcon(bool p_flag)
    {
        btn_Dialogue.gameObject.SetActive(p_flag);
    }

    public void SetViewList(List<bool> p_List)
    {
        viewList = p_List;
    }

    public List<bool> GetViewList()
    {
        return viewList;
    }
}
