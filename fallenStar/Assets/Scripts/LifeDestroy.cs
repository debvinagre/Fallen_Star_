using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDestroy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            Object.Destroy(this.gameObject);
        }
    }
}
