using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public bool isView = false;

    [SerializeField] DialougeEvent dialogueEvent = null;

    public void SetDialogueLine(Vector2 p_Line)
    {
        dialogueEvent.line = p_Line;
    }

    public Dialogue[] GetDialogue()
    {
        dialogueEvent.dialogues = ScriptManager.instance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);
        return dialogueEvent.dialogues;
    }
}
