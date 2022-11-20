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
    public int stickman;
    public int stickmanLevel = 1;
    public int neededStickman;
    public int stickmanLinear = 10;
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
    #endregion

    private void Awake()
    {
        Instance = this;

        #region Skill Prefs
        incomeLevelTextUI.text = PlayerPrefs.GetInt("IncomeLevel").ToString();
        incomeLevel = PlayerPrefs.GetInt("IncomeLevel", 1);
        incomeLinear = PlayerPrefs.GetInt("IncomeLinear", 10);

        fireRateLevelTextUI.text = PlayerPrefs.GetInt("FireRateLevel").ToString();
        stickmanLevel = PlayerPrefs.GetInt("FireRateLevel", 1);
        stickmanLinear = PlayerPrefs.GetInt("FireRateLinear", 10);

        income = PlayerPrefs.GetInt("Income", 1);

        incomeText.text = PlayerPrefs.GetInt("Income").ToString();
        neededIncome = PlayerPrefs.GetInt("NeededIncome", 10);

        stickman = PlayerPrefs.GetInt("FireRate", 10);

        fireRateText.text = PlayerPrefs.GetInt("FireRate").ToString();
        neededStickman = PlayerPrefs.GetInt("NeededFireRate", 10);

        incomeText.text = neededIncome.ToString();
        fireRateText.text = neededStickman.ToString();

        incomeLevelTextUI.text = incomeLevel.ToString();
        fireRateLevelTextUI.text = stickmanLevel.ToString();

        coin = PlayerPrefs.GetInt("Coin", 1);
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

    public void StickmanUpgrade()
    {
        if (neededStickman <= coin)
        {
            CheckPlayers.Instance.SetUpgradeStickman();

            coin -= neededStickman;
            coinText.text = coin.ToString();
            PlayerPrefs.SetInt("Coin", coin);

            stickman += stickmanLinear / 5;
            PlayerPrefs.SetInt("FireRate", stickman);

            fireRateText.text = PlayerPrefs.GetInt("NeededFireRate").ToString();
            neededStickman += stickmanLinear;
            //fireRateText.text = neededFireRate.ToString();
            FireRateThousandSystem();
            PlayerPrefs.SetInt("NeededFireRate", neededStickman);


            fireRateLevelTextUI.text = PlayerPrefs.GetInt("FireRateLevel").ToString();
            stickmanLevel++;
            fireRateLevelTextUI.text = stickmanLevel.ToString();
            stickmanLinear += 200;
            PlayerPrefs.SetInt("FireRateLevel", stickmanLevel);
            PlayerPrefs.SetInt("FireRateLinear", stickmanLinear);
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
        if (neededStickman <= coin)
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
        if (neededStickman < 1000)
        {
            fireRateText.text = neededStickman.ToString();
        }
        else if (neededStickman >= 1000)
        {
            int k = neededStickman / 1000;
            float y = (neededStickman - (k * 1000)) / 100;
            fireRateText.text = k.ToString() + "." + y.ToString() + "K";
        }
    }
}
