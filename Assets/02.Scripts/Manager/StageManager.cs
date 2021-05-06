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
    int increasePoint = 0;

    public void SettingItemImage()
    {
        Item t_weapon = ItemDatabase.instance.GetWeaponItemByID(currentStage.weaponID);
        sptr_WeaponImage.sprite = t_weapon.itemImage;
    }

    public void SettingStage(int p_num)
    {
        increasePoint = 0;
        currentStage = stageArray[p_num];
        curNum = p_num;
        SettingItemImage();
    }

    public void SettingNextStage()
    {
        if(curNum <= stageArray.Length-1)
        {
            increasePoint = 0;
            currentStage = stageArray[curNum];
            Debug.Log("현재 스테이지 " + curNum);
            SettingItemImage();
            curNum++;
            
        }
    }

    public void SettingRandomStage()
    {
        int ran = Random.Range(0, stageArray.Length);
        increasePoint = 0;
        curNum = ran;
        currentStage = stageArray[curNum];
        SettingItemImage();
    }

    public int GetCurStageLevel()
    {
        Debug.Log("스테이지 레벨 : " + currentStage.stageLevel);
        return currentStage.stageLevel;
    }

    public int GetCurStageMaxLevel()
    {
        return currentStage.stageMaxLevel;
    }

    public float GetCurStageTimingValue()
    {
        return currentStage.timingTime;
    }

    public void IncreaseProcessivity(ComboHit p_combohit)
    {
        
        float levelCount = 0.0f;

        levelCount = Mathf.Pow(0.1f, currentStage.stageLevel);

       // Debug.Log("레벨 보너스 " + levelCount);
        switch (p_combohit)
        {
            case ComboHit.PERFECT:
                increasePoint += (int)(currentStage.maxProcessvitiy * (0.3f));
                break;
            case ComboHit.COOL:
                increasePoint += (int)(currentStage.maxProcessvitiy * (0.2f));
                break;
            case ComboHit.GOOD:
                increasePoint += (int)(currentStage.maxProcessvitiy * (0.1f));
                break;
            case ComboHit.MISS:
                increasePoint = 0;
                break;
            
        }

        //currentStage.stageProcessivity += increasePoint;
       // Debug.Log("진행도 : " + increasePoint);
    }

    public float GetStageProcesivity()
    {
        return increasePoint;
    }

    public float GetStageMaxProcess()
    {
        return currentStage.maxProcessvitiy;
    }

    public void EndClearStage()
    {
        clearUI.OpenUI();
        clearUI.SettingItem(currentStage.weaponID);
    }

    public void EndFailureStage()
    {
        clearUI.OpenFailureUI();
    }

    public void EndTimeAttack()
    {
        clearUI.OpenTimeAttackUI();
    }

   
}
