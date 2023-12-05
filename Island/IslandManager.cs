using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    public GameObject changeIslandView;

    public GameObject alarmObj;

    public ChangeIslandContent changeIslandContent;

    public RectTransform changeIslandContentTransform;

    public List<ChangeIslandContent> changeIslandContentList = new List<ChangeIslandContent>();

    Sprite[] islandArray;

    public GameManager gameManager;
    public ChangeFoodManager changeFoodManager;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;
    UpgradeDataBase upgradeDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (upgradeDataBase == null) upgradeDataBase = Resources.Load("UpgradeDataBase") as UpgradeDataBase;

        islandArray = imageDataBase.GetIslandArray();

        changeIslandView.SetActive(false);

        alarmObj.SetActive(false);
    }

    private void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(IslandType)).Length; i++)
        {
            ChangeIslandContent monster = Instantiate(changeIslandContent);
            monster.transform.SetParent(changeIslandContentTransform);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;
            monster.Initialize(IslandType.Island1 + i, islandArray[i], this);
            monster.gameObject.SetActive(true);

            changeIslandContentList.Add(monster);
        }

        changeIslandContentTransform.anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenChangeIslandView()
    {
        if (!changeIslandView.activeInHierarchy)
        {
            changeIslandView.SetActive(true);

            Initialize();

            FirebaseAnalytics.LogEvent("OpenIsland");
        }
        else
        {
            changeIslandView.SetActive(false);

            alarmObj.SetActive(false);
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < changeIslandContentList.Count; i++)
        {
            changeIslandContentList[i].Locked();
            changeIslandContentList[i].UnSelected();
        }

        changeIslandContentList[(int)GameStateManager.instance.IslandType].Selected();

        changeIslandContentList[0].UnLock();
        changeIslandContentList[0].SetLevel(playerDataBase.NextFoodNumber / 6);

        if (playerDataBase.IslandNumber >= 1)
        {
            changeIslandContentList[1].UnLock();
            changeIslandContentList[1].SetLevel(playerDataBase.NextFoodNumber2 / 8);
        }

        if (playerDataBase.IslandNumber >= 2)
        {
            changeIslandContentList[2].UnLock();
            changeIslandContentList[2].SetLevel(playerDataBase.NextFoodNumber3 / 6);
        }

        if (playerDataBase.IslandNumber >= 3)
        {
            changeIslandContentList[3].UnLock();
            changeIslandContentList[3].SetLevel(playerDataBase.NextFoodNumber4 / 8);
        }
    }

    public void ChangeIsland(IslandType type)
    {
        if (GameStateManager.instance.IslandType == type) return;

        changeIslandView.SetActive(false);

        gameManager.ChangeIsland(type);

        if(changeFoodManager.changeFoodView.activeInHierarchy)
        {
            changeFoodManager.CheckFood();
        }
    }
}
