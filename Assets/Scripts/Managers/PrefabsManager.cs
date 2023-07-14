using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShapeElementData
{
    public ElementType type;
    public GameObject go;
    public Sprite sprite;
}

[System.Serializable]
public class PotShapePrefabData
{
    public PotShapeType shape;
    public GameObject prefab;
    public Sprite sprite;
}

public class PrefabsManager : MonoBehaviour
{
    public static PrefabsManager Instance;


    [Header("도자기")]
    public List<GameObject> pots;
    [Header("재료")]
    public List<GameObject> elements;
    [Header("도자기 틀")]
    public GameObject maker;
    [Header("손님")]
    public List<GameObject> merchants;


    [Space(10)]

    [Header("재료 Generator")]
    public List<SpawnSpotController> elementSpawner;

    [Header("도자기 틀 Generator")]
    public BaseMakerSpawnerController baseMakerSpawner;


    [Space(10)]
    public List<ShapeElementData> shapeElementDatas;
    public Dictionary<ElementType,ShapeElementData> elementPrefabs = new Dictionary<ElementType, ShapeElementData>();
   
    public List<PotShapePrefabData> potShapePrefabDatas;
    public Dictionary<PotShapeType,PotShapePrefabData> potPrefabs = new Dictionary<PotShapeType,PotShapePrefabData>();

    private void Start()
    {
        Instance = this;

        foreach(var item in shapeElementDatas) 
        {
            elementPrefabs.Add(item.type,item);
        }
        foreach(var item in potShapePrefabDatas)
        {
            potPrefabs.Add(item.shape,item);
        }
    }
    public GameObject GetRandomMerchant()
    {
        return merchants[merchants.GetRandomIndex()];
    }
    public GameObject GetRandomPots()
    {
        return pots[pots.GetRandomIndex()];
    }

}
