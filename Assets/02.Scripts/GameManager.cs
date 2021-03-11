using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Gold;                        // 게임 화폐
    public static GameManager instance; 

    int gameLevel;                          // 게임 난이도 
    
    public bool isCreateGuideNote;          // 가이드 노트 생성 여부
    bool isStart = false;                   // 게임 시작 여부 
    bool oneTime = true;                    // 코루틴을 한 번만 호출하기 위한 제어 변수
    
    public int gameScore;            // 게임 점수 

    [SerializeField] NoteManager noteManager = null;

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
            PlayGame();
            CheckNoteEnded();
            GetInput();
        }
        
    }
    
    // 난이도 조절
    void LevelControl()
    {

    }

    public void PlayGame()
    {
        if (!isCreateGuideNote)
        {
            noteManager.CreateNote(gameLevel);
            isCreateGuideNote = true;
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

        noteManager.ClearNote();
        isCreateGuideNote = false;
        oneTime = true;     
    }

    // 방향키 입력 검사 
    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            noteManager.CreateNoteWithUser(KeyCode.UpArrow);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            noteManager.CreateNoteWithUser(KeyCode.DownArrow);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            noteManager.CreateNoteWithUser(KeyCode.RightArrow);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            noteManager.CreateNoteWithUser(KeyCode.LeftArrow);
    }

    // 상자 터치 입력 검사 p_Num : 1,2,3,4 위 아래 오른 왼
    public void GetTouchButton(int p_Num)
    {
        switch (p_Num)
        {
            case 1:
                noteManager.CreateNoteWithUser(KeyCode.UpArrow);
                break;
            case 2:
                noteManager.CreateNoteWithUser(KeyCode.DownArrow);
                break;
            case 3:
                noteManager.CreateNoteWithUser(KeyCode.LeftArrow);
                break;
            case 4:
                noteManager.CreateNoteWithUser(KeyCode.RightArrow);
                break;

        }
    }

}
