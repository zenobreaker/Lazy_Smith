using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{

    [SerializeField] Animator NoteClearAnimator = null;
    [SerializeField] Animator judgementAnimator = null;
    [SerializeField] Image img_judgement = null;
    [SerializeField] Sprite[] judgementSprite = null;

    public void SettingAnim(bool p_flag)
    {
        NoteClearAnimator.gameObject.SetActive(p_flag);
        judgementAnimator.gameObject.SetActive(p_flag);
    }

    public void judgementEffect(int p_num)
    {
        img_judgement.sprite = judgementSprite[p_num];
        judgementAnimator.SetTrigger("Hit");
    }

    public void NoteClearEffect()
    {
        NoteClearAnimator.SetTrigger("Clear");
    }

    public void ResetEffect()
    {
        NoteClearAnimator.ResetTrigger("Clear");
        judgementAnimator.ResetTrigger("Hit");

    }
}
