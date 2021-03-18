using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string ItemID;
    public string[] matrerialID;
    public int[] each;
}

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;

    public List<Recipe> recipeList = new List<Recipe>(); 
    public Dictionary<string, Recipe> recipeDic = new Dictionary<string, Recipe>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }

    public void Start()
    {
        SetRecipeList();
    }

    public void SetRecipeList()
    {
        List<Item> weapons = ItemDatabase.instance.GetWeapons();

        for (int i = 0; i < weapons.Count; i++)
        {
            for (int x = 0; x < recipeList.Count; x++)
            {
                if (weapons[i].itemID.Equals(recipeList[x].ItemID))
                    recipeDic.Add(weapons[i].itemID, recipeList[x]);
            }
        }
    }


    public Recipe GetRecipe(string p_itemID)
    {
        Debug.Log("아이디 검색 " + p_itemID);
        return recipeDic[p_itemID];
    }

    public Recipe[] GetRecipes()
    {
        Recipe[] t_recipe = new Recipe[recipeDic.Values.Count];
        recipeDic.Values.CopyTo(t_recipe,0);
        return t_recipe;
    }
    
}
