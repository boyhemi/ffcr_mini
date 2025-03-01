using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointGenerator1 : MonoBehaviour
{
    // Start is called before the first frame update
    public optimizePool initCheckpoint;
    public void checkPointSpawn (Vector3 positionStart)
    {
            GameObject checkpoint = initCheckpoint.GetPooledObject();
            checkpoint.transform.position = positionStart;
            checkpoint.SetActive(true);
    }
}
