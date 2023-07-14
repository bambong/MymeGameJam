using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<GameObject> customers;


    [Space(10)]

    [Header("재료 Generator")]
    public List<SpawnSpotController> elementSpawner;

    [Header("도자기 틀 Generator")]
    public SpawnSpotController makerSpawner;


    private void Start()
    {
        Instance = this;
    }



}
