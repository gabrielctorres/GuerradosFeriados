using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class Enemies : MonoBehaviour
{
    public EnemiesData enemiesData;
    public Character personagemData;
    public Image lifeBar;
    public GameObject lifeBackground;
    public TextMeshProUGUI txtLife;
    public Player playerController;    
    public IAData iaData;
    float life, lifeMax;
    int damage;
    int moneyKill;
    public float Life { get => life; set => life = value; }
    public float LifeMax { get => lifeMax; set => lifeMax = value; }
    public int Damage { get => damage; set => damage = value; }
    public int MoneyKill { get => moneyKill; set => moneyKill = value; }   



    public void UpdateLife()
    {
        txtLife.text = Life.ToString() + " / " + LifeMax.ToString();
        lifeBar.fillAmount = (float)Life / (float)LifeMax;
    }

    public virtual void TakeDamage(int damage)
    {
        StartCoroutine(nameof(ActiveLifeBar));
        life -= damage;
        if(life <= 0)
        {
            playerController.SpawnTextPopUP(MoneyKill,this.transform);
            playerController.money += MoneyKill;            
        }
    }


    public IEnumerator ActiveLifeBar()
    {
        lifeBackground.SetActive(true);
        yield return new WaitForSeconds(5f);
        lifeBackground.SetActive(false);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Character>() != null)
        {
            personagemData = collision.GetComponent<Character>();
        }

        if (collision.CompareTag("Flecha"))
        {
            if(personagemData != null)
            {
                TakeDamage(personagemData.Damage);
            }                    
        }

        if (collision.CompareTag("Tornado"))
        {
            Destroy(collision.gameObject,4f);
            if (personagemData != null)
            {
                TakeDamage(collision.GetComponent<Tornado>().damage);               
            }
        }
    }




    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>() != null)
        {
            personagemData = null;
        }    
        
    }

}
