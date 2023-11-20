using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFoodManager : MonoBehaviour
{
    public GameObject changeFoodView;

    public GameObject alarmObj;

    public ChangeFoodContent changeFoodContent;

    public RectTransform changeFoodContentTransform;

    public List<ChangeFoodContent> changeFoodContentList = new List<ChangeFoodContent>();
    public List<ChangeFoodContent> changeCandyList = new List<ChangeFoodContent>();

    Sprite[] foodChangeArray;
    Sprite[] candyArray;

    public GameManager gameManager;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;
    UpgradeDataBase upgradeDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (upgradeDataBase == null) upgradeDataBase = Resources.Load("UpgradeDataBase") as UpgradeDataBase;

        foodChangeArray = imageDataBase.GetFoodChangeArray();
        candyArray = imageDataBase.GetCandyArray();

        changeFoodView.SetActive(false);

        alarmObj.SetActive(false);
    }

    private void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(FoodType)).Length; i++)
        {
            ChangeFoodContent monster = Instantiate(changeFoodContent);
            monster.transform.SetParent(changeFoodContentTransform);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;
            monster.InitializeFood(FoodType.Hamburger + i, foodChangeArray[i], this);
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

        changeFoodContentTransform.anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenChangeFoodView()
    {
        if(!changeFoodView.activeInHierarchy)
        {
            changeFoodView.SetActive(true);

            alarmObj.SetActive(false);

            Initialize();

            GameStateManager.instance.Pause = true;
        }
        else
        {
            changeFoodView.SetActive(false);

            GameStateManager.instance.Pause = false;
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < changeFoodContentList.Count; i++)
        {
            changeFoodContentList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < changeCandyList.Count; i++)
        {
            changeCandyList[i].gameObject.SetActive(false);
        }

        if (GameStateManager.instance.GameType == GameType.Rank)
        {
            changeFoodContentList[7].gameObject.SetActive(true);
            changeCandyList[9].gameObject.SetActive(true);

            switch (GameStateManager.instance.IslandType)
            {
                case IslandType.Island1:
                    changeFoodContentList[7].Selected();
                    changeCandyList[9].UnSelected();
                    break;
                case IslandType.Island2:
                    changeFoodContentList[7].UnSelected();
                    changeCandyList[9].Selected();
                    break;
                case IslandType.Island3:
                    break;
                case IslandType.Island4:
                    break;
            }

            changeFoodContentList[7].UnLock();
            changeFoodContentList[7].SetLevel(GameStateManager.instance.RibsLevel, upgradeDataBase.GetMaxLevel(FoodType.Ribs));

            changeCandyList[9].SetLevel(GameStateManager.instance.ChocolateLevel, upgradeDataBase.GetMaxLevelCandy(CandyType.Chocolate));

            if (playerDataBase.IslandNumber > 0 || playerDataBase.RankLevel2 > 0)
            {
                changeCandyList[9].UnLock();
            }
            else
            {
                changeCandyList[9].Locked();
            }
            return;
        }


        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                for (int i = 0; i < changeFoodContentList.Count; i++)
                {
                    changeFoodContentList[i].gameObject.SetActive(true);
                    changeFoodContentList[i].Locked();
                    changeFoodContentList[i].UnSelected();
                }

                changeFoodContentList[(int)GameStateManager.instance.FoodType].Selected();

                changeFoodContentList[0].SetLevel(GameStateManager.instance.HamburgerLevel, upgradeDataBase.GetMaxLevel(FoodType.Hamburger));
                changeFoodContentList[1].SetLevel(GameStateManager.instance.SandwichLevel, upgradeDataBase.GetMaxLevel(FoodType.Sandwich));
                changeFoodContentList[2].SetLevel(GameStateManager.instance.SnackLabLevel, upgradeDataBase.GetMaxLevel(FoodType.SnackLab));
                changeFoodContentList[3].SetLevel(GameStateManager.instance.DrinkLevel, upgradeDataBase.GetMaxLevel(FoodType.Drink));
                changeFoodContentList[4].SetLevel(GameStateManager.instance.PizzaLevel, upgradeDataBase.GetMaxLevel(FoodType.Pizza));
                changeFoodContentList[5].SetLevel(GameStateManager.instance.DonutLevel, upgradeDataBase.GetMaxLevel(FoodType.Donut));
                changeFoodContentList[6].SetLevel(GameStateManager.instance.FriesLevel, upgradeDataBase.GetMaxLevel(FoodType.Fries));
                changeFoodContentList[7].gameObject.SetActive(false);

                changeFoodContentList[0].UnLock();

                if (playerDataBase.HamburgerMaxValue >= 1)
                {
                    changeFoodContentList[1].UnLock();
                }

                if (playerDataBase.SandwichMaxValue >= 1)
                {
                    changeFoodContentList[1].UnLock();
                    changeFoodContentList[2].UnLock();
                }

                if (playerDataBase.SnackLabMaxValue >= 1)
                {
                    changeFoodContentList[1].UnLock();
                    changeFoodContentList[2].UnLock();
                    changeFoodContentList[3].UnLock();
                }

                if (playerDataBase.DrinkMaxValue >= 1)
                {
                    changeFoodContentList[1].UnLock();
                    changeFoodContentList[2].UnLock();
                    changeFoodContentList[3].UnLock();
                    changeFoodContentList[4].UnLock();
                }

                if (playerDataBase.PizzaMaxValue >= 1)
                {
                    changeFoodContentList[1].UnLock();
                    changeFoodContentList[2].UnLock();
                    changeFoodContentList[3].UnLock();
                    changeFoodContentList[4].UnLock();
                    changeFoodContentList[5].UnLock();

                    if (!GameStateManager.instance.AppReview)
                    {
                        gameManager.OpenAppReview();

                        GameStateManager.instance.AppReview = true;
                    }
                }

                if (playerDataBase.DonutMaxValue >= 1)
                {
                    changeFoodContentList[1].UnLock();
                    changeFoodContentList[2].UnLock();
                    changeFoodContentList[3].UnLock();
                    changeFoodContentList[4].UnLock();
                    changeFoodContentList[5].UnLock();
                    changeFoodContentList[6].UnLock();
                }

                break;
            case IslandType.Island2:
                for (int i = 0; i < changeCandyList.Count; i++)
                {
                    changeCandyList[i].gameObject.SetActive(true);
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

                if (playerDataBase.Candy1MaxValue >= 1)
                {
                    changeCandyList[1].UnLock();
                }

                if (playerDataBase.Candy2MaxValue >= 1)
                {
                    changeCandyList[1].UnLock();
                    changeCandyList[2].UnLock();
                }

                if (playerDataBase.Candy3MaxValue >= 1)
                {
                    changeCandyList[1].UnLock();
                    changeCandyList[2].UnLock();
                    changeCandyList[3].UnLock();
                }

                if (playerDataBase.Candy4MaxValue >= 1)
                {
                    changeCandyList[1].UnLock();
                    changeCandyList[2].UnLock();
                    changeCandyList[3].UnLock();
                    changeCandyList[4].UnLock();
                }

                if (playerDataBase.Candy5MaxValue >= 1)
                {
                    changeCandyList[1].UnLock();
                    changeCandyList[2].UnLock();
                    changeCandyList[3].UnLock();
                    changeCandyList[4].UnLock();
                    changeCandyList[5].UnLock();
                }

                if (playerDataBase.Candy6MaxValue >= 1)
                {
                    changeCandyList[1].UnLock();
                    changeCandyList[2].UnLock();
                    changeCandyList[3].UnLock();
                    changeCandyList[4].UnLock();
                    changeCandyList[5].UnLock();
                    changeCandyList[6].UnLock();
                }

                if (playerDataBase.Candy7MaxValue >= 1)
                {
                    changeCandyList[1].UnLock();
                    changeCandyList[2].UnLock();
                    changeCandyList[3].UnLock();
                    changeCandyList[4].UnLock();
                    changeCandyList[5].UnLock();
                    changeCandyList[6].UnLock();
                    changeCandyList[7].UnLock();
                }

                if (playerDataBase.Candy8MaxValue >= 1)
                {
                    changeCandyList[1].UnLock();
                    changeCandyList[2].UnLock();
                    changeCandyList[3].UnLock();
                    changeCandyList[4].UnLock();
                    changeCandyList[5].UnLock();
                    changeCandyList[6].UnLock();
                    changeCandyList[7].UnLock();
                    changeCandyList[8].UnLock();
                }

                break;
        }
    }

    public void ChangeFood(FoodType type)
    {
        if (GameStateManager.instance.FoodType == type) return;

        changeFoodView.SetActive(false);

        gameManager.ChangeFood(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }

    public void ChangeRankFood(FoodType type)
    {
        if (GameStateManager.instance.IslandType == IslandType.Island1) return;

        GameStateManager.instance.IslandType = IslandType.Island1;

        changeFoodView.SetActive(false);

        gameManager.ChangeFood(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }

    public void ChangeCandy(CandyType type)
    {
        if (GameStateManager.instance.CandyType == type) return;

        changeFoodView.SetActive(false);

        gameManager.ChangeCandy(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }

    public void ChangeRankCandy(CandyType type)
    {
        if (GameStateManager.instance.IslandType == IslandType.Island2) return;

        GameStateManager.instance.IslandType = IslandType.Island2;

        changeFoodView.SetActive(false);

        gameManager.ChangeCandy(type);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }
}
