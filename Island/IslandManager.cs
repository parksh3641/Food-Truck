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

    private float progress;

    public GameManager gameManager;

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
    }

    public void OpenChangeIslandView()
    {
        if (!changeIslandView.activeInHierarchy)
        {
            changeIslandView.SetActive(true);

            Initialize();
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
        changeIslandContentList[0].SetLevel(1);

        if (playerDataBase.IslandNumber >= 1)
        {
            changeIslandContentList[1].UnLock();

            progress = 0;

            if (playerDataBase.Candy1MaxValue > 0)
            {
                progress += 1;
            }

            if (playerDataBase.Candy2MaxValue > 0)
            {
                progress += 1;
            }

            if (playerDataBase.Candy3MaxValue > 0)
            {
                progress += 1;
            }

            if (playerDataBase.Candy4MaxValue > 0)
            {
                progress += 1;
            }

            if (playerDataBase.Candy5MaxValue > 0)
            {
                progress += 1;
            }

            if (playerDataBase.Candy6MaxValue > 0)
            {
                progress += 1;
            }

            if (playerDataBase.Candy7MaxValue > 0)
            {
                progress += 1;
            }

            if (playerDataBase.Candy8MaxValue > 0)
            {
                progress += 1;
            }

            if (playerDataBase.Candy9MaxValue > 0)
            {
                progress += 1;
            }

            changeIslandContentList[1].SetLevel(progress / 9);
        }
    }

    public void ChangeIsland(IslandType type)
    {
        if (GameStateManager.instance.IslandType == type) return;

        changeIslandView.SetActive(false);

        gameManager.ChangeIsland(type);
    }
}
