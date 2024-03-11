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

    public Notion notion2; //선물 등장
    public Notion notion3; //레벨 업, 미식 점수
    public Notion notion4; //파괴 방어


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
        notion2.gameObject.SetActive(false);
        notion3.gameObject.SetActive(false);
        notion4.gameObject.SetActive(false);
    }

    public void UseNotion(NotionType type)
    {
        notion.gameObject.SetActive(false);

        foreach(var list in notionColor)
        {
            if (list.notionType.Equals(type))
            {
                notion.txt.text = LocalizationManager.instance.GetString(list.notionType.ToString());
                SetColor(list.colorType, notion.txt);
                SetEffect(list.effectType, notion.txt);
            }
        }    

        notion.gameObject.SetActive(true);
    }

    public void UseNotion3(Color color, string txt)
    {
        notion3.gameObject.SetActive(false);

        notion3.txt.text = txt;
        notion3.txt.color = color;

        notion3.gameObject.SetActive(true);
    }

    public void UseNotion2(NotionType type)
    {
        notion2.gameObject.SetActive(false);

        foreach (var list in notionColor)
        {
            if (list.notionType.Equals(type))
            {
                notion2.txt.text = LocalizationManager.instance.GetString(list.notionType.ToString());
                SetColor(list.colorType, notion2.txt);
                SetEffect(list.effectType, notion2.txt);
            }
        }

        notion2.gameObject.SetActive(true);
    }

    public void UseNotion3(NotionType type)
    {
        notion3.gameObject.SetActive(false);

        foreach (var list in notionColor)
        {
            if (list.notionType.Equals(type))
            {
                notion3.txt.text = LocalizationManager.instance.GetString(list.notionType.ToString());
                SetColor(list.colorType, notion3.txt);
                SetEffect(list.effectType, notion3.txt);
            }
        }

        notion3.gameObject.SetActive(true);
    }

    public void UseNotion4(NotionType type)
    {
        notion4.gameObject.SetActive(false);

        foreach (var list in notionColor)
        {
            if (list.notionType.Equals(type))
            {
                notion4.txt.text = LocalizationManager.instance.GetString(list.notionType.ToString());
                SetColor(list.colorType, notion4.txt);
                SetEffect(list.effectType, notion4.txt);
            }
        }

        notion4.gameObject.SetActive(true);
    }

    public void SetEffect(EffectType type, Text txt)
    {
        switch (type)
        {
            case EffectType.Default:

                break;
            case EffectType.Vibration:
                txt.gameObject.transform.DOPunchPosition(Vector3.left * 50, 0.5f);
                break;
        }
    }

    void SetColor(ColorType type, Text txt)
    {
        switch (type)
        {
            case ColorType.White:
                txt.color = new Color(1, 1, 1);
                break;
            case ColorType.Red:
                txt.color = new Color(1, 0, 0);
                break;
            case ColorType.Orange:
                txt.color = new Color(1, 150f / 255f, 0);
                break;
            case ColorType.Yellow:
                txt.color = new Color(1, 1, 0);
                break;
            case ColorType.Green:
                txt.color = new Color(0, 1, 0);
                break;
            case ColorType.SkyBlue:
                txt.color = new Color(0, 1, 1);
                break;
            case ColorType.Blue:
                txt.color = new Color(0, 150f / 255f, 1);
                break;
            case ColorType.Purple:
                txt.color = new Color(1, 50f / 255f, 1);
                break;
            case ColorType.Pink:
                txt.color = new Color(1, 150 / 255f, 1);
                break;
            case ColorType.Black:
                txt.color = new Color(0, 0, 0);
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
    Portion1_Info,
    Portion2_Info,
    Portion3_Info,
    Portion4_Info,
    Portion5_Info,
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
    CouponNotion4,
    TipInfoNotion,
    UnLockedNotion1,
    UnLockedNotion2,
    UnLockedNotion3,
    UnLockedNotion4,
    BuyShopNotion,
    ChangeNotion,
    RecoverNotion,
    LowPoint,
    Levelup,
    SeasonWaitNotion,
    SuccessPromotion,
    NotEnoughConditions,
    SpeicalFoodNotion,
    SaveNotion,
    Icon_Locked1,
    Icon_Locked2,
    Icon_Locked3,
    Icon_Locked4,
    Icon_Locked5,
    Icon_Locked6,
    Icon_Locked7,
    Icon_Locked8,
    Icon_Locked9,
    Icon_Locked10,
    Icon_Locked11,
    Icon_Locked12,
    Icon_Locked13,
    SetRandomAbility,
    LowEventTicket,
    SuccessEnter,
    SuccessAttack,
    FailAttack,
    SuccessAttackX2,
    SuccessSleepFood,
    LimitIslandHeart,
    StartDungeon
}