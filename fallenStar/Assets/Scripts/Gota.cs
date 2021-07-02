using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gota : MonoBehaviour
{
    [SerializeField] private Player player;
    public GameObject gota;
    public bool isOff = false, isOn = true;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            gota.SetActive(false);
            player.TakeDamage();
            isOff = true;
            isOn = false;
        }
        if(collision.gameObject.tag == "Ground"){
            gota.SetActive(false);
            isOff = true;
            isOn = false;
        }
    }
}
