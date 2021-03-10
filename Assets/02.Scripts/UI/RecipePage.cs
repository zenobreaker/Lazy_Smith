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
    [SerializeField] ItemDatabase itemDatabase = null;

    public List<RecipeItem> recipeItems = new List<RecipeItem>();
    public List<RecipeSlot> recipeSlots = new List<RecipeSlot>();

    void SetRecipeList()
    {
        List<Item> _items = itemDatabase.GetWeapons();

        for (int i = 0; i < _items.Count; i++)
        {
            RecipeItem recipeItem = new RecipeItem();
            recipeItem.item = _items[i];
            recipeItems.Add(recipeItem);
        }
    }

    void SetSlotandList()
    {
        for (int i = 0; i < recipeItems.Count; i++)
        {
            RecipeSlot clone = Instantiate(recipeSlot, Vector3.zero, Quaternion.identity);
            clone.transform.SetParent(go_ParentPanel.transform);
            clone.SetItem(recipeItems[i].item);
            recipeSlots.Add(clone);
        }
    }



    public void OpenUI()
    {
        go_BaseUI.SetActive(true);
    }

    public void HideUI()
    {
        go_BaseUI.SetActive(false);
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
        SetSlotandList();
    }


}
