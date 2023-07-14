using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
    public static PrefabsManager Instance;

    [Header("���ڱ�")]
    public List<GameObject> pots;
    [Header("���")]
    public List<GameObject> elements;
    [Header("���ڱ� Ʋ")]
    public GameObject maker;
    [Header("�մ�")]
    public List<GameObject> customers;


    [Space(10)]

    [Header("��� Generator")]
    public List<SpawnSpotController> elementSpawner;

    [Header("���ڱ� Ʋ Generator")]
    public SpawnSpotController makerSpawner;


    private void Start()
    {
        Instance = this;
    }



}
