using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoPanelController : MonoBehaviour
{

    public MainMenuManager menuManager;
    public CanvasGroup innerPanel;
    public List<GameObject> pages;
    public Animator animator;
    [HideInInspector]
    public int curPage;
    private bool isOpen;
    private readonly string OPEN_ANIM_KEY = "IsOpen";
    private bool isChange;
    public void Open() 
    {
        if(isOpen)
        {
            return;
        }
        isOpen = true;
        curPage = 0;
        UpdatePage();
        gameObject.SetActive(true);
        animator.SetBool(OPEN_ANIM_KEY,true);
        StartCoroutine(OpenInner());
        menuManager.ButtonOn(false);
    }
    public void Close() 
    {
        if(!isOpen)
        {
            return;
        }
        isOpen = false;
        animator.SetBool(OPEN_ANIM_KEY,false);
        StartCoroutine(CloseInner());
        menuManager.ButtonOn(true);
    }
    public void OnCloseEnd()
    {
        gameObject.SetActive(false);
    }
    public void OnNextButtonActive() 
    {
        if(isChange) 
        {
            return;
        }
       
        if(curPage >= pages.Count - 1) 
        {
            curPage = 0;
            Close();
            return;
        }
        curPage++;
        StartCoroutine(ChangePage());
    }

    public void UpdatePage() 
    {
        for(int i= 0; i < pages.Count; ++i) 
        {
            if(i == curPage) 
            {
                pages[i].gameObject.SetActive(true);
                continue;            
            }
            pages[i].gameObject.SetActive(false);
        }
    }

    IEnumerator OpenInner() 
    {
        isChange = true;
        innerPanel.alpha = 0;
       yield return new WaitForSeconds(0.5f);
        float curTime = 0;

        while(curTime < 1)
        {
            curTime += Time.deltaTime;
            innerPanel.alpha = Mathf.Lerp(0,1,curTime);
            yield return null;
        }
        innerPanel.alpha = 1;
        isChange = false;
    }
    IEnumerator CloseInner()
    {
        isChange = true;
        float curTime = 0;

        while(curTime < 1)
        {
            curTime += Time.deltaTime;
            innerPanel.alpha = Mathf.Lerp(1,0,curTime);
            yield return null;
        }
        innerPanel.alpha = 0;
        isChange = false;
    }


    IEnumerator ChangePage() 
    {
        isChange = true;
        float curTime = 0;

        while(curTime < 1)
        {
            curTime += Time.deltaTime ;
            innerPanel.alpha = Mathf.Lerp(1,0,curTime);
            yield return null;
        }
        innerPanel.alpha = 0;
        UpdatePage();
        curTime = 0;
        while(curTime < 1)
        {
            curTime += Time.deltaTime;
            innerPanel.alpha = Mathf.Lerp(0,1,curTime);
            yield return null;
        }
        innerPanel.alpha = 1;
        isChange = false;
    }
}
