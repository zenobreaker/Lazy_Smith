using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instacne;

    [SerializeField] GameObject go_Lobby = null;
    [SerializeField] GameObject go_InGame = null;
    [SerializeField] GameObject go_FadeUI = null;
    [SerializeField] Text txt_Money = null;


    public void TurnOnFadeUI()
    {
        go_FadeUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void TurnOffFadeUI()
    {
        go_FadeUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void TurnOnGameUI()
    {
        go_InGame.SetActive(true);
        go_Lobby.SetActive(false);
    }

    public void TurnOnLobby()
    {
        go_InGame.SetActive(false);
        go_Lobby.SetActive(true);
    }


    private void Awake()
    {
        if (instacne == null)
            instacne = this;
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
