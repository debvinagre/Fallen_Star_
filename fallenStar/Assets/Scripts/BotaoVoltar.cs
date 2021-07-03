using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoVoltar : MonoBehaviour
{
    public void backToMainMenu()
    {
        SceneManager.LoadScene ("Menu");
    }
}
