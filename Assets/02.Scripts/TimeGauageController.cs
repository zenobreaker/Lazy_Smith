using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeGauageController : MonoBehaviour
{
    [SerializeField] Slider timeSlider = null;
    [SerializeField] Image img_fillGauage = null;

    bool isOver;

    public bool IsTimeOver()
    {
        return isOver;
    }

    // 코루틴 종료 
    public void StopTimer()
    {
        StopCoroutine(GauageDown());
        isOver = false;
    }

    public void StartTimer()
    {
        StartCoroutine(GauageDown());
    }

    // 슬라이더 최대값 변경 
    public void SetMaxValue(float p_Max)
    {
        isOver = false;
        timeSlider.maxValue = p_Max;
        timeSlider.value = p_Max;
    }

    IEnumerator GauageDown()
    {
        while (timeSlider.value > 0)
        {
            timeSlider.value -= Time.deltaTime;
            ChangeFillColor();
            yield return null;
        }

        if (timeSlider.value <= 0)
            isOver = true;
    }

    void ChangeFillColor()
    {
        float currentValue = timeSlider.value / timeSlider.maxValue * 100;

        if (currentValue > 60)
        {
            img_fillGauage.color = Color.green;
        }else if(currentValue <= 60 && currentValue > 30)
        {
            img_fillGauage.color = Color.yellow;
        }else
            img_fillGauage.color = Color.red;
    }
}
