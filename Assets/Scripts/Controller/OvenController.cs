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
    private Animator ovenAnimator;

    [SerializeField]
    private Button completeButton;
    [SerializeField]
    public Image colorImage;

    [HideInInspector]
    public bool isBaking = false;

    public BakeRank curRank = BakeRank.None;
    
    public PotShapeType curpotType;
    public List<ElementType> curElementTypes;
  
    
    public Color normalColor;
    public Color bakingColor;
    public Color bakecompleteColor;
    public Color burnColor;


    public Coroutine bakeCo;

    public readonly float BAKE_TIME = 3f;
    public readonly float GOOD_STAY_TIME = 2f;
    public readonly float BAKE_OVER_TIME = 2f;
    public readonly string WORK_ANIM_KEY = "IsWork";
    public readonly string WORK_SPEED_KEY = "WorkSpeed";

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
     
        curpotType = maker.myType;
        curElementTypes = new List<ElementType>();
        for(int i =0; i < maker.frames.Count; ++i) 
        {
            if(maker.frames[i].curElementType == ElementType.None)
            {
                return false;
            }

            curElementTypes.Add(maker.frames[i].curElementType);
        }

        isBaking = true;
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
        ovenAnimator.SetBool(WORK_ANIM_KEY,false);
        completeButton.gameObject.SetActive(false);
        colorImage.color = normalColor;
        SoundManager.Instance.PlayAudio_Select();
        if(curRank == BakeRank.Burn) 
        {
            SoundManager.Instance.PlayAudio_Effect_OvenOvercook();
            return;
        }
        SoundManager.Instance.PlayAudio_Effect_OvenSuccess();

        var result = PrefabsManager.Instance.potPrefabs[curpotType].resultMakerGo;

        var tempPot = Instantiate(result,Vector3.zero,Quaternion.identity,GameManager.Instance.resultLayout).GetComponent<PotController>();

        for(int i = 0; i < curElementTypes.Count; ++i)
        {
            tempPot.elementTypes.Add(curElementTypes[i]);
            tempPot.images[i].sprite = PrefabsManager.Instance.elementPrefabs[curElementTypes[i]].sprite;
        }
    }


    IEnumerator Bake() 
    {
        SoundManager.Instance.PlayAudio_Effect_OvenEnter();
        ovenAnimator.SetBool(WORK_ANIM_KEY,true);
        ovenAnimator.SetFloat(WORK_SPEED_KEY,1);
        double curTime = 0;
        double factor = 1 / BAKE_TIME;
        colorImage.color = bakingColor;
        while(curTime < 1) 
        {
            curTime += Time.deltaTime * factor;
            ovenAnimator.SetFloat(WORK_SPEED_KEY,1 + (float)curTime);
            colorImage.color = Color.Lerp(bakingColor,bakecompleteColor,(float)curTime);
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
