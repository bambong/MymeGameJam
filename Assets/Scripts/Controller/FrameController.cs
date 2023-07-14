using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameController : MonoBehaviour
{

    [HideInInspector]
    public RectTransform rect;
    [HideInInspector]
    public ElementController curElement;

    public ElementType curElementType 
    {
        get 
        {
            if(curElement == null) 
            {
                return ElementType.None;
            }
            return curElement.myType;
        }
    
    }

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
        element.transform.SetParent(transform);
    }
}
