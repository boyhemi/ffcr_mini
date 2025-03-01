using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCam : MonoBehaviour
{
    public PlayerController initPlayer;
    private float movingDistance;
    public Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        initPlayer = FindObjectOfType<PlayerController>();
        lastPosition = initPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movingDistance = initPlayer.transform.position.x - lastPosition.x;
        transform.position = new Vector3(transform.position.x + movingDistance, transform.position.y, transform.position.z);
        lastPosition = initPlayer.transform.position;   
    }
}
