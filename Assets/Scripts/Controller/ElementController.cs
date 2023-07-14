using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementController : MonoBehaviour ,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public RectTransform rect;

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
  
    public void OnDrag(PointerEventData eventData)
    {
        rect.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
    }


    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }







}
