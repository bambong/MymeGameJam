using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HammerController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector]
    public RectTransform rect;
    [HideInInspector]
    public Vector3 startPos;

    private int prevIndex =0;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        prevIndex = transform.GetSiblingIndex();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartPosUpdate();
        prevIndex = transform.GetSiblingIndex();
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
        foreach(var item in raycastResults)
        {
            if(item.gameObject.CompareTag("Pot"))
            {
                Destroy(item.gameObject);
                break;
            }
            else if(item.gameObject.CompareTag("Maker"))
            {
                Destroy(item.gameObject);
                PrefabsManager.Instance.makerSpawner.ForceGenerate();
                break;
            }
        }
        rect.transform.position = startPos;
        transform.SetSiblingIndex(prevIndex);
    }
   
}
