using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPanelController : MonoBehaviour
{
    public float transitionTime = 2f;
    public CanvasGroup canvasGroup;
    public bool isOpen;

  
    public void Open() 
    {
        if(isOpen) 
        {
            return;
        }
        isOpen = true;
        StartCoroutine(OpenCo());
    }
    public void Close()
    {
        if(!isOpen)
        {
            return;
        }
        isOpen =false;
        StartCoroutine(CloseCo());
    }
    IEnumerator OpenCo() 
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 0;
        double curTime = 0;
        double factor = 1 / transitionTime;
        while(curTime < 1)
        {
            curTime += Time.deltaTime * factor;
            canvasGroup.alpha = Mathf.Lerp(0,1,(float)curTime);

            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    IEnumerator CloseCo()
    {
        canvasGroup.alpha = 1;
        double curTime = 0;
        double factor = 1 / transitionTime;
        while(curTime < 1)
        {
            curTime += Time.deltaTime * factor;
            canvasGroup.alpha = Mathf.Lerp(1,0,(float)curTime);

            yield return null;
        }
        canvasGroup.alpha = 0;
        canvasGroup.interactable =false;
        canvasGroup.blocksRaycasts = false;
    }

}
