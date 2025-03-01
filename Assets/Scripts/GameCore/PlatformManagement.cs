using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformManagement : MonoBehaviour
{
    public GameObject corePlatform;
    public Transform PlatformGenerationPoint;
    public float BetweenDistance;

   // public GameObject[] corePlatforms;
    private int platformSelector;
    private float[] coreWidths;
    public optimizePool[] initPools;


    private float coreWidth;

    public float distanceWidthMin;
    public float distanceWidthMax;

    // minimum
    private float minimumHeight;
    public Transform maximumPointHeight;
    [SerializeField] float maxHeight;
    public float maxHeightDiff;
    private float heightDiff;

    // Coin Generation
    public CoinGenerate initCoinGenerate;
    public float coinThresholdRandom;

    // Enemy spikes
    public float randomThresholdSpike;
    public float randomThresholdAirSpike;
    private int spikeSelector;
    public optimizePool initSpike;
    public AirSpikeGenerator initAirSpikeGenerator;

    // moving enemies: slime
    public float randomThresholdFoeSlime;
        // moving enemies: slug
    public float randomThresholdFoeSlug;
        // moving enemies: white cloud
    public float randomThresholdFoeWhiteCloud;
        // moving enemies: black cloud
    public float randomThresholdFoeBlackCloud;


    public optimizePool initMovingFoe;
    public LandFoeGenerator initGeneratorFoe;

    // checkpoints
    public CheckpointGenerator1 initCheckpoint;
    public optimizePool initSpawnCheckPoint;
    private float randomThresholdCheckpoint = 100f;

    //player spawn
    public float respawnDelay;
    public PlayerController gamePlayer;

    // boost powerup
    public float heightPowerUp;
    public optimizePool initPowerUpPool;
    public float thresholdPowerUp;
    // BulletBoost PowerUp;
    public float heightBulletBoostPowerUp;
    public optimizePool initBulletBoostPowerUpPool;
    public float thresholdBulletBoostPowerUp;
    // magnet powerup
    public float heightMagnetPowerUp;
    public optimizePool initMagnetPowerUpPool;
    public float thresholdMagnetPowerUp;
    // Super Coin PowerUp
    public float heightSuperCoin;
    public optimizePool initSuperCoinPowerUpPool;
    public float thresholdSuperCoinPowerUp;
    // Super Coin When active
    public optimizePool initSuperCoinPool;
    public float randomThresholdSuperCoin;
    public SuperCoinGenerator initSuperCoinGenerator;
    public float heightSuperCoinPowerUp;


    // countdownController
    public countdownController initCountdown;

    // background
    [SerializeField] Material Day;
    [SerializeField] Material Night;
    [SerializeField] Material Snow;
    [SerializeField] Material Lava;
    private int configuredLoadedBg;


    void Awake()
    {
        configuredLoadedBg = PlayerPrefs.GetInt("selectedBackground");
        loadBackground();

    }


    // Start is called before the first frame update
    void Start()
    {
        coreWidths = new float[initPools.Length];

       for (int i = 0; i < initPools.Length; i++)
       {
           coreWidths[i] = initPools[i].poolingObject.GetComponent<BoxCollider2D>().size.x;

       }

       minimumHeight = transform.position.y;
       maxHeight = maximumPointHeight.position.y;

       initCoinGenerate = FindObjectOfType<CoinGenerate>();
       gamePlayer = FindObjectOfType<PlayerController>();
    }

    void loadBackground()
    {
        switch (configuredLoadedBg)
        {
            case 0:
            RenderSettings.skybox = Day;
            break;

            case 1:
            RenderSettings.skybox = Night;
            break;

            case 2:
            RenderSettings.skybox = Snow;
            break;

            case 3:
            RenderSettings.skybox = Lava;
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (initCountdown.timerCountdown <= 0)
        {
        if (transform.position.x < PlatformGenerationPoint.position.x)
        {
                BetweenDistance = Random.Range (distanceWidthMin, distanceWidthMax);
                platformSelector = Random.Range(0, initPools.Length);

                if (heightDiff > maxHeight)
                {
                    heightDiff = maxHeight;

                }
                else if (heightDiff < minimumHeight)
                {

                    heightDiff = minimumHeight;

                }


                if (Random.Range(1f, 100f) < thresholdPowerUp)
                {
                    GameObject loadPowerUp = initPowerUpPool.GetPooledObject();

                    loadPowerUp.transform.position = transform.position + new Vector3(BetweenDistance / 2f, Random.Range (0f ,heightPowerUp + 2), 0f);
                    loadPowerUp.SetActive(true);
                }

                if (Random.Range(1f, 100f) < thresholdBulletBoostPowerUp && (PlayerPrefs.GetInt("selectedDifficulty") == 1 || PlayerPrefs.GetInt("selectedDifficulty") == 2) && PlayerPrefs.GetInt("ammoBoost") == 0)
                {
                    GameObject loadBulletBoostPowerUp = initBulletBoostPowerUpPool.GetPooledObject();

                    loadBulletBoostPowerUp.transform.position = transform.position + new Vector3(BetweenDistance / 2f, Random.Range (0f ,heightBulletBoostPowerUp + 2), 0f);
                    loadBulletBoostPowerUp.SetActive(true);
                }

                if (Random.Range(1f, 100f) < thresholdSuperCoinPowerUp)
                {
                    GameObject loadSuperCoinPowerUp = initSuperCoinPowerUpPool.GetPooledObject();

                    loadSuperCoinPowerUp.transform.position = transform.position + new Vector3(BetweenDistance / 2f, Random.Range (0f ,heightSuperCoinPowerUp + 2), 0f);
                    loadSuperCoinPowerUp.SetActive(true);
                }

                if (Random.Range(1f, 100f) < thresholdMagnetPowerUp)
                {
                    if (coinThresholdRandom > 0)
                    {
                    GameObject loadMagPowerUp = initMagnetPowerUpPool.GetPooledObject();

                    loadMagPowerUp.transform.position = transform.position + new Vector3(BetweenDistance / 5f, Random.Range (0f ,heightMagnetPowerUp), 0f);
                    loadMagPowerUp.SetActive(true);
                    }

                }
 

                transform.position = new Vector3(transform.position.x + (coreWidths[platformSelector] / 2 ) + BetweenDistance, heightDiff, transform.position.z);
                
                heightDiff = transform.position.y + Random.Range (maxHeightDiff, -maxHeightDiff);
                
                GameObject initPlatform =  initPools[platformSelector].GetPooledObject();
                initPlatform.transform.position = transform.position;
                initPlatform.transform.rotation = transform.rotation;
                initPlatform.SetActive(true);

                if (Random.Range(1f, 100f) < coinThresholdRandom)
                {
                
                initCoinGenerate.coinSpawn(new Vector3(transform.position.x + 3f, transform.position.y + 1f, transform.position.z));

                
                }

                 if (Random.Range(1f, 100f) < randomThresholdAirSpike)
                {

                    initAirSpikeGenerator.airSpikeSpawn(new Vector3(transform.position.x + -2f, transform.position.y + 6f, transform.position.z));


                }

                if (Random.Range(1f, 100f) < randomThresholdCheckpoint)
                {

                    initCheckpoint.checkPointSpawn(new Vector3(transform.position.x, transform.position.y, transform.position.z));
                }

                if (Random.Range(90f, 100f) < randomThresholdSuperCoin)
                {
                    initSuperCoinGenerator.superCoinSwappedSpawn(new Vector3(transform.position.x + -2f, transform.position.y + 6f, transform.position.z));
                }



                if (Random.Range(1f, 100f) < randomThresholdFoeSlime)
                {
                
                initGeneratorFoe.landFoeSpawn(new Vector3(transform.position.x + 2f, transform.position.y + 0.9f, transform.position.z));


                }
                if (Random.Range(1f, 100f) < randomThresholdFoeSlug)
                {
                
                initGeneratorFoe.landFoeSpawnSlug(new Vector3(transform.position.x + 2f, transform.position.y + 1f, transform.position.z));


                }
                if (Random.Range(1f, 100f) < randomThresholdFoeWhiteCloud)
                {
                initGeneratorFoe.landFoeSpawnWhiteCloud(new Vector3(transform.position.x + 5f, transform.position.y + 2f, transform.position.z));
                }

                if (Random.Range(1f, 100f) < randomThresholdFoeBlackCloud)
                {
                initGeneratorFoe.landFoeSpawnBlackCloud(new Vector3(transform.position.x + 5f, transform.position.y + 2f, transform.position.z));
                }

                if (Random.Range(1f, 100f) < randomThresholdSpike)
                {
                    GameObject startSpike = initSpike.GetPooledObject();

                    float randomSpikePosition = Random.Range(-coreWidths[platformSelector] / 2f + 1f, coreWidths[platformSelector] / 2f - 1f);
                    Vector3 positioningSpike = new Vector3(2.5f, 0.6f, 0f); 
                    startSpike.transform.position = transform.position + positioningSpike;
                    startSpike.transform.rotation = transform.rotation;
                    startSpike.SetActive(true);
                }


            }
               

        }
    }


}
