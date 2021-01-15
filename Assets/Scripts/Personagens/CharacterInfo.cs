using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Datas/CharacterInfo", order = 1)]
public class CharacterInfo : ScriptableObject
{
    public int life;
    public int lifeMax;
    public int damage;
    public float speed;
    public int price;

}
