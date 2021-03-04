using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    /*   노트 매니저
     *   노트 랜덤하게 일정 개수만큼 배치 
     *   난이도 별로 개수가 다름 
     */

    [Header("화살표 노트")]
    [SerializeField] GameObject go_UpArrow = null;      // 화살표 프리팹  
    [SerializeField] GameObject go_DownArrow = null;
    [SerializeField] GameObject go_RightArrow = null;
    [SerializeField] GameObject go_LeftArrow = null;

    [Header("생성 가이드 노트 박스")]
    [SerializeField] GameObject go_GuideBox = null;       // 게임 노트를 두는 박스  
    
    [Header("생성 유저 노트 박스")]
    [SerializeField] GameObject go_UserBox = null;       // 유저가 노트를 두는 박스  


    // 랜덤하게 화살표 뽑기
    GameObject CreateRandomArrow()
    {
        int rand = Random.Range(1, 5);

        switch (rand)
        {
            case 1:
                return go_UpArrow;
            case 2:
                return go_DownArrow;
            case 3:
                return go_RightArrow;
            case 4:
                return go_LeftArrow;
            default:    // 에러 방지 
                return go_UpArrow;
        }
    }

    // 노트 생성 메소드
    public void CreateNote(int p_Max)
    {
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
        GameObject clone = null;

        switch (p_KeyCode)
        {
            case KeyCode.UpArrow:
                clone = Instantiate(go_UpArrow, go_UserBox.transform);
                break;
            case KeyCode.DownArrow:
                clone = Instantiate(go_DownArrow, go_UserBox.transform);
                break;
            case KeyCode.RightArrow:
                clone = Instantiate(go_RightArrow, go_UserBox.transform);
                break;
            case KeyCode.LeftArrow:
                clone = Instantiate(go_LeftArrow, go_UserBox.transform);
                break;
            default:
                clone = Instantiate(go_UpArrow, go_UserBox.transform);
                break;
        }

        if(clone != null)
            go_UserBox.transform.SetParent(clone.transform);
    }

}
