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
        }

        icon.sprite = sp;

        islandManager = manager;

        titleText.localizationName = type.ToString();
        titleText.ReLoad();
    }

    public void SetLevel(float level)
    {
        titlePlusText.text = LocalizationManager.instance.GetString("Progress") + " : " + (level * 100).ToString("N1") +"%";

        switch (islandType)
        {
            case IslandType.Island1:
                upgradeText.text = LocalizationManager.instance.GetString("IslandUpgrade") + " : 100%";
                sellPriceText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " : +0%";

                break;
            case IslandType.Island2:
                upgradeText.text = LocalizationManager.instance.GetString("IslandUpgrade") + " : 95%";
                sellPriceText.text = LocalizationManager.instance.GetString("IslandSellPrice") + " : +10%";

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
