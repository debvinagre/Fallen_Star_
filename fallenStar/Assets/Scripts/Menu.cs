using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public string mapa1;
    public string cenas;
    public string creditos;
    public AudioManager audio;

    //Chama jogo inicial (Fase Caverna)
    public void Jogar()
    {
        SceneManager.LoadScene(mapa1);
        audio.Play("Button");
    }
    public void Cenas()
    {
        SceneManager.LoadScene(cenas);
        
    }

    public void Créditos()
    {
        
    }
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
