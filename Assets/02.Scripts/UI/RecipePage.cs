using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    List<bool> recipeLockList = new List<bool>();       // 레시피 해금 여부 리스트 

    [SerializeField] RecipeAlert ra_AlertUI = null;
    [SerializeField] GameObject go_RacipeUI = null;
    [SerializeField] GameObject go_RecipeContent = null;
    [SerializeField] MaterialQuest[] materialQuests = null;

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
            recipeLockList.Add(true);
            recipeSlots.Add(clone);
        }
    }

    // 레시피 해금 
    public void UnlockRecipe(int _targetNum)
    {
        recipeLockList[_targetNum] = false;
    }

    // 사용가능한 레시피 확인 
    public void CheckUsedRecipes()
    {
        for (int i = 0; i < recipeLockList.Count; i++)
        {
            if (!recipeLockList[i])
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



    public void OpenUI()
    {
        go_BaseUI.SetActive(true);
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
        SetRecipe(_recipeSlot.MyItem.itemID);
        go_RacipeUI.SetActive(true);
        SoundManager.instance.PlaySE("ButtonClick");
    }

    public void HideRecipeUI()
    {
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
            var clone = Instantiate(materialQuests[x], go_RecipeContent.transform);
            Item t_Item = ItemDatabase.instance.GetMetrialItemByID(t_ItemsID[x]);
            clone.SettingUI(t_Item, Inventory.instance.GetMaterialItemCount(t_Item), t_Recipe.each[x]);
            clone.gameObject.SetActive(true);
        }
    }

    // 재료가 다 모였는지 확인 
    public void CheckRecipeUnlock()
    {
        //RecipeManager.instance.CheckUnlockRecipe()
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


}
