using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Saída()
    {
        //Por enquanto dentro da Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        //Para quando estiver compilado
        Application.Quit();
    }
}
