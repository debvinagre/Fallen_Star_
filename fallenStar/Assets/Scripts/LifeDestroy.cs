using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDestroy : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerData data;
    public AudioManager audio;


    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            player.life++;
            data.temporaryMaxLife++;
            audio.Play("LifeUp");
            Object.Destroy(this.gameObject);
        }
    }
}
