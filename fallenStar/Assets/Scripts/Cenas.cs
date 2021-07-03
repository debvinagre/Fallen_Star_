using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cenas : MonoBehaviour
{
   
    public void selectScene()
    {
        switch (this.gameObject.name)
        {
        case "Cena1.1":
            SceneManager.LoadScene ("Teste1");
            break;
        case "Cena2Botão":
            SceneManager.LoadScene("Cena2");
            break;
        case "Cena3Botão":
            SceneManager.LoadScene("Cena3");
            break;
           
        }
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
