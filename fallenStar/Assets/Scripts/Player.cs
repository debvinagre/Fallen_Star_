using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

     public float Speed;
     public float JumpForce;
     public bool isJumping;
     public bool doubleJump;
     private bool IsOnBrilhinhos = false;
     private bool TimeIsOver = true;
     private Rigidbody2D rig;
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
        Move();
        Jump();
        if(IsOnBrilhinhos)
        {
            Timer();
        }
    }

    void Move()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            anim.SetBool("running",true);
            transform.localScale = new Vector3(1,1,1);
        }else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            anim.SetBool("running",true);
            transform.localScale = new Vector3(-1,1,1);
        }else if(Input.GetButtonUp("Horizontal")){
            anim.SetBool("running",false);
        }
        float horizontalMovement = Input.GetAxis("Horizontal") * Speed;
        float verticalMovement = rig.velocity.y;

        rig.velocity = new Vector2(horizontalMovement, verticalMovement);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(isJumping == false)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
            }
            else
            {
                if(doubleJump)
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
            Physics2D.gravity = new Vector2(0, -9.8f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 0)
        {
            isJumping = false;
        }

        if(collision.gameObject.tag == "Brilhinhos")
        {
            IsOnBrilhinhos = true;
            TimeIsOver = false;
            anim.SetBool("exitFloat", false);
            anim.SetBool("enterFloat", true);

            if(IsOnBrilhinhos && !TimeIsOver)
            {
                Physics2D.gravity = new Vector2(0, 0f);
                rig.velocity = new Vector2(0f, 1.5f);
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 0)
        {
            isJumping = true;
        }
    }
}