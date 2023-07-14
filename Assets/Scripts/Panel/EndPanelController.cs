using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanelController : MonoBehaviour
{
    public GameObject enterPanel;
    public ResultPanelController resultPanel;
    public CanvasGroup innerPanel;

    public void Open() 
    {
        gameObject.SetActive(true);
    }

    public void Close() 
    {
        gameObject.SetActive(false);
    }
    public void ChangeResultPanel() 
    {
        StartCoroutine(ChangeResultPanelCo());
    }

    IEnumerator ChangeResultPanelCo() 
    {
        float curTime = 0;
      
        while(curTime < 1)
        {
            curTime += Time.deltaTime;
            innerPanel.alpha = Mathf.Lerp(1,0,curTime);
            yield return null;
        }
        innerPanel.alpha = 0;
        curTime = 0;
        enterPanel.gameObject.SetActive(false);
        resultPanel.Open();
        while(curTime < 1)
        {
            curTime += Time.deltaTime;
            innerPanel.alpha = Mathf.Lerp(0,1,curTime);
            yield return null;
        }
        innerPanel.alpha = 1;
        yield return new WaitForSeconds(0.5f);
        resultPanel.StartShowResult();

    }
}
