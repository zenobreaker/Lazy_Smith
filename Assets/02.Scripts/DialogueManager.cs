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

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay = 0.0f;

    int lineCount = 0;  // 대화 카운트 
    int contextCount = 0;   // 대사 카운트 

   // [SerializeField] InteractionController theIC = null;

    private void Update()
    {
        if (isDialouge)
        {
            if (isNext)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
                {
                    isNext = false;
                    txt_Dialogue.text = "";

                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());
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


    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialouge = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";

        dialogues = p_dialogues;

        StartCoroutine(TypeWriter());
    }

    public void ShowDialogue(Dialogue[] p_dialogues, bool isEnd = false)
    {
        isDialouge = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";

        dialogues = p_dialogues;

        StartCoroutine(TypeWriter());

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

    }

    IEnumerator TypeWriter()
    {
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");

        txt_Name.text = dialogues[lineCount].name;

        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            txt_Dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }

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
    }


    public void SkipDialogue()
    {
        StopCoroutine(TypeWriter());
        EndDialogue();
        go_SkipAlert.SetActive(false);
    }


    public void CancelSkipDialogue()
    {
        go_SkipAlert.SetActive(false);
    }
}
