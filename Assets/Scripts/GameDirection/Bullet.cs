using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
public float speed = 20f;
public Rigidbody2D rb;
public AudioSource BulletSound;
public GameObject diedEffect;
private scoreManager initScore;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        BulletSound = GameObject.Find("BulletSound").GetComponent<AudioSource>();
        initScore = FindObjectOfType<scoreManager>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerEnter2D (Collider2D attack)
    {
         if(attack.CompareTag("Foe"))
        {
            Instantiate(diedEffect, transform.position, transform.rotation);
            attack.gameObject.SetActive(false);
            gameObject.SetActive(false);
            initScore.incrementScore(2);
        }
        else if(attack.CompareTag("Moving Foe"))
        {
            Instantiate(diedEffect, transform.position, transform.rotation);
            attack.gameObject.SetActive(false);
            gameObject.SetActive(false);
            initScore.incrementScore(5);
        }
        else if(attack.CompareTag("AirSpike"))
        {
           Instantiate(diedEffect, transform.position, transform.rotation);
           gameObject.SetActive(false);

        }

            if (BulletSound.isPlaying)
            {
                BulletSound.Stop();
                BulletSound.Play();

            }
            else
            {

            BulletSound.Play();

            }        

    }



        

}
