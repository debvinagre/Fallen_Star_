using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    [SerializeField] PlayerData data;
    public void Saída()
    {
        //Por enquanto dentro da Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        //Para quando estiver compilado
        SceneManager.LoadScene("Menu");
        data.maxLife = 3;

    }
}
