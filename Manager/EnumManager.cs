using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumManager : MonoBehaviour
{

}
public enum GameType
{
    Story,
    Rank
}

public enum MoneyType
{
    CoinA,
    Crystal,
    CoinB
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
    UpgradeCount,
    TotalLevel,
    GourmetLevel,
    Level
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
    QuestReward,
    Shield,
}

public enum CharacterType
{
    Character1,
    Character2,
    Character3,
    Character4,
    Character5,
    Character6,
    Character7,
    Character8,
    Character9,
    Character10,
    Character11,
    Character12,
    Character13,
    Character14,
    Character15,
    Character16,
    Character17,
    Character18,
    Character19,
    Character20,
    Character21,
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
    Butterfly11,
    Butterfly12,
    Butterfly13,
    Butterfly14,
    Butterfly15,
    Butterfly16,
    Butterfly17,
    Butterfly18,
    Butterfly19,
    Butterfly20,
    Butterfly21,
    Butterfly22,
    Butterfly23,
    Butterfly24,
    Butterfly25,
    Butterfly26,
    Butterfly27,
    Butterfly28,
}

public enum ItemType
{
    DailyReward,
    AdReward_Gold,
    DefDestroyTicket,
    GoldShop1,
    GoldShop2,
    GoldShop3,
    AdReward_Portion,
    RemoveAds,
    PortionSet1,
    PortionSet2,
    PortionSet3,
    DailyReward_Portion,
    GoldX2,
    CrystalShop1,
    CrystalShop2,
    CrystalShop3,
    CrystalShop4,
    CrystalShop5,
    CrystalShop6,
    Portion1,
    Portion2,
    Portion3,
    Portion4,
    Portion5,
    DefDestroyTicketSlices,
    DefDestroyTicketPiece,
    BuffTicketSet1,
    BuffTicketSet2,
    BuffTicketSet3,
    DefTicketSet1,
    DefTicketSet2,
    DefTicketSet3,
    SuperOffline,
    AdReward_Crystal,
    AutoUpgrade,
    AutoPresent,
    BuffTicket,
    SkillTicket,
    RepairTicket,
    RepairTicket10,
    EquipExp,
    DungeonKey1,
    DungeonKey2,
    DungeonKey3,
    DungeonKey4,
}

public enum PackageType
{
    Package1,
    Package2,
    Package3,
    Package4,
    Package5,
    Package6
}

public enum BuyType
{
    Free,
    Rm,
    Ad,
    Coin,
    Crystal,
    Exchange,
    RankPoint
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
    Food1,
    Food2,
    Food3,
    Food4,
    Food5,
    Food6,
    Food7,
    Ribs
}

public enum CandyType
{
    Candy1,
    Candy2,
    Candy3,
    Candy4,
    Candy5,
    Candy6,
    Candy7,
    Candy8,
    Candy9,
    Chocolate
}

public enum JapaneseFoodType
{
    JapaneseFood1,
    JapaneseFood2,
    JapaneseFood3,
    JapaneseFood4,
    JapaneseFood5,
    JapaneseFood6,
    JapaneseFood7,
    Ramen
}

public enum DessertType
{
    Dessert1,
    Dessert2,
    Dessert3,
    Dessert4,
    Dessert5,
    Dessert6,
    Dessert7,
    Dessert8,
    Dessert9,
    FruitSkewers
}

public enum PassiveEffect
{
    None,
    UpgradePercentUp,
    DefDestroyPercentUp,
    SellPriceX2Up,
    SellPricePercentUp,
    ExpUp,
    SuccessX2PercentUp,
    TotalPercentUp
}

public enum RewardType
{
    Gold,
    DefDestroyTicket,
    Portion1,
    Portion2,
    Portion3,
    Portion4,
    PortionSet,
    Crystal,
    Exp,
    Treasure1,
    Treasure2,
    Treasure3,
    Treasure4,
    Treasure5,
    Treasure6,
    Portion5,
    Treasure7,
    Treasure8,
    Treasure9,
    TreasureBox,
    DefDestroyTicketPiece,
    BuffTicket,
    Portion6,
    SkillTicket,
    Treasure10,
    Treasure11,
    Treasure12,
    Gold2,
    Gold3,
    RankPoint,
    RepairTicket,
    RemoveAds,
    GoldX2,
    AutoUpgrade,
    AutoPresent,
    Island1,
    Island2,
    Island3,
    Island4,
    SpeicalCharacter,
    EquipExp,
    DungeonKey1,
    DungeonKey2,
    DungeonKey3,
    DungeonKey4,
}

public enum QuestType
{
    UpgradeCount,
    SellCount,
    UseSources,
    OpenChestBox,
    YummyTime
}

public enum SkillType
{
    Skill1,
    Skill2,
    Skill3,
    Skill4,
    Skill5,
    Skill6,
    Skill7,
    Skill8,
    Skill9,
    Skill10,
    Skill11,
    Skill12,
    Skill13,
    Skill14
}

public enum IslandType
{
    Island1,
    Island2,
    Island3,
    Island4,
}

public enum TreasureType
{
    Treasure1,
    Treasure2,
    Treasure3,
    Treasure4,
    Treasure5,
    Treasure6,
    Treasure7,
    Treasure8,
    Treasure9,
    Treasure10,
    Treasure11,
    Treasure12,
}

public enum TotemsType
{
    Totems1,
    Totems2,
    Totems3,
    Totems4,
    Totems5,
    Totems6,
    Totems7,
    Totems8,
    Totems9,
    Totems10,
    Totems11,
    Totems12,
}

public enum FlowerType
{
    Flower1,
    Flower2,
    Flower3,
    Flower4,
    Flower5,
    Flower6,
    Flower7,
}

public enum ChefType
{
    Cook1_1,
    Cook1_2,
    Cook1_3,
    Cook1_4,
    Cook2_1,
    Cook2_2,
    Cook2_3,
    Cook2_4,
    Cook3_1,
    Cook3_2,
    Cook3_3,
    Cook3_4,
    Cook4_1,
    Cook4_2,
    Cook4_3,
    Cook4_4,
    Cook5_1,
    Cook5_2,
    Cook5_3,
    Cook5_4,
    Cook6_1,
    Cook6_2,
    Cook6_3,
    Cook6_4,
}

public enum DungeonType
{
    Dungeon1,
    Dungeon2,
    Dungeon3,
    Dungeon4,
}

public enum IconType
{
    Icon1,
}