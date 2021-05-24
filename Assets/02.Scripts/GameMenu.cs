using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : TabManual
{
    public GameObject go_BackGround;
    public GameObject soundTab; // 사운드 조정 탭
    //public GameObject missionTab; // 임무 탭
    public GameObject gameTab; // 그래픽 조정 탭 
    public GameObject infoTab; 


    void Start()
    {
        if (soundTab.activeSelf)
            selectedTab = soundTab;
        else
            selectedTab = gameTab;
    }

    public override void OpenUI()
    {
        base.OpenUI();
        go_BackGround.SetActive(true);
    }

    public override void HideUI()
    {
        base.HideUI();
        go_BackGround.SetActive(false);
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android
            || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                TabSettingToGame(2);
                OpenUI();
            }
        }
       
    }

    public void TabSettingToGame(int p_tabNumber)
    {
        SoundManager.instance.PlaySE("Confirm_Click");
        switch (p_tabNumber)
        {
            case 0:
                TabSlotOpen(soundTab);
                break;
            case 2:
                TabSlotOpen(gameTab);
                break;
            case 3:
                TabSlotOpen(infoTab);
                break;
        }
    }

    // 로비에서 세팅
    public void TabSettingToLobby(int _tabNumber)
    {
        tabNumber = _tabNumber;
        SoundManager.instance.PlaySE("Confirm_Click");
        switch (tabNumber)
        {
            case 0:
                TabSlotOpen(soundTab);
                break;
            case 1:
                TabSlotOpen(gameTab);
                //StartCoroutine(TimeStopAndStart());
                break;
            case 2:
                TabSlotOpen(infoTab);
                break;
        }
    }

    IEnumerator TimeStopAndStart()
    {
        Debug.Log("멈춤");
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2f);
        Debug.Log("재시작");
        Time.timeScale = 1;
    }

    public void GameEnd()
    {
        Application.Quit();
    }

    public void GoToTitle()
    {
        SaveManager.instance.SaveData();
        SceneManager.LoadScene(0);
    }
}
