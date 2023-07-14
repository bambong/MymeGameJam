using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BakeRank 
{
    None,
    Good,
    Burn
}



public class OvenController : MonoBehaviour 
{

    [HideInInspector]
    public bool isBaking = false;

    public BakeRank curRank = BakeRank.None;
    public GameObject curPot;
    private Image image;
  
    
    public Color normalColor;
    public Color bakecompleteColor;
    public Color burnColor;



    public static float BAKE_TIME = 3f;
    public static float GOOD_STAY_TIME = 2f;
    public static float BAKE_OVER_TIME = 2f;

    
    private void Start()
    {
        image = GetComponent<Image>();
    }

    public bool OnDrop(MakerController maker) 
    {
        if(isBaking) 
        {
            return false;
        }
        isBaking = true;
        var resultPot =  GameManager.Instance.CheckRecipes(maker); 
        if(resultPot == null)
        {
            return false;
        }
        curPot = resultPot;
        StartCoroutine(Bake());
        return true;
    }
    public void BakeComplete() 
    {

    
    }


    IEnumerator Bake() 
    {
        double curTime = 0;
        double factor = 1 / BAKE_TIME;

        while(curTime < 1) 
        {
            curTime += Time.deltaTime * factor;
            image.color = Color.Lerp(normalColor,bakecompleteColor,(float)curTime);
            yield return null;
        }
        image.color = bakecompleteColor;
        curRank = BakeRank.Good;
        curTime = 0;
        factor = 1 / GOOD_STAY_TIME;
        while(curTime < 1)
        {
            curTime += Time.deltaTime * factor;
            yield return null;
        }
        curTime = 0;
        factor = 1 / BAKE_OVER_TIME;
        while(curTime < 1)
        {
            curTime += Time.deltaTime * factor;
            image.color = Color.Lerp(bakecompleteColor,burnColor,(float)curTime);
            yield return null;
        }
        curRank = BakeRank.Burn;
    }
   
}
