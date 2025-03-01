using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // player speed
    public float playerSpeed;

    // Player Gravity
    public float forceJump;

    // Game Manager
    public scoreManager initScoreManager;
    
    // Player jump
    public bool onGround;
    public LayerMask initGround;
    public Transform groundDetection;
    public float detectionRadius;

    // SPEED UP
    public float speedBoost;
    public float speedCheckpointBoost;
    private float speedCheckpointCount;

    // Jump response timer
    public float timingJumping;
    private float jumpTimer;
    private bool JumpStopped;
    private bool allowDoubleJumping;

    // Player Asset core
    private Rigidbody2D myRigidbody;
    private Collider2D colliderCore;
    private Animator myAnim;

    // Respawn Point
    public Vector3 respawnPoint;

    //Platform Management
    public PlatformManagement gamePlaformManagement;

    // sound
    public AudioSource JumpSound;
    public AudioSource diedSound;
    public AudioSource bgmMusic;
    public AudioSource failSound;

    // magnetactive
    public bool resetMagPowerUp;


    // gameover
    public gameOver initGameOver;
    public GameObject panelGameOver;
    // initplayerController
    public static PlayerController init;
    // start game flag
    public bool allowMovement;
    // die effect
    public GameObject DestroyPlayerEffect;

    // followcam
    public followCam cam;



    // invincibility
    public bool isInvincibilityActive;
    public Text indicateInvincibility;
    public AudioSource invincibilityMusic;
    Renderer ren;
    Color c;
    // audiosource bg
    public countdownController initCountdown;
    public AudioSource gameOverSound;
    public AudioSource boostMusic;

    // ffcs
    public ffcsController initFfcs;

    // boost 
    public PowerBoostManagement pb;
    public bool resetPowerUp;

    // difficulty
    private int configuredSetDifficulty;



    void Awake() {
        if (init == null)
        {
            init = this;
        }
        indicateInvincibility.gameObject.SetActive(false);
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        configuredSetDifficulty = PlayerPrefs.GetInt("selectedDifficulty");
    }


    // Start is called before the first frame update
    void Start()
    {
        pb = FindObjectOfType<PowerBoostManagement>();
        panelGameOver.SetActive(false);
        Time.timeScale = 1;
        allowMovement = false;
        myRigidbody = GetComponent<Rigidbody2D>();
        colliderCore = GetComponent<Collider2D>();
        myAnim = GetComponent<Animator>();

        jumpTimer = timingJumping;

        JumpStopped = true;
        respawnPoint = transform.position;

    }

         private bool IsPointerOverUIObject() {
             PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
             eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
         List<RaycastResult> results = new List<RaycastResult>();
         EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
         return results.Count > 0;
     }



    // Update is called once per frame
    void Update()
    {

        if (allowMovement == true)
        {
            onGround = Physics2D.OverlapCircle(groundDetection.position, detectionRadius, initGround);

        if (transform.position.x > speedCheckpointCount)
        {

            speedCheckpointCount += speedCheckpointBoost;
            speedCheckpointBoost = speedCheckpointBoost * speedBoost;
            playerSpeed =  playerSpeed * speedBoost;

        }

        myRigidbody.velocity = new Vector2(playerSpeed, myRigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)
        {
        if (!IsPointerOverUIObject())
         {
            if (onGround == true)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, forceJump);
                JumpStopped = false;
                JumpSound.Play();
            }

            if (!onGround && allowDoubleJumping)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, forceJump);
                jumpTimer = timingJumping;
                JumpStopped = false;
                allowDoubleJumping = false;
                JumpSound.Play();
            }

            if (onGround == true)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, forceJump);
                JumpStopped = false;
            }

            if (!onGround && allowDoubleJumping)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, forceJump);
                jumpTimer = timingJumping;
                JumpStopped = false;
                allowDoubleJumping = false;

            }


          }

           

        }

        


        if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began && (JumpStopped))
        {

         if (!IsPointerOverUIObject())
         {
            if (jumpTimer > 0 )
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, forceJump);
                jumpTimer -= Time.deltaTime;
                JumpStopped = true;
            }

         }
            


        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)
        {

            if (!IsPointerOverUIObject())
            {
            jumpTimer = 0;
            JumpStopped = true;
                
            }

        }

        if(onGround)
        {
            initScoreManager.countScore();
            jumpTimer = timingJumping;
            allowDoubleJumping = true;

        }

        myAnim.SetFloat ("speed", myRigidbody.velocity.x);
        myAnim.SetBool ("isGround", onGround);


        }
        
       

       
    }


   private void gameOver()
    {
        if (pb.isActivePowerUp == true || pb.isMagPowerUp)
            {         
                resetPowerUp = true;
                initScoreManager.setScore();
                initFfcs.allowAttack = false;
                initFfcs.BulletIcon.gameObject.SetActive(false);
                initFfcs.BulletCountText.gameObject.SetActive(false);
                bgmMusic.Stop();
                failSound.Play();   
                Instantiate(DestroyPlayerEffect, transform.position, Quaternion.identity);
                GameObject.Find ("Player").transform.localScale = new Vector3(0, 0, 0);
                cam.enabled = false;
                StartCoroutine(loadGameOverMenu());
            }
        else
        {
            initScoreManager.setScore();
            initFfcs.allowAttack = false;
            initFfcs.BulletIcon.gameObject.SetActive(false);
            initFfcs.BulletCountText.gameObject.SetActive(false);
            bgmMusic.Stop();
            failSound.Play();   
            Instantiate(DestroyPlayerEffect, transform.position, Quaternion.identity);
            GameObject.Find ("Player").transform.localScale = new Vector3(0, 0, 0);
            cam.enabled = false;
            StartCoroutine(loadGameOverMenu());

        }

    }

    IEnumerator loadGameOverMenu()
    {
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 0;
        cam.enabled = true;
        gameObject.SetActive(false);
        gameOverSound.Play();   
        panelGameOver.SetActive(true);
    }







     private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Foe" || other.gameObject.tag == "Moving Foe" || other.gameObject.tag == "AirSpike")
        {

            if (!isInvincibilityActive)
            {
                gameOver();
            }

        }
        
    }

     void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Checkpoint")
        {
            switch (configuredSetDifficulty)
            {
                case 0:
                if (gamePlaformManagement.randomThresholdAirSpike < 100 && gamePlaformManagement.randomThresholdSpike < 40 
                && gamePlaformManagement.randomThresholdFoeSlime == 50  && gamePlaformManagement.randomThresholdFoeSlug == 0
                && gamePlaformManagement.randomThresholdFoeWhiteCloud == 0  && gamePlaformManagement.randomThresholdFoeBlackCloud == 0) 
                {
                gamePlaformManagement.randomThresholdAirSpike += 10;
                gamePlaformManagement.randomThresholdSpike += 3;
                gamePlaformManagement.randomThresholdFoeSlime += 1;
                gamePlaformManagement.randomThresholdFoeSlug += 0;
                gamePlaformManagement.randomThresholdFoeWhiteCloud += 0;
                gamePlaformManagement.randomThresholdFoeBlackCloud += 0;


                }
                else
                {
                gamePlaformManagement.randomThresholdAirSpike = 100;
                gamePlaformManagement.randomThresholdSpike = 40;
                gamePlaformManagement.randomThresholdFoeSlime = 50;
                gamePlaformManagement.randomThresholdFoeSlug = 0;
                gamePlaformManagement.randomThresholdFoeWhiteCloud = 0;
                gamePlaformManagement.randomThresholdFoeBlackCloud = 0;
                }

                break;

                case 1:

                if (gamePlaformManagement.randomThresholdAirSpike < 100 && gamePlaformManagement.randomThresholdSpike < 50 
                && gamePlaformManagement.randomThresholdFoeSlime == 45  && gamePlaformManagement.randomThresholdFoeSlug == 25
                && gamePlaformManagement.randomThresholdFoeWhiteCloud == 10  && gamePlaformManagement.randomThresholdFoeBlackCloud == 5) 
                {
                gamePlaformManagement.randomThresholdAirSpike += 20;
                gamePlaformManagement.randomThresholdSpike += 2;
                gamePlaformManagement.randomThresholdFoeSlime += 1;
                gamePlaformManagement.randomThresholdFoeSlug += 1;
                gamePlaformManagement.randomThresholdFoeWhiteCloud += 1;
                gamePlaformManagement.randomThresholdFoeBlackCloud += 1;


                }
                else
                {
                gamePlaformManagement.randomThresholdAirSpike = 100;
                gamePlaformManagement.randomThresholdSpike = 50;
                gamePlaformManagement.randomThresholdFoeSlime = 45;
                gamePlaformManagement.randomThresholdFoeSlug = 25;
                gamePlaformManagement.randomThresholdFoeWhiteCloud = 10;
                gamePlaformManagement.randomThresholdFoeBlackCloud = 5;
                }

                break;

                case 2:
                if (gamePlaformManagement.randomThresholdAirSpike < 100 && gamePlaformManagement.randomThresholdSpike < 70 
                && gamePlaformManagement.randomThresholdFoeSlime == 85  && gamePlaformManagement.randomThresholdFoeSlug == 70
                && gamePlaformManagement.randomThresholdFoeWhiteCloud == 65  && gamePlaformManagement.randomThresholdFoeBlackCloud == 55) 
                {
                gamePlaformManagement.randomThresholdAirSpike += 20;
                gamePlaformManagement.randomThresholdSpike += 5;
                gamePlaformManagement.randomThresholdFoeSlime += 3;
                gamePlaformManagement.randomThresholdFoeSlug += 2;
                gamePlaformManagement.randomThresholdFoeWhiteCloud += 2;
                gamePlaformManagement.randomThresholdFoeBlackCloud += 2;
                }
                else
                {
                gamePlaformManagement.randomThresholdAirSpike = 100;
                gamePlaformManagement.randomThresholdSpike = 70;
                gamePlaformManagement.randomThresholdFoeSlime = 85;
                gamePlaformManagement.randomThresholdFoeSlug = 70;
                gamePlaformManagement.randomThresholdFoeWhiteCloud = 65;
                gamePlaformManagement.randomThresholdFoeBlackCloud = 55;
                }
                break;


            }

        }


        if(other.CompareTag("Foe") || other.CompareTag("Moving Foe"))
        {
            if (isInvincibilityActive)
            {
                initScoreManager.incrementScore(+3);
                other.gameObject.SetActive(false);
            }

        }
       
    }



}
