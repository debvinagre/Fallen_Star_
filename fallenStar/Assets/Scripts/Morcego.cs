using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morcego : MonoBehaviour
{
    private float dist;
    private Vector2 finalPoint, restPosition;
    [SerializeField] private Player player;
    [SerializeField] private BatVerifier vl,vr;
    [SerializeField] private BatStabilyzer bs;
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private float detect;
    [SerializeField] private float waitTime;
    [Range(0f,20f)]
    [SerializeField] private float xSpeed,ySpeed;
    private float timer = 0f;
    private bool canAttack, cooldown, isLeaping, notOnGround;
    public Animator anim;

    void Start(){
        canAttack = true;
        cooldown = false;
        Time.timeScale = 1f;
        anim.SetBool("activate", false);
        anim.SetBool("deactivate", false);
        anim.SetBool("freeTransition", true);
    }

    void Update(){
        dist = Vector2.Distance(this.transform.position, player.transform.position);
        if(dist <= detect && canAttack && !player.anim.GetBool("shadowHability")){
            batAttack();
        }
        if(cooldown){
            Timer();
            if(bs.landing && notOnGround){
                rig.velocity = new Vector2(0,0);
            }
        }
        if (vl.canLeap == false && vr.canLeap == false)
        {
            isLeaping = true;
        }else{
            isLeaping = false;
        }
        if(rig.velocity.y == 0f){
            anim.SetBool("activate" ,false);
            anim.SetBool("deactivate", true);
        }
    
    }

    void batAttack(){
        anim.SetBool("activate", true);
        anim.SetBool("deactivate", false);
        //Esquerda
        if(player.transform.position.x <= transform.position.x && (vl.canLeap || isLeaping)){
            canAttack = false;
            cooldown = true;
            rig.AddForce(new Vector2(-1f*xSpeed,-1f*ySpeed), ForceMode2D.Impulse);
        //Direita
        }else if(player.transform.position.x > transform.position.x && (vr.canLeap || isLeaping)){
            canAttack = false;
            cooldown = true;
            rig.AddForce(new Vector2(1f*xSpeed,-1f*ySpeed), ForceMode2D.Impulse);
        }
    }

    void Timer()
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            notOnGround = true;
        }
        if(timer > waitTime)
        {
            cooldown = false;
            timer = 0f;
            Time.timeScale = 1f;
            canAttack = true;
            notOnGround = false;
        }
    }
    
}
