using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabricaNoel : Enemies
{

    public WaveController waveControll;

    private void Start()
    {
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Character>() != null)
        {
            TakeDamage(collision.GetComponent<Character>().Damage);            
            Destroy(collision.gameObject);            
        }
    }


}
