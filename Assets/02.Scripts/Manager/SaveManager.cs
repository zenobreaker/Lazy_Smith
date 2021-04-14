﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int money;
    public float bgmSoundValue;
    public float sfxSoundValue;
    public List<bool> recipeUnlockList = new List<bool>();
    public List<string> weaponItemList = new List<string>();
    public List<int> wItemCountList = new List<int>();
    public List<string> materialItemList = new List<string>();
    public List<int> mItemCountList = new List<int>();
}

public class SaveManager : MonoBehaviour
{
    [SerializeField] GameObject go_BackGround = null;

    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTROTY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private Inventory theInven;
    private RecipePage theRecipe;
    private SoundController theSC; 

    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIRECTROTY = Application.dataPath + "/Saves/";

        if (!Directory.Exists(SAVE_DATA_DIRECTROTY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTROTY);

        LoadData();
    }

    public void SaveData()
    {
        if(theInven == null)
            theInven = FindObjectOfType<Inventory>();
        if (theRecipe == null)
            theRecipe = FindObjectOfType<RecipePage>();
        if (theSC == null)
            theSC = FindObjectOfType<SoundController>();

        Item[] wItems = theInven.SaveWeaponData();
        Item[] mItems = theInven.SaveMaterialData();

        saveData.money = GameManager.money;
        saveData.recipeUnlockList = theRecipe.SaveRecipeData();
        saveData.sfxSoundValue = theSC.sfxSlider.value;
        saveData.bgmSoundValue = theSC.bgmSlider.value;

        for (int i = 0; i < wItems.Length; i++)
        {
            if (saveData.weaponItemList.Contains(wItems[i].itemID))
            {
                int idx = saveData.weaponItemList.IndexOf(wItems[i].itemID);
                saveData.wItemCountList[idx] = wItems[idx].itemCount;
            }
            else
            {
                saveData.weaponItemList.Add(wItems[i].itemID);
                saveData.wItemCountList.Add(wItems[i].itemCount);
            }
        }

        for (int j = 0; j < mItems.Length; j++)
        {
            if (saveData.materialItemList.Contains(mItems[j].itemID))
            {
                int idx = saveData.materialItemList.IndexOf(mItems[j].itemID);
                saveData.mItemCountList[idx] = mItems[idx].itemCount;
            }
            else
            {
                saveData.materialItemList.Add(mItems[j].itemID);
                saveData.mItemCountList.Add(mItems[j].itemCount);
            }
        }

        string json = JsonUtility.ToJson(saveData);



        File.WriteAllText(SAVE_DATA_DIRECTROTY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        StartCoroutine(LoadingData());
    }

    IEnumerator LoadingData()
    {
        go_BackGround.SetActive(true);

        if (File.Exists(SAVE_DATA_DIRECTROTY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTROTY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (theInven == null)
                theInven = FindObjectOfType<Inventory>();
            if (theRecipe == null)
                theRecipe = FindObjectOfType<RecipePage>();
            if (theSC == null)
                theSC = FindObjectOfType<SoundController>();

            for (int i = 0; i < saveData.weaponItemList.Count; i++)
            {
                theInven.LoadWeaponData(saveData.weaponItemList[i], saveData.wItemCountList[i]);
                yield return null;
            }

            for (int x = 0; x < saveData.materialItemList.Count; x++)
            {
                theInven.LoadMaterialData(saveData.materialItemList[x], saveData.mItemCountList[x]);
                yield return null;
            }

            theRecipe.LoadRecipeData(saveData.recipeUnlockList);

            GameManager.money = saveData.money;
            
            theSC.SetSfxVolume(saveData.sfxSoundValue);
            theSC.SetBGMVolume(saveData.bgmSoundValue);

            Debug.Log("로드 완료");
        }
        else
        {
            Debug.Log("저장된 파일이 없습니다.");
        }
        go_BackGround.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
