using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIslandContent : MonoBehaviour
{
    public IslandType islandType = IslandType.Island1;

    public Image icon;
    public Image levelUpIcon;

    public LocalizationContent titleText;
    public Text titlePlusText;
    public Text upgradeText;
    public Text sellPriceText;

    public Text needPriceText;

    public GameObject lockedObj;
    public GameObject selectedObj;

    public GameObject levelUpLockedObj;

    public Sprite[] buttonImgArray;

    public Image buttonImg;
    public Image LevelUpButtonImg;

    public Image background;

    private bool isDelay = false;

    private int value = 2;

    Color foodColor = new Color(206 / 255f, 141 / 255f, 1);
    Color candyColor = new Color(247 / 255f, 160 / 255f, 0);
    Color jpColor = new Color(94 / 255f, 102 / 255f, 220 / 255f);
    Color dessertColor = new Color(242 / 255f, 138 / 255f, 222 / 255f);

    IslandManager islandManager;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    public void Initialize(IslandType type, Sprite sp, IslandManager manager)
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        islandType = type;

        switch (islandType)
        {
            case IslandType.Island1:
                background.color = foodColor;
                break;
            case IslandType.Island2:
                background.color = candyColor;
                break;
            case IslandType.Island3:
                background.color = jpColor;
                break;
            case IslandType.Island4:
                background.color = dessertColor;
                break;
        }

        icon.sprite = sp;
        levelUpIcon.sprite = sp;

        islandManager = manager;
    }

    public void LevelInitialize()
    {
        titleText.localizationName = islandType.ToString();

        switch (islandType)
        {
            case IslandType.Island1:
                needPriceText.text = playerDataBase.Island1Count + "/" + ((playerDataBase.Island1Level + 1) * 100).ToString();
                titleText.plusText = "  Lv." + (playerDataBase.Island1Level + 1);

                upgradeText.text = LocalizationManager.instance.GetString("IslandUpgrade") + " : 0%";
                sellPriceText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " : +" + (playerDataBase.Island1Level * value) + "%";

                if (playerDataBase.Island1Count >= ((playerDataBase.Island1Level + 1) * 100))
                {
                    levelUpLockedObj.SetActive(false);
                }
                else
                {
                    levelUpLockedObj.SetActive(true);
                }
                break;
            case IslandType.Island2:
                needPriceText.text = playerDataBase.Island2Count + "/" + ((playerDataBase.Island2Level + 1) * 100).ToString();
                titleText.plusText = "  Lv." + (playerDataBase.Island2Level + 1);

                upgradeText.text = LocalizationManager.instance.GetString("IslandUpgrade") + " : +5%";
                sellPriceText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " : +" + (10 + (playerDataBase.Island2Level * value)) + "%";

                if (playerDataBase.Island2Count >= ((playerDataBase.Island2Level + 1) * 100))
                {
                    levelUpLockedObj.SetActive(false);
                }
                else
                {
                    levelUpLockedObj.SetActive(true);
                }
                break;
            case IslandType.Island3:
                needPriceText.text = playerDataBase.Island3Count + "/" + ((playerDataBase.Island3Level + 1) * 100).ToString();
                titleText.plusText = "  Lv." + (playerDataBase.Island3Level + 1);

                upgradeText.text = LocalizationManager.instance.GetString("IslandUpgrade") + " : +10%";
                sellPriceText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " : +" + (20 + (playerDataBase.Island3Level * value)) + "%";

                if (playerDataBase.Island3Count >= ((playerDataBase.Island3Level + 1) * 100))
                {
                    levelUpLockedObj.SetActive(false);
                }
                else
                {
                    levelUpLockedObj.SetActive(true);
                }
                break;
            case IslandType.Island4:
                needPriceText.text = playerDataBase.Island4Count + "/" + ((playerDataBase.Island4Level + 1) * 100).ToString();
                titleText.plusText = "  Lv." + (playerDataBase.Island4Level + 1);

                upgradeText.text = LocalizationManager.instance.GetString("IslandUpgrade") + " : +15%";
                sellPriceText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " : +" + (30 + (playerDataBase.Island4Level * value)) + "%";

                if (playerDataBase.Island4Count >= ((playerDataBase.Island4Level + 1) * 100))
                {
                    levelUpLockedObj.SetActive(false);
                }
                else
                {
                    levelUpLockedObj.SetActive(true);
                }
                break;
        }
        titleText.ReLoad();
    }

    public void SetLevel(float level)
    {
        titlePlusText.text = LocalizationManager.instance.GetString("Progress") + " : " + (level * 100).ToString("N1") +"%";
    }

    public void Locked()
    {
        lockedObj.SetActive(true);

        SetLevel(0);

        buttonImg.sprite = buttonImgArray[0];
    }

    public void UnLock()
    {
        lockedObj.SetActive(false);

        if(!selectedObj.activeInHierarchy)
        {
            buttonImg.sprite = buttonImgArray[1];
        }
    }

    public void Selected()
    {
        selectedObj.SetActive(true);

        buttonImg.sprite = buttonImgArray[0];
    }

    public void UnSelected()
    {
        selectedObj.SetActive(false);
    }

    public void OnClick()
    {
        islandManager.ChangeIsland(islandType);
    }

    public void LevelUp()
    {
        if (isDelay) return;

        islandManager.LevelUp(islandType);

        isDelay = true;
        Invoke("Delay", 0.4f);
    }

    void Delay()
    {
        isDelay = false;
    }
}
