using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotaSpawn : MonoBehaviour
{
    private float waitTime = 2f;
    private float timer = 0f;
    private bool TimeIsOver = true;
    public GameObject gotaObj;
    public Gota gota;

    void Start(){
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeIsOver)
        {
            Gota();
        }
        if (!TimeIsOver)
        {
            Timer();
        }
    }
    void Timer()
    {
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            timer = 0f;
            Time.timeScale = 1f;
            TimeIsOver = true;
        }
    }
    void Gota(){
        TimeIsOver = false;
        gotaObj.transform.position = transform.position;
        gotaObj.SetActive(true);
        gota.isOn = true;
        gota.isOff = false;
        
    }
}
