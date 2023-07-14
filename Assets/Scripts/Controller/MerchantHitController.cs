using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantHitController : MonoBehaviour
{
    public MerchantController merchantController;
    public bool OnDrop(PotController pot) 
    {
        return merchantController.OnDrop(pot);
    }
}
