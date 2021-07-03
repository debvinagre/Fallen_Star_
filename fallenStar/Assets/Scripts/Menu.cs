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

    //Chama jogo inicial (Fase Caverna)
    public void Jogar()
    {
        SceneManager.LoadScene(mapa1);
    }
    public void Cenas()
    {
        SceneManager.LoadScene(cenas);
    }

    public void Créditos()
    {
        SceneManager.LoadScene(creditos);
    }
    public void Saída()
    {
        //Por enquanto dentro da Unity
        UnityEditor.EditorApplication.isPlaying = false;
        //Para quando estiver compilado
        //Application.Quit();
    }
}
