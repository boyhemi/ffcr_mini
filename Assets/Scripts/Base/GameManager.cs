using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Text yourScore;
    public Text highScore;
    public Text coinText;
    public GameObject RetryButton;
    public int rewardedRespawnAttempt = 0;

    float scoreCount;
    float highScoreCount;
    int coinCount;

    public PlatformManagement gamePlaformManagement;
    public gameOver initGameOver;
    public Animator reloadAnim;
    public Image fadeImage;
    public float initTime = - 5f;



    void Start()
    {   
        scoreManager.init.scoreText.gameObject.SetActive(false);
        scoreManager.init.coinText.gameObject.SetActive(false);
        scoreManager.init.coinIcon.gameObject.SetActive(false);
        fadeImage.gameObject.SetActive(false); 
        Debug.Log(coinCount.ToString());
        scoreCount = PlayerPrefs.GetFloat("Score");
        highScoreCount = PlayerPrefs.GetFloat("Highscore"); 
        coinCount = PlayerPrefs.GetInt("Coincount");
        yourScore.text = Mathf.Round (scoreCount).ToString();
        highScore.text =  Mathf.Round (highScoreCount).ToString();
        coinText.text = coinCount.ToString();
        gamePlaformManagement = FindObjectOfType<PlatformManagement>();
        

    }

    public void Restart()
    {
        
        StartCoroutine(reloadGame());


    }

    IEnumerator reloadGame()
    {

        reloadAnim.enabled = true;
        fadeImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(initTime);
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }

    public void OSStoreRedirect()
    {
        Debug.Log("Redirecting to App Store");
    }

}
