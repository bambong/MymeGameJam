using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MakerController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public PotShapeType myType;
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
        SoundManager.Instance.PlayAudio_Select();
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
                    PrefabsManager.Instance.baseMakerSpawner.DestroyCurBase();
                    SoundManager.Instance.PlayAudio_Drop();
                    return;
                }

            }
            else if(item.gameObject.CompareTag("Hammer"))
            {
                SoundManager.Instance.PlayAudio_Destroy();
                PrefabsManager.Instance.baseMakerSpawner.DestroyCurBase();
                return;
            }

        }
        SoundManager.Instance.PlayAudio_Error();
        rect.transform.position = startPos;
    }

}
