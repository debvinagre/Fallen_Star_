using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public AudioManager audio;
    //Brilho Pulo Duplo
    public ParticleSystem firstSystem;
    //Brilho Gelo
    public ParticleSystem secondSystem;
    //Brilho Flutuar
    public ParticleSystem thirdSystem;
    public int life;
    private bool Invencivel = false;
    private bool idle;
    public float Speed;
    public float JumpForce;
    public bool isJumping;
    public bool doubleJump;
    private bool isOnFloat = false;
    private bool IsOnBrilhinhos = false;
    private bool IsOnBrilhinhosFinal = false;
    private bool IsOnEspinhos = false;
    private bool TimeIsOver = true;
    private bool canMove = true;
    public bool firstCamC, secondCamC;
    public Rigidbody2D rig;
    private int dir;
    private float iceEffect;
    private bool isOnIce, airJump, achatar,achatado;
    public string state = "Luz";
    private BoxCollider2D box;
    [SerializeField] private LayerMask teto;
    //Timer
    private float waitTime = 3f;
    private float timer = 0f;
    private float inWaitTime = 1.5f;
    private float invTimer = 0f;
    //Animator
    public Animator anim;
    public Vector2 spawnPoint;
    [SerializeField] private string sceneToLoad;
    //Scriptable
    [SerializeField] private PlayerData data;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = new Vector2(0, -9.8f);
        life = data.maxLife;
        isJumping = true;
        rig = GetComponent<Rigidbody2D>();
        Time.timeScale = 1f;
        anim.SetBool("freeTransition",true);
        box = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(canMove){
            Move();
        }
        if(!anim.GetBool("shadowHability")){
            Jump();
        }
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
            if (!anim.GetBool("shadowHability")){
                TakeDamage();
            }
        }
        if (rig.velocity.y == 0f && rig.velocity.x == 0f)
        {
            idle = true;
        }else{
            idle = false;
        }
        if(life == 0){
            Die();
        }

    }


    void Achatar(bool valor){
        if(valor == true){
            if (idle == true && state == "Escuro")
            {
                audio.Play("PoderSombra");
                achatado = true;
                anim.SetBool("shadowHability", true);
                box.size = new Vector2(0.5f,0.35f);
                box.offset = new Vector2(0,-0.25f);
                
            }
        }else{
            if (anim.GetBool("shadowHability"))
            {
                audio.Play("PoderSombra");
                achatado = false;
                anim.SetBool("shadowHability", false);
                box.size = new Vector2(0.5f,0.9f);
                box.offset = new Vector2(0,0);
            }
        }
    }

    void Move()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){      
            Achatar(true);
            achatar = true;
        }
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
            achatar = false;
        }
        if(achatado && !achatar){
            var sensor = Physics2D.Raycast((Vector2)transform.position + Vector2.down/3, Vector2.up,1,teto);
            if(sensor.collider == null){
                Achatar(false);
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
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
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
                rig.velocity = new Vector2(0f, JumpForce);
                doubleJump = true;
                audio.Play("Pulo");
            }
            else
            {
                if((anim.GetBool("isFalling") || anim.GetBool("shadowIsFalling")) && airJump){
                    doubleJump = true;
                    airJump = false;
                }
                if(state == "Luz"){
                    anim.SetBool("jumping", false);
                }else{
                    anim.SetBool("shadowJumping", false);
                }
                if(doubleJump && isOnFloat == false)
                {
                    rig.velocity = new Vector2(0f, JumpForce);
                    doubleJump = false;
                    airJump = false;
                    audio.Play("PuloDuplo");
                    CreateDust();
                }
            }
            
        }
    }

    void Die(){
        SceneManager.LoadScene(sceneToLoad);
        data.temporaryMaxLife = 0;
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
                anim.SetBool("shadowDamage", false);
                Debug.Log(anim.GetBool("shadowDamage"));
            }
        }
    }

    public void TakeDamage()
    {
        audio.Play("Dano");
        life -= 1;
        Invencivel = true;
        if(state == "Luz"){
            anim.SetBool("damage",true);
        }else{
            anim.SetBool("shadowDamage", true);
        }
    }
    
    public void Transformation(){

        if(state == "Luz"){
            audio.Play("LuzSombra");
            anim.SetBool("transformShadow", true);
            anim.SetBool("transformLight", false);
            anim.SetBool("running", false);
            anim.SetBool("enterFloat", false);
            anim.SetBool("exitFloat", false);
            anim.SetBool("isFalling", false);
            anim.SetBool("jumping", false);
            anim.SetBool("endFall", false);
            anim.SetBool("damage",false);
            state = "Escuro";
        }else{
            audio.Play("SombraLuz");
            anim.SetBool("transformShadow", false);
            anim.SetBool("transformLight", true);
            anim.SetBool("shadowRunning", false);
            anim.SetBool("shadowEnterFloat", false);
            anim.SetBool("shadowExitFloat", false);
            anim.SetBool("shadowIsFalling", false);
            anim.SetBool("shadowJumping", false);
            anim.SetBool("shadowEndFall", false);
            anim.SetBool("shadowHability", false);
            anim.SetBool("shadowDamage", false);
            state = "Luz";
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpawnPoint")
        {
            spawnPoint = new Vector2(collision.gameObject.transform.position.x,collision.gameObject.transform.position.y);
        }

        if (collision.gameObject.tag == "FilhoOff")
        {
            firstCamC = true;
        }
        if (collision.gameObject.tag == "FilhoOff2")
        {
            secondCamC = true;
        }
              
        //Contato com o gelo
        if(collision.gameObject.tag == "Ice"){
            isOnIce = true;
            audio.Play("Gelo");
            if(dir == 1){
                iceEffect = Speed * 1.3f;
                CreateDustGelo();
            }else if (dir == -1)
            {
                iceEffect = Speed * -1.3f;
                CreateDustGelo();
            }
        }
        //Contato com o ch??o
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
            if(rig.velocity.y <= -15f){
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
            if (!isOnIce)
            {
                iceEffect = 0f;
            }
            if(rig.velocity.y <= -15f){
                TakeDamage();
            }

        }
        //Contato com layer 0 no geral
        if(collision.gameObject.layer == 0)
        {
            isJumping = false;
            airJump = true;
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
                audio.Play("FlutuarInicio");
                Physics2D.gravity = new Vector2(0, 0f);
                rig.velocity = new Vector2(0f, 2.1f);
            }
            
        }
        if(collision.gameObject.tag == "BrilhoFinal")
        {
            IsOnBrilhinhosFinal = true;
            isOnFloat = true;
            if(state == "Luz"){
                anim.SetBool("exitFloat", false);
                anim.SetBool("enterFloat", true);
                CreateDustFlutuar();
            }else{
                anim.SetBool("shadowExitFloat", false);
                anim.SetBool("shadowEnterFloat", true);
            }

            if(IsOnBrilhinhosFinal)
            {
                Physics2D.gravity = new Vector2(0, 0f);
                rig.velocity = new Vector2(0f, 4.1f);
                canMove = false;
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
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform"){
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
