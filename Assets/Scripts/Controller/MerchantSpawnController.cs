using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSpawnController : MonoBehaviour
{
    public RectTransform rect;
    
    private float curTime = 3f;
    [Header("상인 스폰 시간")]
    public float spawnTime = 3f;

    public int maxMerchant = 5;
    public float startPos = -1200f;
    public float posDiff = 240f;


    [HideInInspector]
    public List<MerchantController> currentMerchantControllers = new List<MerchantController>();

    public void SpawnUpdate()
    {
        curTime += Time.deltaTime;
        if(curTime >= GameManager.Instance.CurrentLevelData.spawnTime) 
        {
            if(currentMerchantControllers.Count >= maxMerchant)
            {
                return;
            }

            SpawnMerchant();
            curTime = 0;
        }
    }

    public void SpawnMerchant() 
    {
        var merchant = GameManager.Instance.CurrentLevelData.enableMerchantSpawn.GetRandom();
        var go = Instantiate(merchant,transform.position,Quaternion.identity,transform);
        var merchantController = go.GetComponent<MerchantController>();
        var pot = PrefabsManager.Instance.GetRandomPots();
        
        merchantController.OnSpawn(new Vector3(startPos + (posDiff* currentMerchantControllers.Count),0,0),pot.GetComponent<PotController>());
        currentMerchantControllers.Add(merchantController);

    }
    public void DestroyMerChantCount(MerchantController merchantController)
    {
        for(int i =0; i< currentMerchantControllers.Count; ++i) 
        {
            if(currentMerchantControllers[i] == merchantController) 
            {
                currentMerchantControllers.RemoveAt(i);
                for(int j =i; j < currentMerchantControllers.Count; ++j)
                {
                    currentMerchantControllers[j].desirePos = new Vector3(startPos + (posDiff * j),0,0);
                }
                return;
            }
        }
    }

}
