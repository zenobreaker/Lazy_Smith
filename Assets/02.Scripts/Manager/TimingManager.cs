using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    int[] judgementRecord = new int[4];
     
    float effectTime;
    public float maxTime; 

    public void GameStart()
    {
        StartCoroutine(TimingCo());
    }

    public void SettingLevelTime(float p_num)
    {
        maxTime = p_num;
    }


  
     IEnumerator TimingCo()
    {
        while (GameManager.instance.isStart)
        {
            if (effectTime > 0)
                effectTime -= Time.deltaTime;

            yield return null;
        }
    }

}
