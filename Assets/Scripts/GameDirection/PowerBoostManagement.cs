using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerBoostManagement : MonoBehaviour
{
    // Main
    private bool boostedPoints;
    private bool safeMode;
    public bool isActivePowerUp;
    public bool isMagPowerUp;
    private float counterPowerUpPeriod;
    private float normalSecsPerPoint;
    [SerializeField] float spikeRate;
    [SerializeField] float AirSpikeRate;

    private bool isActivePowerUpTimeAddition;

    // Init
    private scoreManager initScoreManager;
    private PlatformManagement initPlatformManagement;
    private ffcsController initFFCSController;
    public countdownController initCountdown;

    // destroy
    private PlatformDestroyer[] initLandSpikeList;
    private PlatformDestroyer[] initAirSpikeList;
    private PlatformDestroyer[] initPowerUpList;


    private gameOver initGameOver;
    public PlayerController initPlayer;

    // PowerUpTimeText
    public GameObject PowerUpImage;
    public Text PowerUpTimeText;

    // Super Coin PowerUp
    private bool isSuperCoinActive;
    private float counterSuperCoinPowerUp;

    // BulletBoost PowerUp
    public bool isBulletBoostActive;

    
    // Start is called before the first frame update
    void Start()
    {
        PowerUpTimeText.gameObject.SetActive(false);
        PowerUpImage.gameObject.SetActive(false);
        initScoreManager =  FindObjectOfType<scoreManager>();
        initPlatformManagement = FindObjectOfType<PlatformManagement>();
        initGameOver = FindObjectOfType<gameOver>();
        initPlayer = FindObjectOfType<PlayerController>();
        initCountdown = FindObjectOfType<countdownController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isActivePowerUp)
        {
            PowerUpTimeText.gameObject.SetActive(true);
            PowerUpImage.gameObject.SetActive(true);
            initPlatformManagement.thresholdPowerUp = 0f;
            initPlatformManagement.thresholdMagnetPowerUp = 0f;
            initPlatformManagement.thresholdSuperCoinPowerUp = 0f;
            counterPowerUpPeriod -= Time.deltaTime;
            PowerUpTimeText.text =  Mathf.Round (counterPowerUpPeriod).ToString();


            initPowerUpList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < initPowerUpList.Length; i++)
            {
                if(initPowerUpList[i].gameObject.name.Contains ("PowerUp") || initPowerUpList[i].gameObject.name.Contains ("Magnet") || initPowerUpList[i].gameObject.name.Contains ("SuperCoinPowerUp") || initPowerUpList[i].gameObject.name.Contains ("BulletBoost") )
                {
                initPowerUpList[i].gameObject.SetActive(false);


                }
            }
    
            if (initGameOver.resetPowerUp)
            {
                    initPlatformManagement.thresholdPowerUp = 10f;
                    initPlatformManagement.thresholdMagnetPowerUp = 20f;
                    initPlatformManagement.thresholdSuperCoinPowerUp = 5f;
                    counterPowerUpPeriod = 0;
                    initGameOver.resetPowerUp = false;
                    isActivePowerUpTimeAddition = false;
                    PowerUpTimeText.gameObject.SetActive(false);
                    PowerUpImage.gameObject.SetActive(false);
                    initPlayer.boostMusic.Stop();

            }

           else if (initGameOver.fallInitiatedResetPowerUp)
            {
                    initPlatformManagement.thresholdPowerUp = 10f;
                    initPlatformManagement.thresholdMagnetPowerUp = 20f;
                    initPlatformManagement.thresholdSuperCoinPowerUp = 5f;
                    initGameOver.resetPowerUp = false;
                    isActivePowerUpTimeAddition = false;
                    PowerUpTimeText.gameObject.SetActive(false);
                    PowerUpImage.gameObject.SetActive(false);
                    initPlayer.boostMusic.Stop();

            }
            
            else if (initPlayer.resetPowerUp)
            {
                    initPlatformManagement.thresholdPowerUp = 10f;
                    initPlatformManagement.thresholdMagnetPowerUp = 20f;
                    initPlatformManagement.thresholdSuperCoinPowerUp = 5f;
                    counterPowerUpPeriod = 0;
                    initGameOver.resetPowerUp = false;
                    isActivePowerUpTimeAddition = false;
                    PowerUpTimeText.gameObject.SetActive(false);
                    PowerUpImage.gameObject.SetActive(false);

            }

            if(boostedPoints)
            {

                initScoreManager.pointsPerSuccessfulLanding = normalSecsPerPoint * 5f;
                initScoreManager.enableBoost = true;
            }
            if(safeMode)
            {
                initPlatformManagement.randomThresholdSpike = 0f;
                initPlatformManagement.randomThresholdAirSpike = 0f;


            }

            if (counterPowerUpPeriod <= 0)
            {
                initPlatformManagement.thresholdPowerUp = 10f;
                initPlatformManagement.thresholdMagnetPowerUp = 20f;
                initPlatformManagement.thresholdSuperCoinPowerUp = 5f;
                initScoreManager.pointsPerSuccessfulLanding = normalSecsPerPoint;
                initPlatformManagement.randomThresholdSpike = spikeRate;
                initPlatformManagement.randomThresholdAirSpike = AirSpikeRate;
                initScoreManager.enableBoost = false;
                isActivePowerUp = false;
                isActivePowerUpTimeAddition = false;
                PowerUpTimeText.gameObject.SetActive(false);
                PowerUpImage.gameObject.SetActive(false);
                initPlayer.boostMusic.Stop();
                if (initGameOver.resetPowerUp || initPlayer.resetPowerUp)
                {
                initPlayer.boostMusic.Stop();
                PowerUpTimeText.gameObject.SetActive(false);
                PowerUpImage.gameObject.SetActive(false);

                }
                else if (!initGameOver.resetPowerUp || !initPlayer.resetPowerUp)
                {
                initPlayer.bgmMusic.Play();
                }
                else if (initGameOver.fallInitiatedResetPowerUp)
                {

                }
            }

        }

        if (isBulletBoostActive)
        {
            initPowerUpList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < initPowerUpList.Length; i++)
            {
                if(initPowerUpList[i].gameObject.name.Contains ("BulletBoost"))
                {
                initPowerUpList[i].gameObject.SetActive(false);
                }
            }


             if (initGameOver.resetPowerUp)
            {
                isBulletBoostActive = false;

            }

           else if (initGameOver.fallInitiatedResetPowerUp)
            {
                isBulletBoostActive = false;
            }
        }
        


         if (isSuperCoinActive)
        {
            initPlatformManagement.randomThresholdSpike = 0f;
            initPlatformManagement.randomThresholdAirSpike = 0f;
            initPlatformManagement.thresholdSuperCoinPowerUp = 0f;
            initPlatformManagement.randomThresholdSuperCoin = 100f;
            counterSuperCoinPowerUp -= Time.deltaTime;

            initPowerUpList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < initPowerUpList.Length; i++)
            {
                if(initPowerUpList[i].gameObject.name.Contains ("PowerUp") || initPowerUpList[i].gameObject.name.Contains ("Magnet") || initPowerUpList[i].gameObject.name.Contains ("SuperCoinPowerUp")  )
                {
                initPowerUpList[i].gameObject.SetActive(false);


                }
            }
    


            if (counterSuperCoinPowerUp <= 0)
            {
                initPlatformManagement.randomThresholdSpike = 55f;
                initPlatformManagement.randomThresholdAirSpike = 100f;
                initPlatformManagement.randomThresholdSuperCoin = 0f;
                initPlatformManagement.thresholdSuperCoinPowerUp = 10f;
                isSuperCoinActive = false;
                isActivePowerUpTimeAddition = false;
            
            }
            
            }
        }


    public void initiatePowerUp(bool point, bool initSafe, float period)
    {
      boostedPoints = point;
      safeMode = initSafe;
      counterPowerUpPeriod = period;
      normalSecsPerPoint = initScoreManager.pointsPerSuccessfulLanding;
      spikeRate = initPlatformManagement.randomThresholdSpike;
      AirSpikeRate = initPlatformManagement.randomThresholdAirSpike;
      isActivePowerUp = true;


      if (safeMode)
        {
            initLandSpikeList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < initLandSpikeList.Length; i++)
            {
                if(initLandSpikeList[i].gameObject.name.Contains ("Spike"))
                {
                initLandSpikeList[i].gameObject.SetActive(false);


                }
            }
        }
    }

    public void initiateSuperCoinPowerUp(bool initSafe, float period)
    {
      counterSuperCoinPowerUp = period;
      isSuperCoinActive = true;


      if (isSuperCoinActive)
        {
            initAirSpikeList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < initAirSpikeList.Length; i++)
            {
                if(initAirSpikeList[i].gameObject.name.Contains ("AirSpike"))
                {
                initAirSpikeList[i].gameObject.SetActive(false);

                }
            }
        }
    }


    public void initiateBulletBoost()
    {
     isBulletBoostActive = true;

    }
}







