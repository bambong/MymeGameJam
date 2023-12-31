using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PotShapeType 
{
    Type_1,
    Type_2,
    Type_3,
    Type_4
}


public class PotController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public PotShapeType myType;
    [HideInInspector]
    public RectTransform rect;
    [HideInInspector]
    public Vector3 startPos;
    [HideInInspector]
    public Transform prevParent;

    public List<Image> images;
    public List<ElementType> elementTypes = new List<ElementType>();

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        prevParent = transform.parent;
    }
    public void Spawn(MakerController maker) 
    {
        for(int i= 0; i < maker.frames.Count; ++i)
        {
            elementTypes.Add(maker.frames[i].curElementType);
            images[i].sprite = PrefabsManager.Instance.elementPrefabs[maker.frames[i].curElementType].sprite;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        SoundManager.Instance.PlayAudio_Select();
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
        foreach(var item in raycastResults)
        {
            if(item.gameObject.CompareTag("Merchant"))
            {
                if(item.gameObject.GetComponent<MerchantHitController>().OnDrop(this)) 
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }
        SoundManager.Instance.PlayAudio_Error();
        rect.transform.position = startPos;
        rect.transform.SetParent(prevParent);
    }

}
