using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureContent : MonoBehaviour
{
    public TreasureType treasureType = TreasureType.Treasure1;

    public Text titleText;
    public Image icon;
    public Text levelText;
    public Text infoText;
    public Text effectText;
    public Text upgradeText;

    public Sprite[] upgradeImgArray;

    public Image upgradeButtonImg;

    private int count = 0;
    private int need = 0;
    private int level = 0;
    private int maxLevel = 100;

    private float nowValue = 0;
    private float nextValue = 0;

    private int check = 0;
    private bool isDelay = false;

    private float treasure1Value = 0.2f;
    private float treasure2Value = 0.1f;
    private float treasure3Value = 0.1f;
    private float treasure4Value = 0.3f;
    private float treasure5Value = 1f;
    private float treasure6Value = 0.5f;

    Sprite[] treasureArray;

    TreasureManager treasureManager;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        treasureArray = imageDataBase.GetTreasureArray();
    }

    public void Initialize(TreasureType type, TreasureManager manager)
    {
        treasureType = type;
        treasureManager = manager;

        icon.sprite = treasureArray[(int)type];

        titleText.text = LocalizationManager.instance.GetString(type.ToString());

        check = 0;

        switch (treasureType)
        {
            case TreasureType.Treasure1:
                count = playerDataBase.Treasure1Count;
                level = playerDataBase.Treasure1;
                nowValue = treasure1Value * playerDataBase.Treasure1;
                need = (playerDataBase.Treasure1 / 10) + 1;

                if(playerDataBase.Treasure1 < maxLevel - 1)
                {
                    nextValue = treasure1Value * (playerDataBase.Treasure1 + 1);

                    if(count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = 0;

                    check = 2;
                }

                break;
            case TreasureType.Treasure2:
                count = playerDataBase.Treasure2Count;
                level = playerDataBase.Treasure2;
                nowValue = treasure2Value * playerDataBase.Treasure2;
                need = (playerDataBase.Treasure2 / 10) + 1;

                if (playerDataBase.Treasure2 < maxLevel - 1)
                {
                    nextValue = treasure2Value * (playerDataBase.Treasure2 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = 0;

                    check = 2;
                }

                break;
            case TreasureType.Treasure3:
                count = playerDataBase.Treasure3Count;
                level = playerDataBase.Treasure3;
                nowValue = treasure3Value * playerDataBase.Treasure3;
                need = (playerDataBase.Treasure3 / 10) + 1;

                if (playerDataBase.Treasure3 < maxLevel - 1)
                {
                    nextValue = treasure3Value * (playerDataBase.Treasure3 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = 0;

                    check = 2;
                }
                break;
            case TreasureType.Treasure4:
                count = playerDataBase.Treasure4Count;
                level = playerDataBase.Treasure4;
                nowValue = treasure4Value * playerDataBase.Treasure4;
                need = (playerDataBase.Treasure4 / 10) + 1;

                if (playerDataBase.Treasure4 < maxLevel - 1)
                {
                    nextValue = treasure4Value * (playerDataBase.Treasure4 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = 0;

                    check = 2;
                }
                break;
            case TreasureType.Treasure5:
                count = playerDataBase.Treasure5Count;
                level = playerDataBase.Treasure5;
                nowValue = treasure5Value * playerDataBase.Treasure5;
                need = (playerDataBase.Treasure5 / 10) + 1;

                if (playerDataBase.Treasure5 < maxLevel - 1)
                {
                    nextValue = treasure5Value * (playerDataBase.Treasure5 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = 0;

                    check = 2;
                }
                break;
            case TreasureType.Treasure6:
                count = playerDataBase.Treasure6Count;
                level = playerDataBase.Treasure6;
                nowValue = treasure6Value * playerDataBase.Treasure6;
                need = (playerDataBase.Treasure6 / 10) + 1;

                if (playerDataBase.Treasure6 < maxLevel - 1)
                {
                    nextValue = treasure6Value * (playerDataBase.Treasure6 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = 0;

                    check = 2;
                }
                break;
        }

        levelText.text = "Lv. " + level + " / " + maxLevel;

        infoText.text = LocalizationManager.instance.GetString(type.ToString() + "_Info");

        effectText.text = nowValue.ToString("N1") + "%  ¢º  " + nextValue.ToString("N1") + "%";

        upgradeText.text = LocalizationManager.instance.GetString("Upgrade") + "\n" + count + " / " + need;

        if (check == 1)
        {
            upgradeButtonImg.sprite = upgradeImgArray[1];
        }
        else
        {
            upgradeButtonImg.sprite = upgradeImgArray[0];
        }
    }

    public void UpgradeButton()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (isDelay) return;

        switch(check)
        {
            case 0:
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                return;
            case 1:
                break;
            case 2:
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.MaxLevel);
                return;
        }

        switch (treasureType)
        {
            case TreasureType.Treasure1:
                playerDataBase.Treasure1Count -= need;
                playerDataBase.Treasure1 += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure1Count", playerDataBase.Treasure1Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure1", playerDataBase.Treasure1);
                break;
            case TreasureType.Treasure2:
                playerDataBase.Treasure2Count -= need;
                playerDataBase.Treasure2 += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure2Count", playerDataBase.Treasure2Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure2", playerDataBase.Treasure2);
                break;
            case TreasureType.Treasure3:
                playerDataBase.Treasure3Count -= need;
                playerDataBase.Treasure3 += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure3Count", playerDataBase.Treasure3Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure3", playerDataBase.Treasure3);
                break;
            case TreasureType.Treasure4:
                playerDataBase.Treasure4Count -= need;
                playerDataBase.Treasure4 += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure4Count", playerDataBase.Treasure4Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure4", playerDataBase.Treasure4);
                break;
            case TreasureType.Treasure5:
                playerDataBase.Treasure5Count -= need;
                playerDataBase.Treasure5 += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure5Count", playerDataBase.Treasure5Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure5", playerDataBase.Treasure5);
                break;
            case TreasureType.Treasure6:
                playerDataBase.Treasure6Count -= need;
                playerDataBase.Treasure6 += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure6Count", playerDataBase.Treasure6Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure6", playerDataBase.Treasure6);
                break;
        }

        Initialize(treasureType, treasureManager);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

        isDelay = true;
        Invoke("Delay", 0.3f);
    }

    void Delay()
    {
        isDelay = false;
    }
}
