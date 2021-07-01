using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatStabilyzer : MonoBehaviour
{
    public bool landing;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Ground"){
            landing = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "Ground"){
            landing = false;
        }
    }
}
