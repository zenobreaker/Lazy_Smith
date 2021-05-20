using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestUI questUI = null;

    Dictionary<int, QuestData> questDic;

    int currentQuestNum = 0;

    void Awake()
    {
        questDic = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        string[] t_weapons; ;
        int[] t_eachs;

        questDic.Add(1, new QuestData("일하기 싫어요.", "101", 2, 2500));
        questDic.Add(2, new QuestData("촌장님의 부탁?", "102", 3, 6500));
        
        t_weapons = new string[3] { "101", "102" ,"103"};
        t_eachs = new int[3] { 2, 2 ,2};
        questDic.Add(3, new QuestData("쉬어가기 쉬운 것", t_weapons, t_eachs,9900));
        
        questDic.Add(4, new QuestData("손님은 맞이해야", "104", 3,15000));

        t_weapons = new string[] { "104","105", "106" };
        t_eachs = new int[3] { 2,3,2 };
        questDic.Add(5, new QuestData("필사적 게으름", t_weapons, t_eachs, 82000));


        t_weapons = new string[] { "107", "108","109" };
        t_eachs = new int[3] {3, 4, 2};
        questDic.Add(6, new QuestData("과거로 부터 선물", t_weapons, t_eachs, 300000));

        // 이후 코로니움 관련 무기 추가 

    }

    public void SetQuest(int p_num)
    {
        currentQuestNum = p_num;
        questDic[p_num].isBeing = true;
        questUI.SettingUI(true);
        questUI.SetQuestContext(questDic[p_num]);
        CheckQuest();
    }

    // 퀘스트 검사 
    public void CheckQuest()
    {
        if (currentQuestNum == 0)
            return;
        else
        {
            questUI.SetQuestContext(questDic[currentQuestNum]);

            QuestData t_quest = questDic[currentQuestNum];
            int targetCount = 0;

            for (int i = 0; i < t_quest.weaponID.Length; i++)
            {
                int t_ItemCount = Inventory.instance.GetWeaponItemByID(t_quest.weaponID[i]);

                if (t_ItemCount >= t_quest.each[i])
                    targetCount++;
            }

            if (targetCount >= t_quest.weaponID.Length)
            {
                Debug.Log("퀘스트 완료 " + questDic[currentQuestNum].isBeing);
                OpenQuestAlert(true);// 퀘스트완료 
            }
            else
                OpenQuestAlert(false);// 퀘스트 조건 미달성
        }
    }

    public void OpenQuestAlert(bool p_flag)
    {
        Debug.Log("퀘스트 조건 완료 ");
        questUI.QuestClearAlert(p_flag);
    }

    // 버튼 
    public void ClearQuest()
    {
        QuestData t_questData = questDic[currentQuestNum];
        questDic[currentQuestNum].isBeing = false;
        questDic[currentQuestNum].isClear = true;
        questUI.QuestClearAlert(false);

        for (int i = 0; i < t_questData.weaponID.Length; i++)
        {
            int t_ItemCount = Inventory.instance.GetWeaponItemByID(t_questData.weaponID[i]);

            if (t_ItemCount >= t_questData.each[i])
                Inventory.instance.DecreaseWeaponCount(t_questData.weaponID[i], t_questData.each[i]);
        }

        questUI.ClearList();
        GameManager.money += t_questData.reward;
        UIManager.instance.SetMoney(GameManager.money);
        questUI.SettingUI(false);

        if (currentQuestNum == 5)
            ShopPage.instance.LastItemView();
    }

    public void SetQuestDic(List<bool> p_boolList, List<bool> p_clearList)
    {
        for (int i = 0; i < p_boolList.Count; i++)
        {
            questDic[i+1].isBeing = p_boolList[i];
            questDic[i + 1].isClear = p_clearList[i];
            if (questDic[i + 1].isBeing)
                SetQuest(i + 1);
            //else if(!questDic[i + 1].isBeing && !questDic[i + 1].isClear)
            //{
            //    SetQuest(i + 1);
            //}
        }
    }

    public List<bool> GetQuestBeingList()
    {
        List<bool> t_BoolList = new List<bool>();

        for (int i = 1; i <= questDic.Count; i++)
        {
            t_BoolList.Add(questDic[i].isBeing);
        }
        
        return t_BoolList;
    }

    public void SetQuestClear(List<bool> p_ClearList)
    {
        for (int i = 0; i < p_ClearList.Count; i++)
        {
            questDic[i + 1].isClear = p_ClearList[i];
        }
    }

    public List<bool> GetQuestClearList(){
        List<bool> t_ClearList = new List<bool>();

        for (int i = 1; i <= questDic.Count; i++)
        {
            t_ClearList.Add(questDic[i].isClear);
        }

        return t_ClearList;
    }

    public bool GetisClear(int p_num)
    {
       // Debug.Log("번호 : " + p_num + "클리어? : " + questDic[p_num].isClear);
        return questDic[p_num].isClear;
    }
}
