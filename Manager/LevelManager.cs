using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelView;

    public Text titleText;
    public Text titleInfoText;
    public Text levelText;
    public Text expText;
    public Image expFillamount;

    public LocalizationContent infoText;
    public Text accessDateText;

    private int level = 0;
    private int nowExp = 0;
    private int nextExp = 0;

    PlayerDataBase playerDataBase;
    LevelDataBase levelDataBase;
    AnimalDataBase animalDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (levelDataBase == null) levelDataBase = Resources.Load("LevelDataBase") as LevelDataBase;
        if (animalDataBase == null) animalDataBase = Resources.Load("AnimalDataBase") as AnimalDataBase;

        levelDataBase.Initialize();

        levelView.SetActive(false);
    }


    public void OpenLevelView()
    {
        if (!levelView.activeInHierarchy)
        {
            levelView.SetActive(true);

            Initialize();

            infoText.ReLoad();

            GameStateManager.instance.Pause = true;

            FirebaseAnalytics.LogEvent("OpenProfile");
        }
        else
        {
            levelView.SetActive(false);

            GameStateManager.instance.Pause = false;
        }
    }

    public void Initialize()
    {
        level = levelDataBase.GetLevel(playerDataBase.Exp);

        if(playerDataBase.Level != level)
        {
            playerDataBase.Level = level;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("Level", playerDataBase.Level);
        }

        GameStateManager.instance.Level = level;

        titleText.text = "Lv." + (level + 1).ToString();
        levelText.text = (level + 1).ToString();

        titleInfoText.text = LocalizationManager.instance.GetString("LevelInfo") + "  (+" + (10 + (int)animalDataBase.GetAnimalEffect(playerDataBase.GetAnimalHighNumber())) +")";

        nowExp = levelDataBase.GetNowExp(playerDataBase.Exp);
        nextExp = levelDataBase.GetNextExp(level);

        expText.text = nowExp + " / " + nextExp;

        expFillamount.fillAmount = (nowExp * 1.0f) / (nextExp * 1.0f);

        infoText.localizationName = "SuccessPercent";
        infoText.plusText = " : +" + (level * 0.1f).ToString("N1") + "%";

        accessDateText.text = LocalizationManager.instance.GetString("AccessDate") + " : " + playerDataBase.AccessDate;
    }
}
