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

    [SerializeField]
    private Button completeButton;
    [SerializeField]
    public Image colorImage;

    [HideInInspector]
    public bool isBaking = false;

    public BakeRank curRank = BakeRank.None;
    public GameObject curPot;
  
    
    public Color normalColor;
    public Color bakecompleteColor;
    public Color burnColor;


    public Coroutine bakeCo;

    public static float BAKE_TIME = 3f;
    public static float GOOD_STAY_TIME = 2f;
    public static float BAKE_OVER_TIME = 2f;

    
    private void Start()
    {
        completeButton.gameObject.SetActive(false);
        completeButton.onClick.AddListener(BakeComplete);
    }

    public bool OnDrop(MakerController maker) 
    {
        if(isBaking) 
        {
            return false;
        }
        var resultPot =  GameManager.Instance.CheckRecipes(maker); 
        if(resultPot == null)
        {
            return false;
        }
        isBaking = true;
        curPot = resultPot;
        bakeCo = StartCoroutine(Bake());
        return true;
    }
    public void BakeComplete() 
    {
        if(!isBaking)
        {
            return;
        }

        if(bakeCo != null) 
        {
            StopCoroutine(bakeCo);
        }
        isBaking = false;
        completeButton.gameObject.SetActive(false);
        colorImage.color = normalColor;
        
        if(curRank == BakeRank.Burn) 
        {
            return;
        }

        GameObject.Instantiate(curPot,Vector3.zero,Quaternion.identity,GameManager.Instance.resultLayout.transform);
    }


    IEnumerator Bake() 
    {
        double curTime = 0;
        double factor = 1 / BAKE_TIME;

        while(curTime < 1) 
        {
            curTime += Time.deltaTime * factor;
            colorImage.color = Color.Lerp(normalColor,bakecompleteColor,(float)curTime);
            yield return null;
        }
        completeButton.gameObject.SetActive(true);
        colorImage.color = bakecompleteColor;
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
            colorImage.color = Color.Lerp(bakecompleteColor,burnColor,(float)curTime);
            yield return null;
        }
        curRank = BakeRank.Burn;
        bakeCo = null;
    }
   
}
