using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] NoteManager noteManager = null; 

    void Start()
    {
        Debug.Log("게임 매니저 테스트");
    }

    private void Update()
    {
        GetInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 테스트용 하나 생성 
            noteManager.CreateNote(1);
        }
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
