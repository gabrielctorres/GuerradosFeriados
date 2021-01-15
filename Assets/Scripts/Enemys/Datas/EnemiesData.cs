using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Datas/EnemyData",order = 1)]
public class EnemiesData : ScriptableObject
{
    public float life;
    public float lifeMax;
    public int damage;
    public int moneyKíllMin;
    public int moneyKíllMax;
}
