using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour

{
    public GameObject platformDestuctionPoint;
    // Start is called before the first frame update
    void Start()
    {
     platformDestuctionPoint = GameObject.Find ("PlatformDestroyPoint");    
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < platformDestuctionPoint.transform.position.x)
        {
             gameObject.SetActive(false);
        }
    }
}
