using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeDestroy : MonoBehaviour
{

    public GameObject DestroyEffect;

    void OnTriggerEnter2D(Collider2D other) {
    
    if (other.gameObject.tag == "Bullet")
    {
        Instantiate(DestroyEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
    }

}
