using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optimizePool : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject poolingObject;

    public int pooledAmount;

    List<GameObject> pooledObjects;

 // Use this for initialization
      void Start () {
      pooledObjects = new List<GameObject>();

        for(int i =0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(poolingObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
  

 }

    public GameObject GetPooledObject()
    {
        for(int i=0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(poolingObject);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;


    }

}