using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingManager : MonoBehaviour
{
    int[] judgementRecord = new int[4];

    [SerializeField] Image img_timingImage = null;
    [SerializeField] Image img_WaringFrame = null;

    bool isGoodTiming;      // 좋은 점수를 유지하는 타이밍 확인  
    bool isWarning = false;
    float curTime;          // 진행하는 시간 수 
    float maxTime;
    IEnumerator timing;
    int timingCount = 0;

    public void StartTiming(float p_time)
    {
        curTime = p_time;
        maxTime = p_time;

        if (timing != null)
            StopTiming();
        
        timing = TimingCo();
        StartCoroutine(timing);
    }

 
    public float GetTime() {
        return maxTime;
    }

    public void StopTiming()
    {
        timingCount = 0;
        StopCoroutine(timing);
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
        else if (rate <= 40 && rate > 10)
            return 2;
        else if (rate <= 10 && rate > 0)
            return 3;
        else 
            return 4;
    }

     IEnumerator TimingCo()
    {
        timingCount++;
        Debug.Log("현재 진행 시간  = " + maxTime);

        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            img_timingImage.fillAmount = curTime / maxTime;
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

            if (t_FeverTime < 3 && !isWarning)
            {
                isWarning = true;
                StartCoroutine(FlashingWarnig(t_FeverTime));
            }

            isGoodTiming = true;
            yield return null;
        }
        Debug.Log("피버타임 끝");
        isGoodTiming = false;
        img_WaringFrame.gameObject.SetActive(false);
        isWarning = false;
        //NoteManager.isFever = false;
    }

    IEnumerator FlashingWarnig(float p_Time)
    {
        img_WaringFrame.gameObject.SetActive(true);
        Color t_color = img_WaringFrame.color;
        t_color.a = 1;
        bool isSwtich = false;

        while (p_Time > 0)
        {
            p_Time -= Time.deltaTime;

            if (!isSwtich)
            {
                t_color.a -= Time.deltaTime;
                if (t_color.a <= 0)
                    isSwtich = true;
            }
            else if (isSwtich)
            {
                t_color.a += Time.deltaTime;
                if(t_color.a >= 1)
                    isSwtich = false;
            }

            img_WaringFrame.color = t_color;
            yield return null;
            
        }
        img_WaringFrame.gameObject.SetActive(false);
    }

}
