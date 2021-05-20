using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] GameObject BackGround = null;
    [SerializeField] Image[] img_Char = null;
    [SerializeField] GameObject go_DialogueBar = null;
    [SerializeField] GameObject go_DialogueNameBar = null;
    [SerializeField] GameObject go_TouchScreen = null;

    [Header("스킵 알림창")]
    [SerializeField] GameObject go_SkipAlert = null; 
    
    [SerializeField] Text txt_Dialogue = null;
    [SerializeField] Text txt_Name = null;

    [SerializeField] Image img_CreditScreen = null;
    public float fadeSpeed;
    //erializeField] Button btn_Skip = null; 

    Dialogue[] dialogues;

    bool isDialouge = false;
    bool isNext = false; //특정 키 입력 대기.
    bool isTalking = false;
    bool oneTime = true;
    bool isSkip = false;
    bool isTouch = false;

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay = 0.0f;

    int lineCount = 0;  // 대화 카운트 
    int contextCount = 0;   // 대사 카운트 
    int coCoint = 0;
    IEnumerator dialogueScript; 

   // [SerializeField] InteractionController theIC = null;

    private void Update()
    {
      
        if (isDialouge && !isSkip)
        {
            //if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && isTalking && oneTime)
            //{
            //    oneTime = false;
            //    StopCoroutine(dialogueScript);
            //    dialogueScript = null;
            //    txt_Dialogue.text = dialogues[lineCount].contexts[contextCount];
            //    isTalking = false;
            //}
            //else if((Input.GetMouseButtonDown(0)|| Input.GetKeyDown(KeyCode.Space)) && !isTalking && dialogueScript == null)
            //{
            //    oneTime = true;
            //    isNext = true;
            //}

            if (isNext)
            {
                //(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                if (isTouch)
                {
                    isTouch = false;
                    isNext = false;
                    txt_Dialogue.text = "";
                    dialogueScript = TypeWriter();

                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(dialogueScript);
                    }
                    else
                    {
                        contextCount = 0;
                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(dialogueScript);
                        }
                        else
                        {
                            EndDialogue();
                        }
                    }
                }
            }
          
        }
    }

    public void TouchScreen()
    {
        if (isDialouge && !isSkip)
        {
            if (isTalking && oneTime)
            {
                oneTime = false;
                StopCoroutine(dialogueScript);
                // dialogueScript = null;
                string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
                t_ReplaceText = t_ReplaceText.Replace("'", ",");
                t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");
                txt_Dialogue.text = t_ReplaceText;
                isTalking = false;
            }
            else if (!isTalking)
            {
                oneTime = true;
                isNext = true;
            }
            if (isNext)
                isTouch = true;
        }
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialouge = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";

        go_TouchScreen.SetActive(true);
        dialogues = p_dialogues;
        dialogueScript = TypeWriter();
        StartCoroutine(dialogueScript);
    }

    public void ShowDialogue(Dialogue[] p_dialogues, bool isEnd = false)
    {
        isDialouge = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";

        dialogues = p_dialogues;
        go_TouchScreen.SetActive(true);
        dialogueScript = TypeWriter();
        StartCoroutine(dialogueScript);

        if (isEnd)
            StartCoroutine(EndCardOpen());

    }

    void EndDialogue()
    {
        isDialouge = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        SettingUI(false);
        go_TouchScreen.SetActive(false);
    }

    IEnumerator TypeWriter()
    {
        SettingUI(true);
        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");
        
        coCoint++;
        txt_Name.text = dialogues[lineCount].name;

        isTalking = true;
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
          
            txt_Dialogue.text += t_ReplaceText[i];
           
            yield return new WaitForSeconds(textDelay);
             
        }
    
        txt_Dialogue.text = t_ReplaceText;
        isTalking = false;
        isNext = true;
    }


    IEnumerator EndCardOpen()
    {
        yield return new WaitUntil(() => !isDialouge);

        
        img_CreditScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Color t_color = img_CreditScreen.color;
        t_color.a = 1;
        img_CreditScreen.color = t_color;

        while(t_color.a > 0)
        {
            t_color.a -= fadeSpeed;
            img_CreditScreen.color = t_color;
            yield return null;
        }

        img_CreditScreen.gameObject.SetActive(false);

    }

    void SettingUI(bool p_flag)
    {
        BackGround.SetActive(p_flag);
        go_DialogueBar.SetActive(p_flag);
        if (p_flag)
        {
            if(dialogues[lineCount].name == "")
                go_DialogueNameBar.SetActive(false);
            else
            {
                go_DialogueNameBar.SetActive(true);
                txt_Name.text = dialogues[lineCount].name;
            }
        }
       
        //for (int i = 0; i < img_Char.Length; i++)
        //{
        //    img_Char[i].gameObject.SetActive(p_flag);
        //}
    }



    public void OpenSkipAlert()
    {
        go_SkipAlert.SetActive(true);
        isSkip = true;
    }


    public void SkipDialogue()
    {
        StopCoroutine(dialogueScript);
        EndDialogue();
        go_SkipAlert.SetActive(false);
        isSkip = false;
        
    }


    public void CancelSkipDialogue()
    {
        go_SkipAlert.SetActive(false);
        isSkip = false;
    }
}
