using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonContent : MonoBehaviour
{
    public DungeonType dungeonType = DungeonType.Dungeon1;

    public RewardType rewardType = RewardType.Gold;
    public RewardType rewardType2 = RewardType.AbilityPoint;
    public ItemType itemType = ItemType.DungeonKey1;

    public LocalizationContent titleText;
    public LocalizationContent infoText;

    public Image icon;

    public Image rewardImg1;
    public Image rewardImg2;
    
    public Image itemImg;
    public Text itemNumberText;

    public GameObject lockedObj;
    public Text lockedObjText;

    private int need = 0;

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
    }

    public void Initialize(DungeonManager manager, DungeonType type, RewardType type1, RewardType type2, ItemType item, int need)
    {
        dungeonManager = manager;

        dungeonType = type;
        rewardType = type1;
        rewardType2 = type2;
        itemType = item;

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                titleText.localizationName = "Dungeon1Title";
                infoText.localizationName = "Dungeon1_Info";

                itemNumberText.text = playerDataBase.DungeonKey1 + "/2";
                break;
            case DungeonType.Dungeon2:
                titleText.localizationName = "Dungeon2Title";
                infoText.localizationName = "Dungeon2_Info";

                itemNumberText.text = playerDataBase.DungeonKey2 + "/2";
                break;
            case DungeonType.Dungeon3:
                titleText.localizationName = "Dungeon3Title";
                infoText.localizationName = "Dungeon3_Info";

                itemNumberText.text = playerDataBase.DungeonKey3 + "/2";
                break;
            case DungeonType.Dungeon4:
                titleText.localizationName = "Dungeon4Title";
                infoText.localizationName = "Dungeon4_Info";

                itemNumberText.text = playerDataBase.DungeonKey4 + "/2";
                break;
        }

        titleText.ReLoad();
        infoText.ReLoad();

        icon.sprite = dungeonArray[(int)dungeonType];
        rewardImg1.sprite = rewardArray[(int)rewardType];
        rewardImg2.sprite = rewardArray[(int)rewardType2];
        itemImg.sprite = itemArray[(int)itemType];

        if(playerDataBase.GourmetLevel >= need)
        {
            lockedObj.SetActive(false);
        }
        else
        {
            lockedObj.SetActive(true);
            lockedObjText.text = MoneyUnitString.ToCurrencyString(need) + " ก่";
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
}
