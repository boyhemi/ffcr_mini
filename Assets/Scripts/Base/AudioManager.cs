using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isSoundOn;
    public Toggle soundToggle;
    public AudioSource bgmMusic;
    [SerializeField] Slider volSlider;
    [SerializeField] Text volText;


    void Awake()
    {
  //      isSoundOn = PlayerPrefs.GetInt("coreSound")==1?true:false;




    }

    void Start () {

        initSoundVol();
        initSoundState(); 

    }

    void initSoundVol ()
    {
        if (!PlayerPrefs.HasKey("coreSoundVolume"))
         {
             PlayerPrefs.SetFloat("coreSoundVolume", 1);
             LoadVol();
         }
         else
         {
            LoadVol();
         }
    }

    void initSoundState()
    {
        if (!PlayerPrefs.HasKey("coreSound"))
         {
             PlayerPrefs.SetInt("coreSound", 1);
             LoadSoundStatus();
         }
         else
         {
             LoadSoundStatus();
         }
    }


    public void changeVol()
    {
        AudioListener.volume = volSlider.value;
        SaveVol();

    }

    void LoadVol()
    {
        volSlider.value = PlayerPrefs.GetFloat("coreSoundVolume");
    }
    void LoadSoundStatus()
    {
        soundToggle.isOn = PlayerPrefs.GetInt("coreSound")==1?true:false;
        AudioListener.volume = PlayerPrefs.GetFloat("coreSoundVolume");

        if (!isSoundOn)
        {
        volText.gameObject.SetActive(true);
        volSlider.gameObject.SetActive(true);
        }
        else
        {
        volText.gameObject.SetActive(false);
        volSlider.gameObject.SetActive(false);
        }

    }

    void SaveVol()
    {
        PlayerPrefs.GetFloat("coreSoundVolume", volSlider.value);
    }
    

    public void saveSoundState()
    {

        if (!isSoundOn)
        {
        isSoundOn = true;
        AudioListener.pause = isSoundOn;
        PlayerPrefs.SetInt("coreSound", isSoundOn ? 0 : 1);
        volText.gameObject.SetActive(false);
        volSlider.gameObject.SetActive(false);

        }
        else
        {
        isSoundOn = false;
        AudioListener.pause = isSoundOn;
        PlayerPrefs.SetInt("coreSound", isSoundOn ? 0 : 1);
        volText.gameObject.SetActive(true);
        volSlider.gameObject.SetActive(true);

        }

    }

}
