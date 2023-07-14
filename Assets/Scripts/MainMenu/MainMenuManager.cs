using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button[] buttonList;
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color highlightColor = Color.white;

    [SerializeField] GameObject[] HowToPage;
    [SerializeField] GameObject CreditPage;
    [SerializeField] GameObject[] cutScenePage;

    private void Start()
    {
        CreditPage.SetActive(false);
        foreach (var item in HowToPage)
        {
            item.SetActive(false);
        }
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
        HowToPage[0].SetActive(true);
        HowToPage[1].SetActive(true);
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


    public void UnityButtonInput_HowTo_Page1()
    {
        HowToPage[0].SetActive(false);
    }
    public void UnityButtonInput_HowTo_Page2()
    {
        HowToPage[1].SetActive(false);
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
}
