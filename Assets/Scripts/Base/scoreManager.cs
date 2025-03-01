using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    // Score
    public Text scoreText;
    public float scoreCount;
    public float highScoreCount;

    // Coin
    public Text coinText;
    public Image coinIcon;
    public int coinCount;
    public int TotalCoinCount;

    
    //Score Boost
    public float pointsPerSuccessfulLanding;
    public bool scoreIncreasing;

    public static scoreManager init;

    // Powerup specific
    public bool enableBoost;



    private void Awake() {

        if (init == null)
        {
            init = this;

        }
        
        scoreCount = 0;
        coinCount = 0;


        
        if (PlayerPrefs.GetFloat("Highscore") == 0)
        {
            PlayerPrefs.SetFloat("Highscore", 0);

        }
         PlayerPrefs.SetFloat("Score", scoreCount);


       if (PlayerPrefs.GetFloat("Coincount") == 0)
        {
            PlayerPrefs.SetFloat("Coincount", 0);

        }
         PlayerPrefs.SetFloat("Coincount", coinCount);


    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("TotalCoins", TotalCoinCount);
    }

    // Update is called once per frame
    void Update()
    {
       scoreText.text = "" + Mathf.Round (scoreCount);
       coinText.text = "x" + coinCount.ToString();
        
    }


    public void countCoin ()
    {

        coinCount++;
        scoreCount++;
        coinText.text = "x" + coinCount.ToString();


    }

    public void countScore()
    {
        if (enableBoost)
        {
        scoreCount += pointsPerSuccessfulLanding * Time.deltaTime * 2; 
        }
        else
        {
        scoreCount += pointsPerSuccessfulLanding * Time.deltaTime;
        }
        
       highScoreCount = scoreCount;

       scoreText.text = "Score: " + Mathf.Round (scoreCount);
    }

    public void incrementScore(int score)
    {
        if (enableBoost)
        {
        scoreCount += score + 20;
        }
        else
        {
        scoreCount += score;
        }
        
       highScoreCount = scoreCount;

       scoreText.text = "Score: " + Mathf.Round (scoreCount);
    }
    

    public void coinUpdate()
    {
        TotalCoinCount = PlayerPrefs.GetInt("TotalCoins");
        if (TotalCoinCount <= 100000)
        {
            if (TotalCoinCount > coinCount)
            {
            TotalCoinCount += coinCount;
            PlayerPrefs.SetInt("TotalCoins", TotalCoinCount);
            PlayerPrefs.Save();
            }
            else
            {
            TotalCoinCount += coinCount;
            PlayerPrefs.SetInt("TotalCoins", TotalCoinCount);
            TotalCoinCount = PlayerPrefs.GetInt("TotalCoins");
            PlayerPrefs.Save();

            }

        }
        else
        {
            Debug.Log("The Coins has exceeded 100000");
        }

    }

    public void setScore ()
    {
       scoreText.text = scoreCount.ToString();
       // related to Score
        if (PlayerPrefs.GetFloat("Highscore") < scoreCount)
        {
            PlayerPrefs.SetFloat("Highscore", highScoreCount);
            PlayerPrefs.SetFloat("Score", scoreCount);
        }
        else 
        {
             PlayerPrefs.SetFloat("Score", scoreCount);
        }

        // related to coins
        coinText.text = coinCount.ToString();
        PlayerPrefs.SetInt("Coincount", coinCount);
        coinUpdate();
    }
}
