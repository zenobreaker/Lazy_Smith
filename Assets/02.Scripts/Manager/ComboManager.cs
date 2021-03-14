using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public int comboCount;
    public int maxCombo; 

    [SerializeField] Text txt_ComboText = null; 
    

    public void IncreaseCombo()
    {
        comboCount++;

        if (comboCount >= maxCombo)
            comboCount = maxCombo;

        if(comboCount> 2)
        {
            txt_ComboText.gameObject.SetActive(true);
            txt_ComboText.text = string.Format("{0:#,##0}", comboCount) + "Combo!";
            GameManager.instance.IncreaseLevel();
        }

    }

    public void InitailCombo()
    {
        comboCount = 0;
    }

    public void ResetCombo()
    {
        comboCount = 0;
        txt_ComboText.text = "Combo!";
        txt_ComboText.gameObject.SetActive(false);
        //txt_ComboText.text = string.Format("{0:#,##0}", comboCount) + "Combo!";
    }

    
}
