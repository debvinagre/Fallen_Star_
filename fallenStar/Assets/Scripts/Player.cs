using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
     
     [SerializeField] private Text lifeMarker;
     //private int maxLife = 3, life = 3;
     public float Speed;
     public float JumpForce;
     public bool isJumping;
     public bool doubleJump;
     private bool isOnFloat = false;
     private bool IsOnBrilhinhos = false;
     private bool TimeIsOver = true;
     private Rigidbody2D rig;
     private int dir;
     private float iceEffect;
     private bool isOnIce;
     //Timer
     private float waitTime = 3f;
     private float timer = 0f;
     //Animator
     public bool idle = true;
     public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Time.timeScale = 1f;
        anim.SetBool("freeTransition",true);
    }
    // Update is called once per frame
    void Update()
    {
        //lifeMarker.text = life.ToString() + "/" + maxLife.ToString();
        Move();
        Jump();
        if(IsOnBrilhinhos)
        {
            Timer();
        }
        if(rig.velocity.y <-1f)
        {
            anim.SetBool("isFalling", true);
        }else
        {
            anim.SetBool("isFalling", false);
        }
    }

    void Move()
    {
        if(Input.GetButtonUp("Horizontal")){
            anim.SetBool("running",false);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            anim.SetBool("running",true);
            dir = 1;   
            transform.localScale = new Vector3(1,1,1);
        }else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            anim.SetBool("running",true);
            dir = -1;
            transform.localScale = new Vector3(-1,1,1);
        }
        float horizontalMovement = Input.GetAxis("Horizontal") * Speed;
        float verticalMovement = rig.velocity.y;

        rig.velocity = new Vector2(horizontalMovement + iceEffect, verticalMovement);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            anim.SetBool("jumping", true);
            anim.SetBool("endFall", false);

            if(isJumping == false)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
            }
            else
            {
                anim.SetBool("jumping", false);
                if(doubleJump && isOnFloat == false)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
            
        }
    }

    void Timer()
    {
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            anim.SetBool("exitFloat", true);
            anim.SetBool("enterFloat", false);
            timer = 0f;
            Time.timeScale = 1f;
            TimeIsOver = true;
            IsOnBrilhinhos = false;
            isOnFloat = false;
            Physics2D.gravity = new Vector2(0, -9.8f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        //Contato com o gelo
        if(collision.gameObject.tag == "Ice"){
            isOnIce = true;
            if(dir == 1){
                iceEffect = Speed * 1.3f;
            }else if (dir == -1)
            {
                iceEffect = Speed * -1.3f;
            }
        }
        //Contato com o chão
        if(collision.gameObject.tag == "Ground"){
            anim.SetBool("endFall", true);
            anim.SetBool("jumping", false);
            anim.SetBool("isFalling", false);
            if (!isOnIce)
            {
                iceEffect = 0f;
            }
        }
        //Contato com plataforma
        if(collision.gameObject.tag == "Platform"){
            anim.SetBool("endFall", true);
            anim.SetBool("jumping", false);
            anim.SetBool("isFalling", false);

        }
        //Contato com layer 0 no geral
        if(collision.gameObject.layer == 0)
        {
            isJumping = false;
        }
        //Contato com os brilhos
        if(collision.gameObject.tag == "Brilhinhos")
        {
            IsOnBrilhinhos = true;
            TimeIsOver = false;
            isOnFloat = true;
            anim.SetBool("exitFloat", false);
            anim.SetBool("enterFloat", true);

            if(IsOnBrilhinhos && !TimeIsOver)
            {
                Physics2D.gravity = new Vector2(0, 0f);
                rig.velocity = new Vector2(0f, 1.4f);
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Ice"){
            isOnIce = false;
            if (isJumping == true)
            {
                
            }else{
                iceEffect = 0f;
            }
        }
        if(collision.gameObject.tag == "Ground"){
            anim.SetBool("endFall", false);
        }
        
        if(collision.gameObject.layer == 0)
        {
            isJumping = true;
        }
    }
}
