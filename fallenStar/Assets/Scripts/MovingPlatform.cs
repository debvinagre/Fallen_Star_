using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class MovingPlatform : MonoBehaviour{

    [SerializeField] private float maxV;
    [SerializeField] private float maxH;
    [SerializeField] bool MoveV;
    [SerializeField] bool MoveH;
    [SerializeField] float movingSpeedX, movingSpeedY;
    [SerializeField] private GameObject Player;
    private float minV;
    private float minH;
    private float speedX;
    private float speedY;
    private bool isOnMaxV = false;
    private bool isOnMaxH = false;
    private bool isOnMinV = true;
    private bool isOnMinH = true;
    private Rigidbody2D plat;
    [SerializeField] private Transform tran;
    // Start is called before the first frame update
    void Start()
    {
        plat = GetComponent<Rigidbody2D>();
        //tran = GetComponent<Transform>();
        minH = tran.position.x;
        minV = tran.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        platformMove();
    }

    void platformMove(){
        if(MoveH){
            //Verifica se está na posição máxima horizontal
            if(tran.position.x >= maxH+minH){
                isOnMaxH = true;  
            }else{
                isOnMaxH = false;
            }
            //Verifica se está na posição mínima horizontal
            if(tran.position.x <= minH){
                isOnMinH = true;
            }else{
                isOnMinH = false;
            }
            //Configura o movimento
            if(isOnMaxH){
                speedX = -movingSpeedX;
            }
            if(isOnMinH){
                speedX = movingSpeedX;
            }

        }else{
            speedX = 0f;
        }

        if(MoveV){

            //Verifica se está na posição máxima vertical
            if(tran.position.y >= maxV+minV){
                isOnMaxV = true;  
            }else{
                isOnMaxV = false;
            }
            //Verifica se está na posição mínima vertical        
            if(tran.position.y <= minV){
                isOnMinV = true;
            }else{
                isOnMinV = false;
            }
            //Configura o movimento        
            if(isOnMaxV){
                speedY = -movingSpeedY;
            }        
            if(isOnMinV){
                speedY = movingSpeedY;
            }

        }else{
            speedY = 0f;
        }
        //Aplica o movimento configurado à plataforma  
        //plat.velocity = new Vector2(speedX,speedY);
        tran.Translate(new Vector2(speedX,speedY)*Time.deltaTime);
    }
    //Transforma o player em filho ao entrar em contato com um trigger colocado na plataforma
    private void OnTriggerEnter2D(Collider2D pl){
        //Debug.Log(pl.name);
        if(pl.gameObject == Player){
            Player.transform.parent = tran;
        }
    }
    //Retorna o player ao estado normal
    private void OnTriggerExit2D(Collider2D pl){
        if(pl.gameObject == Player){
            Player.transform.parent = null;
        }
    }
}
