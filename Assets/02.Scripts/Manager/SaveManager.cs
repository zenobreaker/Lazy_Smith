using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public List<bool> stroyViewList = new List<bool>();
    public List<bool> questBeingList = new List<bool>();
    public List<bool> questClearList = new List<bool>();
}

public class SaveManager : MonoBehaviour
{
    [SerializeField] GameObject go_BackGround = null;

    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTROTY;
    private string SAVE_FILENAME = "/SaveFile.json";

    private Inventory theInven;
    private RecipePage theRecipe;
    private SoundController theSC;
    [SerializeField] InteractionController theIC = null;
    [SerializeField] QuestManager theQuest = null;

    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIRECTROTY = Application.persistentDataPath + "/Saves/";
        //Debug.Log(SAVE_DATA_DIRECTROTY);

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
        bool[] viewArr = theIC.GetViewList().ToArray();
        bool[] beingArr = theQuest.GetQuestBeingList().ToArray();
        bool[] clearArr = theQuest.GetQuestClearList().ToArray();

        saveData.money = GameManager.money;
        saveData.recipeUnlockList = theRecipe.SaveRecipeData();
        saveData.sfxSoundValue = Mathf.Floor(theSC.sfxSlider.value*10)/10;
        saveData.bgmSoundValue = Mathf.Floor(theSC.bgmSlider.value*10)/10;

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

        if (saveData.stroyViewList.Count < 1)
        {
            Debug.Log("저장 길이 0 구축 시작");
            for (int x = 0; x < theIC.GetViewList().Count; x++)
            {
                saveData.stroyViewList.Add(viewArr[x]);
            }
        }else if(saveData.stroyViewList.Count == viewArr.Length)
        {
            for (int i = 0; i < saveData.stroyViewList.Count; i++)
            {
                saveData.stroyViewList[i] = viewArr[i];
            }
        }

        if (saveData.questBeingList.Count < 1)
        {
            for (int y = 0; y < beingArr.Length; y++)
            {
                saveData.questBeingList.Add(beingArr[y]);
            }
        }
        else if(saveData.questBeingList.Count == beingArr.Length)
        {
            for (int i = 0; i < saveData.questBeingList.Count; i++)
            {
                saveData.questBeingList[i] = beingArr[i];
            }
        }

        if(saveData.questClearList.Count < 1)
        {
            for (int z = 0; z < clearArr.Length; z++)
            {
                saveData.questClearList.Add(clearArr[z]);
            }
        }else if(saveData.questClearList.Count == clearArr.Length)
        {
            for (int i = 0; i < saveData.questClearList.Count; i++)
            {
                saveData.questClearList[i] = clearArr[i];
            }
        }

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTROTY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (SceneManager.GetActiveScene().name.Equals("Title"))
            LoadSoundData();
        else 
            StartCoroutine(LoadingData());
    }

    void LoadSoundData()
    {

        if (File.Exists(SAVE_DATA_DIRECTROTY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTROTY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            Debug.Log(saveData.sfxSoundValue + " , " + saveData.bgmSoundValue);
            for (int i = 0; i < SoundManager.instance.sfxPlayer.Length; i++)
            {
                SoundManager.instance.sfxPlayer[i].volume = saveData.sfxSoundValue;
            }

            SoundManager.instance.bgmPlayer.volume = (saveData.bgmSoundValue);
        }
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
            UIManager.instance.SetMoney(saveData.money);
            
            theSC.SetSfxVolume(saveData.sfxSoundValue);
            theSC.SetBGMVolume(saveData.bgmSoundValue);
            theIC.SetViewList(saveData.stroyViewList);
            theQuest.SetQuestDic(saveData.questBeingList,saveData.questClearList);
           
            yield return null;

            Debug.Log("로드 완료");
            theIC.StartFirstStroy();
        }
        else
        {
            GameManager.money = 0;
            UIManager.instance.SetMoney(GameManager.money);
            theIC.StartFirstStroy();
            Debug.Log("저장된 파일이 없습니다.");
        }
        go_BackGround.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Title"))
            SaveData();
    }
}
