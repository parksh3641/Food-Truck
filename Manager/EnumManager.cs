using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumManager : MonoBehaviour
{

}

public enum MoneyType
{
    Coin,
    Crystal
}

public enum ButtonType
{
    Normal,
    Bomb
}

public enum LevelType
{
    Easy,
    Normal,
    Hard,
    Crazy,
    Insane,
}

public enum RankingType
{
    GourmetLevel,
    DonutLevel,
    UpgradeCount
}

public enum LanguageType
{
    Default = 0,
    Korean,
    English,
    Japanese,
    Chinese,
    Indian,
    Portuguese,
    Russian,
    German,
    Spanish,
    Arabic,
    Bengali,
    Indonesian,
    Italian,
    Dutch,
    Vietnamese,
    Thai
}

public enum LoginType
{
    None = 0,
    Guest,
    Google,
    Facebook,
    Apple
}

public enum GameSfxType
{
    Click,
    Upgrade1,
    Upgrade5,
    UpgradeFail,
    UpgradeMax,
    Purchase,
    MoveScene,
    Wrong,
    Sell,
    GetMoney,
    Success,
    Screen_In,
    Screen_Out,
    Fever_In,
    UseSources,
    NotSources,
    ChestBox,
    QuestReward
}

public enum TruckType
{
    Bread,
    Chips,
    Donut,
    Hamburger,
    Hotdog,
    Icecream,
    Lemonade,
    Noodles,
    Pizza,
    Sushi
}

public enum AnimalType
{
    Colobus,
    Gecko,
    Herring,
    Muskrat,
    Pudu,
    Sparrow,
    Squid,
    Taipan
}

public enum ItemType
{
    DailyReward,
    AdReward_Gold,
    DefDestroyTicket,
    GoldShop1,
    GoldShop2,
    GoldShop3,
    AdReward_Potion,
    RemoveAds,
    PortionSet1,
    PortionSet2,
    PortionSet3,
    DailyReward_Portion,
    GoldX2
}

public enum BuyType
{
    Free,
    Rm,
    Ad,
    Coin
}

public enum RankType
{
    N,
    R,
    SR,
    SSR,
    UR
}

public enum FoodType
{
    Hamburger,
    Sandwich,
    SnackLab,
    Drink,
    Pizza,
    Donut
}

public enum PassiveEffect
{
    None,
    UpgradePercentUp,
    DefDestroyPercentUp,
    SellPriceX2Up,
    SellPricePercentUp
}

public enum RewardType
{
    Gold,
    DefDestroyTicket,
    Portion1,
    Portion2,
    Portion3,
    Portion4,
    PortionSet
}

public enum QuestType
{
    HamburgerMaxValue,
    SandwichMaxValue,
    SnackLabMaxValue,
    DrinkMaxValue,
    PizzaMaxValue,
    UpgradeCount,
    SellCount,
    UseSources,
    OpenChestBox,
    DonutLevel
}

public enum SkillType
{
    Skill1,
    Skill2,
    Skill3,
    Skill4,
    Skill5,
    Skill6
}

public enum ButterflyType
{
    Butterfly1,
    Butterfly2,
    Butterfly3,
    Butterfly4,
    Butterfly5,
    Butterfly6,
    Butterfly7,
    Butterfly8,
    Butterfly9,
    Butterfly10,
}

public enum IslandType
{
    Island1,
    Island2,
    Island3,
    Island4,
    Island5,
    Island6,
    Island7,
    Island8,
    Island9,
    Island10,
}