using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Saci : Character
{
    private float nextFire = 0f;
    private float fireRate = 3f;


    List<Transform> targets = new List<Transform>();
    Transform target;
    bool canAttack;
    public GameObject tornado;
    private void Start()
    {
        anim = GetComponent<Animator>();        
        spriteRender = GetComponent<SpriteRenderer>();
        pathsPoints = GameObject.Find("PlayerController").GetComponent<Player>().pathPointsPlayer;
        wave = GameObject.Find("IA_Controller").GetComponent<WaveController>();
        lifeBackground.SetActive(false);
        Life = characterInfo.life;
        LifeMax = characterInfo.lifeMax;
        speed = characterInfo.speed;
        Damage = characterInfo.damage;        
        timerSlow = 3f;
        timerRegen = 2f;
        timerDamage = 2.5f;
        isCooldownSlow = false;
        isCooldownRegen = false;
        isCooldownDamage = false;
    }

    private void Update()
    {
        if (canAttack && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Attack();
        }
           

        Move();
        ControllerAnimation();
        UpdateLife();

        SlowPersonagem();
        GainDamage();
        RegenLife();
    }

    public override void Attack()
    {
       GameObject tornadoInstanciado = Instantiate(tornado, transform.position, Quaternion.identity);
       tornadoInstanciado.GetComponent<Tornado>().targetTornado = target;
       tornadoInstanciado.GetComponent<Tornado>().damage = Damage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            canAttack = true;
            target = collision.transform;
            enemyInformation = collision.GetComponent<Enemies>();
        }

        if (collision.CompareTag("BolaNeve"))
        {
            Debug.Log("Colidiu" + collision.name);
            TakeDamage(enemyInformation.Damage);         

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
            StartCoroutine("ActiveLifeBar");
            Destroy(collision.gameObject);
        }

        if (collision.GetComponent<OvelhaTarget>() != null)
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
            target = null;
            canAttack = false;
        }
    }
}
