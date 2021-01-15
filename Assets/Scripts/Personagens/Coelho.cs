using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Coelho : Character
{
    public int randomTime;
    public List<GameObject> eggList = new List<GameObject>();

    private int speedBola;
    private void Start()
    {       
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();       
        wave = GameObject.Find("IA_Controller").GetComponent<WaveController>();
        InvokeRepeating("Attack", randomTime, randomTime);
        lifeBackground.SetActive(false);
        Life = characterInfo.life;
        LifeMax = characterInfo.lifeMax;
        speed = characterInfo.speed;
        Damage = characterInfo.damage;
        pathsPoints = GameObject.Find("PlayerController").GetComponent<Player>().pathPointsPlayer;
    }

    private void Update()
    {        
        Move();
        ControllerAnimation();
        SlowPersonagem();
        RegenLife();
        GainDamage();
        UpdateLife();
    }

    public override void Attack()
    {
        int randomValue = Random.Range(0, 3);
        Instantiate(eggList[randomValue], transform.position, Quaternion.identity);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyInformation = collision.GetComponent<Enemies>();
        }

        if (collision.CompareTag("BolaNeve"))
        {
            Debug.Log("Colidiu" + collision.name);
            TakeDamage(enemyInformation.Damage);            
          
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

}
