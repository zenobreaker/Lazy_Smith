using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage 
{
    public string weaponID;                 // 가져올 무기 ID
    public int stageLevel;                  // 스테이지 난이도 
    public float stageProcessivity;         // 스테이지 진행도 
    public float maxProcessvitiy;           // 스테이지 최대 진행도 
}
