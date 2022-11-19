using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncrementalManager : MonoBehaviour
{
    #region Singleton
    public static IncrementalManager Instance;
    #endregion

    #region Variables
    [Header("Incremental")]
    public int income;
    public int incomeLevel = 1;
    public int neededIncome;
    public int incomeLinear = 10;
    public int fireRate;
    public int fireRateLevel = 1;
    public int neededFireRate;
    public int fireRateLinear = 10;
    public int coin;
    public float fireTimer;
    #endregion

    #region UI
    [Header("UI")]
    [SerializeField] TextMeshProUGUI incomeText;
    [SerializeField] TextMeshProUGUI incomeLevelTextUI;

    [SerializeField] TextMeshProUGUI fireRateText;
    [SerializeField] TextMeshProUGUI fireRateLevelTextUI;

    public TextMeshProUGUI coinText;

    [SerializeField] GameObject incomeButton;
    [SerializeField] GameObject fireRateButton;

    [SerializeField] GameObject incomePassiveButton;
    [SerializeField] GameObject fireRatePassiveButton;

    [SerializeField] GameObject incomeLevelText;
    [SerializeField] GameObject fireRateLevelText;

    [SerializeField] GameObject incomeLevelNumber;
    [SerializeField] GameObject fireRateLevelNumber;

    [SerializeField] GameObject incomeUIText;
    [SerializeField] GameObject fireRateUIText;

    #endregion

    private void Awake()
    {
        Instance = this;

        //fireTimer = ShootingManager.Instance.minTime;

        #region Skill Prefs
        incomeLevelTextUI.text = PlayerPrefs.GetInt("IncomeLevel").ToString();
        incomeLevel = PlayerPrefs.GetInt("IncomeLevel", 1);
        incomeLinear = PlayerPrefs.GetInt("IncomeLinear", 100);

        fireRateLevelTextUI.text = PlayerPrefs.GetInt("FireRateLevel").ToString();
        fireRateLevel = PlayerPrefs.GetInt("FireRateLevel", 1);
        fireRateLinear = PlayerPrefs.GetInt("FireRateLinear", 100);

        income = PlayerPrefs.GetInt("Income", 1);

        incomeText.text = PlayerPrefs.GetInt("Income").ToString();
        neededIncome = PlayerPrefs.GetInt("NeededIncome", 100);

        fireRate = PlayerPrefs.GetInt("FireRate", 10);

        fireRateText.text = PlayerPrefs.GetInt("FireRate").ToString();
        neededFireRate = PlayerPrefs.GetInt("NeededFireRate", 100);

        incomeText.text = neededIncome.ToString();
        fireRateText.text = neededFireRate.ToString();

        incomeLevelTextUI.text = incomeLevel.ToString();
        fireRateLevelTextUI.text = fireRateLevel.ToString();

        coin = PlayerPrefs.GetInt("Coin", 10);
        coinText.text = coin.ToString();

        fireTimer = PlayerPrefs.GetFloat("FireTimer", .5f);

        #endregion
    }

    private void Start()
    {
        IncomeThousandSystem();
        FireRateThousandSystem();
    }
    void Update()
    {
        IncomeButtonOnOff();
        FireRateButtonOnOff();
        ThousandSystem();
    }

    public void IncomeUpgrade()
    {
        if (neededIncome <= coin)
        {
            coin -= neededIncome;
            coinText.text = coin.ToString();
            PlayerPrefs.SetInt("Coin", coin);

            income += 1;
            PlayerPrefs.SetInt("Income", income);

            incomeText.text = PlayerPrefs.GetInt("NeededIncome").ToString();
            neededIncome += incomeLinear;
            IncomeThousandSystem();
            //incomeText.text = neededIncome.ToString();
            PlayerPrefs.SetInt("NeededIncome", neededIncome);


            incomeLevelTextUI.text = PlayerPrefs.GetInt("IncomeLevel").ToString();
            incomeLevel++;
            incomeLevelTextUI.text = incomeLevel.ToString();
            PlayerPrefs.SetInt("IncomeLevel", incomeLevel);
            incomeLinear += 200;
            PlayerPrefs.SetInt("IncomeLinear", incomeLinear);
        }
        else
        {
            Debug.Log("Nope");
        }
    }

    public void FireRateUpgrade()
    {
        if (neededFireRate <= coin)
        {
            coin -= neededFireRate;
            coinText.text = coin.ToString();
            PlayerPrefs.SetInt("Coin", coin);

            fireRate += fireRateLinear / 5;
            PlayerPrefs.SetInt("FireRate", fireRate);

            fireRateText.text = PlayerPrefs.GetInt("NeededFireRate").ToString();
            neededFireRate += fireRateLinear;
            //fireRateText.text = neededFireRate.ToString();
            FireRateThousandSystem();
            PlayerPrefs.SetInt("NeededFireRate", neededFireRate);


            fireRateLevelTextUI.text = PlayerPrefs.GetInt("FireRateLevel").ToString();
            fireRateLevel++;
            fireRateLevelTextUI.text = fireRateLevel.ToString();
            fireRateLinear += 200;
            PlayerPrefs.SetInt("FireRateLevel", fireRateLevel);
            PlayerPrefs.SetInt("FireRateLinear", fireRateLinear);

            if (fireTimer <= .1f)
                return;
            fireTimer -= .01f;
            PlayerPrefs.SetFloat("FireTimer", fireTimer);
        }
        else
        {
            Debug.Log("Nope");
        }
    }

    void IncomeButtonOnOff()
    {
        if (neededIncome <= coin)
        {
            incomeButton.SetActive(true);
            incomePassiveButton.SetActive(false);
        }
        else
        {
            incomeButton.SetActive(false);
            incomePassiveButton.SetActive(true);
        }
    }

    void FireRateButtonOnOff()
    {
        if (neededFireRate <= coin)
        {
            fireRateButton.SetActive(true);
            fireRatePassiveButton.SetActive(false);
        }
        else
        {
            fireRateButton.SetActive(false);
            fireRatePassiveButton.SetActive(true);
        }
    }

    public void ThousandSystem()
    {
        if (coin < 1000)
        {
            coinText.text = coin.ToString();
        }
        else if(coin >= 1000)
        {
            int k = coin / 1000;
            float y = (coin - (k * 1000)) / 100;
            coinText.text = k.ToString()  + "." + y.ToString() + "K";
        }
    }

    void IncomeThousandSystem()
    {
        if (neededIncome < 1000)
        {
            incomeText.text = neededIncome.ToString();
        }
        else if (neededIncome >= 1000)
        {
            int k = neededIncome / 1000;
            float y = (neededIncome - (k * 1000)) / 100;
            incomeText.text = k.ToString() + "." + y.ToString() + "K";
        }
    }
    void FireRateThousandSystem()
    {
        if (neededFireRate < 1000)
        {
            fireRateText.text = neededFireRate.ToString();
        }
        else if (neededFireRate >= 1000)
        {
            int k = neededFireRate / 1000;
            float y = (neededFireRate - (k * 1000)) / 100;
            fireRateText.text = k.ToString() + "." + y.ToString() + "K";
        }
    }
}
