using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public bool isView = false;

    [SerializeField] DialougeEvent dialouge = null;

    public Dialogue[] GetDialogue()
    {
        dialouge.dialogues = ScriptManager.instance.GetDialogue((int)dialouge.line.x, (int)dialouge.line.y);
        return dialouge.dialogues;
    }
}
