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
    public Text destroyText;

    public Text needPriceText;

    public GameObject lockedObj;
    public GameObject selectedObj;

    public GameObject levelUpLockedObj;

    public Sprite[] buttonImgArray;

    public Image buttonImg;
    public Image LevelUpButtonImg;

    public Image background;

    private bool isDelay = false;

    private float value = 0.5f;

    Color food1Color = new Color(209 / 255f, 243 / 255f, 224 / 255f);
    Color food2Color = new Color(254 / 255f, 185 / 255f, 200 / 255f);
    Color food3Color = new Color(246 / 255f, 167 / 255f, 186 / 255f);
    Color food4Color = new Color(245 / 255f, 250 / 255f, 241 / 255f);

    Color food5Color = new Color(228 / 255f, 23 / 255f, 73 / 255f);
    Color food6Color = new Color(245 / 255f, 88 / 255f, 123 / 255f);
    Color food7Color = new Color(255 / 255f, 138 / 255f, 92 / 255f);
    Color food8Color = new Color(254 / 255f, 245 / 255f, 145 / 255f);

    Color food9Color = new Color(190 / 255f, 238 / 255f, 246 / 255f);
    Color food10Color = new Color(111 / 255f, 194 / 255f, 208 / 255f);
    Color food11Color = new Color(55 / 255f, 58 / 255f, 109 / 255f);
    Color food12Color = new Color(255 / 255f, 130 / 255f, 70 / 255f);

    Color food13Color = new Color(178 / 255f, 6 / 255f, 176 / 255f);
    Color food14Color = new Color(228 / 255f, 23 / 255f, 73 / 255f);
    Color food15Color = new Color(69 / 255f, 146 / 255f, 175 / 255f);
    Color food16Color = new Color(226 / 255f, 196 / 255f, 168 / 255f);

    Color food17Color = new Color(240 / 255f, 245 / 255f, 159 / 255f);
    Color food18Color = new Color(176 / 255f, 224 / 255f, 168 / 255f);
    Color food19Color = new Color(216 / 255f, 239 / 255f, 240 / 255f);
    Color food20Color = new Color(191 / 255f, 204 / 255f, 126 / 255f);

    IslandManager islandManager;

    PlayerDataBase playerDataBase;
    IslandDataBase islandDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (islandDataBase == null) islandDataBase = Resources.Load("IslandDataBase") as IslandDataBase;
    }

    public void Initialize(IslandType type, Sprite sp, IslandManager manager)
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        islandType = type;

        switch (islandType)
        {
            case IslandType.Island1:
                background.color = food1Color;
                break;
            case IslandType.Island2:
                background.color = food2Color;
                break;
            case IslandType.Island3:
                background.color = food3Color;
                break;
            case IslandType.Island4:
                background.color = food4Color;
                break;
            case IslandType.Island5:
                background.color = food5Color;
                break;
            case IslandType.Island6:
                background.color = food6Color;
                break;
            case IslandType.Island7:
                background.color = food7Color;
                break;
            case IslandType.Island8:
                background.color = food8Color;
                break;
            case IslandType.Island9:
                background.color = food9Color;
                break;
            case IslandType.Island10:
                background.color = food10Color;
                break;
            case IslandType.Island11:
                background.color = food11Color;
                break;
            case IslandType.Island12:
                background.color = food12Color;
                break;
            case IslandType.Island13:
                background.color = food13Color;
                break;
            case IslandType.Island14:
                background.color = food14Color;
                break;
            case IslandType.Island15:
                background.color = food15Color;
                break;
            case IslandType.Island16:
                background.color = food16Color;
                break;
            case IslandType.Island17:
                background.color = food17Color;
                break;
            case IslandType.Island18:
                background.color = food18Color;
                break;
            case IslandType.Island19:
                background.color = food19Color;
                break;
            case IslandType.Island20:
                background.color = food20Color;
                break;
            case IslandType.Island21:
                background.color = food1Color;
                break;
            case IslandType.Island22:
                background.color = food2Color;
                break;
            case IslandType.Island23:
                background.color = food3Color;
                break;
            case IslandType.Island24:
                background.color = food4Color;
                break;
            case IslandType.Island25:
                background.color = food5Color;
                break;
            case IslandType.Island26:
                background.color = food6Color;
                break;
            case IslandType.Island27:
                background.color = food7Color;
                break;
            case IslandType.Island28:
                background.color = food8Color;
                break;
            case IslandType.Island29:
                background.color = food9Color;
                break;
            default:
                background.color = food1Color;
                break;
        }

        icon.sprite = sp;
        levelUpIcon.sprite = sp;

        islandManager = manager;
    }

    public void LevelInitialize()
    {
        titleText.localizationName = islandType.ToString();
        titleText.ReLoad();

        upgradeText.text = LocalizationManager.instance.GetString("SuccessPercent") + " : " + (100 - islandDataBase.GetSuccess(islandType)).ToString() + "%";
        sellPriceText.text = LocalizationManager.instance.GetString("IslandSellPrice") + "  +" + islandDataBase.GetSellPrice(islandType).ToString() + "% ";
        destroyText.text = LocalizationManager.instance.GetString("DestroyPercent") + " : " + islandDataBase.GetDestroy(islandType).ToString() + "%";
    }

    public void SetLevel(float level)
    {
        if(level >= 1)
        {
            titlePlusText.text = LocalizationManager.instance.GetString("Progress") + " : 100.0%";
        }
        else
        {
            titlePlusText.text = LocalizationManager.instance.GetString("Progress") + " : " + (level * 100).ToString("N1") + "%";
        }
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

    void Delay()
    {
        isDelay = false;
    }
}
