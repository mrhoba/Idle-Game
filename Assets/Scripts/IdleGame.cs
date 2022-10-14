using System;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

[Serializable]
public class PlayerData
{
    public BigDouble coinsClickValue;
    public BigDouble coins;
    public BigDouble coinsPerSecond;

    public BigDouble clickUpgrade1Level;

    public BigDouble clickUpgrade2Level;

    public BigDouble productionUpgrade1Level;

    public BigDouble productionUpgrade2Power;
    public BigDouble productionUpgrade2Level;

    public BigDouble gems;
    public BigDouble gemBoost;
    public BigDouble gemsToGet;

    public PlayerData()
    {
        FullReset();
    }

    public void FullReset()
    {
        coinsClickValue = 1;
        coins = 0;
        coinsPerSecond = 0;

        clickUpgrade1Level = 0;

        clickUpgrade2Level = 0;

        productionUpgrade1Level = 0;

        productionUpgrade2Power = 5;
        productionUpgrade2Level = 0;

        gems = 0;
        gemBoost = 0;
        gemsToGet = 0;
    }
}
public class IdleGame : MonoBehaviour
{
    public PlayerData data;
    #region Text Objects
    public Text coinsText;
    public Text clickValueText;
    public Text coinsPerSecText;

    public Text clickUpgrade1Text;
    public Text clickUpgrade1MaxText;
    public Text clickUpgrade2Text;

    public Text productionUpgrade1Text;
    public Text productionUpgrade2Text;

    public Text gemsText;
    public Text gemBoostText;
    public Text gemsToGetText;
    #endregion

    public Image clickUpgrade1Bar;

    #region Variables
    

    #endregion

    public CanvasGroup mainMenuGroup;
    public CanvasGroup upgradesGroup;
    public CanvasGroup settingsGroup;

    //public GameObject settings;


    public void Start()
    {
        Application.targetFrameRate = 60;
        CanvasGroupChanger(true, mainMenuGroup);
        CanvasGroupChanger(false, upgradesGroup);
        CanvasGroupChanger(false, settingsGroup);
        //Load();
        SaveSystem.LoadPlayer(ref data);
    }

    public void CanvasGroupChanger(bool x, CanvasGroup y)
    {
        if(x)
        {
            y.alpha = 1;
            y.interactable = true;
            y.blocksRaycasts = true;
            return;
        }
        y.alpha = 0;
        y.interactable = false;
        y.blocksRaycasts = false;
    }

    #region Save & Load
    //public void Load()
    //{
    //    coins = Parse(PlayerPrefs.GetString("coins", "0"));
    //    coinsClickValue = Parse(PlayerPrefs.GetString("coinsClickValue", "1"));
    //    clickUpgrade2Cost = Parse(PlayerPrefs.GetString("clickUpgrade2Cost", "100"));
    //    productionUpgrade1Cost = Parse(PlayerPrefs.GetString("productionUpgrade1Cost", "25"));
    //    productionUpgrade2Cost = Parse(PlayerPrefs.GetString("productionUpgrade2Cost", "250"));
    //    productionUpgrade2Power = Parse(PlayerPrefs.GetString("productionUpgrade2Power", "5"));

    //    gems = Parse(PlayerPrefs.GetString("gems", "0"));

    //    clickUpgrade1Level = Parse(PlayerPrefs.GetString("clickUpgrade1Level", "0"));
    //    clickUpgrade2Level = Parse(PlayerPrefs.GetString("clickUpgrade2Level", "0"));
    //    productionUpgrade1Level = Parse(PlayerPrefs.GetString("productionUpgrade1Level", "0"));
    //    productionUpgrade2Level = Parse(PlayerPrefs.GetString("productionUpgrade2Level", "0"));
    //}

    //public void Save()
    //{
    //    PlayerPrefs.SetString("coins", coins.ToString());
    //    PlayerPrefs.SetString("coinsClickValue", coinsClickValue.ToString());
    //    PlayerPrefs.SetString("clickUpgrade2Cost", clickUpgrade2Cost.ToString());
    //    PlayerPrefs.SetString("productionUpgrade1Cost", productionUpgrade1Cost.ToString());
    //    PlayerPrefs.SetString("productionUpgrade2Cost", productionUpgrade2Cost.ToString());
    //    PlayerPrefs.SetString("productionUpgrade2Power", productionUpgrade2Power.ToString());

    //    PlayerPrefs.SetString("gems", gems.ToString());

    //    PlayerPrefs.SetString("clickUpgrade1Level", clickUpgrade1Level.ToString());
    //    PlayerPrefs.SetString("clickUpgrade2Level", clickUpgrade2Level.ToString());
    //    PlayerPrefs.SetString("productionUpgrade1Level", productionUpgrade1Level.ToString());
    //    PlayerPrefs.SetString("productionUpgrade2Level", productionUpgrade2Level.ToString());
    //}
    #endregion

    public void Update()
    {
        data.gemsToGet = 150 * Sqrt(data.coins / 1e7);
        data.gemBoost = data.gems * 0.01 + 1;

        gemsToGetText.text = "prestige: \n+" + Floor(data.gemsToGet).ToString("F0") + " Gems";
        gemsText.text = "Gems: " + Floor(data.gems).ToString("F0");
        gemBoostText.text = data.gemBoost.ToString("F2") + "x boost";

        data.coinsPerSecond = (data.productionUpgrade1Level + (data.productionUpgrade2Power * data.productionUpgrade2Level)) * data.gemBoost;

        #region Exponent System
        clickValueText.text = "Click\n+" + ExponentMethod(data.coinsClickValue, "F0") + " Coins";
        coinsText.text = "Coins: " + ExponentMethod(data.coins, "F0");

        coinsPerSecText.text = data.coinsPerSecond.ToString("F2") + " coins/s";
        
        var clickUpgrade1Cost = 10 * Pow(1.07, data.clickUpgrade1Level);
        string clickUpgrade1CostString = ExponentMethod(clickUpgrade1Cost, "F0");
        string clickUpgrade1LevelString = ExponentMethod(data.clickUpgrade1Level, "F0");

        var clickUpgrade2Cost = 100 * Pow(1.07, data.clickUpgrade2Level);
        string clickUpgrade2CostString = ExponentMethod(clickUpgrade2Cost, "F0");
        string clickUpgrade2LevelString = ExponentMethod(data.clickUpgrade2Level, "F0");

        var productionUpgrade1Cost = 25 * Pow(1.07, data.productionUpgrade1Level);
        string productionUpgrade1CostString = ExponentMethod(productionUpgrade1Cost, "F0");
        string productionUpgrade1LevelString = ExponentMethod(data.productionUpgrade1Level, "F0");

        var productionUpgrade2Cost = 250 * Pow(1.07, data.productionUpgrade2Level);
        string productionUpgrade2CostString = ExponentMethod(productionUpgrade2Cost, "F0");
        string productionUpgrade2LevelString = ExponentMethod(data.productionUpgrade2Level, "F0");

        #endregion

        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1CostString + "coins\nPower: +1 Click\nLevel: " + clickUpgrade1LevelString;
        clickUpgrade2Text.text = "Click Upgrade 2\nCost: " + clickUpgrade2CostString + "coins\nPower: +5 Click\nLevel: " + clickUpgrade2LevelString;

        productionUpgrade1Text.text = "Production Upgrade 1\nCost: " + productionUpgrade1CostString + "coins\nPower: +" + data.gemBoost.ToString("F2") + "coins/s\nLevel: " + productionUpgrade1LevelString;
        productionUpgrade2Text.text = "Production Upgrade 2\nCost: " + productionUpgrade2CostString + "coins\nPower: +" + (data.productionUpgrade2Power * data.gemBoost).ToString("F2") + "coins/s\nLevel: " + productionUpgrade2LevelString;

        data.coins += data.coinsPerSecond * Time.deltaTime;
        #region ProgressBar Filling
        if (data.coins / clickUpgrade1Cost < 0.01)
        {
            clickUpgrade1Bar.fillAmount = 0;
        }
        else if (data.coins / clickUpgrade1Cost > 10)
        {
            clickUpgrade1Bar.fillAmount = 1;
        }
        else clickUpgrade1Bar.fillAmount = (float)(data.coins / clickUpgrade1Cost).ToDouble();
        #endregion

        clickUpgrade1MaxText.text = "Buy Max (" + BuyClickUpgrade1MaxCount() + ")";
        SaveSystem.SavePlayer(data);
        //Save();
    }

    public string ExponentMethod(BigDouble x, string y)
    {
        if (x > 1000)
        {
            var exponent = Floor(Log10(Abs(x)));
            var mantissa = x / Pow(10, exponent);
            return mantissa.ToString("F2") + "e" + exponent;
        }
        return x.ToString(y);
    }

    #region Prestige
    public void Prestige()
    {
        if(data.coins > 1000)
        {
            data.coins = 0;
            data.coinsClickValue = 1;
            data.productionUpgrade2Power = 5;

            data.clickUpgrade1Level = 0;
            data.clickUpgrade2Level = 0;
            data.productionUpgrade1Level = 0;
            data.productionUpgrade2Level = 0;

            data.gems += data.gemsToGet;

        }
    }
    #endregion

    //Click Button
    public void Click()
    {
        data.coins += data.coinsClickValue;
    }
    #region Upgrade Buttons

    public BigDouble BuyClickUpgrade1MaxCount()
    {
        var b = 10;
        var c = data.coins;
        var r = 1.07;
        var k = data.clickUpgrade1Level;
        var n = Floor(Log(c * (r - 1) / (b * Pow(r, k)) + 1, r));
        return n;
    }

    public void BuyUpgrade(string upgradeID)
    {
        switch(upgradeID)
        {
            case "C1":
                var cost1 = 10 * Pow(1.07, data.clickUpgrade1Level);
                if (data.coins >= cost1)
                {
                    data.clickUpgrade1Level++;
                    data.coins -= cost1;
                    cost1 *= 1.07;
                    data.coinsClickValue++;
                }
                break;
            case "C1MAX":
                var b = 10;
                var c = data.coins;
                var r = 1.07;
                var k = data.clickUpgrade1Level;
                var n = Floor(Log(c * (r - 1) / (b * Pow(r, k)) + 1, r));

                var cost2 = b * (Pow(r, k) * (Pow(r, n) - 1) / (r - 1));

                if (data.coins >= cost2)
                {
                    data.clickUpgrade1Level += n;
                    data.coins -= cost2;
                    data.coinsClickValue += n;
                }
                break;
            case "C2":
                var cost3 = 100 * Pow(1.07, data.clickUpgrade2Level);
                if (data.coins >= cost3)
                {
                    data.clickUpgrade2Level++;
                    data.coins -= cost3;
                    cost3 *= 1.09;
                    data.coinsClickValue += 5;
                }
                break;
            case "P1":
                var cost4 = 25 * Pow(1.07, data.productionUpgrade1Level);
                if (data.coins >= cost4)
                {
                    data.productionUpgrade1Level++;
                    data.coins -= cost4;
                    cost4 *= 1.07;
                }
                break;
            case "P2":
                var cost5 = 250 * Pow(1.07, data.productionUpgrade2Level);
                if (data.coins >= cost5)
                {
                    data.productionUpgrade2Level++;
                    data.coins -= cost5;
                    cost5 *= 1.07;
                }
                break;
            default:
                Debug.Log("I'm not assigned to a proper upgrade!");
                break;
        }
    }
    #endregion

    public void ChangeTabs(string id)
    {
        switch(id)
        {
            case "upgrades":
                CanvasGroupChanger(false, mainMenuGroup);
                CanvasGroupChanger(true, upgradesGroup);
                break;
            case "main":
                CanvasGroupChanger(true, mainMenuGroup);
                CanvasGroupChanger(false, upgradesGroup);
                CanvasGroupChanger(false, settingsGroup);
                break;
            case "settings":
                CanvasGroupChanger(false, mainMenuGroup);
                CanvasGroupChanger(false, upgradesGroup);
                CanvasGroupChanger(true, settingsGroup);
                break;
        }
    }

    //public void GoToSettings()
    //{
    //    settings.gameObject.SetActive(true);
    //}

    //public void GoBackFromSettings()
    //{
    //    settings.gameObject.SetActive(false);
    //}

    public void FullReset()
    {
        data.FullReset();
    }
}
