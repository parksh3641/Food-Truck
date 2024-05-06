using Firebase.Analytics;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFoodManager : MonoBehaviour
{
    public GameObject changeFoodView;
    public GameObject autoUpgradeView;

    public GameObject moveIsland;
    public GameObject rankMode;

    public LocalizationContent changeModeText;

    public GameObject alarmObj;

    [Title("Proficiency")]
    public Text proficiencyLevelText;
    public Text proficiencyValueText;
    public Image proficiencyFillamount;
    public Text proficiencyEffectText;

    [Title("Auto")]
    public GameObject autoUpgradeLocked;
    public Image autoUpgradeButton;
    public Text autoUpgradeText;

    public Text islandTitleText;

    public GameObject autoPresentLocked;
    public Image autoPresentButton;
    public Text autoPresentText;

    public Image[] autoUpgradeArray;

    Color onColor = new Color(38 / 255f, 238 / 255f, 130 / 255f);
    Color offColor = new Color(1, 97 / 255f, 92 / 255f);

    public ChangeFoodContent changeFoodContent;

    public RectTransform changeFoodContentTransform;

    public List<ChangeFoodContent> foodContentList = new List<ChangeFoodContent>();
    public List<ChangeFoodContent> rankFoodContentList = new List<ChangeFoodContent>();

    Sprite[] foodArray;
    Sprite[] rankFoodArray;

    private int exp = 0;
    private int level = 0;
    private int nowExp = 0;
    private int nextExp = 0;
    private int number = 0;

    private bool init = false;

    public LockManager lockManager;
    public IslandManager islandManager;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;
    UpgradeDataBase upgradeDataBase;
    ProficiencyDataBase proficiencyDataBase;
    LevelDataBase levelDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (upgradeDataBase == null) upgradeDataBase = Resources.Load("UpgradeDataBase") as UpgradeDataBase;
        if (proficiencyDataBase == null) proficiencyDataBase = Resources.Load("ProficiencyDataBase") as ProficiencyDataBase;
        if (levelDataBase == null) levelDataBase = Resources.Load("LevelDataBase") as LevelDataBase;

        proficiencyDataBase.Initialize();

        foodArray = imageDataBase.GetFoodIconArray();
        rankFoodArray = imageDataBase.GetRankFoodIconArray();

        changeFoodView.SetActive(false);
        autoUpgradeView.SetActive(false);

        alarmObj.SetActive(false);
    }

    public void Initialize()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(FoodType)).Length; i++)
        {
            ChangeFoodContent monster = Instantiate(changeFoodContent);
            monster.transform.SetParent(changeFoodContentTransform);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;
            monster.Initialize_Food(FoodType.Food1 + i, foodArray[i], this);
            monster.gameObject.SetActive(true);

            foodContentList.Add(monster);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(RankFoodType)).Length; i++)
        {
            ChangeFoodContent monster = Instantiate(changeFoodContent);
            monster.transform.SetParent(changeFoodContentTransform);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;
            monster.Initialize_RankFood(RankFoodType.RankFood1 + i, rankFoodArray[i], this);
            monster.gameObject.SetActive(true);

            rankFoodContentList.Add(monster);
        }

        init = true;

        changeFoodContentTransform.anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenChangeFoodView()
    {
        if (!changeFoodView.activeInHierarchy)
        {
            changeFoodView.SetActive(true);

            if(!init)
            {
                Initialize();
            }

            alarmObj.SetActive(false);

            CheckFood();

            autoUpgradeLocked.SetActive(true);
            autoPresentLocked.SetActive(true);

            if (playerDataBase.AutoUpgrade)
            {
                autoUpgradeLocked.SetActive(false);
            }

            if (playerDataBase.AutoPresent)
            {
                autoPresentLocked.SetActive(false);
            }

            if (GameStateManager.instance.AutoUpgrade)
            {
                autoUpgradeButton.color = onColor;
                autoUpgradeText.text = LocalizationManager.instance.GetString("ON");
            }
            else
            {
                autoUpgradeButton.color = offColor;
                autoUpgradeText.text = LocalizationManager.instance.GetString("OFF");
            }

            if (GameStateManager.instance.AutoPresent)
            {
                autoPresentButton.color = onColor;
                autoPresentText.text = LocalizationManager.instance.GetString("ON");
            }
            else
            {
                autoPresentButton.color = offColor;
                autoPresentText.text = LocalizationManager.instance.GetString("OFF");
            }

            GameManager.instance.moveArrow3.SetActive(false);

            if (playerDataBase.InGameTutorial == 0)
            {
                lockManager.NextFoodTutorial();

                playerDataBase.InGameTutorial = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("InGameTutorial", playerDataBase.InGameTutorial);
            }


            //GameStateManager.instance.Pause = true;

            FirebaseAnalytics.LogEvent("Open_ChangeFood");
        }
        else
        {
            changeFoodView.SetActive(false);

            GameManager.instance.CheckAuto();
            GameManager.instance.CheckPercent();
            GameManager.instance.CheckPortion();
            GameManager.instance.CheckDefTicket();

            //GameStateManager.instance.Pause = false;
        }
    }

    public void CloseChangeFoodView()
    {
        changeFoodView.SetActive(false);
    }

    public void OpenAutoUpgradeView()
    {
        if (!playerDataBase.AutoUpgrade)
        {
            BuyShopNotion();
            return;
        }

        if (!autoUpgradeView.activeInHierarchy)
        {
            autoUpgradeView.SetActive(true);

            ChangeAutoUpgradeLevel(GameStateManager.instance.AutoUpgradeLevel);
        }
        else 
        {
            autoUpgradeView.SetActive(false);
        }
    }

    public void ChangeAutoUpgradeLevel(int number)
    {
        for(int i = 0; i < autoUpgradeArray.Length; i ++)
        {
            autoUpgradeArray[i].color = Color.white;
        }

        autoUpgradeArray[(number / 5) - 1].color = onColor;

        if(number < 5)
        {
            GameStateManager.instance.AutoUpgradeLevel = 5;
        }

        if(GameStateManager.instance.AutoUpgradeLevel != number)
        {
            GameStateManager.instance.AutoUpgradeLevel = number;

            SoundManager.instance.PlaySFX(GameSfxType.Success);
            NotionManager.instance.UseNotion(NotionType.ChangeNotion);
        }
    }

    public void CheckProficiency()
    {
        exp = 0;
        exp = playerDataBase.island_Total_Data.GetMaxTotalValue();

        level = proficiencyDataBase.GetMotherLevel(exp);

        nowExp = proficiencyDataBase.GetMotherNowExp(exp);
        nextExp = proficiencyDataBase.GetMotherNextExp(level);

        playerDataBase.Proficiency = level;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Proficiency", playerDataBase.Proficiency);
    }

    public void CheckFood()
    {
        CheckProficiency();

        moveIsland.SetActive(false);
        if (playerDataBase.IslandNumber > 0 && GameStateManager.instance.GameType != GameType.Rank)
        {
            moveIsland.SetActive(true);
        }

        rankMode.SetActive(false);
        if(playerDataBase.Level > 4)
        {
            rankMode.SetActive(true);

            if(GameStateManager.instance.GameType == GameType.Story)
            {
                changeModeText.localizationName = "ChangeRanking";
            }
            else
            {
                changeModeText.localizationName = "ChangeNormal";
            }

            changeModeText.ReLoad();
        }

        proficiencyLevelText.text = level.ToString();

        proficiencyValueText.text = LocalizationManager.instance.GetString("Total") + " : ( " + nowExp + " / " + nextExp + " )";
        proficiencyFillamount.fillAmount = (nowExp * 1.0f) / (nextExp * 1.0f);

        proficiencyEffectText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " +" + (level * 2) + "%   (+2%)";

        for (int i = 0; i < foodContentList.Count; i++)
        {
            foodContentList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < rankFoodContentList.Count; i++)
        {
            rankFoodContentList[i].gameObject.SetActive(false);
        }

        number = ((int)GameStateManager.instance.IslandType * GameStateManager.instance.Island);

        if (GameStateManager.instance.GameType == GameType.Story)
        {
            islandTitleText.text = LocalizationManager.instance.GetString(GameStateManager.instance.IslandType.ToString());

            for (int i = 0; i < GameStateManager.instance.Island; i ++)
            {
                foodContentList[number + i].gameObject.SetActive(true);
                foodContentList[number + i].CheckFoodProficiency();
                foodContentList[number + i].Locked();
                foodContentList[number + i].UnSelected();
                foodContentList[number + i].SetLevel(GameStateManager.instance.FoodLevel[number + i], upgradeDataBase.GetFoodMaxLevel(FoodType.Food1 + number + i));
            }

            foodContentList[number].UnLock();
            foodContentList[(int)GameStateManager.instance.FoodType].Selected();

            for (int j = 0; j < playerDataBase.NextFoodNumber + 1; j++)
            {
                if (foodContentList[j].gameObject.activeInHierarchy)
                {
                    foodContentList[j].UnLock();
                }
            }
        }
        else
        {
            islandTitleText.text = LocalizationManager.instance.GetString("Ranking2");

            for (int i = 0; i < rankFoodContentList.Count; i++)
            {
                rankFoodContentList[i].gameObject.SetActive(true);
                rankFoodContentList[i].CheckFoodProficiency();
                rankFoodContentList[i].Locked();
                rankFoodContentList[i].UnSelected();
                rankFoodContentList[i].SetLevel(GameStateManager.instance.RankFoodLevel[i], upgradeDataBase.GetRankFoodMaxLevel(RankFoodType.RankFood1 + i));

                rankFoodContentList[(int)GameStateManager.instance.RankFoodType].Selected();

                rankFoodContentList[0].UnLock();
            }

            for(int i = 0; i < 10; i ++)
            {
                rankFoodContentList[i].UnLock();
            }
        }
    }

    public void ChangeFood(FoodType type)
    {
        if (GameStateManager.instance.FoodType == type) return;

        OpenChangeFoodView();

        GameManager.instance.ChangeFood(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }

    public void ChangeRankFood(RankFoodType type)
    {
        OpenChangeFoodView();

        GameManager.instance.ChangeRankFood(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }
    public void BuyShopNotion()
    {
        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
        NotionManager.instance.UseNotion(NotionType.BuyShopNotion);
    }

    public void ChangeAutoUpgrade()
    {
        if (!playerDataBase.AutoUpgrade) return;

        if(GameStateManager.instance.AutoUpgrade)
        {
            GameStateManager.instance.AutoUpgrade = false;

            autoUpgradeButton.color = offColor;
            autoUpgradeText.text = LocalizationManager.instance.GetString("OFF");
        }
        else
        {
            GameStateManager.instance.AutoUpgrade = true;

            autoUpgradeButton.color = onColor;
            autoUpgradeText.text = LocalizationManager.instance.GetString("ON");
        }
    }

    public void ChangeAutoPresent()
    {
        if (!playerDataBase.AutoPresent) return;

        if (GameStateManager.instance.AutoPresent)
        {
            GameStateManager.instance.AutoPresent = false;

            autoPresentButton.color = offColor;
            autoPresentText.text = LocalizationManager.instance.GetString("OFF");
        }
        else
        {
            GameStateManager.instance.AutoPresent = true;

            autoPresentButton.color = onColor;
            autoPresentText.text = LocalizationManager.instance.GetString("ON");
        }
    }


    public void ChangeFoodMoveArrow(FoodType type)
    {
        if ((int)type > System.Enum.GetValues(typeof(IslandType)).Length - 1) return;

        foodContentList[(int)type].SetMoveArrow();
    }

    public void OpenMoveIsland()
    {
        islandManager.OpenChangeIslandView();
    }

    public void ChangeMode()
    {
        if(GameStateManager.instance.GameType == GameType.Story)
        {
            GameManager.instance.GameStart(1);
        }
        else
        {
            GameManager.instance.GameStart(0);
        }

        CheckFood();

        changeFoodContentTransform.anchoredPosition = new Vector2(0, -9999);
    }
}
