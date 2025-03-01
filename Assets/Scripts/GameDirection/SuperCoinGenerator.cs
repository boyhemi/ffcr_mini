using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCoinGenerator : MonoBehaviour
{
    public optimizePool initSuperCoinSwapPool;
    public void superCoinSwappedSpawn (Vector3 positionStart)
    {
            GameObject superCoinSwap = initSuperCoinSwapPool.GetPooledObject();
            superCoinSwap.transform.position = positionStart;
            superCoinSwap.SetActive(true);
    }
}
