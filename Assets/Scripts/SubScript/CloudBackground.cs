using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBackground : MonoBehaviour
{
    [SerializeField] RectTransform[] objectList;
    float[] times = { 0, 0.3f, 0.6f };
    [SerializeField] Vector2[] objectPosList;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for(int index=0; index < 3; ++index)
        {
            objectList[index].anchoredPosition = Vector2.Lerp(Vector2.up* objectPosList[index].y, objectPosList[index], Mathf.Repeat(times[index],1) );
            times[index] += Time.deltaTime*0.1f;
        }




    }
}
