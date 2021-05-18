using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameExplain : TabManual
{
    [SerializeField] GameObject content = null;

    [SerializeField] Scrollbar hScrollbar = null; 
    [SerializeField] Image img_CraftTutorial = null;
    [SerializeField] Image img_Background = null;
    [SerializeField] Button btn_Craft = null;
    [SerializeField] Button btn_Game = null; 

    List<Image> imgList = new List<Image>(32);

    private void Start()
    {
        SettingImage();
        TabSetting(0);
    }

    public override void OpenUI()
    {
        base.OpenUI();
        img_Background.gameObject.SetActive(true);
    }

    public override void HideUI()
    {
        base.HideUI();
        img_Background.gameObject.SetActive(false);
    }

    void SettingCraftImage()
    {
        for (int i = 0; i < 3; i++)
        {
            imgList[i].sprite = Resources.Load<Sprite>("Tutorial/Craft"+ (i+1));
            //imgList.Add(img_CraftTutorial);
            //Instantiate(img_CraftTutorial, content.transform);
            imgList[i].gameObject.SetActive(true);
        }

        hScrollbar.value = 0;
    }

    void SettingGameImage()
    {
        for (int i = 0; i < 2; i++)
        {
            imgList[i].sprite = Resources.Load<Sprite>("Tutorial/time" + (i+1));
            imgList[i].gameObject.SetActive(true);
        }
        hScrollbar.value = 0;
    }

    void SettingImage()
    {
        for (int i = 1; i < 4; i++)
        {
            var clone = Instantiate(img_CraftTutorial, content.transform);
            imgList.Add(clone);
            clone.gameObject.SetActive(false);
        }
    }

    void ClearPage()
    {
        //imgList.Clear();
        for (int i = 0; i < imgList.Count; i++)
        {
            imgList[i].gameObject.SetActive(false);
            imgList[i].sprite = null;
        }
    }

    public void TabSetting(int _num)
    {
        ClearPage();

        switch (_num)
        {
            case 0:
                SettingCraftImage();
                btn_Craft.interactable = false;
                btn_Game.interactable = true;
                break;
            case 1:
                SettingGameImage();
                btn_Craft.interactable = true;
                btn_Game.interactable = false;
                break;
        }
    }

}
