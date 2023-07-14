using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameController : MonoBehaviour
{

    [HideInInspector]
    public RectTransform rect;
    [HideInInspector]
    public ElementController curElement;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnDrop(ElementController element) 
    {
        if(curElement != null) 
        {
            Destroy(curElement);
        }
        curElement = element;
        element.rect.transform.position = rect.transform.position;
    }
}
