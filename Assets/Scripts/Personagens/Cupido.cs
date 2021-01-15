using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Cupido : Character
{
    private float nextFire = 0f;
    private float fireRate = 1f;
    public bool canMove;
    Transform target;   
    public GameObject flecha;
    public override void Attack()
    {       
        if (target != null && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 newRotation = new Vector3(0, 0, -60);
            GameObject flechaInstanciada = Instantiate(flecha, transform.position,Quaternion.identity);
            flechaInstanciada.transform.Rotate(newRotation);
            Vector2 dir = (target.transform.position - transform.position) * 3f;
            flechaInstanciada.GetComponent<Rigidbody2D>().velocity = dir;          
        }

           
    }

    private void Start()
    {        
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        pathsPoints = GameObject.Find("PlayerController").GetComponent<Player>().pathPointsPlayer;
        wave = GameObject.Find("IA_Controller").GetComponent<WaveController>();
        lifeBackground.SetActive(false);
        speed = characterInfo.speed;
        Damage = characterInfo.damage;
        canMove = true;
        Life = characterInfo.life;
        LifeMax = characterInfo.lifeMax;
        timerSlow = 3f;
        timerRegen = 2f;
        timerDamage = 2.5f;
        isCooldownSlow = false;
        isCooldownRegen = false;
        isCooldownDamage = false;
    }

    private void Update()
    {    

       
        if (canMove)
            Move();
        else
            Attack();


        ControllerAnimation();
        SlowPersonagem();
        RegenLife();
        GainDamage();
        UpdateLife();
        GetCupido();
    }


    public void GetCupido()
    {
        Vector2 direction = new Vector2(1, 0.5f);        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);      
        if (hit.collider.GetComponent<Cupido>() != null && !hit.collider.GetComponent<Cupido>().canMove)
        {
            this.canMove = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            canMove = false;
            target = collision.transform;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            enemyInformation = collision.GetComponent<Enemies>();
        }
        if (collision.CompareTag("BolaNeve"))
        {
            TakeDamage(enemyInformation.enemiesData.damage);                
        }

        if (collision.CompareTag("Egg_Regen"))
        {
            isCooldownRegen = true;
            StartCoroutine("ActiveLifeBar");
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Egg_Damage"))
        {
            isCooldownDamage = true;
            Destroy(collision.gameObject);
        }


        if(collision.GetComponent<OvelhaTarget>() != null)
        {
            TakeDamage(collision.GetComponent<OvelhaTarget>().Damage);
            isCooldownSlow = true;
            ParticleSystem twitchInstanciado = Instantiate(collision.GetComponent<OvelhaTarget>().ExplosaoTwitch, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);           
            twitchInstanciado.Play();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            canMove = true;
            target = null;
        }
    } 
}
