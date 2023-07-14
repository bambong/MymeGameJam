using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMakerSpawnerController : MonoBehaviour
{
    public List<ShapeButtonController> shapeButtonControllers;
    
    [Header("스폰 오브젝트 부모")]
    public Transform spawnerParent;
    public MakerController curBase;

    public void DestroyCurBase() 
    {
        if(curBase == null) 
        {
            return;
        }
        Destroy(curBase.gameObject);
        curBase = null;
       
        foreach(var item in shapeButtonControllers)
        {
            item.ButtonUpdate();
        }

    }
    public void Generate(PotShapeType type) 
    {
        DestroyCurBase();
        var prefab = PrefabsManager.Instance.potPrefabs[type].baseMakerGo;
        var go = Instantiate(prefab,spawnerParent.transform.position,Quaternion.identity,spawnerParent);
        curBase = go.GetComponent<MakerController>();

        foreach(var item in shapeButtonControllers)
        {
            item.ButtonUpdate();
        }
    }

}
