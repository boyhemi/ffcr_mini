using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DifficultyController : MonoBehaviour
{
    //Toggle
    [SerializeField] Toggle toggleEasy, toggleMedium, toggleHard;
    private int configuredDifficulty;


    // Start is called before the first frame update
    void Awake()
    {
        configuredDifficulty = PlayerPrefs.GetInt("selectedDifficulty");
        initSetDifficulty();
    }

    void initSetDifficulty()
    {

        if (!PlayerPrefs.HasKey("selectedDifficulty"))
         {
             PlayerPrefs.SetInt("selectedDifficulty", 0);
             loadconfiguredDifficulty();
         }
         else
         {
             loadconfiguredDifficulty();
         }
    }

     void loadconfiguredDifficulty()
    {
        switch (configuredDifficulty)
        {
            case 0:
            toggleEasy.isOn = true;
            toggleMedium.isOn = false;
            toggleHard.isOn = false;
            break;


            case 1:
            toggleEasy.isOn = false;
            toggleMedium.isOn = true;
            toggleHard.isOn = false;
            break;

            case 2:
            toggleEasy.isOn = false;
            toggleMedium.isOn = false;
            toggleHard.isOn = true;
            break;
        }
 
    }

    // select toggle

    public void easyToggleSelected()
    {
        PlayerPrefs.SetInt("selectedDifficulty", 0);
        PlayerPrefs.Save();
    }

    public void normalToggleSelected()
    {
        PlayerPrefs.SetInt("selectedDifficulty", 1);
        PlayerPrefs.Save();
    }

    public void hardToggleSelected()
    {
        PlayerPrefs.SetInt("selectedDifficulty", 2);
        PlayerPrefs.Save();
    }




}
