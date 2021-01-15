using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class WaveController : MonoBehaviour
{
    public int waveCurrent = 0;
    private int waveMax = 35;
    public bool canGameover = false;
    int total;
    public List<GameObject> charactersInScene = new List<GameObject>();


    public TextMeshProUGUI txtWaveCount;


    public GameObject menuCardBoard;     
    public GameObject menuGameover;
    public TextMeshProUGUI textGamerover;
    public TextMeshProUGUI textAviso;
    public GameObject buttonStart;
    public GameObject buttonSpeed;
    public GameObject menuInformation;
    public IAController iaController;
    public FabricaNoel fabricaData;
    public Player playerController;
    public bool startWave = false;

    private void Start()
    {
        iaController = GetComponent<IAController>();
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        total += playerController.PriceCoelho + playerController.PriceCupido + playerController.PriceSaci;
        menuGameover.SetActive(false);
        buttonSpeed.SetActive(false);
        buttonStart.SetActive(true);
        menuInformation.SetActive(true);
        menuCardBoard.SetActive(true);
    }


    public void WaveStart()
    {
        if(waveCurrent < waveMax)
        {
            Time.timeScale = 1;
            charactersInScene.Clear();
            buttonStart.SetActive(false);
            buttonSpeed.SetActive(true);
            menuInformation.SetActive(false);
            waveCurrent++;          
            total += playerController.PriceCoelho + playerController.PriceCupido + playerController.PriceSaci;
        }
        else if( waveCurrent >= waveMax)
        {
            Time.timeScale = 1;
            OpenMenu();
            textGamerover.text = "Vitoria";
            textAviso.text = "Parabens voce conseguiu passar da ultima wave";
        }

    }

    public void SpeedGame()
    {
        Time.timeScale = 2;
    }

    void WaveRestart()
    {
        Time.timeScale = 1;
        charactersInScene.Clear();
        buttonStart.SetActive(true);
        buttonSpeed.SetActive(false);
        menuInformation.SetActive(true);        
        total += playerController.PriceCoelho + playerController.PriceCupido + playerController.PriceSaci;
    }

    private void Update()
    {
        txtWaveCount.text = "Wave: " + waveCurrent.ToString() + " / " + waveMax.ToString();

        NextWave();

        if (charactersInScene[charactersInScene.Count - 1] == null)
        {
            GameOver();
        }
    }

    public void NextWave()
    {
        if (fabricaData.Life <= 0)
        {
            Debug.Log("Agurando proxima Wave");            
            WaveRestart();
            iaController.RandomizeSpawn();
            DestroyCharacterScene();
            BuffLifeFabrica();
            IncreaseSpawnMaxTowers();
        }
    }

    public void GameOver()
    {
        if(fabricaData.Life > 0 && temDinheiro())
        {
            WaveRestart();            
        }
        else if(fabricaData.Life > 0 && !temDinheiro())
        {            
            OpenMenu();
            textGamerover.text = "Derrota";
            textAviso.text = "Parece que voce nao tinha dinheiro para continuar";
        }
    }

    public bool temDinheiro()
    {
        if(playerController.money >= playerController.PriceCoelho || playerController.money >= playerController.PriceCupido || playerController.money >= playerController.PriceSaci)
        {
            return true;
        }
        else if(playerController.money < playerController.PriceCoelho || playerController.money < playerController.PriceCupido || playerController.money < playerController.PriceSaci)
        {
            return false;
        }
        return false;
    }


    public void OpenMenu()
    {
        menuGameover.SetActive(true);
        menuCardBoard.SetActive(false);
        menuInformation.SetActive(false);
        buttonStart.SetActive(false);
    }


    public bool PersonagemInScene()
    {
        for (int i = 0; i < charactersInScene.Count; i++)
        {
            if (charactersInScene[i] == null)
                return false;
            else if(charactersInScene[i] != null)
                return true;

        }
        return false;
    }

    public void DestroyCharacterScene()
    {
        for (int i = 0; i < charactersInScene.Count; i++)
        {
            if(charactersInScene[i] != null)
            {
                charactersInScene[i].GetComponent<Character>().TakeDamage(1000);
            }           
            
        }       
    }


    public void IncreaseSpawnMaxTowers()
    {
        if(iaController.SpawnMax < 30)
        {
            if(iaController.SpawnMax == iaController.iaInformation.spawnMax)
            {
                int maxNew = iaController.iaInformation.spawnMax + 2;
                iaController.SpawnMax = maxNew;
            }
            else
            {
                int maxNew = iaController.SpawnMax + 2;
                iaController.SpawnMax = maxNew;
            }

        }       
        
    }

    public void BuffLifeFabrica()
    {        
        if(fabricaData.Life == fabricaData.enemiesData.lifeMax)
        {
            float lifeNew = fabricaData.enemiesData.lifeMax * 2;
            fabricaData.Life = lifeNew;
            fabricaData.LifeMax = lifeNew;
        }
        else
        {
            float lifeNew = fabricaData.LifeMax * 2;
            fabricaData.Life = lifeNew;
            fabricaData.LifeMax = lifeNew;
        }

    }

   
   
}
