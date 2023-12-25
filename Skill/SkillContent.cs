using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillContent : MonoBehaviour
{
    public SkillType skillType = SkillType.Skill1;
    public MoneyType moneyType = MoneyType.CoinA;

    public Image icon;

    public Text titleText;

    public Text nowValueText;
    public Text nextValueText;

    public Text levelText;

    public GameObject buttonGold;
    public Text goldText;

    public GameObject buttonCrystal;
    public Text crystalText;

    private float skill1Value = 0.2f;
    private float skill2Value = 0.3f;
    private float skill3Value = 1f;
    private float skill4Value = 0.2f;
    private float skill5Value = 0.2f;
    private float skill6Value = 0.2f;
    private float skill12Value = 0.2f;
    private float skill13Value = 0.2f;

    private float skill7Value = 0.1f;
    private float skill8Value = 0.2f;
    private float skill9Value = 0.05f;
    private float skill10Value = 0.3f;
    private float skill11Value = 0.5f;
    private float skill14Value = 0.3f;

    private int priceGold = 100000;
    private int pricePortion = 50000;
    private int priceCrystal = 1;
    private int maxCrystal = 20;

    private int maxLevelGold = 100;
    private int maxLevelCrystal = 100;

    private int level = 0;
    private float nowValue = 0;
    private int value = 0;

    bool isDelay = false;


    Sprite[] skillArray;

    ImageDataBase imageDataBase;
    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        skillArray = imageDataBase.GetSkillArray();

        isDelay = false;
    }

    public void Initialize()
    {
        icon.sprite = skillArray[(int)skillType];

        titleText.text = LocalizationManager.instance.GetString(skillType.ToString());

        switch (skillType)
        {
            case SkillType.Skill1:
                level = playerDataBase.Skill1;
                nowValue = skill1Value * playerDataBase.Skill1;

                if (playerDataBase.Skill1 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill1Value * (playerDataBase.Skill1 + 1)).ToString() + "%";

                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill1 + 1) * priceGold * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1);

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }

                break;
            case SkillType.Skill2:
                level = playerDataBase.Skill2;
                nowValue = skill2Value * playerDataBase.Skill2;

                if (playerDataBase.Skill2 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill2Value * (playerDataBase.Skill2 + 1)).ToString() + "%";

                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill2 + 1) * priceGold * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1);

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill3:
                level = playerDataBase.Skill3;
                nowValue = skill3Value * playerDataBase.Skill3;

                if (playerDataBase.Skill3 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill3Value * (playerDataBase.Skill3 + 1)).ToString() + "%";

                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill3 + 1) * priceGold * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1);

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill4:
                level = playerDataBase.Skill4;
                nowValue = skill4Value * playerDataBase.Skill4;

                if (playerDataBase.Skill4 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill4Value * (playerDataBase.Skill4 + 1)).ToString() + "%";
                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill4 + 1) * pricePortion * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1);

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill5:
                level = playerDataBase.Skill5;
                nowValue = skill5Value * playerDataBase.Skill5;

                if (playerDataBase.Skill5 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill5Value * (playerDataBase.Skill5 + 1)).ToString() + "%";
                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill5 + 1) * pricePortion * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1);

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill6:
                level = playerDataBase.Skill6;
                nowValue = skill6Value * playerDataBase.Skill6;

                if (playerDataBase.Skill6 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill6Value * (playerDataBase.Skill6 + 1)).ToString() + "%";
                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill6 + 1) * pricePortion * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1);

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill7:
                level = playerDataBase.Skill7;
                nowValue = skill7Value * playerDataBase.Skill7;

                if (playerDataBase.Skill7 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill7Value * (playerDataBase.Skill7 + 1)).ToString() + "%";
                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill7 + 1) * priceGold * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1) + 9;

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill8:
                level = playerDataBase.Skill8;
                nowValue = skill8Value * playerDataBase.Skill8;

                if (playerDataBase.Skill8 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill8Value * (playerDataBase.Skill8 + 1)).ToString() + "%";
                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill8 + 1) * priceGold * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1) + 9;

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill9:
                level = playerDataBase.Skill9;
                nowValue = skill9Value * playerDataBase.Skill9;

                if (playerDataBase.Skill9 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill9Value * (playerDataBase.Skill9 + 1)).ToString() + "%";
                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill9 + 1) * priceGold * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1) + 9;

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill10:
                level = playerDataBase.Skill10;
                nowValue = skill10Value * playerDataBase.Skill10;

                if (playerDataBase.Skill10 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill10Value * (playerDataBase.Skill10 + 1)).ToString() + "%";
                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill10 + 1) * priceGold * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1) + 9;

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill11:
                level = playerDataBase.Skill11;
                nowValue = skill11Value * playerDataBase.Skill11;

                if (playerDataBase.Skill11 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill11Value * (playerDataBase.Skill11 + 1)).ToString() + "%";
                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill11 + 1) * priceGold * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1) + 9;

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill12:
                level = playerDataBase.Skill12;
                nowValue = skill12Value * playerDataBase.Skill12;

                if (playerDataBase.Skill12 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill12Value * (playerDataBase.Skill12 + 1)).ToString() + "%";
                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill12 + 1) * pricePortion * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1);

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill13:
                level = playerDataBase.Skill13;
                nowValue = skill13Value * playerDataBase.Skill13;

                if (playerDataBase.Skill13 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill13Value * (playerDataBase.Skill13 + 1)).ToString() + "%";
                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill13 + 1) * pricePortion * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1);

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill14:
                level = playerDataBase.Skill14;
                nowValue = skill14Value * playerDataBase.Skill14;

                if (playerDataBase.Skill14 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill14Value * (playerDataBase.Skill14 + 1)).ToString() + "%";
                    if (moneyType == MoneyType.CoinA)
                    {
                        value = (int)((playerDataBase.Skill14 + 1) * priceGold * (1.0f - (0.003f * playerDataBase.Treasure4)));
                    }
                    else
                    {
                        value = priceCrystal * (level + 1) + 9;

                        if (value >= maxCrystal)
                        {
                            value = maxCrystal;
                        }
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
        }

        if (moneyType == MoneyType.CoinA)
        {
            levelText.text = "Lv. " + level + " / " + maxLevelGold.ToString();
        }
        else
        {
            levelText.text = "Lv. " + level + " / " + maxLevelCrystal.ToString();
        }

        nowValueText.text = nowValue.ToString() + "%";

        if (moneyType == MoneyType.CoinA)
        {
            buttonGold.SetActive(true);
            buttonCrystal.SetActive(false);

            goldText.text = MoneyUnitString.ToCurrencyString(value);
        }
        else
        {
            buttonGold.SetActive(false);
            buttonCrystal.SetActive(true);

            crystalText.text = MoneyUnitString.ToCurrencyString(value);
        }
    }

    public void LevelUpButton()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (isDelay) return;

        switch (skillType)
        {
            case SkillType.Skill1:
                if (playerDataBase.Skill1 + 1 > maxLevelGold)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill2:
                if (playerDataBase.Skill2 + 1 > maxLevelGold)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill3:
                if (playerDataBase.Skill3 + 1 > maxLevelGold)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill4:
                if (playerDataBase.Skill4 + 1 > maxLevelGold)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill5:
                if (playerDataBase.Skill5 + 1 > maxLevelGold)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill6:
                if (playerDataBase.Skill6 + 1 > maxLevelGold)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill7:
                if (playerDataBase.Skill7 + 1 > maxLevelCrystal)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill8:
                if (playerDataBase.Skill8 + 1 > maxLevelCrystal)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill9:
                if (playerDataBase.Skill9 + 1 > maxLevelCrystal)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill10:
                if (playerDataBase.Skill10 + 1 > maxLevelCrystal)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill11:
                if (playerDataBase.Skill11 + 1 > maxLevelCrystal)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill12:
                if (playerDataBase.Skill12 + 1 > maxLevelCrystal)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill13:
                if (playerDataBase.Skill13 + 1 > maxLevelCrystal)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill14:
                if (playerDataBase.Skill14 + 1 > maxLevelCrystal)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
        }

        if (moneyType == MoneyType.CoinA)
        {
            if (playerDataBase.Coin >= value)
            {
                PlayfabManager.instance.UpdateSubtractGold(value);
            }
            else
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.LowCoin);
                return;
            }
        }
        else
        {
            if (playerDataBase.Crystal >= value)
            {
                PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, value);
            }
            else
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.LowCrystal);
                return;
            }
        }


        switch (skillType)
        {
            case SkillType.Skill1:
                playerDataBase.Skill1 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill1", playerDataBase.Skill1);

                break;
            case SkillType.Skill2:
                playerDataBase.Skill2 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill2", playerDataBase.Skill2);

                break;
            case SkillType.Skill3:
                playerDataBase.Skill3 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill3", playerDataBase.Skill3);

                break;
            case SkillType.Skill4:
                playerDataBase.Skill4 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill4", playerDataBase.Skill4);

                break;
            case SkillType.Skill5:
                playerDataBase.Skill5 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill5", playerDataBase.Skill5);

                break;
            case SkillType.Skill6:
                playerDataBase.Skill6 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill6", playerDataBase.Skill6);

                break;
            case SkillType.Skill7:
                playerDataBase.Skill7 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill7", playerDataBase.Skill7);

                break;
            case SkillType.Skill8:
                playerDataBase.Skill8 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill8", playerDataBase.Skill8);

                break;
            case SkillType.Skill9:
                playerDataBase.Skill9 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill9", playerDataBase.Skill9);

                break;
            case SkillType.Skill10:
                playerDataBase.Skill10 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill10", playerDataBase.Skill10);

                break;
            case SkillType.Skill11:
                playerDataBase.Skill11 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill11", playerDataBase.Skill11);

                break;
            case SkillType.Skill12:
                playerDataBase.Skill12 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill12", playerDataBase.Skill12);

                break;
            case SkillType.Skill13:
                playerDataBase.Skill13 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill13", playerDataBase.Skill13);

                break;
            case SkillType.Skill14:
                playerDataBase.Skill14 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill14", playerDataBase.Skill14);

                break;
        }

        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

        Initialize();

        isDelay = true;
        Invoke("Delay", 0.4f);

    }

    void Delay()
    {
        isDelay = false;
    }
}
