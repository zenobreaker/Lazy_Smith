using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    [SerializeField] Animator hammerAnimator = null;
    [SerializeField] Animator hitAnimator = null;
    [SerializeField] SpriteRenderer weaponSprite = null;
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
    }

     void SwingHammer()
    {
        hammerAnimator.SetTrigger("Hit");
    }

     void AppearEffect()
    {
        var clone = Instantiate(hitAnimator.gameObject,targetTR.position,Quaternion.identity);
        clone.GetComponent<Animator>().SetTrigger("Hit");
    }


    IEnumerator SwingAction()
    {
        SwingHammer();

        yield return new WaitForSeconds(0.3f);
        int rand = Random.Range(1, 5);
        string hammer = "Hammer0" + rand.ToString();
        
        SoundManager.instance.PlaySE(hammer);
        AppearEffect();
    }
}
