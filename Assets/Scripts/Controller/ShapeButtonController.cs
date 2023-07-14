using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeButtonController : MonoBehaviour
{
    public PotShapeType myType;
    [HideInInspector]
    public Image image;
    public Button button;
    private void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void ButtonUpdate() 
    {
        if(PrefabsManager.Instance.baseMakerSpawner.curBase  == null || PrefabsManager.Instance.baseMakerSpawner.curBase.myType != myType) 
        {
            image.color = Color.white;
            button.interactable = true;
        }
        else 
        {
            image.color = Color.gray;
            button.interactable = false;
        }
    }

    public void ButtonActive() 
    {
        PrefabsManager.Instance.baseMakerSpawner.Generate(myType);
    }

}
