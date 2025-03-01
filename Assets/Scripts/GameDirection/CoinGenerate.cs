using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerate : MonoBehaviour
{
    public optimizePool initcoinPool;
    public float distanceCoins;
    public void coinSpawn (Vector3 positionStart)
    {
            GameObject coin1 = initcoinPool.GetPooledObject();
            coin1.transform.position = positionStart;
            coin1.SetActive(true);

           GameObject coin2 = initcoinPool.GetPooledObject();
            coin2.transform.position = new Vector3 (positionStart.x - distanceCoins, positionStart.y, positionStart.z);
            coin2.SetActive(true);

            GameObject coin3 = initcoinPool.GetPooledObject();
            coin3.transform.position = new Vector3 (positionStart.x + distanceCoins, positionStart.y, positionStart.z);
            coin3.SetActive(true);


    }
    // Start is called before the first frame update

}
