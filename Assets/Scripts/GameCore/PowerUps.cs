using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public bool boostedPoints;
    public bool safeMode;
    private float powerUpTimeAddition = 15;
    private bool isActivePowerUpTimeAddition;

    public float timePowerUp;

    private PowerBoostManagement initBoostManager;
    public Sprite[] initSprites;
    
    // Start is called before the first frame update
    void Start()
    {
        initBoostManager =  FindObjectOfType<PowerBoostManagement>();
    }

    void Awake() {
        int selectPowerUp = Random.Range(0, 1);

        switch (selectPowerUp)
        {
            case 0: boostedPoints = true;
            break;

            case 1: safeMode = true;
            break;

        }
            GetComponent<SpriteRenderer>().sprite = initSprites[selectPowerUp];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Player")
        {
            if (isActivePowerUpTimeAddition)
            {
            float combinedTime = timePowerUp + powerUpTimeAddition;
            initBoostManager.initiatePowerUp(boostedPoints, safeMode, combinedTime);   
            }
            else
            {
            initBoostManager.initiatePowerUp(boostedPoints, safeMode, timePowerUp);   

            }
            gameObject.SetActive(false);
            initBoostManager.initPlayer.bgmMusic.Stop();
            initBoostManager.initPlayer.boostMusic.Play();
        }

    }

}
