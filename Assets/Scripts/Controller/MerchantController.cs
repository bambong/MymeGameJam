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
    private Image potImage;


    
    [Header("점수 얼마나 줄지")]
    public int scoreAmount = 30;

    [Header("얼마나 기다려 줄지")]
    public float waitTime = 10f;

    public float moveSpeed = 10;

    [HideInInspector]
    public PotType desirePot;

    private Coroutine waitCo;

    private bool isSpawning = true;

    private bool isComplete = false;

    private float damage = 10f; 

    public int MyIndex { get; set; }

    private readonly string IDLE_ANIM_KEY = "Merchant_Anim_Idle";
    private readonly string MOVE_ANIM_KEY = "Merchant_Anim_Move";
    private readonly string RUNAWAY_ANIM_KEY = "Merchant_Anim_Runaway";

    public void OnSpawn(Vector3 desirePos, PotController pot)
    {
        thinkBox.gameObject.SetActive(false);
        SetAnimMove();
        potImage.sprite = pot.image.sprite;
        desirePot = pot.myType;
        StartCoroutine(SpawnMove(desirePos));
    }
    public bool OnDrop(PotController pot) 
    {
        if(isComplete || isSpawning) 
        {
            return false;
        }

        if(desirePot != pot.myType) 
        {
            return false;
        }

        GameManager.Instance.AddScore(scoreAmount);
        SetAnimRunAway();
        return true;

    }

    public void SetAnimMove() 
    {
        animator.Play(MOVE_ANIM_KEY);
    }
    public void SetAnimIdle()
    {
        animator.Play(IDLE_ANIM_KEY);
    }
    public void SetAnimRunAway()
    { 
        animator.Play(RUNAWAY_ANIM_KEY);
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
        GameManager.Instance.merchantSpawnController.DecreaseMerChantCount();
    }


}
