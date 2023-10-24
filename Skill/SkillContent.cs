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

    private int skill1Value = 1;
    private int skill2Value = -1;
    private int skill3Value = +1;

    private int price = 500000;


    Sprite[] skillArray;

    ImageDataBase imageDataBase;
    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        skillArray = imageDataBase.GetSkillArray();
    }

    public void Initialize()
    {
        icon.sprite = skillArray[(int)skillType];

        titleText.text = LocalizationManager.instance.GetString(skillType.ToString());

        switch (skillType)
        {
            case SkillType.Skill1:
                levelText.text = "Lv. " + (playerDataBase.Skill1 + 1) + " / 20";

                nowValueText.text = (skill1Value * (playerDataBase.Skill1 + 1)).ToString() + "%";

                if(playerDataBase.Skill1 < 19)
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
                levelText.text = "Lv. " + (playerDataBase.Skill2 + 1) + " / 20";

                nowValueText.text = (skill2Value * (playerDataBase.Skill2 + 1)).ToString() + "%";

                if (playerDataBase.Skill2 < 19)
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
                levelText.text = "Lv. " + (playerDataBase.Skill3 + 1) + " / 20";

                nowValueText.text = (skill3Value * (playerDataBase.Skill3 + 1)).ToString() + "%";

                if (playerDataBase.Skill3 < 19)
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

        switch (skillType)
        {
            case SkillType.Skill1:

                if(playerDataBase.Skill1 < 19)
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

                if (playerDataBase.Skill2 < 19)
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

                if (playerDataBase.Skill3 < 19)
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
        }

        Initialize();

    }
}