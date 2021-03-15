using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] Stage[] stageArray = null;
    Stage currentStage; 




    public void SettingStage(int p_num)
    {
        currentStage = stageArray[p_num];
    }

    public int GetCurStageLevel()
    {
        Debug.Log("스테이지 레벨 : " + currentStage.stageLevel);
        return currentStage.stageLevel;
    }

    public float GetCurStageTimingValue()
    {
        return currentStage.timingTime;
    }

    public void IncreaseProcessivity(ComboHit p_combohit)
    {
        int t_increasePoint = 0; 

        switch (p_combohit)
        {
            case ComboHit.PERFECT:
                t_increasePoint = (int)(currentStage.maxProcessvitiy * 0.3f);
                break;
            case ComboHit.COOL:
                t_increasePoint = (int)(currentStage.maxProcessvitiy * 0.2f);
                break;
            case ComboHit.GOOD:
                t_increasePoint = (int)(currentStage.maxProcessvitiy * 0.1f);
                break;
            case ComboHit.MISS:
                t_increasePoint = 0;
                break;
        }

        currentStage.stageProcessivity += t_increasePoint;
    }

    public float GetStageProcesivity()
    {
        return currentStage.stageProcessivity;
    }

    public void IncreaseStageProcessivity(float p_num)
    {
        if (currentStage.stageProcessivity < currentStage.maxProcessvitiy)
        {
            currentStage.stageProcessivity += p_num;
        }
        else if (currentStage.stageProcessivity == currentStage.maxProcessvitiy)
        {
            // 게임 종료 
        }
    }
}
