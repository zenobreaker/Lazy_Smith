using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestUI questUI = null;

    Dictionary<int, QuestData> questDic;

    int currentQuestNum = 0;
    bool isQuest = false;

    void Awake()
    {
        questDic = new Dictionary<int, QuestData>();
        GenerateData();
    }


    void GenerateData()
    {
        string[] t_weapons; ;
        int[] t_eachs;

        questDic.Add(1, new QuestData("감각 되찾기", "101", 2, 1000));
        questDic.Add(2, new QuestData("촌장님의 부탁?", "102", 3, 3000));
        
        t_weapons = new string[2] { "101", "102" };
        t_eachs = new int[2] { 2, 2 };
        questDic.Add(3, new QuestData("장사 준비", t_weapons, t_eachs,4000));
        
        questDic.Add(4, new QuestData("손님 맞이", "104", 3,10000));

        t_weapons = new string[] { "103","104", "105" };
        t_eachs = new int[] { 2,3,2 };
        questDic.Add(5, new QuestData("게으름", t_weapons, t_eachs,12000));


        t_weapons = new string[] { "104", "105" };
        t_eachs = new int[] {3, 4 };
        questDic.Add(6, new QuestData("수상한 의뢰인 1", t_weapons, t_eachs,100000));

        // 이후 코로니움 관련 무기 추가 

    }

    public void SetQuest(int p_num)
    {
        currentQuestNum = p_num;
        questDic[p_num].isBeing = true;
        questUI.SettingUI(true);
        questUI.SetQuestContext(questDic[p_num]);
        CheckQuest();
        isQuest = true;
    }

    // 퀘스트 검사 
    public void CheckQuest()
    {
        questUI.SetQuestContext(questDic[currentQuestNum]);

        QuestData t_quest = questDic[currentQuestNum];
        int targetCount = 0;

        for (int i = 0; i < t_quest.weaponID.Length; i++)
        {
            int t_ItemCount = Inventory.instance.GetWeaponItemByID(t_quest.weaponID[i]);
            
            if(t_ItemCount >= t_quest.each[i])
                targetCount++;
        }

        if (targetCount >= t_quest.weaponID.Length)
            OpenQuestAlert();// 퀘스트완료 

    }

    public void OpenQuestAlert()
    {
        Debug.Log("퀘스트 조건 완료 ");
        questUI.QuestClearAlert(true);
    }

    public void ClearQuest()
    {
        QuestData t_questData = questDic[currentQuestNum];
        questDic[currentQuestNum].isBeing = false;
        questUI.QuestClearAlert(false);

        for (int i = 0; i < t_questData.weaponID.Length; i++)
        {
            int t_ItemCount = Inventory.instance.GetWeaponItemByID(t_questData.weaponID[i]);

            if (t_ItemCount >= t_questData.each[i])
                Inventory.instance.DecreaseWeaponCount(t_questData.weaponID[i], t_questData.each[i]);
        }

        questUI.ClearList();
        GameManager.money += t_questData.reward;
        UIManager.instance.SetMoney(t_questData.reward);
        questUI.SettingUI(false);
        isQuest = false;
    }

    public void SetQuestDic(List<bool> p_boolList)
    {
        for (int i = 0; i < p_boolList.Count; i++)
        {
            questDic[i+1].isBeing = p_boolList[i];
            if (questDic[i + 1].isBeing)
                SetQuest(i + 1);
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
}
