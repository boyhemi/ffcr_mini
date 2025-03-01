using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCoinPowerUp : MonoBehaviour
{
    private PowerBoostManagement initBoostManager;

    public bool safeMode;
    public float timeSuperCoinPowerUp;
    private bool isActiveSuperCoinPowerUpTimeAddition;
    private float timeSuperCoinPowerUpAddition = 20;


    // Start is called before the first frame update
    void Start()
    {
        initBoostManager =  FindObjectOfType<PowerBoostManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Player")
        {
            if (isActiveSuperCoinPowerUpTimeAddition)
            {
                float comboTime = timeSuperCoinPowerUp + timeSuperCoinPowerUpAddition;
                initBoostManager.initiateSuperCoinPowerUp(safeMode, comboTime);   
                gameObject.SetActive(false);
            }
            else
            {
            initBoostManager.initiateSuperCoinPowerUp(safeMode, timeSuperCoinPowerUp);   
            gameObject.SetActive(false);
            }

        }

    }
}
