using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public int comboCount;
    public int maxCombo;
    public int score;
    IEnumerator timeout;

    int perCount = 0;
    int coolCount = 0;
    int goodCount = 0;
    int feverCount = 0;

    [SerializeField] Text txt_ComboText = null;

    private void Start()
    {
        timeout = CountTimeOut();
    }
    public void IncreaseCombo()
    {
        comboCount++;
       
        StopCoroutine(timeout);
        txt_ComboText.color = new Color(txt_ComboText.color.r, txt_ComboText.color.g, txt_ComboText.color.b, 1);
        
        if (comboCount > maxCombo)
              maxCombo = comboCount;

        if(comboCount >= 2)
        {
            txt_ComboText.gameObject.SetActive(true);
            txt_ComboText.text = string.Format("{0:#,##0}", comboCount) + "Combo!";
            timeout = CountTimeOut();
            StartCoroutine(timeout);
        }

    }

    public void InitailCombo()
    {
        comboCount = 0;
        score = 0;
        perCount = 0;
        coolCount = 0;
        goodCount = 0;
        feverCount = 0;
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
    
    public void IncreaseScore(ComboHit p_ComboHit)
    {
        switch (p_ComboHit)
        {
            case ComboHit.PERFECT:
                score += 100;
                perCount++;
                break;
            case ComboHit.COOL:
                score += 70;
                coolCount++;
                break;
            case ComboHit.GOOD:
                score += 25;
                goodCount++;
                break;
            case ComboHit.FEVER:
                score += 120;
                feverCount++;
                break;
        }
    }
}
