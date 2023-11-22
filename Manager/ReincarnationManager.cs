using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReincarnationManager : MonoBehaviour
{
    public GameObject reincarnationView;

    public GameObject alarm;

    public Text crystalText;
    public Text countText;
    public Text passiveText;
    public Text adText;

    public GameObject lockedObj;


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

        alarm.SetActive(true);
    }

    public void OpenReincarnationView()
    {
        if(!reincarnationView.activeInHierarchy)
        {
            reincarnationView.SetActive(true);

            Initialize();

            alarm.SetActive(false);
        }
        else
        {
            reincarnationView.SetActive(false);
        }
    }

    void Initialize()
    {
        crystal = 0;

        lockedObj.SetActive(true);

        if (playerDataBase.IslandNumber > 0)
        {
            crystal += 30;
            crystal += playerDataBase.NextFoodNumber2 * 5;

            lockedObj.SetActive(false);
        }

        passiveText.text = crystal.ToString() + " (+" + (playerDataBase.Skill11 * 2).ToString() + "%)";

        crystal = crystal + (crystal * (0.04f * (playerDataBase.Skill11 * 1)));

        crystalText.text = MoneyUnitString.ToCurrencyString((int)crystal).ToString();

        countText.text = LocalizationManager.instance.GetString("Reincarnation_Count") + " : " + playerDataBase.ReincarnationCount;

        adText.text = LocalizationManager.instance.GetString("Reincarnation_Ad") + "\n+" + MoneyUnitString.ToCurrencyString((int)crystal * 2).ToString();
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

        playerDataBase.IslandNumber = 0;
        playerDataBase.ReincarnationCount += 1;

        playerDataBase.NextFoodNumber = 0;
        playerDataBase.NextFoodNumber2 = 0;

        gameManager.Reincarnation();
        tutorialManager.Reincarnation();

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("ReincarnationCount", playerDataBase.ReincarnationCount);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber2);
    }
}
