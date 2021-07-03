using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fases2 : MonoBehaviour
{

    public string mapa1;
    public string mapa2;
    public string mapa3;

    //Jogar1 (mapa1), Jogar2(mapa2) e Jogar3(mapa3) são respectivamente as cenas Caverna, Floresta e Montanha
    public void Jogar1()
    {
        SceneManager.LoadScene(mapa1);
    }

    public void Jogar2()
    {
        SceneManager.LoadScene(mapa2);
    }

    public void Jogar3()
    {
        SceneManager.LoadScene(mapa3);
    }
}
