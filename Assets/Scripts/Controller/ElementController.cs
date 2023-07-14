using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum ElementType 
{
    None,
    Type_1,
    Type_2,
    Type_3,
    Type_4
}


public class ElementController : MonoBehaviour ,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public ElementType myType;
    [HideInInspector]
    public RectTransform rect;
    [HideInInspector]
    public Vector3 startPos;
    [HideInInspector]
    public Transform prevParent;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        prevParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartPosUpdate();
        prevParent = transform.parent;
        transform.SetParent(GameManager.Instance.elementMoveParent);
        transform.SetAsLastSibling();
    }
  
    public void OnDrag(PointerEventData eventData)
    {
        rect.transform.position = eventData.position;
    }

    private void StartPosUpdate() 
    {
        startPos = rect.transform.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        var gr = GameManager.Instance.graphicRaycaster;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        gr.Raycast(eventData,raycastResults);
        foreach(var item  in raycastResults) 
        {
            if(item.gameObject.CompareTag("Frame")) 
            {
                item.gameObject.GetComponent<FrameController>().OnDrop(this);
                return;
            }
        }
        rect.transform.position = startPos;
        rect.transform.SetParent(prevParent);
    }


 






}
