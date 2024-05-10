using Firebase.Analytics;
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

    public GameObject buttonCP;
    public Text cpText;

    public GameObject buttonTicket;

    public Image buttonGoldImg;
    public Image buttonCrystalImg;
    public Image buttonCPImg;
    public Image buttonTicketImg;

    public Sprite[] buttonImgArray;

    private float skill1Value = 0.2f;
    private float skill2Value = 0.2f;
    private float skill3Value = 0.2f;
    private float skill4Value = 0.2f;
    private float skill5Value = 0.2f;
    private float skill6Value = 0.2f;
    private float skill12Value = 0.2f;
    private float skill13Value = 0.2f;

    private float skill7Value = 0.5f;
    private float skill8Value = 1f;
    private float skill9Value = 0.25f;
    private float skill10Value = 0.3f;
    private float skill11Value = 0.5f;
    private float skill14Value = 0.3f;

    private float skill15Value = 0.5f;
    private float skill16Value = 0.5f;
    private float skill17Value = 0.5f;
    private float skill18Value = 1f;
    private float skill19Value = 0.25f;

    private int priceGold = 500000;

    private int priceCrystal = 30;
    private int maxCrystal = 30;

    private int priceCP = 10;
    private int addPriceCP = 10;
    private int maxCP = 500;

    private int maxLevelGold = 500;
    private int maxLevelCrystal = 500;
    private int maxLevelChallenge = 100;

    private int level = 0;
    private int plusLevel = 1;

    private float nowValue = 0;
    private long value = 0;

    private int index = 2;

    bool isDelay = false;


    Sprite[] skillArray;

    ImageDataBase imageDataBase;
    PlayerDataBase playerDataBase;
    SkillManager skillManager;


    private void Awake()
    {
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        skillArray = imageDataBase.GetSkillArray();

        isDelay = false;
    }

    public void Initialize(SkillManager manager)
    {
        skillManager = manager;

        icon.sprite = skillArray[(int)skillType];

        titleText.text = LocalizationManager.instance.GetString(skillType.ToString());

        buttonTicket.SetActive(true);

        switch (skillType)
        {
            case SkillType.Skill1:
                level = playerDataBase.Skill1;
                nowValue = skill1Value * playerDataBase.Skill1;

                if (playerDataBase.Skill1 < maxLevelGold)
                {
                    nextValueText.text = (skill1Value * (playerDataBase.Skill1 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill2 < maxLevelGold)
                {
                    nextValueText.text = (skill2Value * (playerDataBase.Skill2 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill3 < maxLevelGold)
                {
                    nextValueText.text = (skill3Value * (playerDataBase.Skill3 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill4 < maxLevelGold)
                {
                    nextValueText.text = (skill4Value * (playerDataBase.Skill4 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill5 < maxLevelGold)
                {
                    nextValueText.text = (skill5Value * (playerDataBase.Skill5 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill6 < maxLevelGold)
                {
                    nextValueText.text = (skill6Value * (playerDataBase.Skill6 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill7 < maxLevelCrystal)
                {
                    nextValueText.text = (skill7Value * (playerDataBase.Skill7 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill8 < maxLevelCrystal)
                {
                    nextValueText.text = (skill8Value * (playerDataBase.Skill8 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill9 < maxLevelCrystal)
                {
                    nextValueText.text = (skill9Value * (playerDataBase.Skill9 + 1)).ToString("N2") + "%";
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

                if (playerDataBase.Skill10 < 100)
                {
                    nextValueText.text = (skill10Value * (playerDataBase.Skill10 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill11 < maxLevelCrystal)
                {
                    nextValueText.text = (skill11Value * (playerDataBase.Skill11 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill12 < maxLevelGold)
                {
                    nextValueText.text = (skill12Value * (playerDataBase.Skill12 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill13 < maxLevelGold)
                {
                    nextValueText.text = (skill13Value * (playerDataBase.Skill13 + 1)).ToString("N1") + "%";
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

                if (playerDataBase.Skill14 < 100)
                {
                    nextValueText.text = (skill14Value * (playerDataBase.Skill14 + 1)).ToString("N1") + "%";
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill15:
                level = playerDataBase.Skill15;
                nowValue = skill15Value * playerDataBase.Skill15;

                if (playerDataBase.Skill15 < maxLevelChallenge)
                {
                    nextValueText.text = (skill15Value * (playerDataBase.Skill15 + 1)).ToString("N1") + "%";
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill16:
                level = playerDataBase.Skill16;
                nowValue = skill16Value * playerDataBase.Skill16;

                if (playerDataBase.Skill16 < maxLevelChallenge)
                {
                    nextValueText.text = (skill16Value * (playerDataBase.Skill16 + 1)).ToString("N1") + "%";
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill17:
                level = playerDataBase.Skill17;
                nowValue = skill17Value * playerDataBase.Skill17;

                if (playerDataBase.Skill17 < maxLevelChallenge)
                {
                    nextValueText.text = (skill17Value * (playerDataBase.Skill17 + 1)).ToString("N1") + "%";
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill18:
                level = playerDataBase.Skill18;
                nowValue = skill18Value * playerDataBase.Skill18;

                if (playerDataBase.Skill18 < maxLevelChallenge)
                {
                    nextValueText.text = (skill18Value * (playerDataBase.Skill18 + 1)).ToString("N1") + "%";
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
            case SkillType.Skill19:
                level = playerDataBase.Skill19;
                nowValue = skill19Value * playerDataBase.Skill19;

                if (playerDataBase.Skill19 < maxLevelChallenge)
                {
                    nextValueText.text = (skill19Value * (playerDataBase.Skill19 + 1)).ToString("N2") + "%";
                }
                else
                {
                    nextValueText.text = "-";
                    value = 0;
                }
                break;
        }

        switch (moneyType)
        {
            case MoneyType.CoinA:
                CheckPrice(index);

                levelText.text = "Lv. " + level + " / " + maxLevelGold.ToString();

                buttonGold.SetActive(true);
                buttonCrystal.SetActive(false);
                buttonCP.SetActive(false);
                break;
            case MoneyType.Crystal:
                value = priceCrystal * (level + 1);

                if (value >= maxCrystal)
                {
                    value = maxCrystal;
                }

                if(skillType == SkillType.Skill10 || skillType == SkillType.Skill14)
                {
                    levelText.text = "Lv. " + level + " / 100";
                }
                else
                {
                    levelText.text = "Lv. " + level + " / " + maxLevelCrystal.ToString();
                }

                buttonGold.SetActive(false);
                buttonCrystal.SetActive(true);
                buttonCP.SetActive(false);

                crystalText.text = MoneyUnitString.ToCurrencyString(value);
                break;
            case MoneyType.CoinB:
                break;
            case MoneyType.CP:
                value = priceCP + (addPriceCP * (level + 1));

                if (value >= maxCP)
                {
                    value = maxCP;
                }

                levelText.text = "Lv. " + level + " / " + maxLevelChallenge.ToString();

                buttonGold.SetActive(false);
                buttonCrystal.SetActive(false);
                buttonCP.SetActive(true);

                cpText.text = MoneyUnitString.ToCurrencyString(value);
                break;
        }


        if (skillType == SkillType.Skill9 || skillType == SkillType.Skill19)
        {
            nowValueText.text = nowValue.ToString("N2") + "%";
        }
        else
        {
            nowValueText.text = nowValue.ToString("N1") + "%";
        }

        switch (moneyType)
        {
            case MoneyType.CoinA:
                if (level > maxLevelGold - 1)
                {
                    buttonGold.SetActive(false);
                    buttonTicket.SetActive(false);
                    buttonCrystal.SetActive(false);
                    buttonCP.SetActive(false);
                }
                break;
            case MoneyType.Crystal:
                if(skillType == SkillType.Skill10 || skillType == SkillType.Skill14)
                {
                    if (level > 99)
                    {
                        buttonGold.SetActive(false);
                        buttonTicket.SetActive(false);
                        buttonCrystal.SetActive(false);
                        buttonCP.SetActive(false);
                    }
                }
                else
                {
                    if (level > maxLevelCrystal - 1)
                    {
                        buttonGold.SetActive(false);
                        buttonTicket.SetActive(false);
                        buttonCrystal.SetActive(false);
                        buttonCP.SetActive(false);
                    }
                }
                break;
            case MoneyType.CoinB:
                break;
            case MoneyType.CP:
                if (level > maxLevelChallenge - 1)
                {
                    buttonGold.SetActive(false);
                    buttonTicket.SetActive(false);
                    buttonCrystal.SetActive(false);
                    buttonCP.SetActive(false);
                }
                break;
        }


        Checking();
    }

    public void LevelUpButton(int number)
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
                if (playerDataBase.Skill10 + 1 > 100)
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
                if (playerDataBase.Skill12 + 1 > maxLevelGold)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill13:
                if (playerDataBase.Skill13 + 1 > maxLevelGold)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill14:
                if (playerDataBase.Skill14 + 1 > 100)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill15:
                if (playerDataBase.Skill15 + 1 > maxLevelChallenge)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill16:
                if (playerDataBase.Skill16 + 1 > maxLevelChallenge)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill17:
                if (playerDataBase.Skill17 + 1 > maxLevelChallenge)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill18:
                if (playerDataBase.Skill18 + 1 > maxLevelChallenge)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
            case SkillType.Skill19:
                if (playerDataBase.Skill19 + 1 > maxLevelChallenge)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                break;
        }

        if (number == 0)
        {
            switch (moneyType)
            {
                case MoneyType.CoinA:
                    GameManager.instance.RenewalVC();

                    if (playerDataBase.Coin >= value)
                    {
                        PlayfabManager.instance.UpdateSubtractGold(value);
                    }
                    else
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LowCoin);

                        ShopManager.instance.LowCoin(value);
                        return;
                    }
                    break;
                case MoneyType.Crystal:
                    if (playerDataBase.Crystal >= value)
                    {
                        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, (int)value);
                    }
                    else
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LowCrystal);
                        return;
                    }
                    break;
                case MoneyType.CoinB:
                    break;
                case MoneyType.CP:
                    if (playerDataBase.ChallengePoint >= value)
                    {
                        playerDataBase.ChallengePoint -= (int)value;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("ChallengePoint", playerDataBase.ChallengePoint);
                    }
                    else
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LowPoint);
                        return;
                    }
                    break;
            }
        }
        else
        {
            if(playerDataBase.SkillTicket > 0)
            {
                playerDataBase.SkillTicket -= 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("SkillTickets", playerDataBase.SkillTicket);
            }
            else
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                return;
            }
        }


        switch (skillType)
        {
            case SkillType.Skill1:
                playerDataBase.Skill1 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill1", playerDataBase.Skill1);

                break;
            case SkillType.Skill2:
                playerDataBase.Skill2 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill2", playerDataBase.Skill2);

                break;
            case SkillType.Skill3:
                playerDataBase.Skill3 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill3", playerDataBase.Skill3);

                break;
            case SkillType.Skill4:
                playerDataBase.Skill4 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill4", playerDataBase.Skill4);

                break;
            case SkillType.Skill5:
                playerDataBase.Skill5 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill5", playerDataBase.Skill5);

                break;
            case SkillType.Skill6:
                playerDataBase.Skill6 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill6", playerDataBase.Skill6);

                break;
            case SkillType.Skill7:
                playerDataBase.Skill7 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill7", playerDataBase.Skill7);

                break;
            case SkillType.Skill8:
                playerDataBase.Skill8 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill8", playerDataBase.Skill8);

                break;
            case SkillType.Skill9:
                playerDataBase.Skill9 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill9", playerDataBase.Skill9);

                break;
            case SkillType.Skill10:
                playerDataBase.Skill10 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill10", playerDataBase.Skill10);

                break;
            case SkillType.Skill11:
                playerDataBase.Skill11 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill11", playerDataBase.Skill11);

                break;
            case SkillType.Skill12:
                playerDataBase.Skill12 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill12", playerDataBase.Skill12);

                break;
            case SkillType.Skill13:
                playerDataBase.Skill13 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill13", playerDataBase.Skill13);

                break;
            case SkillType.Skill14:
                playerDataBase.Skill14 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill14", playerDataBase.Skill14);

                break;
            case SkillType.Skill15:
                playerDataBase.Skill15 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill15", playerDataBase.Skill15);
                break;
            case SkillType.Skill16:
                playerDataBase.Skill16 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill16", playerDataBase.Skill16);
                break;
            case SkillType.Skill17:
                playerDataBase.Skill17 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill17", playerDataBase.Skill17);
                break;
            case SkillType.Skill18:
                playerDataBase.Skill18 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill18", playerDataBase.Skill18);
                break;
            case SkillType.Skill19:
                playerDataBase.Skill19 += plusLevel;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill19", playerDataBase.Skill19);
                break;
        }

        FirebaseAnalytics.LogEvent("LevelUp_" + skillType.ToString());

        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

        Initialize(skillManager);

        skillManager.Initialize();

        isDelay = true;
        Invoke("Delay", 0.4f);

    }

    void Delay()
    {
        isDelay = false;
    }

    public void Checking()
    {
        buttonGoldImg.sprite = buttonImgArray[0];
        buttonCrystalImg.sprite = buttonImgArray[0];
        buttonCPImg.sprite = buttonImgArray[0];
        buttonTicketImg.sprite = buttonImgArray[0];

        switch (moneyType)
        {
            case MoneyType.CoinA:
                if (playerDataBase.Coin >= value)
                {
                    buttonGoldImg.sprite = buttonImgArray[1];
                }
                break;
            case MoneyType.Crystal:
                if (playerDataBase.Crystal >= value)
                {
                    buttonCrystalImg.sprite = buttonImgArray[2];
                }
                break;
            case MoneyType.CoinB:
                break;
            case MoneyType.CP:
                if (playerDataBase.ChallengePoint >= value)
                {
                    buttonCPImg.sprite = buttonImgArray[3];
                }
                break;
        }

        if (playerDataBase.SkillTicket > 0)
        {
            buttonTicketImg.sprite = buttonImgArray[4];
        }
    }

    public void CheckPrice(int number)
    {
        if(moneyType == MoneyType.CoinA)
        {
            index = number;

            switch (number)
            {
                case 0:
                    if (level + 50 > maxLevelGold - 1)
                    {
                        plusLevel = maxLevelGold - level;
                        value = CalculateTotalCost(level, plusLevel);
                    }
                    else
                    {
                        value = CalculateTotalCost(level, 50);
                        plusLevel = 50;
                    }
                    break;
                case 1:
                    if(level + 10 > maxLevelGold - 1)
                    {
                        plusLevel = maxLevelGold - level;
                        value = CalculateTotalCost(level, plusLevel);
                    }
                    else
                    {
                        value = CalculateTotalCost(level, 10);
                        plusLevel = 10;
                    }
                    break;
                case 2:
                    value = CalculateTotalCost(level, 1);
                    plusLevel = 1;
                    break;
            }

            value = (long)(value * (1.0f - (0.003f * playerDataBase.Treasure4)));
            goldText.text = MoneyUnitString.ToCurrencyString(value);

            Checking();
        }
    }

    public long CalculateTotalCost(int currentLevel, int levelsToIncrease)
    {
        long totalCost = 0;
        double additionalCost = priceGold;

        for (int i = 0; i < levelsToIncrease; i++)
        {
            totalCost += (long)additionalCost;
            totalCost += (long)(additionalCost * ((currentLevel + i) * 0.1f));
        }

        return totalCost;
    }
}
