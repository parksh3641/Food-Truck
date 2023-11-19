using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelView;

    public Text titleText;
    public Text levelText;
    public Text expText;
    public Image expFillamount;

    public LocalizationContent infoText;

    private int level = 0;
    private int nowExp = 0;
    private int nextExp = 0;

    PlayerDataBase playerDataBase;
    LevelDataBase levelDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (levelDataBase == null) levelDataBase = Resources.Load("LevelDataBase") as LevelDataBase;

        levelDataBase.Initialize();

        levelView.SetActive(false);
    }

    private void Start()
    {

    }


    public void OpenLevelView()
    {
        if (!levelView.activeInHierarchy)
        {
            levelView.SetActive(true);

            FirebaseAnalytics.LogEvent("OpenLevel");

            Initialize();
        }
        else
        {
            levelView.SetActive(false);
        }
    }

    public void Initialize()
    {
        level = levelDataBase.GetLevel(playerDataBase.UpgradeCount);

        GameStateManager.instance.Level = level;

        titleText.text = "Lv." + level.ToString();
        levelText.text = level.ToString();

        nowExp = levelDataBase.GetNowExp(playerDataBase.UpgradeCount);
        nextExp = levelDataBase.GetNextExp(level);

        expText.text = nowExp + " / " + nextExp;

        expFillamount.fillAmount = (nowExp * 1.0f) / (nextExp * 1.0f);

        infoText.localizationName = "SuccessPercent";
        infoText.plusText = " : +" + (level * 0.2f) + "%";
    }
}
