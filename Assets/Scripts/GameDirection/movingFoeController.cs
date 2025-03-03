using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingFoeController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float dist;
	[SerializeField]  bool movingRight;
    public Transform groundDetection;

	// Use this for initialization
	void Update () {

     transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, dist);
        if(groundInfo.collider == false){
            if(movingRight == true){
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
            } else {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;         
            }

		    }
        }

     void OnCollisionEnter2D(Collision2D trig) {
       if (trig.gameObject.CompareTag("Foe")){

			if (movingRight){
                transform.eulerAngles = new Vector3(0, -180, 0);
				movingRight = false;
			}
			else{
                transform.eulerAngles = new Vector3(0, 0, 0);
				movingRight = true;
			}	
		}

		
	}
    

}