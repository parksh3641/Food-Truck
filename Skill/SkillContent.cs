using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillContent : MonoBehaviour
{
    public SkillType skillType = SkillType.Skill1;
    public MoneyType moneyType = MoneyType.Coin;

    public Image icon;

    public Text titleText;

    public Text nowValueText;
    public Text nextValueText;

    public Text levelText;

    public GameObject buttonGold;
    public Text goldText;

    public GameObject buttonCrystal;
    public Text crystalText;

    private float skill1Value = 0.1f;
    private float skill2Value = 0.1f;
    private float skill3Value = 0.1f;
    private float skill4Value = 0.2f;
    private float skill5Value = 0.2f;
    private float skill6Value = 0.2f;

    private float skill7Value = 0.1f;
    private float skill8Value = 0.2f;
    private float skill9Value = 0.2f;
    private float skill10Value = 0.3f;
    private float skill11Value = 1f;

    private int priceGold = 5000;
    private int priceCrystal = 5;

    private int maxLevelGold = 200;
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
                level = playerDataBase.Skill1 + 1;
                nowValue = skill1Value * (playerDataBase.Skill1 + 1);

                if (playerDataBase.Skill1 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill1Value * (playerDataBase.Skill1 + 2)).ToString() + "%";

                    if (moneyType == MoneyType.Coin)
                    {
                        value = (playerDataBase.Skill1 + 1) * priceGold;
                    }
                    else
                    {
                        value = (playerDataBase.Skill1 + 1) * priceCrystal;
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }

                break;
            case SkillType.Skill2:
                level = playerDataBase.Skill2 + 1;
                nowValue = skill2Value * (playerDataBase.Skill2 + 1);

                if (playerDataBase.Skill2 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill2Value * (playerDataBase.Skill2 + 2)).ToString() + "%";

                    if (moneyType == MoneyType.Coin)
                    {
                        value = (playerDataBase.Skill2 + 1) * priceGold;
                    }
                    else
                    {
                        value = (playerDataBase.Skill2 + 1) * priceCrystal;
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill3:
                level = playerDataBase.Skill3 + 1;
                nowValue = skill3Value * (playerDataBase.Skill3 + 1);

                if (playerDataBase.Skill3 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill3Value * (playerDataBase.Skill3 + 2)).ToString() + "%";

                    if (moneyType == MoneyType.Coin)
                    {
                        value = (playerDataBase.Skill3 + 1) * priceGold;
                    }
                    else
                    {
                        value = (playerDataBase.Skill3 + 1) * priceCrystal;
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill4:
                level = playerDataBase.Skill4 + 1;
                nowValue = skill4Value * (playerDataBase.Skill4 + 1);

                if (playerDataBase.Skill4 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill4Value * (playerDataBase.Skill4 + 2)).ToString() + "%";
                    if (moneyType == MoneyType.Coin)
                    {
                        value = (playerDataBase.Skill4 + 1) * priceGold;
                    }
                    else
                    {
                        value = (playerDataBase.Skill4 + 1) * priceCrystal;
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill5:
                level = playerDataBase.Skill5 + 1;
                nowValue = skill5Value * (playerDataBase.Skill5 + 1);

                if (playerDataBase.Skill5 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill5Value * (playerDataBase.Skill5 + 2)).ToString() + "%";
                    if (moneyType == MoneyType.Coin)
                    {
                        value = (playerDataBase.Skill5 + 1) * priceGold;
                    }
                    else
                    {
                        value = (playerDataBase.Skill5 + 1) * priceCrystal;
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill6:
                level = playerDataBase.Skill6 + 1;
                nowValue = skill6Value * (playerDataBase.Skill6 + 1);

                if (playerDataBase.Skill6 < maxLevelGold - 1)
                {
                    nextValueText.text = (skill6Value * (playerDataBase.Skill6 + 2)).ToString() + "%";
                    if (moneyType == MoneyType.Coin)
                    {
                        value = (playerDataBase.Skill6 + 1) * priceGold;
                    }
                    else
                    {
                        value = (playerDataBase.Skill6 + 1) * priceCrystal;
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill7:
                level = playerDataBase.Skill7 + 1;
                nowValue = skill7Value * (playerDataBase.Skill7 + 1);

                if (playerDataBase.Skill7 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill7Value * (playerDataBase.Skill7 + 2)).ToString() + "%";
                    if (moneyType == MoneyType.Coin)
                    {
                        value = (playerDataBase.Skill7 + 1) * priceGold;
                    }
                    else
                    {
                        value = (playerDataBase.Skill7 + 1) * priceCrystal;
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill8:
                level = playerDataBase.Skill8 + 1;
                nowValue = skill8Value * (playerDataBase.Skill8 + 1);

                if (playerDataBase.Skill8 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill8Value * (playerDataBase.Skill8 + 2)).ToString() + "%";
                    if (moneyType == MoneyType.Coin)
                    {
                        value = (playerDataBase.Skill8 + 1) * priceGold;
                    }
                    else
                    {
                        value = (playerDataBase.Skill8 + 1) * priceCrystal;
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill9:
                level = playerDataBase.Skill9 + 1;
                nowValue = skill9Value * (playerDataBase.Skill9 + 1);

                if (playerDataBase.Skill9 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill9Value * (playerDataBase.Skill9 + 2)).ToString() + "%";
                    if (moneyType == MoneyType.Coin)
                    {
                        value = (playerDataBase.Skill9 + 1) * priceGold;
                    }
                    else
                    {
                        value = (playerDataBase.Skill9 + 1) * priceCrystal;
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill10:
                level = playerDataBase.Skill10 + 1;
                nowValue = skill10Value * (playerDataBase.Skill10 + 1);

                if (playerDataBase.Skill10 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill10Value * (playerDataBase.Skill10 + 2)).ToString() + "%";
                    if (moneyType == MoneyType.Coin)
                    {
                        value = (playerDataBase.Skill10 + 1) * priceGold;
                    }
                    else
                    {
                        value = (playerDataBase.Skill10 + 1) * priceCrystal;
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill11:
                level = playerDataBase.Skill11 + 1;
                nowValue = skill11Value * (playerDataBase.Skill11 + 1);

                if (playerDataBase.Skill11 < maxLevelCrystal - 1)
                {
                    nextValueText.text = (skill11Value * (playerDataBase.Skill11 + 2)).ToString() + "%";
                    if (moneyType == MoneyType.Coin)
                    {
                        value = (playerDataBase.Skill11 + 1) * priceGold;
                    }
                    else
                    {
                        value = (playerDataBase.Skill11 + 1) * priceCrystal;
                    }
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
        }

        if(moneyType == MoneyType.Coin)
        {
            levelText.text = "Lv. " + level + " / " + maxLevelGold.ToString();
        }
        else
        {
            levelText.text = "Lv. " + level + " / " + maxLevelCrystal.ToString();
        }

        nowValueText.text = nowValue.ToString() + "%";

        if (moneyType == MoneyType.Coin)
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
        }

        if (moneyType == MoneyType.Coin)
        {
            if (playerDataBase.Coin >= value)
            {
                PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, value);
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
        }

        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

        Initialize();

        isDelay = true;
        Invoke("Delay", 0.2f);

    }

    void Delay()
    {
        isDelay = false;
    }
}
