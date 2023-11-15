using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReincarnationManager : MonoBehaviour
{
    public GameObject reincarnationView;

    public Text crystalText;
    public Text countText;
    public Text adText;


    private float crystal = 0;

    private int number = 0;

    public FadeInOut fadeInOut;

    public TutorialManager tutorialManager;
    public GameManager gameManager;

    WaitForSeconds waitForSeconds = new WaitForSeconds(0.3f);

    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        reincarnationView.SetActive(false);


    }

    public void OpenReincarnationView()
    {
        if(!reincarnationView.activeInHierarchy)
        {
            reincarnationView.SetActive(true);

            Initialize();
        }
        else
        {
            reincarnationView.SetActive(false);
        }
    }

    void Initialize()
    {
        crystal = 0;

        if (playerDataBase.FriesMaxValue > 0)
        {
            crystal += 100;
        }

        if(playerDataBase.Candy1MaxValue > 0)
        {
            crystal += 5;
        }

        if (playerDataBase.Candy2MaxValue > 0)
        {
            crystal += 10;
        }

        if (playerDataBase.Candy3MaxValue > 0)
        {
            crystal += 10;
        }

        if (playerDataBase.Candy4MaxValue > 0)
        {
            crystal += 15;
        }

        if (playerDataBase.Candy5MaxValue > 0)
        {
            crystal += 15;
        }

        if (playerDataBase.Candy6MaxValue > 0)
        {
            crystal += 20;
        }

        if (playerDataBase.Candy7MaxValue > 0)
        {
            crystal += 20;
        }

        if (playerDataBase.Candy8MaxValue > 0)
        {
            crystal += 25;
        }

        if (playerDataBase.Candy9MaxValue > 0)
        {
            crystal += 25;
        }

        crystal = crystal + (crystal * (0.02f * (playerDataBase.Skill11 * 1)));

        crystalText.text = MoneyUnitString.ToCurrencyString((int)crystal).ToString();

        countText.text = LocalizationManager.instance.GetString("Reincarnation_Count") + " : " + playerDataBase.ReincarnationCount;

        adText.text = LocalizationManager.instance.GetString("Reincarnation_Ad") + "\n+" + MoneyUnitString.ToCurrencyString((int)crystal).ToString();
    }

    public void Free()
    {
        if (crystal == 0) return;

        number = 0;
        StartCoroutine(ReincarnationCoroution());
    }

    public void Ad()
    {
        if (crystal == 0) return;

        GoogleAdsManager.instance.admobReward_ReincarnationX2.ShowAd(4);
    }

    public void SuccessWatchAd()
    {
        number = 1;
        StartCoroutine(ReincarnationCoroution());

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);
    }


    IEnumerator ReincarnationCoroution()
    {
        fadeInOut.FadeOut();

        yield return new WaitForSeconds(1.0f);

        reincarnationView.SetActive(false);

        if(number == 0)
        {
            PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, (int)crystal);
        }
        else
        {
            PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, (int)crystal * 2);
        }

        GameStateManager.instance.IslandType = IslandType.Island1;
        GameStateManager.instance.FoodType = FoodType.Hamburger;
        GameStateManager.instance.CandyType = CandyType.Candy1;

        GameStateManager.instance.HamburgerLevel = 0;
        GameStateManager.instance.SandwichLevel = 0;
        GameStateManager.instance.SnackLabLevel = 0;
        GameStateManager.instance.DrinkLevel = 0;
        GameStateManager.instance.PizzaLevel = 0;
        GameStateManager.instance.FriesLevel = 0;

        GameStateManager.instance.Candy1Level = 0;
        GameStateManager.instance.Candy2Level = 0;
        GameStateManager.instance.Candy3Level = 0;
        GameStateManager.instance.Candy4Level = 0;
        GameStateManager.instance.Candy5Level = 0;
        GameStateManager.instance.Candy6Level = 0;
        GameStateManager.instance.Candy7Level = 0;
        GameStateManager.instance.Candy8Level = 0;
        GameStateManager.instance.Candy9Level = 0;

        playerDataBase.HamburgerMaxValue = 0;
        playerDataBase.SandwichMaxValue = 0;
        playerDataBase.SnackLabMaxValue = 0;
        playerDataBase.DrinkMaxValue = 0;
        playerDataBase.PizzaMaxValue = 0;
        playerDataBase.DonutMaxValue = 0;
        playerDataBase.FriesMaxValue = 0;

        playerDataBase.Candy1MaxValue = 0;
        playerDataBase.Candy2MaxValue = 0;
        playerDataBase.Candy3MaxValue = 0;
        playerDataBase.Candy4MaxValue = 0;
        playerDataBase.Candy5MaxValue = 0;
        playerDataBase.Candy6MaxValue = 0;
        playerDataBase.Candy7MaxValue = 0;
        playerDataBase.Candy8MaxValue = 0;
        playerDataBase.Candy9MaxValue = 0;

        playerDataBase.IslandNumber = 0;
        playerDataBase.ReincarnationCount += 1;

        playerDataBase.NextFoodNumber = 0;
        playerDataBase.NextFoodNumber2 = 0;

        gameManager.Reincarnation();
        tutorialManager.Reincarnation();

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("HamburgerMaxValue", playerDataBase.HamburgerMaxValue);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SandwichMaxValue", playerDataBase.SandwichMaxValue);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SnackLabMaxValue", playerDataBase.SnackLabMaxValue);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DrinkMaxValue", playerDataBase.DrinkMaxValue);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("PizzaMaxValue", playerDataBase.PizzaMaxValue);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DonutMaxValue", playerDataBase.DonutMaxValue);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("FriesMaxValue", playerDataBase.FriesMaxValue);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy1MaxValue", playerDataBase.Candy1MaxValue);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy2MaxValue", playerDataBase.Candy2MaxValue);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy3MaxValue", playerDataBase.Candy3MaxValue);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy4MaxValue", playerDataBase.Candy4MaxValue);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy5MaxValue", playerDataBase.Candy5MaxValue);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy6MaxValue", playerDataBase.Candy6MaxValue);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy7MaxValue", playerDataBase.Candy7MaxValue);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy8MaxValue", playerDataBase.Candy8MaxValue);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy9MaxValue", playerDataBase.Candy9MaxValue);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("ReincarnationCount", playerDataBase.ReincarnationCount);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber2);
    }
}
