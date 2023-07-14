using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultPanelController : MonoBehaviour
{
    public Button menuButton;
    public RectTransform rect;
    public TextMeshProUGUI merchantCountText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultScoreText;

    public readonly float RESULT_TEXT_ANIM_TIME = 2f;
    public readonly float MERCHANT_COUNT_SCORE = 500f;
    public void Open() 
    {
        rect.gameObject.SetActive(true);
    }
    public void Close()
    {
        rect.gameObject.SetActive(false);
    }

    public void StartShowResult() 
    {
        StartCoroutine(ShowResult());
    }

    IEnumerator ShowResult() 
    {
        double curTime = 0;
        double factor = 1/ RESULT_TEXT_ANIM_TIME;
        while(curTime < 1) 
        {
            curTime += Time.deltaTime * factor;
            merchantCountText.text = $"ÃÑ °í°´ ¼ö : {(int)Mathf.Lerp(0,GameManager.Instance.merchantCount,(float)curTime)}";

            yield return null;
        }
        merchantCountText.text = $"ÃÑ °í°´ ¼ö : {GameManager.Instance.merchantCount}";
        curTime = 0;
        while(curTime < 1)
        {
            curTime += Time.deltaTime * factor;
            scoreText.text = $"¸í¼º : {(int)Mathf.Lerp(0,GameManager.Instance.score,(float)curTime)}";

            yield return null;
        }
        curTime = 0;
        scoreText.text = $"¸í¼º : {GameManager.Instance.score}";
        var resultRate = (GameManager.Instance.merchantCount * MERCHANT_COUNT_SCORE) + GameManager.Instance.score;
        while(curTime < 1)
        {
            curTime += Time.deltaTime * factor;
            resultScoreText.text = $"ÃÖÁ¾Á¡¼ö : {(int)Mathf.Lerp(0,resultRate,(float)curTime)}";

            yield return null;
        }
        resultScoreText.text = $"ÃÖÁ¾Á¡¼ö : {resultRate}";
        yield return new WaitForSeconds(1f);
        menuButton.gameObject.SetActive(true);

    }

    public void OnMenuButtonActive() 
    {
        SoundManager.Instance.PlayAudio_Select();
        SceneMangerEx.Instance.LoadScene(SceneType.MainGameScene);
        menuButton.interactable = false;
    }

}
