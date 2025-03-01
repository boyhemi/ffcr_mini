using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSpikeGenerator : MonoBehaviour
{
    public optimizePool initSpikePool;
    public void airSpikeSpawn (Vector3 positionStart)
    {
            GameObject airSpike1 = initSpikePool.GetPooledObject();
            airSpike1.transform.position = positionStart;
            airSpike1.SetActive(true);
    }

}