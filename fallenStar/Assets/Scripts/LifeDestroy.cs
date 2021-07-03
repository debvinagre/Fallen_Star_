using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDestroy : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerData data;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            player.life++;
            data.maxLife++;
            Object.Destroy(this.gameObject);
        }
    }
}
