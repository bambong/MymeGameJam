using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpotController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    private void Start()
    {
        Generate();
    }

    public void Generate() 
    {
        if(transform.childCount > 0) 
        {
            return;
        }

        Instantiate(prefab,transform.position,Quaternion.identity,transform);
    }
    public void ForceGenerate() 
    {
        Instantiate(prefab,transform.position,Quaternion.identity,transform);
    }

}
