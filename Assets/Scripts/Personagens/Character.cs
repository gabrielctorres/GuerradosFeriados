using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Character : MonoBehaviour
{
    public float timerSlow;
    public bool isCooldownSlow;
    public float timerRegen;
    public bool isCooldownRegen;
    public float timerDamage;
    public bool isCooldownDamage;

    private int damage;
    public float speed;
    public Enemies enemyInformation;
    public CharacterInfo characterInfo;
    public List<GameObject> pathsPoints = new List<GameObject>();  
    public Animator anim;
    public SpriteRenderer spriteRender;
    Vector3 posPoint;
    private int waypointIndex;
    public Image lifeBar;
    public GameObject lifeBackground;
    public TextMeshProUGUI txtLife;
    public WaveController wave;
    private int life;
    private int maxLife;

    public int Life { get => life; set => life = value; }
    public int LifeMax { get => maxLife; set => maxLife = value; }
    public int Damage { get => damage; set => damage = value; }

    public abstract void Attack();




    public virtual void Move()
    {     
        transform.position = Vector2.MoveTowards(transform.position, pathsPoints[waypointIndex].transform.position, Time.deltaTime * speed);

        if(Vector2.Distance(transform.position,pathsPoints[waypointIndex].transform.position) < 0.1f)
        {
            
            if(waypointIndex < pathsPoints.Count - 1)
            {
                waypointIndex++;
            }     
        }       
    }

    public void UpdateLife()
    {
        txtLife.text = Life.ToString() + " / " + LifeMax.ToString();
        lifeBar.fillAmount = (float)Life / (float)LifeMax;

        if (Life <= 0)
        {
            wave.charactersInScene.Remove(this.gameObject);
            wave.canGameover = true; 
            Destroy(this.gameObject);           
        }
    }


    public virtual void TakeDamage(int damage)
    {
        StartCoroutine("ActiveLifeBar");
        this.Life -= damage;       
    }

    public  virtual void ControllerAnimation()
    {
        if (transform.position.x <= 0 && transform.position.y >= 0)
        {
            anim.SetBool("Costas", true);
            anim.SetBool("Frente", false);
            spriteRender.flipX = false;
        }
        else if (transform.position.x >= 0 && transform.position.y >= 0)
        {
            anim.SetBool("Costas", false);
            anim.SetBool("Frente", true);
            spriteRender.flipX = true;

        }
        else if (transform.position.x >= 0.21 && transform.position.x <= 4.89 && transform.position.y >= 2.18 && transform.position.y <= 6.97)
        {
            anim.SetBool("Costas", false);
            anim.SetBool("Frente", true);
            spriteRender.flipX = false;
        }
        else if (transform.position.x >= 0 && transform.position.y <= 0)
        {
            anim.SetBool("Costas", false);
            anim.SetBool("Frente", true);
            spriteRender.flipX = false;
        }

    }


    public Vector3 ReturnDirection()
    {
        if(waypointIndex < pathsPoints.Count - 1)
        {
            posPoint = new Vector3(pathsPoints[waypointIndex + 1].transform.position.x, pathsPoints[waypointIndex + 1].transform.position.y, 0);
        }            

        Vector3 posCharacter = new Vector3(transform.position.x, transform.position.x, 0);        
        Vector3 dir = posPoint - posCharacter;
        return dir;
    }

    public void SlowPersonagem()
    {
        if (timerSlow > 0 && isCooldownSlow)
        {
            timerSlow -= Time.deltaTime;
            speed = 0.3f;
        }

        if (timerSlow <= 0)
        {
            isCooldownSlow = false;
            timerSlow = 0;
            speed = characterInfo.speed;
        }
    }

    public void RegenLife()
    {
        if (timerRegen > 0 && isCooldownRegen)
        {
            timerRegen -= Time.deltaTime;
            if (Life <= LifeMax)
            {
                Life++;
            }

        }
        if (timerRegen <= 0)
        {
            isCooldownRegen = false;
            timerRegen = 0;
        }
    }

    public void GainDamage()
    {
        if (timerDamage > 0 && isCooldownDamage)
        {
            timerDamage -= Time.deltaTime;
            Damage = characterInfo.damage * 2;

        }
        if (timerDamage <= 0)
        {
            isCooldownDamage = false;
            timerDamage = 0;
            Damage = characterInfo.damage;
        }
    }

    public IEnumerator ActiveLifeBar()
    {
        lifeBackground.SetActive(true);
        yield return new WaitForSeconds(5f);
        lifeBackground.SetActive(false);
    }

    public void Path(List<GameObject> paths)
    {
        pathsPoints = paths;
    }

    public void LevelUP(int value)
    {
        LifeMax = characterInfo.lifeMax + value;
        Life = characterInfo.life + value;
        damage = characterInfo.damage + value /2;
        
    }

}
