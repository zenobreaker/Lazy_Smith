using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    float effectTime; 


    void CheckTiming()
    {

    }

    private void Update()
    {
        if (effectTime > 0)
            effectTime -= Time.deltaTime;
    }

}
