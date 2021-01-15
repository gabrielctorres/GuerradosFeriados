using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public Transform targetTornado;
    int speed;
    public int damage;
    float timerDamage;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(2, 4);
        timerDamage = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTornado != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetTornado.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemies>() != null)
        {
            if(timerDamage > 0)
            {
                timerDamage -= Time.deltaTime;
            }
            else
            {
                collision.GetComponent<Enemies>().TakeDamage(1);
                timerDamage = 0f;
            }
        }
    }
}
