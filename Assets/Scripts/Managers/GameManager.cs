using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("���� ������")]
    [SerializeField]
    private List<LevelData> levelDatas;
    
    [Header("�ִ� ü��")]
    [SerializeField]
    private float maxHp = 100f;
    
    [HideInInspector]
    public GameStateController stateController;

    public MerchantSpawnController merchantSpawnController;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public Image hpFillImage;

    [Header("UI Parent")]
    public Transform elementMoveParent;
    public Transform resultLayout;
    
    [Header("���� ������ ������")]
    public GraphicRaycaster graphicRaycaster;

    public EndPanelController endPanel;



    public int score;
    private float curHp;
    private int curLevel = 0;
    public int merchantCount; 
    public LevelData CurrentLevelData { get => levelDatas[curLevel]; }

    private void Start()
    {
        Instance = this;
        SoundManager.Instance.PlayAudio_BGM();
        stateController = new GameStateController(this);
        score = 0;
        curLevel = 0;
        curHp = maxHp;
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

    #region Score

    public void AddScore(int amount) 
    {
        score += amount;
        UpdateScoreText();
    }

    public void UpdateScoreText() 
    {
        scoreText.text = $"�� : {score}";
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
        hpFillImage.fillAmount = curHp / maxHp;
    }
    #endregion Hp
    
    public void UpdateLevel() 
    { 
        if(curLevel >= levelDatas.Count -1) 
        {
            return;
        }
        if(levelDatas[curLevel+1].scoreForThisLevel <= score) 
        {
            curLevel++;
        }
    }

}



