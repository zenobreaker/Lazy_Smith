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
        StopCoroutine(CountTimeOut());
        txt_ComboText.color = new Color(txt_ComboText.color.r, txt_ComboText.color.g, txt_ComboText.color.b, 1);
        
        if (comboCount >= maxCombo)
            comboCount = maxCombo;

        if(comboCount >= 2)
        {
            txt_ComboText.gameObject.SetActive(true);
            txt_ComboText.text = string.Format("{0:#,##0}", comboCount) + "Combo!";
            StartCoroutine(CountTimeOut());
        }

    }

    public void InitailCombo()
    {
        comboCount = 0;
    }

    public void ResetCombo()
    {
        comboCount = 0;
        txt_ComboText.color = new Color(txt_ComboText.color.r, txt_ComboText.color.g, txt_ComboText.color.b,1);
        txt_ComboText.text = "Combo!";
        txt_ComboText.gameObject.SetActive(false);
        //txt_ComboText.text = string.Format("{0:#,##0}", comboCount) + "Combo!";
    }


    public IEnumerator CountTimeOut()
    {
        Color colorAlpha = new Color(txt_ComboText.color.r, txt_ComboText.color.g, txt_ComboText.color.b, 1);

        while (colorAlpha.a > 0) {
            colorAlpha.a -= 0.1f;
            txt_ComboText.color = colorAlpha;
            yield return new WaitForSeconds(1f);
        }
        comboCount = 0;
    }
    
}
