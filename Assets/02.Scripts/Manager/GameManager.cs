using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static int money;                        // 게임 화폐
    public static GameManager instance;

    public int gameLevel;                          // 게임 난이도 

    public int gameScore;            // 게임 점수 

   // [SerializeField] GameObject go_PrepareUI = null;

    [SerializeField] NoteManager noteManager = null;
    [SerializeField] TimeGauageController theTimer = null;
    [SerializeField] StageManager stageManager = null;
    [SerializeField] SaveManager saveManager = null;
    [SerializeField] TimingManager timingManager = null;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

  

    // 진행도 조절 stageManager에게 연결할 징검다리 
    public void IncreaseLevel(ComboHit p_comboHit)
    {
        // 콤보에 따른 진행도 증가 
        stageManager.IncreaseProcessivity(p_comboHit);

    }

    // 게임 시작 (일반 모드)
    public void StartGame(int p_stageNum = 0)
    {
       
        UIManager.instance.TurnOnGameUI();

        Time.timeScale = 1;

        noteManager.ClearGuideNote();
        noteManager.ClearUserNote();

        stageManager.SettingStage(p_stageNum);
        stageManager.SettingGauageUI(true);
        noteManager.SettingGame(stageManager.GetCurStageLevel(), stageManager.GetCurStageTimingValue());
        NoteManager.isFever = false;
        theTimer.SetMaxValue(60);
        theTimer.StartTimer();
    }

    // 타임어택 모드 
    public void StartTimeAttack()
    {
        UIManager.instance.TurnOnGameUI();

        Time.timeScale =1;

        noteManager.ClearGuideNote();
        noteManager.ClearUserNote();

        stageManager.SettingStage(0);
        stageManager.SettingGauageUI(false);
        noteManager.SettingGame(stageManager.GetCurStageLevel(), stageManager.GetCurStageTimingValue(),true);
       
        NoteManager.isFever = false;
        theTimer.SetMaxValue(60);
        theTimer.StartTimer();
    }

    
    // 게임 종료 
    public void GameEnd()
    {
        if (theTimer.IsTimeOver())
        {
            theTimer.StopTimer();

            if (!noteManager.isTimeAttack)
                stageManager.EndFailureStage();
            else if (noteManager.isTimeAttack)
                stageManager.EndTimeAttack();
        }
        else if (stageManager.GetStageProcesivity() >= stageManager.GetStageMaxProcess())
        {
            if (!noteManager.isTimeAttack)
            {
                // 게임 종료 로직 
                theTimer.StopTimer();
                stageManager.EndClearStage();
                noteManager.ClearGame();
            }
            else
            {
                stageManager.SettingRandomStage();
            }
        }
    }

    public void GameReset()
    {
        Time.timeScale = 1;
        noteManager.ClearGame();
        noteManager.ResetEffects();
        theTimer.StopTimer();
        timingManager.StopTiming();
    }


    // 방향키 입력 검사 
    public void GetInput()
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

    // 타이머 게이지 다운 
    public void DownTimeCount(float p_value)
    {
        theTimer.DownTimeCount(p_value);
    }

    public void SaveClick()
    {
        saveManager.SaveData();
    }


    public void LoadClick()
    {
        saveManager.LoadData();
        UIManager.instance.SetMoney(money);
    }
}
