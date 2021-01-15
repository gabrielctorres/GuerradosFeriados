using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamine : Enemies
{
    public List<GameObject> ovelhas = new List<GameObject>();        
    private float nextFire = 0f;
    private float fireRate = 3.5f;
    Animator animChamine;
    Character Target;

    private void Start()
    {
        animChamine = GetComponent<Animator>();
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

            int randomOvelha = Random.Range(0, ovelhas.Count);
            nextFire = Time.time + fireRate;
            GameObject ovelhaInstanciada = Instantiate(ovelhas[randomOvelha], transform.position, Quaternion.identity);
            ovelhaInstanciada.GetComponent<OvelhaTarget>().Damage = Damage;
            Vector2 dir = new Vector2(1, 1);
            ovelhaInstanciada.GetComponent<OvelhaTarget>().positionTarget = collision.transform;
            animChamine.SetBool("Atacando", true);

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            animChamine.SetBool("Atacando", false);
        }
    }
}
