using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    public PlayerController initPlayer;
    public scoreManager initScore;
    private float movingDistance;
    private Vector3 lastPosition;
    public GameObject panelGameOver;
    [SerializeField] PowerBoostManagement initPB;
    [SerializeField] pickupController initPickupController;

    public bool resetPowerUp;
    public bool resetMagPowerUp;
    public bool fallInitiatedResetPowerUp;
    // Start is called before the first frame update
    void Start()
    {
        initPlayer = FindObjectOfType<PlayerController>();
        initPB = FindObjectOfType<PowerBoostManagement>();
        lastPosition = initPlayer.transform.position;
        initPickupController = FindObjectOfType<pickupController>();
    }

    // Update is called once per frame
    void Update()
    {
        movingDistance = initPlayer.transform.position.x - lastPosition.x;
        transform.position = new Vector3(transform.position.x + movingDistance, transform.position.y, transform.position.z);
        lastPosition = initPlayer.transform.position;   
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            if (initPlayer.boostMusic)
            {
                    initPlayer.boostMusic.Pause();
                    fallInitiatedResetPowerUp = true;

                if (initPlayer.bgmMusic.isPlaying)
                {
                    initPlayer.bgmMusic.Stop();
                    initPlayer.gameOverSound.Play();
                }
            }

            else if (initPickupController.isActiveMagnetPowerUp)
            {
                initPickupController.isActiveMagnetPowerUp = false;
            }
                
             initScore.setScore();
             Time.timeScale = 0;
             initPlayer.initFfcs.allowAttack = false;
             initPlayer.initFfcs.BulletIcon.gameObject.SetActive(false);
             initPlayer.initFfcs.BulletCountText.gameObject.SetActive(false);
             panelGameOver.SetActive(true);
             initPlayer.bgmMusic.Stop();
             initPlayer.gameOverSound.Play();

        }    

    }

    public void destroyObj()
    {
    foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
             Destroy(o);
    }

    }

}

