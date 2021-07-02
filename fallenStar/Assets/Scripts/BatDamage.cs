using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDamage : MonoBehaviour
{
    [SerializeField] private Player player;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == 9){
            player.TakeDamage();
        }
    }
}
