using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletBoostPowerUp : MonoBehaviour
{


    [SerializeField] bool isActivatedBulletBoost;
    private PowerBoostManagement initBoostManager;
    
    void Start()
    {
        initBoostManager =  FindObjectOfType<PowerBoostManagement>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Player")
        {
            initBoostManager.initiateBulletBoost();  
            gameObject.SetActive(false);
        }

    }
}
