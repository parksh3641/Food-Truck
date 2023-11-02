using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillContent : MonoBehaviour
{
    public SkillType skillType = SkillType.Skill1;

    public Image icon;

    public Text titleText;

    public Text nowValueText;
    public Text nextValueText;

    public Text levelText;

    public Text goldText;

    private int maxLevel = 200;

    private float skill1Value = 0.1f;
    private float skill2Value = 0.1f;
    private float skill3Value = 0.1f;
    private float skill4Value = 0.2f;
    private float skill5Value = 0.2f;
    private float skill6Value = 0.2f;

    private int price = 5000;

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
                levelText.text = "Lv. " + (playerDataBase.Skill1 + 1) + " / " + maxLevel.ToString();

                nowValueText.text = (skill1Value * (playerDataBase.Skill1 + 1)).ToString() + "%";

                if (playerDataBase.Skill1 < maxLevel - 1)
                {
                    nextValueText.text = (skill1Value * (playerDataBase.Skill1 + 2)).ToString() + "%";
                    goldText.text = MoneyUnitString.ToCurrencyString((playerDataBase.Skill1 + 1) * price);
                }
                else
                {
                    nextValueText.text = "-";
                    goldText.text = "-";
                }

                break;
            case SkillType.Skill2:
                levelText.text = "Lv. " + (playerDataBase.Skill2 + 1) + " / " + maxLevel.ToString();

                nowValueText.text = (skill2Value * (playerDataBase.Skill2 + 1)).ToString() + "%";

                if (playerDataBase.Skill2 < maxLevel - 1)
                {
                    nextValueText.text = (skill2Value * (playerDataBase.Skill2 + 2)).ToString() + "%";
                    goldText.text = MoneyUnitString.ToCurrencyString((playerDataBase.Skill2 + 1) * price);
                }
                else
                {
                    nextValueText.text = "-";
                    goldText.text = "-";
                }
                break;
            case SkillType.Skill3:
                levelText.text = "Lv. " + (playerDataBase.Skill3 + 1) + " / " + maxLevel.ToString();

                nowValueText.text = (skill3Value * (playerDataBase.Skill3 + 1)).ToString() + "%";

                if (playerDataBase.Skill3 < maxLevel - 1)
                {
                    nextValueText.text = (skill3Value * (playerDataBase.Skill3 + 2)).ToString() + "%";
                    goldText.text = MoneyUnitString.ToCurrencyString((playerDataBase.Skill3 + 1) * price);
                }
                else
                {
                    nextValueText.text = "-";
                    goldText.text = "-";
                }
                break;
            case SkillType.Skill4:
                levelText.text = "Lv. " + (playerDataBase.Skill4 + 1) + " / " + maxLevel.ToString();

                nowValueText.text = (skill4Value * (playerDataBase.Skill4 + 1)).ToString() + "%";

                if (playerDataBase.Skill4 < maxLevel - 1)
                {
                    nextValueText.text = (skill4Value * (playerDataBase.Skill4 + 2)).ToString() + "%";
                    goldText.text = MoneyUnitString.ToCurrencyString((playerDataBase.Skill4 + 1) * price);
                }
                else
                {
                    nextValueText.text = "-";
                    goldText.text = "-";
                }
                break;
            case SkillType.Skill5:
                levelText.text = "Lv. " + (playerDataBase.Skill5 + 1) + " / " + maxLevel.ToString();

                nowValueText.text = (skill5Value * (playerDataBase.Skill5 + 1)).ToString() + "%";

                if (playerDataBase.Skill5 < maxLevel - 1)
                {
                    nextValueText.text = (skill5Value * (playerDataBase.Skill5 + 2)).ToString() + "%";
                    goldText.text = MoneyUnitString.ToCurrencyString((playerDataBase.Skill5 + 1) * price);
                }
                else
                {
                    nextValueText.text = "-";
                    goldText.text = "-";
                }
                break;
            case SkillType.Skill6:
                levelText.text = "Lv. " + (playerDataBase.Skill6 + 1) + " / " + maxLevel.ToString();

                nowValueText.text = (skill6Value * (playerDataBase.Skill6 + 1)).ToString() + "%";

                if (playerDataBase.Skill6 < maxLevel - 1)
                {
                    nextValueText.text = (skill6Value * (playerDataBase.Skill6 + 2)).ToString() + "%";
                    goldText.text = MoneyUnitString.ToCurrencyString((playerDataBase.Skill6 + 1) * price);
                }
                else
                {
                    nextValueText.text = "-";
                    goldText.text = "-";
                }
                break;
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

                if (playerDataBase.Skill1 < maxLevel - 1)
                {
                    if (playerDataBase.Coin >= ((playerDataBase.Skill1 + 1) * price))
                    {
                        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, ((playerDataBase.Skill1 + 1) * price));

                        playerDataBase.Skill1 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill1", playerDataBase.Skill1);

                        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

                    }
                    else
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LowCoin);
                    }
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                }

                break;
            case SkillType.Skill2:

                if (playerDataBase.Skill2 < maxLevel - 1)
                {
                    if (playerDataBase.Coin >= ((playerDataBase.Skill2 + 1) * price))
                    {
                        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, ((playerDataBase.Skill2 + 1) * price));

                        playerDataBase.Skill2 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill2", playerDataBase.Skill2);

                        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

                    }
                    else
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LowCoin);
                    }
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                }

                break;
            case SkillType.Skill3:

                if (playerDataBase.Skill3 < maxLevel - 1)
                {
                    if (playerDataBase.Coin >= ((playerDataBase.Skill3 + 1) * price))
                    {
                        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, ((playerDataBase.Skill3 + 1) * price));

                        playerDataBase.Skill3 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill3", playerDataBase.Skill3);

                        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

                    }
                    else
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LowCoin);
                    }
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                }
                break;
            case SkillType.Skill4:
                if (playerDataBase.Skill4 < maxLevel - 1)
                {
                    if (playerDataBase.Coin >= ((playerDataBase.Skill4 + 1) * price))
                    {
                        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, ((playerDataBase.Skill4 + 1) * price));

                        playerDataBase.Skill4 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill4", playerDataBase.Skill4);

                        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

                    }
                    else
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LowCoin);
                    }
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                }
                break;
            case SkillType.Skill5:
                if (playerDataBase.Skill5 < maxLevel - 1)
                {
                    if (playerDataBase.Coin >= ((playerDataBase.Skill5 + 1) * price))
                    {
                        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, ((playerDataBase.Skill5 + 1) * price));

                        playerDataBase.Skill5 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill5", playerDataBase.Skill5);

                        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

                    }
                    else
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LowCoin);
                    }
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                }
                break;
            case SkillType.Skill6:
                if (playerDataBase.Skill6 < maxLevel - 1)
                {
                    if (playerDataBase.Coin >= ((playerDataBase.Skill6 + 1) * price))
                    {
                        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, ((playerDataBase.Skill6 + 1) * price));

                        playerDataBase.Skill6 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Skill6", playerDataBase.Skill6);

                        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

                    }
                    else
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LowCoin);
                    }
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                }
                break;
        }

        Initialize();

        isDelay = true;
        Invoke("Delay", 0.2f);

    }

    void Delay()
    {
        isDelay = false;
    }
}
