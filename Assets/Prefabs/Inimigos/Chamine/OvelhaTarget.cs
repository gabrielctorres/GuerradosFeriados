using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvelhaTarget : MonoBehaviour
{
    public Transform positionTarget;
    public ParticleSystem ExplosaoTwitch;
    int damage;
    public int Damage { get => damage; set => damage = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(positionTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, positionTarget.position, 2 * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject,3f);
        }
        
    }
   

}
