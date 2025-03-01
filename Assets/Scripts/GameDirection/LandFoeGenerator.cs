using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandFoeGenerator : MonoBehaviour
{
    public optimizePool initLandFoePool;
    public optimizePool initLandFoeSlugPool;
    public optimizePool initLandFoeWhiteCloudPool;
    public optimizePool initLandFoeBlackCloudPool;




    public float distanceFoe;
    public void landFoeSpawn (Vector3 positionStart)
    {
            GameObject foe1 = initLandFoePool.GetPooledObject();
            foe1.transform.position = positionStart;
            foe1.SetActive(true);


    }

    public void landFoeSpawnSlug (Vector3 positionStart)
    {
            GameObject foe2 = initLandFoeSlugPool.GetPooledObject();
            foe2.transform.position = positionStart;
            foe2.SetActive(true);
    }

    public void landFoeSpawnWhiteCloud (Vector3 positionStart)
    {
            GameObject foe3 = initLandFoeWhiteCloudPool.GetPooledObject();
            foe3.transform.position = positionStart;
            foe3.SetActive(true);
    }

    public void landFoeSpawnBlackCloud (Vector3 positionStart)
    {
            GameObject foe4 = initLandFoeBlackCloudPool.GetPooledObject();
            foe4.transform.position = positionStart;
            foe4.SetActive(true);
    }
    
}
