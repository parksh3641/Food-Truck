using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotionManager : MonoBehaviour
{
    public static NotionManager instance;

    [Title("MainNotion")]
    public Notion notion;


    public NotionColor[] notionColor;


    [System.Serializable]
    public class NotionColor
    {
        [Space]
        public NotionType notionType;
        public ColorType colorType;
        [Space]
        public EffectType effectType;
    }
    void Awake()
    {
        instance = this;

        notion.gameObject.SetActive(false);
    }

    private void Start()
    {
    }

    void SetColor(ColorType type)
    {
        switch (type)
        {
            case ColorType.White:
                notion.txt.color = new Color(1, 1, 1);
                break;
            case ColorType.Red:
                notion.txt.color = new Color(1, 0, 0);
                break;
            case ColorType.Orange:
                notion.txt.color = new Color(1, 150f / 255f, 0);
                break;
            case ColorType.Yellow:
                notion.txt.color = new Color(1, 1, 0);
                break;
            case ColorType.Green:
                notion.txt.color = new Color(0, 1, 0);
                break;
            case ColorType.SkyBlue:
                notion.txt.color = new Color(0, 1, 1);
                break;
            case ColorType.Blue:
                notion.txt.color = new Color(0, 150f / 255f, 1);
                break;
            case ColorType.Purple:
                notion.txt.color = new Color(1, 50f / 255f, 1);
                break;
            case ColorType.Pink:
                notion.txt.color = new Color(1, 150 / 255f, 1);
                break;
            case ColorType.Black:
                notion.txt.color = new Color(0, 0, 0);
                break;
        }
    }
    public void UseNotion(NotionType type)
    {
        notion.gameObject.SetActive(false);

        foreach(var list in notionColor)
        {
            if (list.notionType.Equals(type))
            {
                notion.txt.text = LocalizationManager.instance.GetString(list.notionType.ToString());
                SetColor(list.colorType);
                SetEffect(list.effectType);
            }
        }    

        notion.gameObject.SetActive(true);
    }

    public void UseNotion(string str)
    {
        notion.gameObject.SetActive(false);
        notion.txt.text = str;
        notion.gameObject.SetActive(true);
    }

    public void SetEffect(EffectType type)
    {
        switch (type)
        {
            case EffectType.Default:

                break;
            case EffectType.Vibration:
                notion.txt.gameObject.transform.DOPunchPosition(Vector3.left * 50, 0.5f);
                break;
        }
    }

    [Button]
    public void TestNotion()
    {
        UseNotion(NotionType.Test);
    }

}

public enum ColorType
{
    White = 0,
    Red,
    Orange,
    Yellow,
    Green,
    SkyBlue,
    Blue,
    Purple,
    Pink,
    Black
}

public enum EffectType
{
    Default = 0,
    Vibration
}

public enum NotionType
{
    Test,
    SignNotion1,
    SignNotion2,
    SignNotion3,
    SignNotion4,
    SignNotion5,
    SignNotion6,
    SuccessRemoveAds,
    NetworkConnectNotion,
    RestorePurchasesNotion,
    Game1_Info,
    Game2_Info,
    Game3_Info,
    LowCoin,
    LowCrystal,
    SuccessBuy,
    SuccessUpgrade,
    FailUpgrade,
    MaxLevel,
    SuccessSell,
    ChangeFoodNotion,
    ChangeTruckNotion,
    LowItemNotion,
    DefDestroyNotion,
    SuccessLanguage,
    SuccessWatchAd,
    SuccessReward,
    CancelPurchase,
    CancelWatchAd,
    FeverNotion,
    LowPortion,
    UsePortionNotion1,
    UsePortionNotion2,
    UsePortionNotion3,
    UsePortionNotion4,
    CouponNotion1,
    CouponNotion2,
    CouponNotion3,
    QuestNotion,
    ChangeAnimalNotion,
    NotPurchaseNotion,
    SuccessSellX2,
    UsePortionNotion5,
    ChangeCharacterNotion,
    ChangeButterflyNotion,
    ChangeIslandNotion,
    PortionInfo1,
    PortionInfo2,
    PortionInfo3,
    PortionInfo4,
    PortionInfo5,
    UseItem,
    PortionInfo6,
    UsePortionNotion6,
    SuccessUpgradeX2,
    FailGetItem,
    ChangeTotemsNotion,
    ChangeFlowerNotion,
    OpenChestBoxNotion,
    SuccessLink,
    FailLink,
    CouponNotion4
}