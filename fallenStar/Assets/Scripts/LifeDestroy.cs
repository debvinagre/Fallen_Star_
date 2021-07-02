using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDestroy : MonoBehaviour
{
    [SerializeField] private Player player;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            player.life++;
            player.maxLife++;
            Object.Destroy(this.gameObject);
        }
    }
}
