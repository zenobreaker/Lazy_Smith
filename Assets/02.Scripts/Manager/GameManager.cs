using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static int Gold;                        // 게임 화폐
    public static GameManager instance; 

    public int gameLevel;                          // 게임 난이도 
    
    public bool isCreateGuideNote;          // 가이드 노트 생성 여부
    public bool isStart = false;                   // 게임 시작 여부 
    bool isStop = false;                    // 게임 중지 여부 
    bool oneTime = true;                    // 코루틴을 한 번만 호출하기 위한 제어 변수
    
    public int gameScore;            // 게임 점수 
    
    [SerializeField] GameObject go_PrepareUI = null;

    [SerializeField] NoteManager noteManager = null;
    [SerializeField] TimeGauageController theTimer = null; 
    [SerializeField] StageManager stageManager = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        if (isStart)
        {
            PrepareGame();
            PlayGame();
            CheckNoteEnded();
            GetInput();
            EndGame();
        }
        
    }
    

    // 진행도 조절 stageManager에게 연결할 징검다리 
    public void IncreaseLevel(ComboHit p_comboHit)
    {
        // 콤보에 따른 진행도 증가 
        stageManager.IncreaseProcessivity(p_comboHit);
        
    }

    // 게임 시작 전 준비 
    public void PrepareGame()
    {
        if (isStop)
        {
            UIManager.instacne.TurnOnGameUI();
            UIManager.instacne.TurnOnFadeUI();
            theTimer.isStart = false;

            if (EventSystem.current.currentSelectedGameObject.layer == 5)
            {
                UIManager.instacne.TurnOffFadeUI();
                theTimer.isStart = true;
                isStop = false;
            }
        }
    }

    public void ReturnLobby()
    {
        UIManager.instacne.TurnOnLobby();
    }

    public void StartGame(int p_stageNum = 0)
    {
        stageManager.SettingStage(p_stageNum);
        noteManager.SettingNoteCount(stageManager.GetCurStageLevel());
        isStart = true;
        isStop = true;
        theTimer.SetMaxValue(60);
        theTimer.isStart = true;
    }

    public void PlayGame()
    {
        if (!isCreateGuideNote)
        {
            noteManager.CreateNote(stageManager.GetCurStageTimingValue());
            isCreateGuideNote = true;
        }
    }

    // 게임 종료 
    public void EndGame()
    {
        if(stageManager.GetStageProcesivity() >= stageManager.GetStageMaxProcess())
        {
            // 게임 종료 로직 
            isStart = false;
            theTimer.isStart = false;
            noteManager.ClearGuideNote();
            noteManager.ClearUserNote();
            stageManager.EndStage();
           // ReturnLobby();
        }
    }


    // 노트를 입력 완료 했는지 검가 
    void CheckNoteEnded()
    {
        if (noteManager.CompleteInput() && oneTime)
        {
            oneTime = false;
            StartCoroutine(CheckNoteComplete());
        }
    }

    // 완전히 입력한 노트를 보여주고 지우는 역할 
    IEnumerator CheckNoteComplete()
    {
        noteManager.CheckCorrectNote();
        yield return new WaitForSeconds(0.3f);  // 딜레이를 줘서 모든 입력한 노트를 순간적으로 보여주고 지움

        noteManager.ClearGuideNote();
        noteManager.ClearUserNote();
        isCreateGuideNote = false;
        oneTime = true;     
    }

    // 방향키 입력 검사 
    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.instance.PlaySE("UpButton");
            noteManager.CreateNoteWithUser(KeyCode.UpArrow);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundManager.instance.PlaySE("DownButton");
            noteManager.CreateNoteWithUser(KeyCode.DownArrow);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SoundManager.instance.PlaySE("RightButton");
            noteManager.CreateNoteWithUser(KeyCode.RightArrow);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SoundManager.instance.PlaySE("LeftButton");
            noteManager.CreateNoteWithUser(KeyCode.LeftArrow);
        }
    }

    // 상자 터치 입력 검사 p_Num : 1,2,3,4 위 아래 오른 왼
    public void GetTouchButton(int p_Num)
    {
        switch (p_Num)
        {
            case 1:
                SoundManager.instance.PlaySE("UpButton");
                noteManager.CreateNoteWithUser(KeyCode.UpArrow);
                break;
            case 2:
                SoundManager.instance.PlaySE("DownButton");
                noteManager.CreateNoteWithUser(KeyCode.DownArrow);
                break;
            case 3:
                SoundManager.instance.PlaySE("RightButton");
                noteManager.CreateNoteWithUser(KeyCode.LeftArrow);
                break;
            case 4:
                SoundManager.instance.PlaySE("LeftButton");
                noteManager.CreateNoteWithUser(KeyCode.RightArrow);
                break;

        }
    }

}
