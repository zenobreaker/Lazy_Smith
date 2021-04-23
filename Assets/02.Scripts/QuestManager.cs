using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int quesetId;

    Dictionary<int, QuestData> questDic;

    void Awake()
    {
        questDic = new Dictionary<int, QuestData>();
        GenerateData();
    }


    void GenerateData()
    {
        string[] t_weapons; ;
        int[] t_eachs;

        questDic.Add(1, new QuestData("���� ��ã��", "101", 2));
        questDic.Add(2, new QuestData("������� ��Ź?", "102", 3));
        
        t_weapons = new string[] { "101,", "102" };
        t_eachs = new int[] { 2, 2 };
        questDic.Add(3, new QuestData("��� �غ�",t_weapons, t_eachs));
        
        questDic.Add(4, new QuestData("�մ� ����", "104", 3));

        t_weapons = new string[] { "103,","104", "105" };
        t_eachs = new int[] { 2,3,2 };
        questDic.Add(5, new QuestData("������ �ٿ�", t_weapons, t_eachs));


        t_weapons = new string[] { "104", "105" };
        t_eachs = new int[] {3, 4 };
        questDic.Add(6, new QuestData("������ �Ƿ��� 1", t_weapons, t_eachs));

        // ���� �ڷδϿ� ���� ���� �߰� 

    }

    public int GetQeustData(int p_ID)
    {
        return quesetId;
    }
}
