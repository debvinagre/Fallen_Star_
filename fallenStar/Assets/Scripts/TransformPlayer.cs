using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPlayer : MonoBehaviour
{
    [SerializeField] private Player player;


    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "PlayerTransform"){
            player.Transformation();
        }
    }
}
