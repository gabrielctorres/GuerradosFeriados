using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonecoNeve : Enemies
{   

    public GameObject prefabBola;
    float speed = 9f;
    Animator animBoneco;

    private float nextFire = 0f;
    private float fireRate = 1f;

    private void Start()
    {
        animBoneco = GetComponent<Animator>();
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        Life = enemiesData.life;
        LifeMax = enemiesData.lifeMax;
        Damage = enemiesData.damage;
        MoneyKill = Random.Range(enemiesData.moneyKíllMin, enemiesData.moneyKíllMax);
        lifeBackground.SetActive(false);        
    }

    private void Update()
    {

        
        UpdateLife();
        if (Life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Character") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;            
            GameObject bolaInstanciada = Instantiate(prefabBola, transform.position, Quaternion.identity);           
            Vector2 dir = (collision.transform.position - transform.position).normalized * speed;
            bolaInstanciada.GetComponent<Rigidbody2D>().velocity = dir;
            Destroy(bolaInstanciada, 6f);
            animBoneco.SetBool("Atacando", true);
        }        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {            
            animBoneco.SetBool("Atacando", false);
        }
    }
}
    
