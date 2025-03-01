using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagetPowerUp : MonoBehaviour
{

    private PowerBoostManagement initBoostManager;
    private pickupController initPickupController;
    private PlatformDestroyer[] initPowerUpList;
    


    // Start is called before the first frame update
    void Start()
    {
        initBoostManager =  FindObjectOfType<PowerBoostManagement>();
        initPickupController =  FindObjectOfType<pickupController>();   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ActivateCoin();
            gameObject.SetActive(false);   
        }
    }
    

    void ActivateCoin()
    {  
       initPickupController.isActiveMagnetPowerUp = true;
       initPowerUpList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < initPowerUpList.Length; i++)
            {
                if(initPowerUpList[i].gameObject.name.Contains ("PowerUp") || initPowerUpList[i].gameObject.name.Contains ("Magnet") || initPowerUpList[i].gameObject.name.Contains ("SuperCoinPowerUp")  )
                {
                initPowerUpList[i].gameObject.SetActive(false);


                }
            }
    }



}
