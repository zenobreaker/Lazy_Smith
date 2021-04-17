using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject go_Lobby = null;
    [SerializeField] GameObject go_InGame = null;
    [SerializeField] GameObject go_GameView = null;
    [SerializeField] GameObject go_PauseUI = null;
    [SerializeField] GameObject go_ExitAlert = null;
    [SerializeField] Text txt_Money = null;




    public void PasuseGame()
    {
        go_PauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void CancelPauseUI()
    {
        go_PauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitPlayingGame()
    {
        go_ExitAlert.SetActive(true);
    }

    public void CancelAlert()
    {
        go_ExitAlert.SetActive(false);
    }

    public void TurnOnGameUI()
    {
        go_InGame.SetActive(true);
        go_GameView.SetActive(true);
        go_Lobby.SetActive(false);
    }

    public void ReturnLobby()
    {
        GameManager.instance.GameReset();
        go_InGame.SetActive(false);
        go_GameView.SetActive(false);
        go_Lobby.SetActive(true);
       

        go_ExitAlert.SetActive(false);
        go_PauseUI.SetActive(false);
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    public void SetMoney(int p_Money)
    {
        txt_Money.text = p_Money.ToString();
    }

    private void Start()
    {
        SetMoney(GameManager.money);
    }
}
