using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumManager : MonoBehaviour
{

}

public enum StoreType
{
    None,
    Google,
    Apple,
    OneStore,
    Amazon,
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
    CoinB,
    CP
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
    RareFoodOpen,
    CleanKitchenStart,
    CleanKitchenEnd
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
    Truck1,
    Truck2,
    Truck3,
    Truck4,
    Truck5,
    Truck6,
    Truck7,
    Truck8,
    Truck9,
    Truck10
}

public enum AnimalType
{
    Animal1,
    Animal2,
    Animal3,
    Animal4,
    Animal5,
    Animal6,
    Animal7,
    Animal8
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
    AbilityPoint,
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
    Package6,
    Package7,
    Package8,
    Package9,
    Package10,
    Package11,
    Package12
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


public enum FoodType
{
    Food1,
    Food2,
    Food3,
    Food4,
    Food5,
    Food6,
    Food7,
    Food8,
    Food9,

    Food10,
    Food11,
    Food12,
    Food13,
    Food14,
    Food15,
    Food16,
    Food17,
    Food18,

    Food19,
    Food20,
    Food21,
    Food22,
    Food23,
    Food24,
    Food25,
    Food26,
    Food27,

    Food28,
    Food29,
    Food30,
    Food31,
    Food32,
    Food33,
    Food34,
    Food35,
    Food36,

    Food37,
    Food38,
    Food39,
    Food40,
    Food41,
    Food42,
    Food43,
    Food44,
    Food45,

    Food46,
    Food47,
    Food48,
    Food49,
    Food50,
    Food51,
    Food52,
    Food53,
    Food54,

    Food55,
    Food56,
    Food57,
    Food58,
    Food59,
    Food60,
    Food61,
    Food62,
    Food63,

    Food64,
    Food65,
    Food66,
    Food67,
    Food68,
    Food69,
    Food70,
    Food71,
    Food72,

    Food73,
    Food74,
    Food75,
    Food76,
    Food77,
    Food78,
    Food79,
    Food80,
    Food81,

    Food82,
    Food83,
    Food84,
    Food85,
    Food86,
    Food87,
    Food88,
    Food89,
    Food90,

    Food91,
    Food92,
    Food93,
    Food94,
    Food95,
    Food96,
    Food97,
    Food98,
    Food99,

    Food100,
    Food101,
    Food102,
    Food103,
    Food104,
    Food105,
    Food106,
    Food107,
    Food108,

    Food109,
    Food110,
    Food111,
    Food112,
    Food113,
    Food114,
    Food115,
    Food116,
    Food117,

    Food118,
    Food119,
    Food120,
    Food121,
    Food122,
    Food123,
    Food124,
    Food125,
    Food126,
    Food127,
    Food128,
    Food129,
    Food130,
    Food131,
    Food132,
    Food133,
    Food134,
    Food135,
    Food136,
    Food137,
    Food138,
    Food139,
    Food140,
    Food141,
    Food142,
    Food143,
    Food144,
    Food145,
}

public enum RankFoodType
{
    RankFood1,
    RankFood2,
    RankFood3,
    RankFood4,
    RankFood5,
    RankFood6,
    RankFood7,
    RankFood8,
    RankFood9,
    RankFood10,
}

public enum PassiveEffect
{
    None,
    UpgradePercentUp,
    DefDestroyPercentUp,
    SellPriceX2Up, //�ƹ��� �Ⱦ�
    SellPricePercentUp,
    ExpUp,
    SuccessX2PercentUp,
    ItemDropPercentUp,
    GoldPerSecondPercentUp
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
    Island1_Heart,
    Island2_Heart,
    Island3_Heart,
    Island4_Heart,
    SpeicalCharacter,
    AbilityPoint,
    DungeonKey1,
    DungeonKey2,
    DungeonKey3,
    DungeonKey4,
    Icon_Ranking1,
    Icon_Ranking2,
    Icon_Ranking3,
    Icon_Ranking4,
    SliverBox,
    GoldBox,
    EventTicket,
    ChallengePoint,
    Icon_Attendance,
    Treasure13,
    Treasure14,
    Treasure15
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
    Skill14,
    Skill15,
    Skill16,
    Skill17,
    Skill18,
    Skill19,
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
    Island11,
    Island12,
    Island13,
    Island14,
    Island15,
    Island16,
    Island17,
    Island18,
    Island19,
    Island20,
    Island21,
    Island22,
    Island23,
    Island24,
    Island25,
    Island26,
    Island27,
    Island28,
    Island29,
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
    Treasure13,
    Treasure14,
    Treasure15,
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
    Cook7_1,
    Cook7_2,
    Cook7_3,
    Cook7_4,
    Cook8_1,
    Cook8_2,
    Cook8_3,
    Cook8_4,
    Cook9_1,
    Cook9_2,
    Cook9_3,
    Cook9_4,
    Cook10_1,
    Cook10_2,
    Cook10_3,
    Cook10_4,
    Cook11_1,
    Cook11_2,
    Cook11_3,
    Cook11_4,
    Cook12_1,
    Cook12_2,
    Cook12_3,
    Cook12_4,
    Cook13_1,
    Cook13_2,
    Cook13_3,
    Cook13_4,
    Cook14_1,
    Cook14_2,
    Cook14_3,
    Cook14_4,
    Cook15_1,
    Cook15_2,
    Cook15_3,
    Cook15_4,
    Cook16_1,
    Cook16_2,
    Cook16_3,
    Cook16_4,
}

public enum ResetType
{
    DailyReward,
    DailyReward_Portion,
    DailyReward_DefTicket,
    DailyAdsReward,
    DailyAdsReward2,
    DailyCastleReward,
    DailyQuestReward,
    DailyTreasureReward,
    DailyReward_Crystal,
    DailyDungeonKey1,
    DailyDungeonKey2,
    DailyDungeonKey3,
    DailyDungeonKey4,
    DailyReset1,
    DailyReset2,
    DailyReset3,
    DailyReset4,
    DailyReset5,
    DailyReset6,
    DailyReset7,
    DailyReset8,
    DailyReset9,
    DailyReset10,
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
    Icon_1,
    Icon_2,
    Icon_3,
    Icon_4,
    Icon_5,
    Icon_6,
    Icon_7,
    Icon_8,
    Icon_9,
    Icon_10,
    Icon_11,
    Icon_12,
    Icon_13,
    Icon_14,
    Icon_15,
    Icon_16,
    Icon_17,
    Icon_18,
    Icon_19,
    Icon_20,
    Icon_21,
    Icon_22,
    Icon_23,
    Icon_24,
    Icon_25,
    Icon_26,
    Icon_27,
    Icon_28,
    Icon_29,
    Icon_30,
    Icon_31,
    Icon_32,
    Icon_33,
    Icon_34,
    Icon_35,
    Icon_36,
    Icon_37,
    Icon_38,
    Icon_39,
    Icon_40,
    Icon_41,
    Icon_42,
    Icon_43,
    Icon_44,
    Icon_45,
    Icon_46,
    Icon_47,
    Icon_48,
    Icon_49,
}

public enum GifticonType
{
    Gifticon_1,
    Gifticon_2,
    Gifticon_3,
    Gifticon_4,
}

public enum EquipType
{
    Equip_Index_1, //��ȭ Ȯ��
    Equip_Index_2, //�Ǹ� ����
    Equip_Index_3, //2�� ��ȭ Ȯ��
    Equip_Index_4, //�ı� ��� Ȯ��
    Equip_Index_5, //�ֹ� û�� - ���� �ӵ�
    Equip_Index_6, //�ֹ� û�� - ���� X2
    Equip_Index_7, //������ �����
    Equip_Index_8, //�ҽ� ���� �ð�
    Equip_Index_9, //�߹� Ÿ�� ���� �ð�
    Equip_Index_10, //ç���� ����
    Equip_Index_11, //�������� ����
    Equip_Index_12, //����Ʈ Ŭ���� ����
}

public enum RankType
{
    Normal,
    Rare,
    Unique,
    Legendary
}