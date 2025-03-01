using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    // Material
    [SerializeField] Material Day;
    [SerializeField] Material Night;
    [SerializeField] Material Snow;
    [SerializeField] Material Lava;



    [SerializeField] Sprite dayImage;
    [SerializeField] Sprite nightImage;
    [SerializeField] Sprite snowImage;
    [SerializeField] Sprite lavaImage;
    [SerializeField] Image panelImageGuide, panelImageInfo, panelImageStore, panelImageSettings;

    //Toggle
    [SerializeField] Toggle toggleDay, toggleNight, toggleSnow, toggleLava;


    private int configuredBg;





    // Start is called before the first frame update
    void Awake()
    {
        configuredBg = PlayerPrefs.GetInt("selectedBackground");
        initSetBackground();
    }


    void initSetBackground()
    {

        if (!PlayerPrefs.HasKey("selectedBackground"))
         {
             PlayerPrefs.SetInt("selectedBackground", 0);
             loadBackground();
         }
         else
         {
             loadBackground();
         }
    }

    void loadBackground()
    {
        switch (configuredBg)
        {
            case 0:
            toggleDay.isOn = true;
            toggleNight.isOn = false;
            toggleSnow.isOn = false;
            toggleLava.isOn = false;
            RenderSettings.skybox = Day;
            panelImageGuide.sprite = dayImage;
            panelImageInfo.sprite = dayImage;
            panelImageStore.sprite = dayImage;
            panelImageSettings.sprite = dayImage;
            break;


            case 1:
            toggleDay.isOn = false;
            toggleNight.isOn = true;
            toggleSnow.isOn = false;
            toggleLava.isOn = false;
            RenderSettings.skybox = Night;
            panelImageGuide.sprite = nightImage;
            panelImageInfo.sprite = nightImage;
            panelImageStore.sprite = nightImage;
            panelImageSettings.sprite = nightImage;

            break;

            case 2:
            toggleDay.isOn = false;
            toggleNight.isOn = false;
            toggleSnow.isOn = true;
            toggleLava.isOn = false;
            RenderSettings.skybox = Snow;
            panelImageGuide.sprite = snowImage;
            panelImageInfo.sprite = snowImage;
            panelImageStore.sprite = snowImage;
            panelImageSettings.sprite = snowImage;
            break;

            case 3:
            toggleDay.isOn = false;
            toggleNight.isOn = false;
            toggleSnow.isOn = false;
            toggleLava.isOn = true;
            RenderSettings.skybox = Lava;
            panelImageGuide.sprite = lavaImage;
            panelImageInfo.sprite = lavaImage;
            panelImageStore.sprite = lavaImage;
            panelImageSettings.sprite = lavaImage;
            break;
        }

    }

    public void dayToggleSelected()
    {
        PlayerPrefs.SetInt("selectedBackground", 0);
        RenderSettings.skybox = Day;
        panelImageGuide.overrideSprite = dayImage;
        panelImageInfo.sprite = dayImage;
        panelImageStore.sprite = dayImage;
        panelImageSettings.sprite = dayImage;
        PlayerPrefs.Save();


    }

    public void nightToggleSelected()
    {
        PlayerPrefs.SetInt("selectedBackground", 1);
        RenderSettings.skybox = Night;
        panelImageGuide.overrideSprite = nightImage;
        panelImageInfo.sprite = nightImage;
        panelImageStore.sprite = nightImage;
        panelImageSettings.sprite = nightImage;
        PlayerPrefs.Save();


    }

    public void snowToggleSelected()
    {
        PlayerPrefs.SetInt("selectedBackground", 2);
        RenderSettings.skybox = Snow;
        panelImageGuide.overrideSprite = snowImage;
        panelImageInfo.sprite = snowImage;
        panelImageStore.sprite = snowImage;
        panelImageSettings.sprite = snowImage;
        PlayerPrefs.Save();

    }
    public void lavaToggleSelected()
    {
        PlayerPrefs.SetInt("selectedBackground", 3);
        RenderSettings.skybox = Lava;
        panelImageGuide.overrideSprite = lavaImage;
        panelImageInfo.sprite = lavaImage;
        panelImageStore.sprite = lavaImage;
        panelImageSettings.sprite = lavaImage; 
        PlayerPrefs.Save();


    }
}
