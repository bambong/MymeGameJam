using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button[] buttonList;
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color highlightColor = Color.white;

    [SerializeField] HowtoPanelController howToPanel;
    [SerializeField] GameObject CreditPage;
    [SerializeField] GameObject[] cutScenePage;

    private void Start()
    {
        CreditPage.SetActive(false);

        foreach (var item in cutScenePage) 
        {
            item.SetActive(false);
        }

    }

    public void UnityButtonInput_Click_GameStart()
    {
        cutScenePage[0].SetActive(true);
    }
    public void UnityButtonInput_Click_HowTo()
    {
        howToPanel.Open();
    }
    public void UnityButtonInput_Click_Credit()
    {
        CreditPage.SetActive(true);

    }
    public void UnityButtonInput_Click_Exit()
    {
        Debug.Log("Á¾·á");
        Application.Quit();
    }


  
    public void UnityButtonInput_Credit_Page()
    {
        CreditPage.SetActive(false);
    }

    public void UnityButtonInput_CutScene_Page1()
    {
        cutScenePage[1].SetActive(true);
    }
    public void UnityButtonInput_CutScene_Page2()
    {
        cutScenePage[2].SetActive(true);
    }
    public void UnityButtonInput_CutScene_Page3()
    {
        cutScenePage[3].SetActive(true);
    }
    public void UnityButtonInput_CutScene_Page4()
    {
        SceneMangerEx.Instance.LoadScene(SceneType.MainGameScene);
        //UnityEngine.SceneManagement.SceneManager.LoadScene();
    }

    public void ButtonOn(bool isOn) 
    {
        for(int i =0; i < buttonList.Length; ++i) 
        {
            buttonList[i].interactable = isOn;
        }
    }
}
