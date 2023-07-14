using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType 
{
    MainMenuScene,
    MainGameScene
}
public class SceneMangerEx : Singleton<SceneMangerEx>,IInit
{
    private TransitionPanelController transition;
    public TransitionPanelController Transition 
    {
        get 
        {
            if(transition == null) 
            {
                var go = GameObject.Instantiate((GameObject)Resources.Load("UI/TransitionPanel"));
                GameObject.DontDestroyOnLoad(go);
                transition = go.GetComponent<TransitionPanelController>();
            }
            return transition;
        }
    }
    public bool isSceneLoading = false;

    public void Init()
    {
    }

    public void LoadScene(SceneType type,Action onComplete = null) 
    {
        if(Transition.isOpen) 
        {
            return;
        }
        Transition.StartCoroutine(ChangeSceneCo(type,onComplete));
    }
    public IEnumerator ChangeSceneCo(SceneType type,Action onComplete = null) 
    {
        isSceneLoading = true;
        Transition.Open();
        yield return new WaitForSeconds(transition.transitionTime);

        var async = SceneManager.LoadSceneAsync(type.ToString());
        while(async.progress < 0.9f) 
        {
            yield return null;
        }
        async.allowSceneActivation = true;

        while(!async.isDone)
        {
            yield return null;
        }
        Transition.Close();
        yield return new WaitForSeconds(transition.transitionTime);
        onComplete?.Invoke();
        isSceneLoading = false;
    }

}
