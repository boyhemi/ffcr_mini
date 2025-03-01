using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedCheckpoint : MonoBehaviour
{

    public bool isCheckpointReached;
    public PlayerController initPlayer;


    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            isCheckpointReached = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
