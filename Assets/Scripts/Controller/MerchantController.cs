using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantController : MonoBehaviour
{
    public RectTransform rect;
    [SerializeField]
    private Animator animator;

    [Header("Think BOX")]
    [SerializeField]
    private Image thinkBox;
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private Transform potImageParent;
    
    [Header("점수 얼마나 줄지")]
    public int scoreAmount = 30;

    [Header("얼마나 기다려 줄지")]
    public float waitTime = 10f;

    public float moveSpeed = 10;


    PotController desirePot;
    public Vector2 desirePos;
    private Coroutine waitCo;

    private bool isSpawning = true;

    private bool isComplete = false;

    private float damage = 10f; 

    public int MyIndex { get; set; }

  
    private readonly string MOVE_ANIM_KEY = "IsMove";
    private readonly string RUNAWAY_ANIM_KEY = "IsRun";

    public void OnSpawn(Vector2 desirePos,PotController pot)
    {
        thinkBox.gameObject.SetActive(false);
        SetAnimMove();
        PickRandomPot();
        this.desirePos = desirePos;
        StartCoroutine(SpawnMove(desirePos));
    }

    public void PickRandomPot()
    {
        var pot = PrefabsManager.Instance.potShapePrefabDatas[PrefabsManager.Instance.potShapePrefabDatas.GetRandomIndex()].resultMakerGo;

        desirePot = Instantiate(pot,potImageParent.transform.position,Quaternion.identity,potImageParent.transform).GetComponent<PotController>();
        
        for(int i=0; i < desirePot.images.Count; ++i) 
        {
            var randomElement = PrefabsManager.Instance.shapeElementDatas[PrefabsManager.Instance.shapeElementDatas.GetRandomIndex()];

            desirePot.elementTypes.Add(randomElement.type);
            desirePot.images[i].sprite = randomElement.sprite;
        }
        desirePot.tag = "Untagged";
        desirePot.enabled = false;

    }
    public bool CheckCorrectPot(PotController pot) 
    {
        if(pot.myType != desirePot.myType) 
        {
            return false;
        }

        for(int i = 0; i < pot.elementTypes.Count; ++i)
        {
            if(pot.elementTypes[i] != desirePot.elementTypes[i])
            {
                return false;
            }
        }
        return true;
        
    }

    public bool OnDrop(PotController pot) 
    {
        if(isComplete || isSpawning) 
        {
            return false;
        }

        if(!CheckCorrectPot(pot)) 
        {
            return false;
        }
        if(waitCo != null) 
        {
            StopCoroutine(waitCo);
            waitCo = null;
        }


        GameManager.Instance.AddScore(scoreAmount);
        SetAnimRunAway();
        return true;
    }

    private void Update()
    {
        if(!isComplete && !isSpawning) 
        {
            var dir = desirePos - rect.anchoredPosition;
            var moveDis = Time.deltaTime * dir.normalized * moveSpeed;
            if(dir.magnitude <= moveDis.magnitude)
            {
                SetAnimIdle();
                rect.anchoredPosition = desirePos;
                return;
            }
            SetAnimMove();
            rect.anchoredPosition += moveDis;
        }
    }

    public void SetAnimMove() 
    {
        animator.SetBool(MOVE_ANIM_KEY,true);
    }
    public void SetAnimIdle()
    {
        animator.SetBool(MOVE_ANIM_KEY,false);
    }
    public void SetAnimRunAway()
    {
        animator.SetBool(RUNAWAY_ANIM_KEY,true);
        GameManager.Instance.merchantSpawnController.DestroyMerChantCount(this);
        thinkBox.gameObject.SetActive(false);
    }

    public IEnumerator Wait() 
    {
        SetAnimIdle();
        double curTime = 0;
        double factor = 1/ waitTime;

        while(curTime < 1)
        {
            curTime += (double)Time.deltaTime * factor;
            fillImage.fillAmount = (float)curTime;
            yield return null;
        }
        GameManager.Instance.DecreaseHp(damage);

        SetAnimRunAway();
        waitCo = null;

    }
    public IEnumerator SpawnMove(Vector2 desirePos)
    {
        SetAnimMove();
        while(true)
        {
            var dir = desirePos - rect.anchoredPosition;
            var moveDis = Time.deltaTime * dir.normalized * moveSpeed;
            if(dir.magnitude <= moveDis.magnitude)
            {
                rect.anchoredPosition = desirePos;
                break;
            }
            rect.anchoredPosition += moveDis;
            
            yield return null;
        }
        isSpawning = false;
        SetAnimIdle();
        thinkBox.gameObject.SetActive(true);
        waitCo = StartCoroutine(Wait());
    }




    public void OnRunAway() 
    {
        Destroy(gameObject);
    }


}
