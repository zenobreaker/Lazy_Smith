using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingManager : MonoBehaviour
{
    int[] judgementRecord = new int[4];

    [SerializeField] Image img_timingImage = null;

    bool isGoodTiming;      // 좋은 점수를 유지하는 타이밍 확인  
    float curTime;          // 진행하는 시간 수 
    float maxTime;
    IEnumerator timing;
    int timingCount = 0;

    public void StartTiming(float p_time)
    {
        curTime = p_time;
        maxTime = p_time;
        
        if(timing != null)
            StopTiming();
        
        timing = TimingCo();
        StartCoroutine(timing);
    }

    public void StopTiming()
    {
        timingCount = 0;
        StopCoroutine(timing);
    }

    public void ResetTiming()
    {
        StopCoroutine(timing);
        curTime = maxTime;
        timing = TimingCo();
        StartCoroutine(timing);
    }

    public bool GetTiming()
    {
        return isGoodTiming;
    }

    public int GetTimeValue()
    {
        float rate = curTime / maxTime * 100;

        if (rate > 70)
            return 0;
        else if (rate <= 70 && rate > 40)
            return 1;
        else if (rate <= 40 && rate > 0)
            return 2;
        else
            return 3;
    }

     IEnumerator TimingCo()
    {
        timingCount++;
        Debug.Log("현재 진행 중인 타이밍 = " + timingCount);

        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            img_timingImage.fillAmount = curTime/maxTime;
            isGoodTiming = true;
            yield return null;
        }

        isGoodTiming = false; 
    }

    public void StartFever()
    {
        StopTiming();
        timing = FeverTimeStart();
        StartCoroutine(timing);
    }

    public bool isStop()
    {
        if (timing != null)
            return true;
        else
            return false;
    }

    IEnumerator FeverTimeStart()
    {
        float t_FeverTime = 10.0f;
        Debug.Log("피버타임 타이머 온 ");
        while (t_FeverTime > 0)
        {
            Debug.Log("피버타임 중 ");
            t_FeverTime -= Time.deltaTime;
            img_timingImage.fillAmount = t_FeverTime / 10;
            isGoodTiming = true;
            yield return null;
        }
        Debug.Log("피버타임 끝");
        isGoodTiming = false;
        //NoteManager.isFever = false;
    }

}
