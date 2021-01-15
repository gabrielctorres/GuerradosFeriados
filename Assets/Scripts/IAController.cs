using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAController : MonoBehaviour
{
    private List<GameObject> spawnPointTowers = new List<GameObject>();
    private List<GameObject> spawnsSelecteds = new List<GameObject>();
   

    public List<GameObject> towersPrefab = new List<GameObject>();

    public IAData iaInformation;
    int spawnMax;
    int RandomIndex;

    public int SpawnMax { get => spawnMax; set => spawnMax = value; }

    private void Start()
    {
        SpawnMax = iaInformation.spawnMax;
        spawnPointTowers = GameObject.FindGameObjectsWithTag("TowersSpawn").ToList<GameObject>();
        RandomizeSpawn();
    }

    private void Update()
    {



    }

    public void RandomizeSpawn()
    {
        for (int i = 0; i < SpawnMax; i++)
        {
           RandomIndex = Random.Range(0, spawnPointTowers.Count);
           spawnsSelecteds.Add(spawnPointTowers[RandomIndex]);
        }        
        
        for (int k = 0; k < spawnsSelecteds.Count; k++)
        {
            GameObject tower = ReturnTower();
            if (spawnsSelecteds[k].transform.childCount == 0)
            {
                GameObject towerInstanciada = Instantiate(tower, spawnsSelecteds[k].transform.position, Quaternion.identity);
                towerInstanciada.transform.SetParent(spawnsSelecteds[k].transform);
            }
               

        }
    }

    private GameObject ReturnTower()
    {
        int RandomTower = 0;

        for (int j = 0; j < towersPrefab.Count; j++)
        {
            RandomTower = Random.Range(0, towersPrefab.Count);
            return towersPrefab[RandomTower];

        }
        return null;
    }



}
