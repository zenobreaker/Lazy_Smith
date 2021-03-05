using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeGauageController : MonoBehaviour
{
    [SerializeField] Slider timeSlider = null;
    [SerializeField] Image img_fillGauage = null;

    public bool isStart; 

    private void LateUpdate()
    {
        if(isStart)
        {
            timeSlider.value -= Time.deltaTime;
            ChangeFillColor();
        }
    }

    // 슬라이더 최대값 변경 
    public void SetMaxValue(float p_Max)
    {
        timeSlider.maxValue = p_Max;
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
