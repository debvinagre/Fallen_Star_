using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatVerifier : MonoBehaviour
{
    public bool canLeap;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Ground"){
            canLeap = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "Ground"){
            canLeap = false;
        }
    }
}
