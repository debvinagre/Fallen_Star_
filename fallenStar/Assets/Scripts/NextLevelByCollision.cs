using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelByCollision : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] PlayerData data;

    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player")
        {
            data.maxLife+=data.temporaryMaxLife;
            data.temporaryMaxLife = 0;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
