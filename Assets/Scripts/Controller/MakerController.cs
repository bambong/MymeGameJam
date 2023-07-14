using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MakerController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector]
    public RectTransform rect;
    [HideInInspector]
    public Vector3 startPos;

    public List<FrameController> frames;


    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartPosUpdate();
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
            if(item.gameObject.CompareTag("Oven"))
            {
                if(item.gameObject.GetComponent<OvenController>().OnDrop(this)) 
                {
                    Destroy(gameObject);
                    PrefabsManager.Instance.makerSpawner.ForceGenerate();
                    return;
                }
            }
            else if(item.gameObject.CompareTag("Hammer"))
            {
                Destroy(gameObject);
                PrefabsManager.Instance.makerSpawner.ForceGenerate();
                return;
            }

        }
        rect.transform.position = startPos;
    }

}
