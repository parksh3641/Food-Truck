using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIslandContent : MonoBehaviour
{
    public IslandType islandType = IslandType.Island1;

    public Image icon;

    public LocalizationContent titleText;
    public Text titlePlusText;
    public Text upgradeText;
    public Text sellPriceText;

    public GameObject lockedObj;
    public GameObject selectedObj;

    public Image background;

    Color foodColor = new Color(206 / 255f, 141 / 255f, 1);
    Color candyColor = new Color(247 / 255f, 160 / 255f, 0);
    Color dessertColor = new Color(242 / 255f, 138 / 255f, 222 / 255f);
    Color appleColor = new Color(94 / 255f, 102 / 255f, 220 / 255f);

    IslandManager islandManager;


    public void Initialize(IslandType type, Sprite sp, IslandManager manager)
    {
        islandType = type;

        switch (type)
        {
            case IslandType.Island1:
                background.color = foodColor;
                break;
            case IslandType.Island2:
                background.color = candyColor;
                break;
            case IslandType.Island3:
                background.color = dessertColor;
                break;
            case IslandType.Island4:
                background.color = appleColor;
                break;
        }

        icon.sprite = sp;

        islandManager = manager;

        titleText.localizationName = type.ToString();
        titleText.plusText = " Lv.1";
        titleText.ReLoad();
    }

    public void SetLevel(float level)
    {
        titlePlusText.text = LocalizationManager.instance.GetString("Progress") + " : " + (level * 100).ToString("N1") +"%";

        switch (islandType)
        {
            case IslandType.Island1:
                upgradeText.text = LocalizationManager.instance.GetString("IslandUpgrade") + " : 0%";
                sellPriceText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " : +0%";

                break;
            case IslandType.Island2:
                upgradeText.text = LocalizationManager.instance.GetString("IslandUpgrade") + " : +3%";
                sellPriceText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " : +20%";

                break;
            case IslandType.Island3:
                upgradeText.text = LocalizationManager.instance.GetString("IslandUpgrade") + " : +5%";
                sellPriceText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " : +40%";

                break;
            case IslandType.Island4:
                upgradeText.text = LocalizationManager.instance.GetString("IslandUpgrade") + " : +7%";
                sellPriceText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " : +60%";

                break;
        }
    }

    public void Locked()
    {
        lockedObj.SetActive(true);

        SetLevel(0);
    }

    public void UnLock()
    {
        lockedObj.SetActive(false);
    }

    public void Selected()
    {
        selectedObj.SetActive(true);
    }

    public void UnSelected()
    {
        selectedObj.SetActive(false);
    }

    public void OnClick()
    {
        islandManager.ChangeIsland(islandType);
    }
}
