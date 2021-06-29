using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
     
    [SerializeField] private Text lifeMarker;
    //Brilho Pulo Duplo
    public ParticleSystem firstSystem;
    //Brilho Gelo
    public ParticleSystem secondSystem;
    //Brilho Flutuar
    public ParticleSystem thirdSystem;
    private int maxLife = 3, life = 3;
    private bool Invencivel = false;
    private bool idle;
    public float Speed;
    public float JumpForce;
    public bool isJumping;
    public bool doubleJump;
    private bool isOnFloat = false;
    private bool IsOnBrilhinhos = false;
    private bool IsOnEspinhos = false;
    private bool TimeIsOver = true;
    private Rigidbody2D rig;
    private int dir;
    private float iceEffect;
    private bool isOnIce;
    private string state = "Luz";
    //Timer
    private float waitTime = 3f;
    private float timer = 0f;
    private float inWaitTime = 1f;
    private float invTimer = 0f;
    //Animator
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
        LifeUpdate();
        Jump();
        if(IsOnBrilhinhos)
        {
            Timer();
        }
        if(rig.velocity.y <-1f)
        {
            if(state == "Luz"){
                anim.SetBool("isFalling", true);
            }else{
                anim.SetBool("shadowIsFalling", true);
            }
        }else
        {
            if(state == "Luz"){
                anim.SetBool("isFalling", false);
            }else{
                anim.SetBool("shadowIsFalling", false);
            }
        }
        if(Invencivel)
        {
            inTimer();
        }
        if(IsOnEspinhos && !Invencivel)
        {
            TakeDamage();
        }
        if (rig.velocity.y == 0f && rig.velocity.x == 0f)
        {
            idle = true;
        }else{
            idle = false;
        }

    }

    void LifeUpdate()
    {
        lifeMarker.text = life.ToString() + "/" + maxLife.ToString();
    }

    void Move()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            if (idle == true && state == "Escuro")
            {
                anim.SetBool("shadowHability", true);
            }
        }
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
            if (anim.GetBool("shadowHability"))
            {
                anim.SetBool("shadowHability", false);
            }
        }
        if(Input.GetButtonUp("Horizontal")){
            if(state == "Luz"){
                anim.SetBool("running",false);
            }else{
                anim.SetBool("shadowRunning", false);
            }
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            if(state == "Luz"){
                anim.SetBool("running",true);
            }else{
                anim.SetBool("shadowRunning", true);
            }
            dir = 1;   
            transform.localScale = new Vector3(1,1,1);
        }else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            if(state == "Luz"){
                anim.SetBool("running",true);
            }else{
                anim.SetBool("shadowRunning", true);
            }
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
            if(state == "Luz"){
                anim.SetBool("jumping", true);
                anim.SetBool("endFall", false);
            }else{
                anim.SetBool("shadowJumping", true);
                anim.SetBool("shadowEndFall", false);
            }

            if(isJumping == false)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
            }
            else
            {
                if(state == "Luz"){
                    anim.SetBool("jumping", false);
                }else{
                    anim.SetBool("shadowJumping", false);
                }
                if(doubleJump && isOnFloat == false)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                    CreateDust();
                }
            }
            
        }
    }

    void Timer()
    {
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            if(state == "Luz"){
                anim.SetBool("exitFloat", true);
                anim.SetBool("enterFloat", false);
            }else{
                anim.SetBool("shadowExitFloat", true);
                anim.SetBool("shadowEnterFloat", false);
            }
            timer = 0f;
            Time.timeScale = 1f;
            TimeIsOver = true;
            IsOnBrilhinhos = false;
            isOnFloat = false;
            Physics2D.gravity = new Vector2(0, -9.8f);
        }
    }

    void inTimer()
    {
        invTimer += Time.deltaTime;
        if(invTimer > inWaitTime)
        {
            invTimer = 0f;
            Time.timeScale = 1f;
            Invencivel = false;
            if(state == "Luz"){
                anim.SetBool("damage",false);
            }else{
                anim.SetBool("damage", false);
            }
        }
    }

    void TakeDamage()
    {
        life -= 1;
        Invencivel = true;
        if(state == "Luz"){
            anim.SetBool("damage",true);
        }else{
            anim.SetBool("damage", true);
        }
    }
    
    public void Transformation(){

        if(state == "Luz"){
            anim.SetBool("transformShadow", true);
            anim.SetBool("transformLight", false);
            anim.SetBool("running", false);
            anim.SetBool("enterFloat", false);
            anim.SetBool("exitFloat", false);
            anim.SetBool("isFalling", false);
            anim.SetBool("jumping", false);
            anim.SetBool("endFall", false);
            state = "Escuro";
        }else{
            anim.SetBool("transformShadow", false);
            anim.SetBool("transformLight", true);
            anim.SetBool("shadowRunning", false);
            anim.SetBool("shadowEnterFloat", false);
            anim.SetBool("shadowExitFloat", false);
            anim.SetBool("shadowIsFalling", false);
            anim.SetBool("shadowJumping", false);
            anim.SetBool("shadowEndFall", false);
            anim.SetBool("shadowHability", false);
            state = "Luz";
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if(collision.gameObject.tag == "PlayerTransform"){
            Transformation();
        }
        */
              
        //Contato com o gelo
        if(collision.gameObject.tag == "Ice"){
            isOnIce = true;
            if(dir == 1){
                iceEffect = Speed * 1.3f;
                CreateDustGelo();
            }else if (dir == -1)
            {
                iceEffect = Speed * -1.3f;
                CreateDustGelo();
            }
        }
        //Contato com o chão
        if(collision.gameObject.tag == "Ground"){
            if(state == "Luz"){
                anim.SetBool("endFall", true);
                anim.SetBool("jumping", false);
                anim.SetBool("isFalling", false);
            }else{
                anim.SetBool("shadowEndFall", true);
                anim.SetBool("shadowJumping", false);
                anim.SetBool("shadowIsFalling", false);
            }
            if (!isOnIce)
            {
                iceEffect = 0f;
            }
            if(rig.velocity.y <= -10f){
                TakeDamage();
            }
        }
        //Contato com plataforma
        if(collision.gameObject.tag == "Platform"){
            if(state == "Luz"){
                anim.SetBool("endFall", true);
                anim.SetBool("jumping", false);
                anim.SetBool("isFalling", false);
            }else{
                anim.SetBool("shadowEndFall", true);
                anim.SetBool("shadowJumping", false);
                anim.SetBool("shadowIsFalling", false);
            }

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
            if(state == "Luz"){
                anim.SetBool("exitFloat", false);
                anim.SetBool("enterFloat", true);
                CreateDustFlutuar();
            }else{
                anim.SetBool("shadowExitFloat", false);
                anim.SetBool("shadowEnterFloat", true);
            }

            if(IsOnBrilhinhos && !TimeIsOver)
            {
                Physics2D.gravity = new Vector2(0, 0f);
                rig.velocity = new Vector2(0f, 1.4f);
            }
            
        }
        //Contato com os Espinhos
        if(collision.gameObject.tag == "Espinhos")
        {
            IsOnEspinhos = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //Contato com o transformador
        
        if(collision.gameObject.tag == "Ice"){
            isOnIce = false;
            if (isJumping == true)
            {
                
            }else{
                iceEffect = 0f;
            }
        }
        if(collision.gameObject.tag == "Ground"){
            if(state == "Luz"){
                anim.SetBool("endFall", false);
            }else{
                anim.SetBool("shadowEndFall", false);
            }
        }
        
        if(collision.gameObject.layer == 0)
        {
            isJumping = true;
        }        
        
        //Contato com os Espinhos
        if(collision.gameObject.tag == "Espinhos")
        {
            IsOnEspinhos = false;
        }
    }

    //Brilhos Pulo Duplo
    void CreateDust()
    {
        firstSystem.Play();
    }

    //Brilhos Gelo
    void CreateDustGelo()
    {
        secondSystem.Play();
    }

    //Brilhos Flutuar
    void CreateDustFlutuar()
    {
        thirdSystem.Play();
    }
}
