using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllRecipesData",menuName = "Scriptable Object/AllRecipesData",order = int.MaxValue)]
public class AllRecipesData : ScriptableObject
{
    public List<RecipeData> recipeDatas;
    
}