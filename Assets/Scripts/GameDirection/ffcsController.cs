using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ffcsController : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject prefabProjectile;
    [SerializeField] int initialAmmo;
    [SerializeField]int maxAmmo;
    [SerializeField] int reloadTime;
    [SerializeField] int regenTime;
    [SerializeField] Text reloadText;
    public Text BulletCountText;
    public Image BulletIcon;

    public bool allowAttack;
    private bool isReloading;
    public static ffcsController init;
    private PowerBoostManagement initPowerBoostManagement;
    private int count = 0;
    private int ammoBoostType; 
    private int selectedDifficultyType; 

    

    void Awake() {
        if (init == null)
        {
            init = this;
        }
        ammoBoostType = PlayerPrefs.GetInt("ammoBoost");
        selectedDifficultyType = PlayerPrefs.GetInt("selectedDifficulty");


    }

    // Start is called before the first frame update
    void Start()
    {
        initPowerBoostManagement = FindObjectOfType<PowerBoostManagement>();
        allowAttack = false;
        reloadText.gameObject.SetActive(false);
        initAmmo();
        initialAmmo = maxAmmo;
        BulletCountText.text = initialAmmo.ToString();
        BulletCountText.gameObject.SetActive(true);
        BulletIcon.gameObject.SetActive(true);

    }

    void initAmmo()
    {

    switch (ammoBoostType)
    {
        case 1:
        {
            switch (selectedDifficultyType)
            {
                case 0:
                initialAmmo = 10;
                maxAmmo = 10;
                initialAmmo = maxAmmo;
                break;

                case 1:
                initialAmmo = 30;
                maxAmmo = 30;
                initialAmmo = maxAmmo;
                break;

                case 2:
                initialAmmo = 30;
                maxAmmo = 30;
                initialAmmo = maxAmmo;
                break;
            }
            break;
        }

        case 0:
        {
            switch (selectedDifficultyType)
            {
                case 1:
                initialAmmo = 15;
                maxAmmo = 15;
                initialAmmo = maxAmmo;
                break;

                case 2:
                initialAmmo = 15;
                maxAmmo = 15;
                initialAmmo = maxAmmo;
                break;
            }
        }
        break;

    }

    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Return))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (allowAttack == true)
        {


            if (isReloading)
            return;

            if (count == 0)
            {
                if (initPowerBoostManagement.isBulletBoostActive)
                {
                count++;
                regenTime = 9;
                initialAmmo = 30;
                maxAmmo = 30;
                initialAmmo = maxAmmo;
                }
            }


            if (initialAmmo <= 0)
            {
                StartCoroutine(ReloadAmmo());
                return;
            }

            else
            {
                Instantiate(prefabProjectile, attackPoint.position, attackPoint.rotation);
                initialAmmo--;

                BulletCountText.text = initialAmmo.ToString();

              if (PlayerPrefs.GetInt("selectedDifficulty") == 0)
              {
                  if (initialAmmo < 3)
                  StartCoroutine(RegenerateAmmo());
              }

             else if (PlayerPrefs.GetInt("selectedDifficulty") == 1 || PlayerPrefs.GetInt("selectedDifficulty") == 2)
              {
                  if (initialAmmo < 4)
                  StartCoroutine(RegenerateAmmo());
              }

            }
        }

    }



    IEnumerator ReloadAmmo()
    {
        isReloading = true;
        reloadText.gameObject.SetActive(true);
        BulletCountText.text = initialAmmo.ToString();
        yield return new WaitForSeconds(reloadTime);
        initialAmmo = maxAmmo;
        reloadText.gameObject.SetActive(false);
        BulletCountText.text = initialAmmo.ToString();
        isReloading = false;
    }

        IEnumerator RegenerateAmmo()
    {
        yield return new WaitForSeconds(regenTime);
        initialAmmo = maxAmmo;
        BulletCountText.text = initialAmmo.ToString();
    }

}
