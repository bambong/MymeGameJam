using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    
    public GameStateController stateController;
    public Transform elementMoveParent;
    public GraphicRaycaster graphicRaycaster;

    public AllRecipesData allRecipesData;
    private void Start()
    {
        Instance = this;
        stateController = new GameStateController(this);
    }

    private void Update()
    {
        stateController.UpdateActive();
    }
    private void FixedUpdate()
    {
        stateController.FixedUpdateActive();
    }


    public GameObject CheckRecipes(MakerController maker) 
    {

        for(int i =0; i < maker.frames.Count; ++i) 
        {
            Debug.Log($"{i} ¹øÂ° ½½·Ô :{maker.frames[i].curElementType}");
        }

        foreach(var recipe in allRecipesData.recipeDatas)
        {

            bool isSuccess = false;
            for(int i =0; i < maker.frames.Count; ++i) 
            {
                if(recipe.typeRecipe[i] != maker.frames[i].curElementType) 
                {
                    isSuccess = false;
                    break;
                }
                isSuccess = true;
            }
            if(isSuccess) 
            {
                return recipe.resultPot;
            }
                
        }
        return null;

    }
    
}



