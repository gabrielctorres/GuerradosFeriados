using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presente : Enemies
{

    public GameObject explosao;    
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Character"))
        {
            anim.SetBool("Explodindo", true);
            StartCoroutine(nameof(Explosao));
            collision.GetComponent<Character>().TakeDamage(enemiesData.damage);
        }

    }


   IEnumerator Explosao()
    {
        while(anim.GetCurrentAnimatorStateInfo(0).IsName("Explodindo"))
        {
            yield return null;
        }
        GameObject explosaoInstanciada = Instantiate(explosao, transform.position, Quaternion.identity);
        explosaoInstanciada.GetComponent<ParticleSystem>().Play();
        Destroy(this.gameObject);
        yield return null;
    }
}
