using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modalPopUp : MonoBehaviour
{
    public GameObject dialogBox;
    public Animator animateDialog;
    public Text textDialog;


    public void dialog (string text) {
        dialogBox.SetActive(true);
        textDialog.text = text;
        animateDialog.SetTrigger("Pop");
    }

     public void close () {
        dialogBox.SetActive(false);
        animateDialog.SetTrigger("Pop");
    }
   
}
