using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideMissionManager : MonoBehaviour
{
    public GameObject guideMissonView;

    public GameObject clearObj;

    public LocalizationContent titleText;
    public Text rewardText;

    private int now = 0;
    private int need = 0;

    private bool clear = false;
    private bool firstReset = false;

    private int reward = 20;

    PlayerDataBase playerDataBase;
    GuiedDataBase guiedDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (guiedDataBase == null) guiedDataBase = Resources.Load("GuiedDataBase") as GuiedDataBase;

        guideMissonView.SetActive(false);
        clearObj.SetActive(false);

        rewardText.text = reward.ToString();
    }

    public void Initialize()
    {
        if(playerDataBase.GuideIndex > 21)
        {
            return;
        }

        guideMissonView.SetActive(true);

        titleText.GetText().color = Color.white;
        titleText.localizationName = "GuiedMisson_" + (playerDataBase.GuideIndex + 1);

        switch(playerDataBase.GuideIndex)
        {
            case 0:
                now = playerDataBase.HamburgerMaxValue;
                need = 1;
                break;
            case 1:
                now = playerDataBase.SandwichMaxValue;
                need = 1;
                break;
            case 2:
                if(!firstReset)
                {
                    firstReset = true;
                    GameStateManager.instance.GetSellGold = 0;
                }

                now = GameStateManager.instance.GetSellGold;
                need = 500000;
                break;
            case 3:
                now = playerDataBase.Character2;
                need = 1;
                break;
            case 4:
                now = playerDataBase.YummyTimeCount;
                need = 1;
                break;
            case 5:
                now = playerDataBase.GetRecipeUpgradeCount();
                need = 5;
                break;
            case 6:
                now = playerDataBase.BuffCount;
                need = 1;
                break;
            case 7:
                now = playerDataBase.QuestCount;
                need = 1;
                break;
            case 8:
                now = playerDataBase.SnackLabMaxValue;
                need = 2;
                break;
            case 9:
                now = playerDataBase.DrinkMaxValue;
                need = 2;
                break;
            case 10:
                if (!firstReset)
                {
                    firstReset = true;
                    GameStateManager.instance.GetSellGold = 0;
                }

                now = GameStateManager.instance.GetSellGold;
                need = 1000000;
                break;
            case 11:
                now = playerDataBase.Character3;
                need = 1;
                break;
            case 12:
                now = playerDataBase.OpenChestBox;
                need = 2;
                break;
            case 13:
                now = playerDataBase.Character4;
                need = 1;
                break;
            case 14:
                now = playerDataBase.PizzaMaxValue;
                need = 2;
                break;
            case 15:
                now = playerDataBase.DonutMaxValue;
                need = 1;
                break;
            case 16:
                if (!firstReset)
                {
                    firstReset = true;
                    GameStateManager.instance.GetSellGold = 0;
                }

                now = GameStateManager.instance.GetSellGold;
                need = 5000000;
                break;
            case 17:
                now = playerDataBase.GeckoAnimal;
                need = 1;
                break;
            case 18:
                now = playerDataBase.OfflineCount;
                need = 1;
                break;
            case 19:
                now = playerDataBase.FriesMaxValue;
                need = 1;
                break;
            case 20:
                now = playerDataBase.Candy1MaxValue;
                need = 1;
                break;
            case 21:
                now = playerDataBase.TreasureCount;
                need = 10;
                break;
        }

        titleText.plusText = "\n( " + MoneyUnitString.ToCurrencyString(now) + " / " + MoneyUnitString.ToCurrencyString(need) + " )";
        titleText.ReLoad();

        CheckMission();
    }

    public void CheckMission()
    {
        clearObj.SetActive(false);
        clear = false;

        if (now >= need)
        {
            CheckClear();
        }
    }

    void CheckClear()
    {
        titleText.GetText().color = Color.yellow;
        clearObj.SetActive(true);
        clear = true;
    }

    public void ClearButton()
    {
        if(clear)
        {
            firstReset = false;

            playerDataBase.GuideIndex += 1;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("GuideIndex", playerDataBase.GuideIndex);

            PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 10);

            SoundManager.instance.PlaySFX(GameSfxType.Success);
            NotionManager.instance.UseNotion(NotionType.SuccessReward);

            Initialize();
        }
    }

}
