using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonContent : MonoBehaviour
{
    public DungeonType dungeonType = DungeonType.Dungeon1;

    public ItemType itemType = ItemType.DungeonKey1;

    public LocalizationContent titleText;
    public LocalizationContent infoText;

    public Image icon;

    public ReceiveContent[] receiveContents;

    public Image itemImg;
    public Text itemNumberText;

    public GameObject lockedObj;
    public Text lockedObjText;

    public GameObject watchAdObj;

    private int plusNumber1 = 0;
    private int plusNumber2 = 0;

    Sprite[] rewardArray;
    Sprite[] itemArray;
    Sprite[] dungeonArray;

    DungeonManager dungeonManager;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        itemArray = imageDataBase.GetItemArray();
        rewardArray = imageDataBase.GetRewardArray();
        dungeonArray = imageDataBase.GetDungeonArray();

        watchAdObj.SetActive(false);
    }

    public void Initialize(DungeonManager manager, DungeonType type, DungeonInfo info, ItemType item, int need)
    {
        dungeonManager = manager;

        dungeonType = type;
        itemType = item;

        watchAdObj.SetActive(false);

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                titleText.localizationName = "Dungeon1Title";
                titleText.plusText = "  Lv." + (playerDataBase.Dungeon1Level + 1);
                infoText.localizationName = "Dungeon1_Info";

                itemNumberText.text = playerDataBase.DungeonKey1 + "/2";

                plusNumber1 = info.rewardInfos[0].addNumber * playerDataBase.Dungeon1Level;
                plusNumber2 = info.rewardInfos[1].addNumber * playerDataBase.Dungeon1Level;

                if(playerDataBase.DungeonKey1 == 0 && playerDataBase.resetInfo.dailyDungeonKey1 == 0)
                {
                    watchAdObj.SetActive(true);
                }
                break;
            case DungeonType.Dungeon2:
                titleText.localizationName = "Dungeon2Title";
                titleText.plusText = "  Lv." + (playerDataBase.Dungeon2Level + 1);
                infoText.localizationName = "Dungeon2_Info";

                itemNumberText.text = playerDataBase.DungeonKey2 + "/2";

                plusNumber1 = info.rewardInfos[0].addNumber * playerDataBase.Dungeon2Level;
                plusNumber2 = info.rewardInfos[1].addNumber * playerDataBase.Dungeon2Level;

                if (playerDataBase.DungeonKey2 == 0 && playerDataBase.resetInfo.dailyDungeonKey2 == 0)
                {
                    watchAdObj.SetActive(true);
                }
                break;
            case DungeonType.Dungeon3:
                titleText.localizationName = "Dungeon3Title";
                titleText.plusText = "  Lv." + (playerDataBase.Dungeon3Level + 1);
                infoText.localizationName = "Dungeon3_Info";

                itemNumberText.text = playerDataBase.DungeonKey3 + "/2";

                plusNumber1 = info.rewardInfos[0].addNumber * playerDataBase.Dungeon3Level;
                plusNumber2 = info.rewardInfos[1].addNumber * playerDataBase.Dungeon3Level;

                if (playerDataBase.DungeonKey3 == 0 && playerDataBase.resetInfo.dailyDungeonKey3 == 0)
                {
                    watchAdObj.SetActive(true);
                }
                break;
            case DungeonType.Dungeon4:
                titleText.localizationName = "Dungeon4Title";
                titleText.plusText = "  Lv." + (playerDataBase.Dungeon4Level + 1);
                infoText.localizationName = "Dungeon4_Info";

                itemNumberText.text = playerDataBase.DungeonKey4 + "/2";

                plusNumber1 = info.rewardInfos[0].addNumber * playerDataBase.Dungeon4Level;
                plusNumber2 = info.rewardInfos[1].addNumber * playerDataBase.Dungeon4Level;

                if (playerDataBase.DungeonKey4 == 0 && playerDataBase.resetInfo.dailyDungeonKey4 == 0)
                {
                    watchAdObj.SetActive(true);
                }
                break;
        }

        titleText.ReLoad();
        infoText.ReLoad();

        icon.sprite = dungeonArray[(int)dungeonType];

        for(int i = 0; i < info.rewardInfos.Count; i ++)
        {
            switch(i)
            {
                case 0:
                    receiveContents[i].Initialize(info.rewardInfos[i].rewardType, info.rewardInfos[i].number + plusNumber1);
                    break;
                case 1:
                    receiveContents[i].Initialize(info.rewardInfos[i].rewardType, info.rewardInfos[i].number + plusNumber2);
                    break;
            }
        }

        itemImg.sprite = itemArray[(int)itemType];

        if(playerDataBase.GourmetLevel >= need)
        {
            lockedObj.SetActive(false);
        }
        else
        {
            lockedObj.SetActive(true);
            lockedObjText.text = MoneyUnitString.ToCurrencyString(need);
        }
    }

    public void EnterDungeon()
    {
        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                if(playerDataBase.DungeonKey1 <= 0)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                    return;
                }
                break;
            case DungeonType.Dungeon2:
                if (playerDataBase.DungeonKey2 <= 0)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                    return;
                }
                break;
            case DungeonType.Dungeon3:
                if (playerDataBase.DungeonKey3 <= 0)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                    return;
                }
                break;
            case DungeonType.Dungeon4:
                if (playerDataBase.DungeonKey4 <= 0)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                    return;
                }
                break;
        }

        dungeonManager.EnterDungeon(dungeonType);
    }

    public void WatchAd()
    {
        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                if (playerDataBase.resetInfo.dailyDungeonKey1 == 1) return;

                GoogleAdsManager.instance.admobReward_Dungeon.ShowAd(9);
                break;
            case DungeonType.Dungeon2:
                if (playerDataBase.resetInfo.dailyDungeonKey2 == 1) return;

                GoogleAdsManager.instance.admobReward_Dungeon.ShowAd(10);
                break;
            case DungeonType.Dungeon3:
                if (playerDataBase.resetInfo.dailyDungeonKey3 == 1) return;

                GoogleAdsManager.instance.admobReward_Dungeon.ShowAd(11);
                break;
            case DungeonType.Dungeon4:
                if (playerDataBase.resetInfo.dailyDungeonKey4 == 1) return;

                GoogleAdsManager.instance.admobReward_Dungeon.ShowAd(12);
                break;
        }
    }
}
