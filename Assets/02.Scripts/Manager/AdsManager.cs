using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager instance; 

    [SerializeField] GameObject go_BaseUI = null;
    [SerializeField] Button btn_AdsButton = null;
    [SerializeField] Text txt_AlertText = null;

    int coin = 0;
    private string playStoreID = "4119133";
    private string appStoreID = " 4119132";

    private string interstitialAd = "video";
    private string rewardedViedoAd = "rewardedVideo";

    public bool isTargetPlayStore;
    public bool isTestAd;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        Advertisement.AddListener(this);
        InitializeAdvertisment();
    }

    private void InitializeAdvertisment()
    {
        if (isTargetPlayStore)
        {
            Advertisement.Initialize(playStoreID, isTestAd); return;
        }
        Advertisement.Initialize(appStoreID, isTestAd);
    }

    public void PlayInterstitialAd()
    {
        if (!Advertisement.IsReady(interstitialAd))
        {
            return;
        }
        Advertisement.Show(interstitialAd);
    }

    // 보상형 광고 
    public void PlayRewardedVideoAd()
    {
        if (!Advertisement.IsReady(rewardedViedoAd)) { return; }
        Advertisement.Show(rewardedViedoAd);
    }

   
    public void SettingUI(bool p_flag)
    {
        txt_AlertText.text = "광고를 보고 보상을 받으시겠습니까?\n 시청완료 : 15000";
        go_BaseUI.SetActive(p_flag);
    }

    public void ApearAdsButton(bool p_flag)
    {
        btn_AdsButton.gameObject.SetActive(p_flag);
    }

    public void OnUnityAdsReady(string placementId)
    {
      //  throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
       // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
       // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                coin = 1500;
                if (placementId == rewardedViedoAd)
                {
                    Debug.Log("Reward The Player");
                    GameManager.money += coin;
                    UIManager.instance.SetMoney(GameManager.money);
                }
                break;
            case ShowResult.Finished:
                coin = 15000;
                if(placementId == rewardedViedoAd)
                {
                    Debug.Log("Reward The Player");
                    GameManager.money += coin;
                    UIManager.instance.SetMoney(GameManager.money);
                    SettingUI(false);
                    ApearAdsButton(false);
                }

                break;
        }
    }
}
