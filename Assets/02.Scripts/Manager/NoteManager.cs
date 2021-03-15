using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboHit { PERFECT, COOL, GOOD, MISS};

public class NoteManager : MonoBehaviour
{
    /*   노트 매니저
     *   노트 랜덤하게 일정 개수만큼 배치 
     *   난이도 별로 개수가 다름 
     */
    int fixedNoteCount = 7;                                     // 고정 수 
    //public int maxNoteCount;                                   // 최대로 생성될 노트 수 
    int currentNoteCount;                                       // 현재 나오는 노트 수 
    int correctCount;
    public int inputCount;                                    // 노트를 누른 수 
    int initCount; 
    ComboHit comboHit;

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
    [SerializeField] TimingManager timingManager = null;

    // 입력을 다 하였는가?
    public bool CompleteInput()
    {
        if (inputCount == currentNoteCount)
        {
            return true;
        }
        else
            return false;
    }

    // 노트 검사 
    public void CheckCorrectNote()
    {
        for (int i = 0; i < currentNoteCount; i++)
        {
            if(userNoteList[i] == guideNoteList[i])
            {
                correctCount++;
            } 
        }

        //Debug.Log(correctCount + "개 맞춤");

        if (correctCount == currentNoteCount)
        {
            //if(timingManager.GetTiming())
            switch(timingManager.GetTimeValue())
            {
                case 0:
                    comboHit = ComboHit.PERFECT;
                    effectManager.judgementEffect(0);
                    break;
                case 1:
                    comboHit = ComboHit.COOL;
                    effectManager.judgementEffect(1); break;
                case 2:
                case 3:
                    comboHit = ComboHit.GOOD;
                    effectManager.judgementEffect(2); 
                    break;
            }
            
            comboManager.IncreaseCombo();
            IncreaseNoteCount();
        }
        else
        {
            comboHit = ComboHit.MISS;
            effectManager.judgementEffect(3);
            comboManager.ResetCombo();
            ResetNoteCount();
        }
        effectManager.NoteClearEffect();
        GameManager.instance.IncreaseLevel(comboHit);
        timingManager.StopTiming();        // 타이밍값 초기화

    }

    // 노트 등장 수 초기화 
    void ResetNoteCount()
    {
        currentNoteCount = initCount;
    }

    // 진행에 따른 노트 수 추가 
    void IncreaseNoteCount()
    {
        if(currentNoteCount <= fixedNoteCount)
            currentNoteCount++;

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

    // 노트 개수 설정 
    public void SettingNoteCount(int p_num)
    {
        initCount = p_num;
        currentNoteCount = p_num;
    }

    // 노트 생성 메소드
    public void CreateNote(float p_time)
    {
        // 타이밍 체크 
        timingManager.StartTiming(p_time);

        for (int  i = 0;  i < currentNoteCount;  i++)
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

        if (inputCount >= currentNoteCount)  // 추가 개수 입력 방지 
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
