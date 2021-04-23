using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{

    [SerializeField] Button btn_Dialogue = null;

    [SerializeField] InteractionEvent theIE = null;
    [SerializeField] DialogueManager theDM = null;

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
                    viewList[0] = true;
                }
                break;
            case 2:
                if (!viewList[1])
                {
                    ScriptManager.instance.SettingData("First-Lazy", theIE);
                    theDM.ShowDialogue(theIE.GetDialogue());
                    viewList[1] = true;
                }
                break;
            case 3:
                if (!viewList[2])
                {
                    ScriptManager.instance.SettingData("Second-Lazy", theIE);
                    theDM.ShowDialogue(theIE.GetDialogue());
                    viewList[2] = true;
                }
                break;
            case 4:
                if (!viewList[3])
                {
                    ScriptManager.instance.SettingData("Fourth-Lazy", theIE);
                    theDM.ShowDialogue(theIE.GetDialogue());
                    viewList[3] = true;
                }
                break;
            case 5:
                if (!viewList[4])
                {
                    ScriptManager.instance.SettingData("Fiveth-Lazy", theIE);
                    theDM.ShowDialogue(theIE.GetDialogue());
                    viewList[4] = true;
                }
                break;
            case 6:
                if (!viewList[5])
                {
                    ScriptManager.instance.SettingData("Last-Lazy", theIE);
                    theDM.ShowDialogue(theIE.GetDialogue());
                    viewList[5] = true;
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
