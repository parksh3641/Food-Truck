using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFoodManager : MonoBehaviour
{
    public GameObject changeFoodView;

    public ChangeFoodContent changeFoodContent;

    public RectTransform changeFoodContentTransform;

    public List<ChangeFoodContent> changeFoodContentList = new List<ChangeFoodContent>();

    Sprite[] foodChangeArray;

    public GameManager gameManager;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        foodChangeArray = imageDataBase.GetFoodChangeArray();

        changeFoodView.SetActive(false);
    }

    private void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(FoodType)).Length; i++)
        {
            ChangeFoodContent monster = Instantiate(changeFoodContent);
            monster.transform.parent = changeFoodContentTransform;
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;
            monster.Initialize(FoodType.Hamburger + i, foodChangeArray[i], this);
            monster.gameObject.SetActive(true);

            changeFoodContentList.Add(monster);
        }
    }

    public void OpenChangeFoodView()
    {
        if(!changeFoodView.activeInHierarchy)
        {
            changeFoodView.SetActive(true);

            Initialize();
        }
        else
        {
            changeFoodView.SetActive(false);
        }
    }

    public void Initialize()
    {
        for(int i = 0; i < changeFoodContentList.Count; i ++)
        {
            changeFoodContentList[i].Locked();
        }

        changeFoodContentList[0].SetLevel(GameStateManager.instance.HamburgerLevel);
        changeFoodContentList[1].SetLevel(GameStateManager.instance.SandwichLevel);
        changeFoodContentList[2].SetLevel(GameStateManager.instance.SnackLabLevel);
        changeFoodContentList[3].SetLevel(GameStateManager.instance.DrinkLevel);
        changeFoodContentList[4].SetLevel(GameStateManager.instance.PizzaLevel);
        changeFoodContentList[5].SetLevel(GameStateManager.instance.DonutLevel);



        changeFoodContentList[0].UnLock();

        if(playerDataBase.HamburgerMaxValue >= 1)
        {
            changeFoodContentList[1].UnLock();
        }

        if (playerDataBase.SandwichMaxValue >= 1)
        {
            changeFoodContentList[2].UnLock();

            if (!GameStateManager.instance.AppReview)
            {
                gameManager.OpenAppReview();

                GameStateManager.instance.AppReview = true;
            }
        }

        if (playerDataBase.SnackLabMaxValue >= 1)
        {
            changeFoodContentList[3].UnLock();
        }

        if (playerDataBase.DrinkMaxValue >= 1)
        {
            changeFoodContentList[4].UnLock();
        }

        if (playerDataBase.PizzaMaxValue >= 1)
        {
            changeFoodContentList[5].UnLock();
        }

        //if (playerDataBase.DonutMaxValue >= 1)
        //{
        //    changeFoodContentList[6].UnLock();
        //}
    }

    public void ChangeFood(FoodType type)
    {
        changeFoodView.SetActive(false);

        gameManager.ChangeFood(type);

        NotionManager.instance.UseNotion(NotionType.ChangeFoodNotion);
    }

}
