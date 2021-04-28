using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RecipeItem
{
    public Item item;
    public int count;
}
public class RecipePage : MonoBehaviour
{
    public static RecipePage instance;

    [SerializeField] GameObject go_BaseUI = null;
    [SerializeField] GameObject go_ParentPanel = null;
    [SerializeField] RecipeSlot recipeSlot = null;
    //[SerializeField] ItemDatabase itemDatabase = null;

    public List<RecipeItem> recipeItems = new List<RecipeItem>();
    public List<RecipeSlot> recipeSlots = new List<RecipeSlot>();
    public List<bool> recipeUnLockList = new List<bool>();       // 레시피 해금 여부 리스트 

    [SerializeField] RecipeAlert ra_AlertUI = null;

    [Header("레시피 UI")]
    [SerializeField] GameObject go_RacipeUI = null;
    [SerializeField] Button btn_confirm = null;

    [Space(10)]
    [SerializeField] GameObject go_RecipeContent = null;
    [SerializeField] MaterialQuest[] materialQuests = null;


    [Header("퀘스트 경보창")]
    [SerializeField] GameObject questAlert = null;
    [SerializeField] Text txt_alert = null; 

    RecipeSlot currentRSlot;

    
    [SerializeField] InteractionController theIC = null;
    [SerializeField] QuestManager theQuest = null;

    // 레시피 리스트에 아이템 추가 
    void SetRecipeList()
    {
        List<Item> _items = ItemDatabase.instance.GetWeapons();

        for (int i = 0; i < _items.Count; i++)
        {
            RecipeItem recipeItem = new RecipeItem();
            recipeItem.item = _items[i];
            recipeItems.Add(recipeItem);
        }
    }

    // 슬롯 생성 후 슬롯 리스트에 추가 
    void SetSlotAndList()
    {
        for (int i = 0; i < recipeItems.Count; i++)
        {
            RecipeSlot clone = Instantiate(recipeSlot, Vector3.zero, Quaternion.identity);
            clone.transform.SetParent(go_ParentPanel.transform);
            clone.SetItem(recipeItems[i].item);
            clone.LockedSlot();
            recipeUnLockList.Add(true);
            recipeSlots.Add(clone);
        }
    }

    // 레시피 해금 
    public void UnlockRecipe(int _targetNum)
    {
        recipeUnLockList[_targetNum] = false;
        if(_targetNum == 2)
            theIC.ShowDialogue(2);   // 2번 퀘스트 해금 
    }

    public bool CheckQuestClear(string p_wID)
    {
        switch(p_wID)
        {
            case "102":
            case "103":
                if (!theQuest.GetisClear(1))
                {
                    txt_alert.text = "퀘스트 \" 감각 되찾기 \" 클리어";
                    return false;
                }
                return true;
            case "104":
            case "105":
            case "106":
                if (!theQuest.GetisClear(3))
                {
                    txt_alert.text = "퀘스트 \"손님 맞이\" 클리어";
                    return false;
                }
                return true;
            case "107": case "108": case "109":
                if (!theQuest.GetisClear(5))
                {
                    txt_alert.text = "퀘스트 \"게으름\" 클리어";
                    return false;
                }
                return true;
        }

        return true;
    }

    // 사용가능한 레시피 확인 
    public void CheckUsedRecipes()
    {
        for (int i = 0; i < recipeUnLockList.Count; i++)
        {
            if (!recipeUnLockList[i])
            {
                recipeSlots[i].UnlockedSlot();
            }
            else
                recipeSlots[i].LockedSlot();
        }
    }

    // 제작하러 가기 
    public void GotoCraft(int p_num = 0)
    {
        HideUI();
        GameManager.instance.StartGame(p_num);
        SoundManager.instance.PlaySE("ButtonClick");
    }

    public void GotoTimeAtack()
    {
        HideUI();
        GameManager.instance.StartTimeAttack();
        SoundManager.instance.PlaySE("ButtonClick");
    }

    public void OpenUI()
    {
        go_BaseUI.SetActive(true);
        CheckUsedRecipes();
        SoundManager.instance.PlaySE("ButtonClick");
    }

    public void HideUI()
    {
        go_BaseUI.SetActive(false);
        ra_AlertUI.HideUI();
        go_RacipeUI.SetActive(false);
        SoundManager.instance.PlaySE("ButtonClick");
    }

    public void OpenAlertUI(RecipeSlot _recipeSlot)
    {
        ra_AlertUI.OpneUI(_recipeSlot);
        ra_AlertUI.SetCount(recipeSlots.FindIndex(x => x == _recipeSlot));
       
        SoundManager.instance.PlaySE("ButtonClick");
    }

    public void OpenRecipeUI(RecipeSlot _recipeSlot)
    {
        currentRSlot = _recipeSlot;
        SetRecipe(_recipeSlot.MyItem.itemID);
        if (!CheckQuestClear(_recipeSlot.MyItem.itemID))
        {
            questAlert.SetActive(true);
            btn_confirm.interactable = false;
        }
        else
        {
            questAlert.SetActive(false);
            btn_confirm.interactable = true;
        }
        go_RacipeUI.SetActive(true);
        SoundManager.instance.PlaySE("ButtonClick");
    }

    public void HideRecipeUI()
    {
        currentRSlot = null;
        go_RacipeUI.SetActive(false);
        ClearMaterialList();
        SoundManager.instance.PlaySE("ButtonClick");
    }


    public void SetRecipe(string p_ItemID)
    {
        Recipe t_Recipe = RecipeManager.instance.GetRecipe(p_ItemID);

        
        string[] t_ItemsID = t_Recipe.matrerialID;

        for (int x = 0; x < t_ItemsID.Length; x++)
        {
          //  var clone = Instantiate(materialQuests[x], go_RecipeContent.transform);
            Item t_Item = ItemDatabase.instance.GetMetrialItemByID(t_ItemsID[x]);
            materialQuests[x].SettingUI(t_Item, Inventory.instance.GetMaterialItemCount(t_Item), t_Recipe.each[x]);
            materialQuests[x].gameObject.SetActive(true);
        }
    }

    // 재료가 다 모였는지 확인 
    public void CheckRecipeUnlock()
    {
        if (RecipeManager.instance.CheckUnlockRecipe(currentRSlot.MyItem.itemID))
        {
            currentRSlot.UnlockedSlot();
            UnlockRecipe(recipeSlots.IndexOf(currentRSlot));
            RecipeManager.instance.UsedRecipeMaterial(currentRSlot.MyItem.itemID);
            HideRecipeUI();
        }
    }

    public void ClearMaterialList()
    {
        for (int i = 0; i < materialQuests.Length; i++)
        {
            materialQuests[i].gameObject.SetActive(false);
        }
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetRecipeList();
        SetSlotAndList();
        UnlockRecipe(0);
        CheckUsedRecipes();
        ClearMaterialList();
        //SetRecipes();
    }


    public List<bool> SaveRecipeData()
    {
        return recipeUnLockList;
    }

    public void LoadRecipeData(List<bool> p_list)
    {
        recipeUnLockList = p_list;
        CheckUsedRecipes();
    }
}
