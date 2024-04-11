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

    public GameObject alarmObj;
    public GameObject islandAlarm;

    [Title("Proficiency")]
    public Text proficiencyLevelText;
    public Text proficiencyValueText;
    public Image proficiencyFillamount;
    public Text proficiencyEffectText;

    [Title("Auto")]
    public GameObject autoUpgradeLocked;
    public Image autoUpgradeButton;
    public Text autoUpgradeText;

    public GameObject autoPresentLocked;
    public Image autoPresentButton;
    public Text autoPresentText;

    public Image[] autoUpgradeArray;

    Color onColor = new Color(38 / 255f, 238 / 255f, 130 / 255f);
    Color offColor = new Color(1, 97 / 255f, 92 / 255f);


    public ChangeFoodContent changeFoodContent;

    public RectTransform changeFoodContentTransform;

    public List<ChangeFoodContent> changeFoodContentList = new List<ChangeFoodContent>();
    public List<ChangeFoodContent> changeCandyList = new List<ChangeFoodContent>();
    public List<ChangeFoodContent> changeJapaneseFoodList = new List<ChangeFoodContent>();
    public List<ChangeFoodContent> changeDessertList = new List<ChangeFoodContent>();

    Sprite[] foodChangeArray;
    Sprite[] candyArray;
    Sprite[] japaneseFoodArray;
    Sprite[] dessertArray;

    private int exp = 0;
    private int level = 0;
    private int nowExp = 0;
    private int nextExp = 0;

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

        foodChangeArray = imageDataBase.GetFoodChangeArray();
        candyArray = imageDataBase.GetCandyArray();
        japaneseFoodArray = imageDataBase.GetJapaneseFoodArray();
        dessertArray = imageDataBase.GetDessertArray();

        changeFoodView.SetActive(false);
        autoUpgradeView.SetActive(false);

        alarmObj.SetActive(false);
        islandAlarm.SetActive(false);
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
            monster.InitializeFood(FoodType.Food1 + i, foodChangeArray[i], this);
            monster.gameObject.SetActive(true);

            changeFoodContentList.Add(monster);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(CandyType)).Length; i++)
        {
            ChangeFoodContent monster = Instantiate(changeFoodContent);
            monster.transform.SetParent(changeFoodContentTransform);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;
            monster.InitializeCandy(CandyType.Candy1 + i, candyArray[i], this);
            monster.gameObject.SetActive(true);

            changeCandyList.Add(monster);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(JapaneseFoodType)).Length; i++)
        {
            ChangeFoodContent monster = Instantiate(changeFoodContent);
            monster.transform.SetParent(changeFoodContentTransform);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;
            monster.InitializeJapaneseFood(JapaneseFoodType.JapaneseFood1 + i, japaneseFoodArray[i], this);
            monster.gameObject.SetActive(true);

            changeJapaneseFoodList.Add(monster);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(DessertType)).Length; i++)
        {
            ChangeFoodContent monster = Instantiate(changeFoodContent);
            monster.transform.SetParent(changeFoodContentTransform);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;
            monster.InitializeDessert(DessertType.Dessert1 + i, dessertArray[i], this);
            monster.gameObject.SetActive(true);

            changeDessertList.Add(monster);
        }

        init = true;

        changeFoodContentTransform.anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenChangeFoodView()
    {
        if(!changeFoodView.activeInHierarchy)
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

        exp = playerDataBase.Food1MaxValue + playerDataBase.Food2MaxValue + playerDataBase.Food3MaxValue + playerDataBase.Food4MaxValue
            + playerDataBase.Food5MaxValue + playerDataBase.Food6MaxValue + playerDataBase.Food7MaxValue + playerDataBase.Candy1MaxValue
            + playerDataBase.Candy2MaxValue + playerDataBase.Candy3MaxValue + playerDataBase.Candy4MaxValue + playerDataBase.Candy5MaxValue
            + playerDataBase.Candy6MaxValue + playerDataBase.Candy7MaxValue + playerDataBase.Candy8MaxValue + playerDataBase.Candy9MaxValue
            + playerDataBase.JapaneseFood1MaxValue + playerDataBase.JapaneseFood2MaxValue + playerDataBase.JapaneseFood3MaxValue
            + playerDataBase.JapaneseFood4MaxValue + playerDataBase.JapaneseFood5MaxValue + playerDataBase.JapaneseFood6MaxValue
            + playerDataBase.JapaneseFood7MaxValue + playerDataBase.Dessert1MaxValue + playerDataBase.Dessert2MaxValue
            + playerDataBase.Dessert3MaxValue + playerDataBase.Dessert4MaxValue + playerDataBase.Dessert5MaxValue + playerDataBase.Dessert6MaxValue
            + playerDataBase.Dessert7MaxValue + playerDataBase.Dessert8MaxValue + playerDataBase.Dessert9MaxValue;

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

        proficiencyLevelText.text = level.ToString();

        proficiencyValueText.text = LocalizationManager.instance.GetString("Total") + " : ( " + nowExp + " / " + nextExp + " )";
        proficiencyFillamount.fillAmount = (nowExp * 1.0f) / (nextExp * 1.0f);

        proficiencyEffectText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " +" + (level * 2) + "%   (+2%)";

        for (int i = 0; i < changeFoodContentList.Count; i++)
        {
            changeFoodContentList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < changeCandyList.Count; i++)
        {
            changeCandyList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < changeJapaneseFoodList.Count; i++)
        {
            changeJapaneseFoodList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < changeDessertList.Count; i++)
        {
            changeDessertList[i].gameObject.SetActive(false);
        }

        if (GameStateManager.instance.GameType == GameType.Rank)
        {
            changeFoodContentList[7].gameObject.SetActive(true);
            changeFoodContentList[7].CheckFoodProficiency();

            changeCandyList[9].gameObject.SetActive(true);
            changeCandyList[9].CheckCandyProficiency();

            changeJapaneseFoodList[7].gameObject.SetActive(true);
            changeJapaneseFoodList[7].CheckJapaneseFoodProficiency();

            changeDessertList[9].gameObject.SetActive(true);
            changeDessertList[9].CheckDessertProficiency();

            switch (GameStateManager.instance.IslandType)
            {
                case IslandType.Island1:
                    changeFoodContentList[7].Selected();
                    changeCandyList[9].UnSelected();
                    changeJapaneseFoodList[7].UnSelected();
                    changeDessertList[9].UnSelected();
                    break;
                case IslandType.Island2:
                    changeFoodContentList[7].UnSelected();
                    changeCandyList[9].Selected();
                    changeJapaneseFoodList[7].UnSelected();
                    changeDessertList[9].UnSelected();
                    break;
                case IslandType.Island3:
                    changeFoodContentList[7].UnSelected();
                    changeCandyList[9].UnSelected();
                    changeJapaneseFoodList[7].Selected();
                    changeDessertList[9].UnSelected();
                    break;
                case IslandType.Island4:
                    changeFoodContentList[7].UnSelected();
                    changeCandyList[9].UnSelected();
                    changeJapaneseFoodList[7].UnSelected();
                    changeDessertList[9].Selected();
                    break;
            }

            changeFoodContentList[7].UnLock();
            changeCandyList[9].Locked();
            changeJapaneseFoodList[7].Locked();
            changeDessertList[9].Locked();

            changeFoodContentList[7].SetLevel(GameStateManager.instance.Food8Level, upgradeDataBase.GetMaxLevelFastFood(FoodType.Ribs));
            changeCandyList[9].SetLevel(GameStateManager.instance.Candy10Level, upgradeDataBase.GetMaxLevelCandy(CandyType.Chocolate));
            changeJapaneseFoodList[7].SetLevel(GameStateManager.instance.JapaneseFood8Level, upgradeDataBase.GetMaxLevelJapaneseFood(JapaneseFoodType.Ramen));
            changeDessertList[9].SetLevel(GameStateManager.instance.Dessert10Level, upgradeDataBase.GetMaxLevelDessert(DessertType.FruitSkewers));

            if (playerDataBase.IslandNumber > 0 || playerDataBase.Candy1MaxValue > 0)
            {
                changeCandyList[9].UnLock();
            }

            if (playerDataBase.IslandNumber > 1 || playerDataBase.JapaneseFood1MaxValue > 0)
            {
                changeJapaneseFoodList[7].UnLock();
            }

            if (playerDataBase.IslandNumber > 2 || playerDataBase.Dessert1MaxValue > 0)
            {
                changeDessertList[9].UnLock();
            }

            return;
        }


        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                for (int i = 0; i < changeFoodContentList.Count; i++)
                {
                    changeFoodContentList[i].gameObject.SetActive(true);
                    changeFoodContentList[i].CheckFoodProficiency();
                    changeFoodContentList[i].Locked();
                    changeFoodContentList[i].UnSelected();
                }

                changeFoodContentList[(int)GameStateManager.instance.FoodType].Selected();
                changeFoodContentList[0].SetLevel(GameStateManager.instance.Food1Level, upgradeDataBase.GetMaxLevelFastFood(FoodType.Food1));
                changeFoodContentList[1].SetLevel(GameStateManager.instance.Food2Level, upgradeDataBase.GetMaxLevelFastFood(FoodType.Food2));
                changeFoodContentList[2].SetLevel(GameStateManager.instance.Food3Level, upgradeDataBase.GetMaxLevelFastFood(FoodType.Food3));
                changeFoodContentList[3].SetLevel(GameStateManager.instance.Food4Level, upgradeDataBase.GetMaxLevelFastFood(FoodType.Food4));
                changeFoodContentList[4].SetLevel(GameStateManager.instance.Food5Level, upgradeDataBase.GetMaxLevelFastFood(FoodType.Food5));
                changeFoodContentList[5].SetLevel(GameStateManager.instance.Food6Level, upgradeDataBase.GetMaxLevelFastFood(FoodType.Food6));
                changeFoodContentList[6].SetLevel(GameStateManager.instance.Food7Level, upgradeDataBase.GetMaxLevelFastFood(FoodType.Food7));
                changeFoodContentList[7].gameObject.SetActive(false);

                changeFoodContentList[0].UnLock();

                for (int i = 0; i < playerDataBase.NextFoodNumber + 1; i++)
                {
                    if (changeFoodContentList[i] != null)
                    {
                        changeFoodContentList[i].UnLock();
                    }
                }

                break;
            case IslandType.Island2:
                for (int i = 0; i < changeCandyList.Count; i++)
                {
                    changeCandyList[i].gameObject.SetActive(true);
                    changeCandyList[i].CheckCandyProficiency();
                    changeCandyList[i].Locked();
                    changeCandyList[i].UnSelected();
                }

                changeCandyList[(int)GameStateManager.instance.CandyType].Selected();
                changeCandyList[0].SetLevel(GameStateManager.instance.Candy1Level, upgradeDataBase.GetMaxLevelCandy(CandyType.Candy1));
                changeCandyList[1].SetLevel(GameStateManager.instance.Candy2Level, upgradeDataBase.GetMaxLevelCandy(CandyType.Candy2));
                changeCandyList[2].SetLevel(GameStateManager.instance.Candy3Level, upgradeDataBase.GetMaxLevelCandy(CandyType.Candy3));
                changeCandyList[3].SetLevel(GameStateManager.instance.Candy4Level, upgradeDataBase.GetMaxLevelCandy(CandyType.Candy4));
                changeCandyList[4].SetLevel(GameStateManager.instance.Candy5Level, upgradeDataBase.GetMaxLevelCandy(CandyType.Candy5));
                changeCandyList[5].SetLevel(GameStateManager.instance.Candy6Level, upgradeDataBase.GetMaxLevelCandy(CandyType.Candy6));
                changeCandyList[6].SetLevel(GameStateManager.instance.Candy7Level, upgradeDataBase.GetMaxLevelCandy(CandyType.Candy7));
                changeCandyList[7].SetLevel(GameStateManager.instance.Candy8Level, upgradeDataBase.GetMaxLevelCandy(CandyType.Candy8));
                changeCandyList[8].SetLevel(GameStateManager.instance.Candy9Level, upgradeDataBase.GetMaxLevelCandy(CandyType.Candy9));
                changeCandyList[9].gameObject.SetActive(false);

                changeCandyList[0].UnLock();

                for (int i = 0; i < playerDataBase.NextFoodNumber2 + 1; i++)
                {
                    if (changeCandyList[i] != null)
                    {
                        changeCandyList[i].UnLock();
                    }
                }

                break;
            case IslandType.Island3:
                for (int i = 0; i < changeJapaneseFoodList.Count; i++)
                {
                    changeJapaneseFoodList[i].gameObject.SetActive(true);
                    changeJapaneseFoodList[i].CheckJapaneseFoodProficiency();
                    changeJapaneseFoodList[i].Locked();
                    changeJapaneseFoodList[i].UnSelected();
                }

                changeJapaneseFoodList[(int)GameStateManager.instance.JapaneseFoodType].Selected();
                changeJapaneseFoodList[0].SetLevel(GameStateManager.instance.JapaneseFood1Level, upgradeDataBase.GetMaxLevelJapaneseFood(JapaneseFoodType.JapaneseFood1));
                changeJapaneseFoodList[1].SetLevel(GameStateManager.instance.JapaneseFood2Level, upgradeDataBase.GetMaxLevelJapaneseFood(JapaneseFoodType.JapaneseFood2));
                changeJapaneseFoodList[2].SetLevel(GameStateManager.instance.JapaneseFood3Level, upgradeDataBase.GetMaxLevelJapaneseFood(JapaneseFoodType.JapaneseFood3));
                changeJapaneseFoodList[3].SetLevel(GameStateManager.instance.JapaneseFood4Level, upgradeDataBase.GetMaxLevelJapaneseFood(JapaneseFoodType.JapaneseFood4));
                changeJapaneseFoodList[4].SetLevel(GameStateManager.instance.JapaneseFood5Level, upgradeDataBase.GetMaxLevelJapaneseFood(JapaneseFoodType.JapaneseFood5));
                changeJapaneseFoodList[5].SetLevel(GameStateManager.instance.JapaneseFood6Level, upgradeDataBase.GetMaxLevelJapaneseFood(JapaneseFoodType.JapaneseFood6));
                changeJapaneseFoodList[6].SetLevel(GameStateManager.instance.JapaneseFood7Level, upgradeDataBase.GetMaxLevelJapaneseFood(JapaneseFoodType.JapaneseFood7));
                changeJapaneseFoodList[7].gameObject.SetActive(false);

                changeJapaneseFoodList[0].UnLock();

                for (int i = 0; i < playerDataBase.NextFoodNumber3 + 1; i++)
                {
                    if (changeJapaneseFoodList[i] != null)
                    {
                        changeJapaneseFoodList[i].UnLock();
                    }
                }
                break;
            case IslandType.Island4:
                for (int i = 0; i < changeDessertList.Count; i++)
                {
                    changeDessertList[i].gameObject.SetActive(true);
                    changeDessertList[i].CheckDessertProficiency();
                    changeDessertList[i].Locked();
                    changeDessertList[i].UnSelected();
                }

                changeDessertList[(int)GameStateManager.instance.DessertType].Selected();
                changeDessertList[0].SetLevel(GameStateManager.instance.Dessert1Level, upgradeDataBase.GetMaxLevelDessert(DessertType.Dessert1));
                changeDessertList[1].SetLevel(GameStateManager.instance.Dessert2Level, upgradeDataBase.GetMaxLevelDessert(DessertType.Dessert2));
                changeDessertList[2].SetLevel(GameStateManager.instance.Dessert3Level, upgradeDataBase.GetMaxLevelDessert(DessertType.Dessert3));
                changeDessertList[3].SetLevel(GameStateManager.instance.Dessert4Level, upgradeDataBase.GetMaxLevelDessert(DessertType.Dessert4));
                changeDessertList[4].SetLevel(GameStateManager.instance.Dessert5Level, upgradeDataBase.GetMaxLevelDessert(DessertType.Dessert5));
                changeDessertList[5].SetLevel(GameStateManager.instance.Dessert6Level, upgradeDataBase.GetMaxLevelDessert(DessertType.Dessert6));
                changeDessertList[6].SetLevel(GameStateManager.instance.Dessert7Level, upgradeDataBase.GetMaxLevelDessert(DessertType.Dessert7));
                changeDessertList[7].SetLevel(GameStateManager.instance.Dessert8Level, upgradeDataBase.GetMaxLevelDessert(DessertType.Dessert8));
                changeDessertList[8].SetLevel(GameStateManager.instance.Dessert9Level, upgradeDataBase.GetMaxLevelDessert(DessertType.Dessert9));
                changeDessertList[9].gameObject.SetActive(false);

                changeDessertList[0].UnLock();

                for (int i = 0; i < playerDataBase.NextFoodNumber4 + 1; i++)
                {
                    if(changeDessertList[i] != null)
                    {
                        changeDessertList[i].UnLock();
                    }
                }
                break;
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

    public void ChangeRankFood(FoodType type)
    {
        if (GameStateManager.instance.IslandType == IslandType.Island1) return;

        GameStateManager.instance.IslandType = IslandType.Island1;

        OpenChangeFoodView();

        GameManager.instance.ChangeFood(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }

    public void ChangeCandy(CandyType type)
    {
        if (GameStateManager.instance.CandyType == type) return;

        OpenChangeFoodView();

        GameManager.instance.ChangeCandy(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }

    public void ChangeRankCandy(CandyType type)
    {
        if (GameStateManager.instance.IslandType == IslandType.Island2) return;

        GameStateManager.instance.IslandType = IslandType.Island2;

        OpenChangeFoodView();

        GameManager.instance.ChangeCandy(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }

    public void ChangeJapaneseFood(JapaneseFoodType type)
    {
        if (GameStateManager.instance.JapaneseFoodType == type) return;

        OpenChangeFoodView();

        GameManager.instance.ChangeJapaneseFood(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }

    public void ChangeRankJapaneseFood(JapaneseFoodType type)
    {
        if (GameStateManager.instance.IslandType == IslandType.Island3) return;

        GameStateManager.instance.IslandType = IslandType.Island3;

        OpenChangeFoodView();

        GameManager.instance.ChangeJapaneseFood(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }

    public void ChangeDessert(DessertType type)
    {
        if (GameStateManager.instance.DessertType == type) return;

        OpenChangeFoodView();

        GameManager.instance.ChangeDessert(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }

    public void ChangeRankDessert(DessertType type)
    {
        if (GameStateManager.instance.IslandType == IslandType.Island4) return;

        GameStateManager.instance.IslandType = IslandType.Island4;

        OpenChangeFoodView();

        GameManager.instance.ChangeDessert(type);

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
        changeFoodContentList[(int)type].SetMoveArrow();
    }

    public void OpenMoveIsland()
    {
        islandAlarm.SetActive(false);

        islandManager.OpenChangeIslandView();
    }
}
