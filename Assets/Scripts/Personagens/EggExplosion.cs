using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggExplosion : MonoBehaviour
{
    CircleCollider2D colliderEgg;
    private SpriteRenderer sprite;
    public ParticleSystem explosao;
    float timerToExplosion;
    float timerToDestroy;
    bool candamage = false;
    bool canDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
        colliderEgg = GetComponent<CircleCollider2D>();
        timerToExplosion = 3f;
        timerToDestroy = 5f;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {       
        if(timerToExplosion > 0)
        {
            timerToExplosion -= Time.deltaTime;
            StartCoroutine(nameof(ChangeScale));
        }
        else
        {
            StopCoroutine(nameof(ChangeScale));           
            timerToExplosion = 0;
            candamage = true;
        }


        if (timerToDestroy > 0)
        {
            timerToDestroy -= Time.deltaTime;          
        }
        else
        {
            canDestroy = true;
            timerToDestroy = 0;
        }


        Color newColor = new Color(Random.value, Random.value, Random.value);

        sprite.color = newColor;

        if (canDestroy)
        {
            ParticleSystem particulaInstanciada = Instantiate(explosao, transform.position, Quaternion.identity);
            particulaInstanciada.Play();
            Destroy(this.gameObject);
        }

    }


    IEnumerator ChangeScale()
    {
        transform.localScale = new Vector3(0.8f, 0.8f, 0);
        yield return new WaitForSeconds(0.3f);
        transform.localScale = new Vector3(0.6f, 0.6f, 0);
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && candamage)
        {
            canDestroy = false;
            collision.GetComponent<Enemies>().TakeDamage(4);
            ParticleSystem particulaInstanciada = Instantiate(explosao, transform.position, Quaternion.identity);
            particulaInstanciada.Play();
            Destroy(this.gameObject);           
        }
    }

}
