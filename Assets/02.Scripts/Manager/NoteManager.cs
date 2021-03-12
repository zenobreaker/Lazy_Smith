using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    /*   노트 매니저
     *   노트 랜덤하게 일정 개수만큼 배치 
     *   난이도 별로 개수가 다름 
     */
    public int maxNoteCount;                                   // 최대로 생성될 노트 수 
    int correctCount;
    public int inputCount;                                    // 노트를 누른 수 

    List<int> guideNoteList = new List<int>();
    List<int> userNoteList = new List<int>();


    [Header("화살표 노트")]
    [SerializeField] GameObject go_UpArrow = null;      // 화살표 프리팹  
    [SerializeField] GameObject go_DownArrow = null;
    [SerializeField] GameObject go_RightArrow = null;
    [SerializeField] GameObject go_LeftArrow = null;

    [Header("생성 가이드 노트 박스")]
    [SerializeField] GameObject go_GuideBox = null;       // 게임 노트를 두는 박스  
    
    [Header("생성 유저 노트 박스")]
    [SerializeField] GameObject go_UserBox = null;       // 유저가 노트를 두는 박스  

    [Header("기타 매니저")]
    [SerializeField] ComboManager comboManager = null;
    [SerializeField] EffectManager effectManager = null; 

    // 입력을 다 하였는가?
    public bool CompleteInput()
    {
        if (inputCount == maxNoteCount)
        {
            return true;
        }
        else
            return false;
    }

    // 노트 검사 
    public void CheckCorrectNote()
    {
        for (int i = 0; i < maxNoteCount; i++)
        {
            if(userNoteList[i] == guideNoteList[i])
            {
                correctCount++;
            } 
        }

        Debug.Log(correctCount + "개 맞춤");

        if (correctCount > 0)
        {
            if(correctCount >= maxNoteCount)
                effectManager.judgementEffect(0);
            else if(correctCount < maxNoteCount &&correctCount > 1)
                effectManager.judgementEffect(1);
            effectManager.NoteClearEffect();
            comboManager.IncreaseCombo();
        }
    }


    // 가이드 및 입력한 노트 및 정보 초기화 
    public void ClearGuideNote()
    {
        for (int i = 0; i < guideNoteList.Count; i++)
        {
            Destroy(go_GuideBox.transform.GetChild(i).gameObject);
          
        }

        correctCount = 0;
        inputCount = 0;
       
        guideNoteList.Clear();
    }

    public void ClearUserNote()
    {
        for (int i = 0; i < userNoteList.Count; i++)
        {
            Destroy(go_UserBox.transform.GetChild(i).gameObject);
        }
        userNoteList.Clear();
    }

    

    // 랜덤하게 화살표 뽑기
    GameObject CreateRandomArrow()
    {
        int rand = Random.Range(1, 5);

        switch (rand)
        {
            case 1:
                guideNoteList.Add(1);
                return go_UpArrow;
            case 2:
                guideNoteList.Add(2);
                return go_DownArrow;
            case 3:
                guideNoteList.Add(3);
                return go_RightArrow;
            case 4:
                guideNoteList.Add(4);
                return go_LeftArrow;
            default:    // 에러 방지 
                guideNoteList.Add(1);
                return go_UpArrow;
        }
    }

    // 노트 생성 메소드
    public void CreateNote(int p_Max)
    {
        maxNoteCount = p_Max;

        for (int  i = 0;  i < p_Max;  i++)
        {
            // 노트 생성 후 가이드 박스에 붙이기 
            var clone = Instantiate(CreateRandomArrow(), go_GuideBox.transform);
            go_GuideBox.transform.SetParent(clone.transform);
        }
    }

    // 유저가 키를 누르면 작동 
    public void CreateNoteWithUser(KeyCode p_KeyCode)
    {
        // 유저가 입력한 노트 리스트에 추가 
        GameObject clone = null;

        if (inputCount >= maxNoteCount)  // 추가 개수 입력 방지 
            return; 

        switch (p_KeyCode)
        {
            case KeyCode.UpArrow:
                userNoteList.Add(1);
                clone = Instantiate(go_UpArrow, go_UserBox.transform);
                break;
            case KeyCode.DownArrow:
                userNoteList.Add(2);
                clone = Instantiate(go_DownArrow, go_UserBox.transform);
                break;
            case KeyCode.RightArrow:
                userNoteList.Add(3);
                clone = Instantiate(go_RightArrow, go_UserBox.transform);
                break;
            case KeyCode.LeftArrow:
                userNoteList.Add(4);
                clone = Instantiate(go_LeftArrow, go_UserBox.transform);
                break;
            default:
                userNoteList.Add(1);
                clone = Instantiate(go_UpArrow, go_UserBox.transform);
                break;
        }
        inputCount++;
        if(clone != null)
            go_UserBox.transform.SetParent(clone.transform);
    }


}
