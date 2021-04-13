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

    public List<InteractionEvent> IEList = new List<InteractionEvent>();


    public void ShowDialogue(int p_EventNum)
    {
        switch (p_EventNum)
        {
            case 1:
                if (!theIE.isView)
                {
                    ScriptManager.instance.SettingData("prologue_Lazy");
                    theDM.ShowDialogue(theIE.GetDialogue());
                    theIE.isView = true;
                }
                break;
        }
        
    }

    public void SettingIcon(bool p_flag)
    {
        btn_Dialogue.gameObject.SetActive(p_flag);
    }


}
