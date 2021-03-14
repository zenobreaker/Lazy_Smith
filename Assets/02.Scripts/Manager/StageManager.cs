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


    public void IncreaseProcessivity(float p_num)
    {
        currentStage.stageProcessivity += p_num;
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
