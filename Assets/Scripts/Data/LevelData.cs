using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class LevelData 
{
    public float scoreForThisLevel;
    public List<PotShapeType> enableShapeSpawn;
    public List<GameObject> enableMerchantSpawn;
    public float spawnTime;
}
