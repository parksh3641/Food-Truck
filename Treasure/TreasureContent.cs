using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureContent : MonoBehaviour
{
    public TreasureType treasureType = TreasureType.Treasure1;

    public Outline background;
    public Text titleText;
    public Image icon;
    public Text levelText;
    public Text infoText;
    public Text effectText;
    public Text upgradePercentText;

    public Text upgradeText;

    public Sprite[] upgradeImgArray;

    public Image upgradeButtonImg;

    public GameObject upgradeButton;

    private int count = 0;
    private int need = 0;
    private int level = 0;
    private int maxLevel = 100;

    private int needUp = 20;

    private int percent = 100;
    private bool success = false;

    private float nowValue = 0;
    private float nextValue = 0;

    private int check = 0;
    private bool isDelay = false;

    private float treasure1Value = 0.2f;
    private float treasure2Value = 0.1f;
    private float treasure3Value = 0.2f;
    private float treasure4Value = 0.3f;
    private float treasure5Value = 0.3f;
    private float treasure6Value = 0.6f;
    private float treasure7Value = 0.4f;
    private float treasure8Value = 0.6f;
    private float treasure9Value = 0.4f;
    private float treasure10Value = 1f;
    private float treasure11Value = 1f;
    private float treasure12Value = 0.3f;
    private float treasure13Value = 0.6f;
    private float treasure14Value = 0.3f;

    Sprite[] treasureArray;

    Color rareColor = new Color(61 / 255f, 208 / 255f, 1);
    Color epicColor = new Color(1, 124 / 255f, 1);
    Color legendaryColor = new Color(1, 1, 0);

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
        if (imageDataBase == null)
        {
            imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
            treasureArray = imageDataBase.GetTreasureArray();
        }

        treasureType = type;
        treasureManager = manager;

        icon.sprite = treasureArray[(int)type];

        titleText.text = LocalizationManager.instance.GetString(type.ToString());

        check = 0;

        if (!treasureManager.treasureView.activeInHierarchy)
        {
            return;
        }

        upgradeButton.SetActive(true);

        switch (treasureType)
        {
            case TreasureType.Treasure1:
                count = playerDataBase.Treasure1Count;
                level = playerDataBase.Treasure1;
                nowValue = treasure1Value * playerDataBase.Treasure1;
                need = (playerDataBase.Treasure1 / needUp) + 1;

                if (playerDataBase.Treasure1 < maxLevel - 1)
                {
                    nextValue = treasure1Value * (playerDataBase.Treasure1 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = treasure1Value * (playerDataBase.Treasure1);

                    check = 2;
                }

                background.effectColor = epicColor;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure2:
                count = playerDataBase.Treasure2Count;
                level = playerDataBase.Treasure2;
                nowValue = treasure2Value * playerDataBase.Treasure2;
                need = (playerDataBase.Treasure2 / needUp) + 1;

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
                    nextValue = treasure2Value * (playerDataBase.Treasure2);

                    check = 2;
                }

                background.effectColor = epicColor;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure3:
                count = playerDataBase.Treasure3Count;
                level = playerDataBase.Treasure3;
                nowValue = treasure3Value * playerDataBase.Treasure3;
                need = (playerDataBase.Treasure3 / needUp) + 1;

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
                    nextValue = treasure3Value * (playerDataBase.Treasure3);

                    check = 2;
                }

                background.effectColor = epicColor;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure4:
                count = playerDataBase.Treasure4Count;
                level = playerDataBase.Treasure4;
                nowValue = treasure4Value * playerDataBase.Treasure4;
                need = (playerDataBase.Treasure4 / needUp) + 1;

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
                    nextValue = treasure4Value * (playerDataBase.Treasure4);

                    check = 2;
                }

                background.effectColor = Color.black;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure5:
                count = playerDataBase.Treasure5Count;
                level = playerDataBase.Treasure5;
                nowValue = treasure5Value * playerDataBase.Treasure5;
                need = (playerDataBase.Treasure5 / needUp) + 1;

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
                    nextValue = treasure5Value * (playerDataBase.Treasure5);

                    check = 2;
                }

                background.effectColor = Color.black;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure6:
                count = playerDataBase.Treasure6Count;
                level = playerDataBase.Treasure6;
                nowValue = treasure6Value * playerDataBase.Treasure6;
                need = (playerDataBase.Treasure6 / needUp) + 1;

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
                    nextValue = treasure6Value * (playerDataBase.Treasure6);

                    check = 2;
                }

                background.effectColor = rareColor;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure7:
                count = playerDataBase.Treasure7Count;
                level = playerDataBase.Treasure7;
                nowValue = treasure7Value * playerDataBase.Treasure7;
                need = (playerDataBase.Treasure7 / needUp) + 1;

                if (playerDataBase.Treasure7 < maxLevel - 1)
                {
                    nextValue = treasure7Value * (playerDataBase.Treasure7 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = treasure7Value * (playerDataBase.Treasure7);

                    check = 2;
                }

                background.effectColor = epicColor;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure8:
                count = playerDataBase.Treasure8Count;
                level = playerDataBase.Treasure8;
                nowValue = treasure8Value * playerDataBase.Treasure8;
                need = (playerDataBase.Treasure8 / needUp) + 1;

                if (playerDataBase.Treasure8 < maxLevel - 1)
                {
                    nextValue = treasure8Value * (playerDataBase.Treasure8 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = treasure8Value * (playerDataBase.Treasure8);

                    check = 2;
                }

                background.effectColor = rareColor;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure9:
                count = playerDataBase.Treasure9Count;
                level = playerDataBase.Treasure9;
                nowValue = treasure9Value * playerDataBase.Treasure9;
                need = (playerDataBase.Treasure9 / needUp) + 1;

                if (playerDataBase.Treasure9 < maxLevel - 1)
                {
                    nextValue = treasure9Value * (playerDataBase.Treasure9 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = treasure9Value * (playerDataBase.Treasure9);

                    check = 2;
                }

                background.effectColor = rareColor;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure10:
                count = playerDataBase.Treasure10Count;
                level = playerDataBase.Treasure10;
                nowValue = treasure10Value * playerDataBase.Treasure10;
                need = (playerDataBase.Treasure10 / needUp) + 1;

                if (playerDataBase.Treasure10 < maxLevel - 1)
                {
                    nextValue = treasure10Value * (playerDataBase.Treasure10 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = treasure10Value * (playerDataBase.Treasure10);

                    check = 2;
                }

                background.effectColor = rareColor;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure11:
                count = playerDataBase.Treasure11Count;
                level = playerDataBase.Treasure11;
                nowValue = treasure11Value * playerDataBase.Treasure11;
                need = (playerDataBase.Treasure11 / needUp) + 1;

                if (playerDataBase.Treasure11 < maxLevel - 1)
                {
                    nextValue = treasure11Value * (playerDataBase.Treasure11 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = treasure11Value * (playerDataBase.Treasure11);

                    check = 2;
                }

                background.effectColor = Color.black;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure12:
                count = playerDataBase.Treasure12Count;
                level = playerDataBase.Treasure12;
                nowValue = treasure12Value * playerDataBase.Treasure12;
                need = (playerDataBase.Treasure12 / needUp) + 1;

                if (playerDataBase.Treasure12 < maxLevel - 1)
                {
                    nextValue = treasure12Value * (playerDataBase.Treasure12 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = treasure12Value * (playerDataBase.Treasure12);

                    check = 2;
                }

                background.effectColor = Color.black;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }

                break;
            case TreasureType.Treasure13:
                count = playerDataBase.Treasure13Count;
                level = playerDataBase.Treasure13;
                nowValue = treasure13Value * playerDataBase.Treasure13;
                need = (playerDataBase.Treasure13 / needUp) + 1;

                if (playerDataBase.Treasure13 < maxLevel - 1)
                {
                    nextValue = treasure13Value * (playerDataBase.Treasure13 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = treasure13Value * (playerDataBase.Treasure13);

                    check = 2;
                }

                background.effectColor = legendaryColor;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }
                break;
            case TreasureType.Treasure14:
                count = playerDataBase.Treasure14Count;
                level = playerDataBase.Treasure14;
                nowValue = treasure14Value * playerDataBase.Treasure14;
                need = (playerDataBase.Treasure14 / needUp) + 1;

                if (playerDataBase.Treasure14 < maxLevel - 1)
                {
                    nextValue = treasure14Value * (playerDataBase.Treasure14 + 1);

                    if (count >= need)
                    {
                        check = 1;
                    }
                }
                else
                {
                    nextValue = treasure14Value * (playerDataBase.Treasure14);

                    check = 2;
                }

                background.effectColor = legendaryColor;

                if (level > 99)
                {
                    upgradeButton.SetActive(false);
                }
                break;
        }

        percent = 100 - ((level / 10) * 5);

        if(percent < 10)
        {
            percent = 10;
        }

        levelText.text = "Lv. " + level + " / " + maxLevel;

        infoText.text = LocalizationManager.instance.GetString(type.ToString() + "_Effect");

        if(treasureType == TreasureType.Treasure3)
        {
            effectText.text = nowValue.ToString("N1") + "%  ▶  " + nextValue.ToString("N1") + "%";
        }
        else
        {
            effectText.text = nowValue.ToString("N1") + "%  ▶  " + nextValue.ToString("N1") + "%";
        }

        upgradePercentText.text = LocalizationManager.instance.GetString("SuccessPercent") + " : " + percent.ToString() + "%";

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

        if (percent >= Random.Range(0, 100f))
        {
            success = true;

            SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
            NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);
        }
        else
        {
            success = false;

            SoundManager.instance.PlaySFX(GameSfxType.UpgradeFail);
            NotionManager.instance.UseNotion(NotionType.FailUpgrade);

            if (GameStateManager.instance.Vibration)
            {
                Handheld.Vibrate();
            }
        }

        switch (treasureType)
        {
            case TreasureType.Treasure1:
                playerDataBase.Treasure1Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure1Count", playerDataBase.Treasure1Count);

                if (success)
                {
                    playerDataBase.Treasure1 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure1", playerDataBase.Treasure1);
                }
                break;
            case TreasureType.Treasure2:
                playerDataBase.Treasure2Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure2Count", playerDataBase.Treasure2Count);

                if (success)
                {
                    playerDataBase.Treasure2 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure2", playerDataBase.Treasure2);
                }
                break;
            case TreasureType.Treasure3:
                playerDataBase.Treasure3Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure3Count", playerDataBase.Treasure3Count);

                if (success)
                {
                    playerDataBase.Treasure3 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure3", playerDataBase.Treasure3);
                }
                break;
            case TreasureType.Treasure4:
                playerDataBase.Treasure4Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure4Count", playerDataBase.Treasure4Count);

                if (success)
                {
                    playerDataBase.Treasure4 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure4", playerDataBase.Treasure4);
                }
                break;
            case TreasureType.Treasure5:
                playerDataBase.Treasure5Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure5Count", playerDataBase.Treasure5Count);

                if (success)
                {
                    playerDataBase.Treasure5 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure5", playerDataBase.Treasure5);
                }
                break;
            case TreasureType.Treasure6:
                playerDataBase.Treasure6Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure6Count", playerDataBase.Treasure6Count);

                if (success)
                {
                    playerDataBase.Treasure6 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure6", playerDataBase.Treasure6);
                }
                break;
            case TreasureType.Treasure7:
                playerDataBase.Treasure7Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure7Count", playerDataBase.Treasure7Count);

                if (success)
                {
                    playerDataBase.Treasure7 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure7", playerDataBase.Treasure7);
                }
                break;
            case TreasureType.Treasure8:
                playerDataBase.Treasure8Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure8Count", playerDataBase.Treasure8Count);

                if (success)
                {
                    playerDataBase.Treasure8 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure8", playerDataBase.Treasure8);
                }
                break;
            case TreasureType.Treasure9:
                playerDataBase.Treasure9Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure9Count", playerDataBase.Treasure9Count);

                if (success)
                {
                    playerDataBase.Treasure9 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure9", playerDataBase.Treasure9);
                }
                break;
            case TreasureType.Treasure10:
                playerDataBase.Treasure10Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure10Count", playerDataBase.Treasure10Count);

                if (success)
                {
                    playerDataBase.Treasure10 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure10", playerDataBase.Treasure10);
                }
                break;
            case TreasureType.Treasure11:
                playerDataBase.Treasure11Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure11Count", playerDataBase.Treasure11Count);

                if (success)
                {
                    playerDataBase.Treasure11 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure11", playerDataBase.Treasure11);
                }
                break;
            case TreasureType.Treasure12:
                playerDataBase.Treasure12Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure12Count", playerDataBase.Treasure12Count);

                if (success)
                {
                    playerDataBase.Treasure12 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure12", playerDataBase.Treasure12);
                }
                break;
            case TreasureType.Treasure13:
                playerDataBase.Treasure13Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure13Count", playerDataBase.Treasure13Count);

                if (success)
                {
                    playerDataBase.Treasure13 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure13", playerDataBase.Treasure13);
                }
                break;
            case TreasureType.Treasure14:
                playerDataBase.Treasure14Count -= need;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure14Count", playerDataBase.Treasure14Count);

                if (success)
                {
                    playerDataBase.Treasure14 += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure14", playerDataBase.Treasure14);
                }
                break;
        }

        Initialize(treasureType, treasureManager);

        GourmetManager.instance.Initialize();

        isDelay = true;
        Invoke("Delay", 0.4f);
    }

    void Delay()
    {
        isDelay = false;
    }
}
