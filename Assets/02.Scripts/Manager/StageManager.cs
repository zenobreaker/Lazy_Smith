using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] ClearUI clearUI = null;

    [SerializeField] Stage[] stageArray = null;

    Stage currentStage;

    [SerializeField] SpriteRenderer sptr_WeaponImage = null;
    int curNum;

    public void SettingItemImage()
    {
        Item t_weapon = ItemDatabase.instance.GetWeaponItemByID(currentStage.weaponID);
        sptr_WeaponImage.sprite = t_weapon.itemImage;
    }

    public void SettingStage(int p_num)
    {
        currentStage = stageArray[p_num];
        curNum = p_num;
        SettingItemImage();
    }

    public void SettingNextStage()
    {
        if(curNum <= stageArray.Length-1)
        {
            currentStage = stageArray[curNum];
            Debug.Log("현재 스테이지 " + curNum);
            SettingItemImage();
            curNum++;
            
        }
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
        float levelCount = 0.0f;

        levelCount = Mathf.Pow(0.1f, currentStage.stageLevel);

        Debug.Log("레벨 보너스 " + levelCount);
        switch (p_combohit)
        {
            case ComboHit.PERFECT:
                t_increasePoint = (int)(currentStage.maxProcessvitiy * (0.3f));
                break;
            case ComboHit.COOL:
                t_increasePoint = (int)(currentStage.maxProcessvitiy * (0.2f));
                break;
            case ComboHit.GOOD:
                t_increasePoint = (int)(currentStage.maxProcessvitiy * (0.1f));
                break;
            case ComboHit.MISS:
                t_increasePoint = 0;
                break;
        }

        currentStage.stageProcessivity += t_increasePoint;
        Debug.Log("진행도 : " + currentStage.stageProcessivity);
    }

    public float GetStageProcesivity()
    {
        return currentStage.stageProcessivity;
    }

    public float GetStageMaxProcess()
    {
        return currentStage.maxProcessvitiy;
    }

    public void EndClearStage()
    {
        if (currentStage.stageProcessivity > 0)
            currentStage.stageProcessivity = 0;

        clearUI.OpenUI();
        clearUI.SettingItem(currentStage.weaponID);
    }

    public void EndFailureStage()
    {
        currentStage.stageProcessivity = 0;
        clearUI.OpenFailureUI();
    }

    public void EndTimeAttack()
    {
        for (int i = 0; i < stageArray.Length; i++)
        {
            stageArray[i].stageProcessivity = 0;
        }
        clearUI.OpenTimeAttackUI();
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
