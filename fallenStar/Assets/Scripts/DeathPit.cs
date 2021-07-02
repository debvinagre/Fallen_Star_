using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Player player;

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.layer == 3)
        {
            player.transform.position = player.spawnPoint;
            player.TakeDamage();
            player.rig.velocity = new Vector2(0f,0f);
        }
    }
}
