using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSpawnController : MonoBehaviour
{
    public RectTransform rect;
    
    private float curTime = 0;
    [Header("상인 스폰 시간")]
    public float spawnTime = 3f;

    public int maxMerchant = 5;
    public float startPos = -1200f;
    public float posDiff = 240f;

    public int curMerChantCount = 0;

    public void SpawnUpdate()
    {
        curTime += Time.deltaTime;
        if(curTime >= spawnTime) 
        {
            if(curMerChantCount >= maxMerchant)
            {
                return;
            }

            SpawnMerchant();
            curTime = 0;
        }
    }

    public void SpawnMerchant() 
    {
        var merchant = PrefabsManager.Instance.GetRandomMerchant();
        var go = Instantiate(merchant,transform.position,Quaternion.identity,transform);
        var merchantController = go.GetComponent<MerchantController>();
        var pot = PrefabsManager.Instance.GetRandomPots();
        
        merchantController.OnSpawn(new Vector3(startPos + (posDiff*curMerChantCount),0,0),pot.GetComponent<PotController>());


        curMerChantCount++;
    }
    public void DecreaseMerChantCount() => curMerChantCount--;


}
