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

        questDic.Add(1, new QuestData("���� ��ã��", "101", 2, 1000));
        questDic.Add(2, new QuestData("������� ��Ź?", "102", 3, 3000));
        
        t_weapons = new string[2] { "101", "102" };
        t_eachs = new int[2] { 2, 2 };
        questDic.Add(3, new QuestData("��� �غ�", t_weapons, t_eachs,4000));
        
        questDic.Add(4, new QuestData("�մ� ����", "104", 3,10000));

        t_weapons = new string[] { "103","104", "105" };
        t_eachs = new int[] { 2,3,2 };
        questDic.Add(5, new QuestData("������", t_weapons, t_eachs,12000));


        t_weapons = new string[] { "104", "105" };
        t_eachs = new int[] {3, 4 };
        questDic.Add(6, new QuestData("������ �Ƿ��� 1", t_weapons, t_eachs,100000));

        // ���� �ڷδϿ� ���� ���� �߰� 

    }

    public void SetQuest(int p_num)
    {
        currentQuestNum = p_num;
        questUI.SettingUI(true);
        questUI.SetQuestContext(questDic[p_num]);
        CheckQuest();
    }

    // ����Ʈ �˻� 
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
            OpenQuestAlert();// ����Ʈ�Ϸ� 

    }

    public void OpenQuestAlert()
    {
        Debug.Log("����Ʈ ���� �Ϸ� ");
        questUI.QuestClearAlert(true);
    }

    public void ClearQuest()
    {
        QuestData t_questData = questDic[currentQuestNum];
        questUI.QuestClearAlert(false);

        for (int i = 0; i < t_questData.weaponID.Length; i++)
        {
            int t_ItemCount = Inventory.instance.GetWeaponItemByID(t_questData.weaponID[i]);

            if (t_ItemCount >= t_questData.each[i])
                Inventory.instance.DecreaseWeaponCount(t_questData.weaponID[i], t_questData.each[i]);
        }

        questUI.ClearList();
        UIManager.instance.SetMoney(t_questData.reward);
        questUI.SettingUI(false);
    }
}
