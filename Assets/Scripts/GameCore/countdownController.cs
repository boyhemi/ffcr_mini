using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdownController : MonoBehaviour
{
    public int timerCountdown;
    public Text displayTimerCount;
    public Text displayInstructions;
    public PlayerController initPlayer;
    public PowerBoostManagement initBoost;
    public GameObject startButton;
    // Start is called before the first frame update

    private void Start() {
        StartCoroutine(CountdownStart());
    }


   IEnumerator CountdownStart()
   {
       while (timerCountdown > 0)
       {
           startButton.gameObject.SetActive(false);
           initPlayer.bgmMusic.Stop();
            displayTimerCount.text = timerCountdown.ToString();

            yield return new WaitForSeconds(1f);

            timerCountdown--;
       }
        displayTimerCount.text = "DOUBLE TAP LEFT SIDE TO START THE GAME!";
        
        yield return new WaitForSeconds(1f);
        startButton.gameObject.SetActive(true);


   }


   public void startGame()
   {
        startButton.gameObject.SetActive(false);
        displayInstructions.gameObject.SetActive(false);
        initPlayer.bgmMusic.Play();

        PlayerController.init.allowMovement = true;
        ffcsController.init.allowAttack = true;
        displayTimerCount.gameObject.SetActive(false);

   }
}
