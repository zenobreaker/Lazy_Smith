using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    [SerializeField] Animator hammerAnimator = null;
    [SerializeField] Animator hitAnimator = null;
    [SerializeField] SpriteRenderer weaponSprite = null;
    [SerializeField] SpriteRenderer feverSprite = null;
    [SerializeField] Transform targetTR = null;

    // 망치 및 아이템 정렬 
    public void SetItems(Sprite p_Sprite)
    {
        weaponSprite.sprite = p_Sprite;
    }

    public void StartAction()
    {
        StopCoroutine(SwingAction());
        StartCoroutine(SwingAction());

        if (NoteManager.isFever)
        {
            StopCoroutine(FeverEffect());
            StartCoroutine(FeverEffect());
        }
    }

     void SwingHammer()
    {
        hammerAnimator.SetTrigger("Hit");
    }




    public void ResetEffect()
    {
        hitAnimator.gameObject.SetActive(false);
        feverSprite.gameObject.SetActive(false);
    }

    IEnumerator SwingAction()
    {
        SwingHammer();

        yield return new WaitForSeconds(0.3f);
        int rand = Random.Range(1, 5);
        string hammer = "Hammer0" + rand.ToString();
        
        SoundManager.instance.PlaySE(hammer);
        StartCoroutine(AppearEffect()); 
    }
    IEnumerator AppearEffect()
    {
        hitAnimator.transform.position = targetTR.position;
        hitAnimator.gameObject.SetActive(true);
        hitAnimator.SetTrigger("Hit");
        yield return new WaitForSeconds(0.35f);
        hitAnimator.gameObject.SetActive(false);
    }

    IEnumerator FeverEffect()
    {
        feverSprite.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        
        feverSprite.gameObject.SetActive(false);
    }
}
