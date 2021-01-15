using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public List<GameObject> pathPointsPlayer = new List<GameObject>();



    [Header("Info")]
    public PlayerData playerInformation;
    public int money;
    int priceWaveMax;
    int qtdCriaturasMax;

    int priceCoelho, priceCupido, priceSaci;


    [Header("Cena")]
    private Queue<GameObject> waveCharacters = new Queue<GameObject>();
    public GameObject spawnPoint;
    public WaveController waveControl;
    public GameObject textupMoney;
    public GameObject priceWaveUP;
    
    [Header("Cartas")]
    public GameObject coelhoPrefab;
    public GameObject cupidoPrefab;
    public GameObject saciPrefab;

    [Header("Textos")]
    public TextMeshProUGUI txtMoney;
    public TextMeshProUGUI txtQuantidadeCriaturas;
    public TextMeshProUGUI txtPriceCoelho;
    public TextMeshProUGUI txtPriceCupido;
    public TextMeshProUGUI txtPriceSaci;
    public TextMeshProUGUI txtPriceWave;
    [Header("TextosCartas")]
    public TextMeshProUGUI txtDamageCoelho;
    public TextMeshProUGUI txtLifeCoelho;
    public TextMeshProUGUI txtDamageCupido;
    public TextMeshProUGUI txtLifeCupido;
    public TextMeshProUGUI txtDamageSaci;
    public TextMeshProUGUI txtLifeSaci;
    [Header("Datas")]
    public CharacterInfo coelhoData;
    public CharacterInfo cupidoData;
    public CharacterInfo saciData;

    public int PriceCoelho { get => priceCoelho; set => priceCoelho = value; }
    public int PriceCupido { get => priceCupido; set => priceCupido = value; }
    public int PriceSaci { get => priceSaci; set => priceSaci = value; }

    private void Start()
    {
        money = playerInformation.money;
        priceWaveUP.SetActive(false);
        qtdCriaturasMax = playerInformation.quantidadeCriaturasMax;
        PriceCoelho = coelhoData.price;
        PriceCupido = cupidoData.price;
        PriceSaci = saciData.price;
        priceWaveMax = 150;
    }


    private void Update()
    {
        if(money <= 0)
        {
            money = 0;
        }

        playerInformation.quantidadeCriaturas = waveCharacters.Count;
        txtQuantidadeCriaturas.text = playerInformation.quantidadeCriaturas.ToString() + "/" + qtdCriaturasMax.ToString();   
        txtMoney.text = money.ToString();
        txtPriceCoelho.text = PriceCoelho.ToString();
        txtPriceCupido.text = PriceCupido.ToString();
        txtPriceSaci.text = PriceSaci.ToString();
        txtPriceWave.text = priceWaveMax.ToString();
        txtDamageCoelho.text = coelhoData.damage.ToString();
        txtLifeCoelho.text = coelhoData.lifeMax.ToString();
        txtDamageCupido.text = cupidoData.damage.ToString();
        txtLifeCupido.text = cupidoData.lifeMax.ToString();
        txtDamageSaci.text = saciData.damage.ToString();
        txtLifeSaci.text = saciData.lifeMax.ToString();

    }


    public void BuyWaveMax()
    {
        if(qtdCriaturasMax < 25 && money >= priceWaveMax)
        {
            qtdCriaturasMax += 3;
            money -= priceWaveMax;
            priceWaveMax += 150;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuGame",LoadSceneMode.Single);
    }


    public void MouseOverButton()
    {
        priceWaveUP.SetActive(true);
    }
    public void MouseOverButtonOff()
    {
        priceWaveUP.SetActive(false);
    }


    public void PlayerStart()
    {       
        if(playerInformation.quantidadeCriaturas > 0)
        {
            InvokeRepeating("GerarCharacter", 1f, 2);
            waveControl.WaveStart();
          
        }
        else
        {
            Debug.Log("Sua Wave Esta Fazia");
        }
    }

    public void CardCoelho()
    {      

        if(money >= PriceCoelho && playerInformation.quantidadeCriaturas <= qtdCriaturasMax)
        {
            money -= PriceCoelho;
            waveCharacters.Enqueue(coelhoPrefab);
        }      

    }

    public void CardCupido()
    {
        if (money >= PriceCupido && playerInformation.quantidadeCriaturas <= qtdCriaturasMax)
        {
            money -= PriceCupido;
            waveCharacters.Enqueue(cupidoPrefab);
        }
    }

    public void CardSaci()
    {
        if (money >= PriceSaci && playerInformation.quantidadeCriaturas <= qtdCriaturasMax)
        {
            money -= PriceSaci;
            waveCharacters.Enqueue(saciPrefab);
        }
    }

    public void GerarCharacter()
    {
        if(waveCharacters.Count >= 1)
        {
            GameObject characterInstanciado =  Instantiate(waveCharacters.Peek(), spawnPoint.transform.position, Quaternion.identity);
            waveControl.charactersInScene.Add(characterInstanciado);
            if (characterInstanciado.GetComponent<Coelho>() != null)
            {
                characterInstanciado.GetComponent<Coelho>().randomTime = Random.Range(7, 20);               
            }
            waveCharacters.Dequeue();
        }
        else
        {
            CancelInvoke("GerarCharacter");            
        }
        
    }

    public void SpawnTextPopUP(int money,Transform transformPosition)
    {
        GameObject textPopup = Instantiate(textupMoney, transformPosition.position, Quaternion.identity);
        Vector2 dir = new Vector2(0, 1f);
        textPopup.GetComponent<Rigidbody2D>().velocity = dir * 2f;
        textPopup.GetComponent<TextMeshPro>().text = money.ToString();
        Destroy(textPopup, 0.9f);
    }


}
