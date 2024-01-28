using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelView;

    public GameObject alarm;

    public Text titleText;
    public Text titleInfoText;
    public Text levelText;
    public Text expText;
    public Image expFillamount;

    public LocalizationContent infoText;
    public Text accessDateText;

    private int nowLevel = 0;
    private int level = 0;
    private int nowExp = 0;
    private int nextExp = 0;

    public AdvancementManager advancementManager;

    PlayerDataBase playerDataBase;
    LevelDataBase levelDataBase;
    AnimalDataBase animalDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (levelDataBase == null) levelDataBase = Resources.Load("LevelDataBase") as LevelDataBase;
        if (animalDataBase == null) animalDataBase = Resources.Load("AnimalDataBase") as AnimalDataBase;

        levelDataBase.Initialize();

        alarm.SetActive(true);

        levelView.SetActive(false);
    }


    public void OpenLevelView()
    {
        if (!levelView.activeInHierarchy)
        {
            levelView.SetActive(true);

            alarm.SetActive(false);

            Initialize();

            advancementManager.OpenView();

            infoText.ReLoad();

            FirebaseAnalytics.LogEvent("OpenProfile");
        }
        else
        {
            levelView.SetActive(false);
        }
    }

    public void Initialize()
    {
        nowLevel = level;
        level = levelDataBase.GetLevel(playerDataBase.Exp);

        if(nowLevel != 0 && level > nowLevel)
        {
            NotionManager.instance.UseNotion3(NotionType.Levelup);

            GameManager.instance.CheckPercent();

            Debug.LogError("·¹º§ ¾÷");
        }

        if(playerDataBase.Level != level)
        {
            playerDataBase.Level = level;
#if !UNITY_EDITOR
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("Level", playerDataBase.Level);
#endif
        }

        GameStateManager.instance.Level = level;

        titleText.text = "Lv." + (level + 1).ToString();
        levelText.text = (level + 1).ToString();

        titleInfoText.text = LocalizationManager.instance.GetString("LevelInfo") + "  <color=#FFFF00>(+" +
            (10 + (int)animalDataBase.GetAnimalEffect(playerDataBase.GetAnimalHighNumber())) +")</color>";

        nowExp = levelDataBase.GetNowExp(playerDataBase.Exp);
        nextExp = levelDataBase.GetNextExp(level);

        expText.text = nowExp + " / " + nextExp;

        expFillamount.fillAmount = (nowExp * 1.0f) / (nextExp * 1.0f);

        infoText.localizationName = "SuccessPercent";
        infoText.plusText = " : +" + (level * 0.05f).ToString("N1") + "%";

        accessDateText.text = LocalizationManager.instance.GetString("AccessDate") + " : " + playerDataBase.AccessDate;
    }
}
