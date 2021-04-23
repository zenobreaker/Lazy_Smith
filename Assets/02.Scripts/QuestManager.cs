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

        questDic.Add(1, new QuestData("감각 되찾기", "101", 2));
        questDic.Add(2, new QuestData("촌장님의 부탁?", "102", 3));
        
        t_weapons = new string[] { "101,", "102" };
        t_eachs = new int[] { 2, 2 };
        questDic.Add(3, new QuestData("장사 준비",t_weapons, t_eachs));
        
        questDic.Add(4, new QuestData("손님 맞이", "104", 3));

        t_weapons = new string[] { "103,","104", "105" };
        t_eachs = new int[] { 2,3,2 };
        questDic.Add(5, new QuestData("나태의 근원", t_weapons, t_eachs));


        t_weapons = new string[] { "104", "105" };
        t_eachs = new int[] {3, 4 };
        questDic.Add(6, new QuestData("수상한 의뢰인 1", t_weapons, t_eachs));

        // 이후 코로니움 관련 무기 추가 

    }

    public int GetQeustData(int p_ID)
    {
        return quesetId;
    }
}
