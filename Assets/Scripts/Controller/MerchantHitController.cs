using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantHitController : MonoBehaviour
{
    public MerchantController merchantController;
    public void OnDrop(PotController pot) 
    {
        merchantController.OnDrop(pot);
    }
}
