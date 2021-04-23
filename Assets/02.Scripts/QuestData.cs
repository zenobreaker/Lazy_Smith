using System.Collections;
using System.Collections.Generic;


public class QuestData 
{
    public string questName;
    public string[] weaponID;
    public int[] each; 

    public QuestData(string p_name, string p_weaponID, int p_each)
    {
        questName = p_name;
        weaponID = new string[1];
        weaponID[0] = p_weaponID;
        each = new int[1];
        each[0] = p_each;
    }

    public QuestData(string p_name, string[] p_weaponIDs, int[] p_eachs)
    {
        questName = p_name;
        weaponID = new string[p_weaponIDs.Length];
        each = new int[p_eachs.Length];

        weaponID = p_weaponIDs;
        each = p_eachs;
    }
}
