using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupController : MonoBehaviour
{
    // Start is called before the first frame update

    public int coinCount;
    private scoreManager initScoreManager;
    public AudioSource CoinSound;

    // platform management
    private PlatformManagement initPlatformManagement;

   // public Transform playerTransform;
    public float moveSpeed = 5f;
     pickupController initPickupController;
    MagetPowerUp initMagnetPowerUp;
    private float magCounterPeriod = 35;
    public bool isActiveMagnetPowerUp;
    private bool isActiveMagnetPowerUpTimeAddition;
    PlayerController initplayerController;
    GameObject[] coinPoolObject;
    private gameOver initGameOver;


    
    void Start()
    {
        initScoreManager = FindObjectOfType<scoreManager>();
        CoinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
        initPickupController = gameObject.GetComponent<pickupController>();
        initMagnetPowerUp = gameObject.GetComponent<MagetPowerUp>();
        coinPoolObject = GameObject.FindGameObjectsWithTag("Coin");
        initGameOver = FindObjectOfType<gameOver>();
        initplayerController = FindObjectOfType<PlayerController>();
        initPlatformManagement = FindObjectOfType<PlatformManagement>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        foreach (GameObject Coin in coinPoolObject)
        {
            if (isActiveMagnetPowerUp)
            {
                magCounterPeriod -= 1 * Time.deltaTime;
                initPlatformManagement.thresholdMagnetPowerUp = 0f;
                initPlatformManagement.thresholdPowerUp = 0f;
                initPlatformManagement.thresholdSuperCoinPowerUp = 0f;
                if (Vector3.Distance(Coin.transform.position, initplayerController.transform.position) < 5)
                {
                    Coin.transform.Translate((initplayerController.transform.position - Coin.transform.position).normalized * 4 * Time.deltaTime, Space.World);
                }
                
                if (initGameOver.resetMagPowerUp)
                {
                    initPlatformManagement.thresholdPowerUp = 10f;
                    initPlatformManagement.thresholdMagnetPowerUp = 20f;
                    initPlatformManagement.thresholdSuperCoinPowerUp = 5f;
                    magCounterPeriod = 0;
                    isActiveMagnetPowerUp = false;
                }
                

                if (magCounterPeriod <=0)
                {
                initPlatformManagement.thresholdMagnetPowerUp = 20f;
                initPlatformManagement.thresholdSuperCoinPowerUp = 5f;
                initPlatformManagement.thresholdPowerUp = 10f;
                isActiveMagnetPowerUp = false;                
                }
            }

        }


    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            initScoreManager.countCoin();
            gameObject.SetActive(false);

            if (CoinSound.isPlaying)
            {
                CoinSound.Stop();
                CoinSound.Play();

            }
            else
            {

            CoinSound.Play();

            }
        }


    }
}
