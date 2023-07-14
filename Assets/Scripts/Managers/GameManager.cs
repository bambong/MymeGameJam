using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    
    
    [HideInInspector]
    public GameStateController stateController;

    public MerchantSpawnController merchantSpawnController;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public Image hpFillImage;


    [Header("UI Parent")]
    public Transform elementMoveParent;
    public Transform resultLayout;
    
    [Header("조합 레시피 데이터")]
    public GraphicRaycaster graphicRaycaster;

    private readonly float MAX_HP = 100f;

    private int score;
    private float curHp;

    private void Start()
    {
        Instance = this;
        stateController = new GameStateController(this);
        score = 0;
        curHp = MAX_HP;
        UpdateHpImage();
        UpdateScoreText();
        stateController.ChangeState(GamePlay.Instance);
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
            Debug.Log($"{i} 번째 슬롯 :{maker.frames[i].curElementType}");
        }

        //foreach(var recipe in allRecipesData.recipeDatas)
        //{

        //    bool isSuccess = false;
        //    for(int i =0; i < maker.frames.Count; ++i) 
        //    {
        //        if(recipe.typeRecipe[i] != maker.frames[i].curElementType) 
        //        {
        //            isSuccess = false;
        //            break;
        //        }
        //        isSuccess = true;
        //    }
        //    if(isSuccess) 
        //    {
        //        return recipe.resultPot;
        //    }
                
        //}
        return null;

    }

    #region Score

    public void AddScore(int amount) 
    {
        score += amount;
        UpdateScoreText();
    }

    public void UpdateScoreText() 
    {
        scoreText.text = $"명성 : {score}";
    }
    #endregion Score
    #region Hp

    public void DecreaseHp(float amount)
    {
        curHp -= amount;
        UpdateHpImage();
        if(curHp <= 0) 
        {
            stateController.ChangeState(GameOver.Instance);
            Debug.Log("GAME OVER");
        }
    }

    public void UpdateHpImage()
    {
        hpFillImage.fillAmount = curHp / MAX_HP;
    }
    #endregion Hp


}



