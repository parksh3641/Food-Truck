using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopClass
{
    public string instanceId = "";

    public int abilityLevel = 0;
    public int rare = 0;
    public int level = 0;
}


[CreateAssetMenu(fileName = "PlayerDataBase", menuName = "ScriptableObjects/PlayerDataBase")]
public class PlayerDataBase : ScriptableObject
{
    [Title("Player")]
    [SerializeField]
    private long saveCoin = 0;
    [SerializeField]
    private long coin = 0;
    [SerializeField]
    private long coinA = 0;
    [SerializeField]
    private long coinB = 0;
    [SerializeField]
    private int crystal = 0;
    [SerializeField]
    private int rankPoint = 0;
    [SerializeField]
    private int icon = 0;
    [SerializeField]
    private int firstReward = 0;
    [SerializeField]
    private string firstDate = "";
    [SerializeField]
    private string firstServerDate = "";

    [Space]
    [Title("SeasonPass")]
    [SerializeField]
    private bool seasonPass = false;
    [SerializeField]
    private int seasonPassLevel = 0;
    [SerializeField]
    private string seasonPassDay = "";
    [SerializeField]
    private string freeSeasonPassData = "";
    [SerializeField]
    private string passSeasonPassData = "";

    [Space]
    [Title("GuideMisson")]
    [SerializeField]
    private int guideIndex = 0;

    [Title("Info")]
    [SerializeField]
    private int nextFoodNumber = 0;
    [SerializeField]
    private int islandNumber = 0;
    [SerializeField]
    private int islandNumber_Ranking = 0;
    [SerializeField]
    private int level = 0;
    [SerializeField]
    private int exp = 0;
    [SerializeField]
    private int advancement = 0;
    [SerializeField]
    private int playTime = 0;
    [SerializeField]
    private int lockTutorial = 0;
    [SerializeField]
    private int inGameTutorial = 0;
    [SerializeField]
    private int eventNumber = 0;
    [SerializeField]
    private int updateNumber = 0;
    [SerializeField]
    private int friendsNumber = 0;
    [SerializeField]
    private int reviewNumber = 0;
    [SerializeField]
    private int openKakao = 0;
    [SerializeField]
    private int testAccount = 0;
    [SerializeField]
    private int update = 0;
    [SerializeField]
    private int abilityPoint = 0;
    [SerializeField]
    private int challengePoint = 0;
    [SerializeField]
    private int getGold = 0;
    [SerializeField]
    private long consumeGold = 0;
    [SerializeField]
    private int buyCrystal = 0;
    [SerializeField]
    private int gourmetLevel = 0;
    [SerializeField]
    private int buffTicket = 0;
    [SerializeField]
    private int skillTicket = 0;
    [SerializeField]
    private int recoverTicket = 0;
    [SerializeField]
    private int proficiency = 0;
    [SerializeField]
    private int accessDate = 0;
    [SerializeField]
    private int castleLevel = 0;
    [SerializeField]
    private string castleDate = "";
    [SerializeField]
    private string castleServerDate = "";
    [SerializeField]
    private int defDestroyTicket = 0;
    [SerializeField]
    private int defDestroyTicketPiece = 0;
    [SerializeField]
    private int islandReward = 0;
    [SerializeField]
    private int gender = 0;

    [Space]
    [Title("Gifticon Event")]
    [SerializeField]
    private int eventTicket = 0;
    [SerializeField]
    private int eventTicketCount = 0;
    [SerializeField]
    private int eventEnter1 = 0;
    [SerializeField]
    private int eventEnter2 = 0;
    [SerializeField]
    private int eventEnter3 = 0;
    [SerializeField]
    private int eventEnter4 = 0;

    [Space]
    [Title("Dungeon")]
    [SerializeField]
    private int abilityLevel = 0; //��� ����
    [SerializeField]
    private int dungeonKey1 = 0;
    [SerializeField]
    private int dungeonKey2 = 0;
    [SerializeField]
    private int dungeonKey3 = 0;
    [SerializeField]
    private int dungeonKey4 = 0;
    [SerializeField]
    private int dungeon1Level = 0;
    [SerializeField]
    private int dungeon2Level = 0;
    [SerializeField]
    private int dungeon3Level = 0;
    [SerializeField]
    private int dungeon4Level = 0;

    [Space]
    [Title("Island")]
    [SerializeField]
    public Island_Total_Data island_Total_Data;

    [Space]
    [Title("Daily")]
    [SerializeField]
    public ResetInfo resetInfo;

    [Space]
    [Title("Equip")]
    [SerializeField]
    public Equip equip;

    [Space]
    [Title("Season")]
    [SerializeField]
    public SeasonRewardInfo seasonRewardInfo;

    [Space]
    [Title("Coupon")]
    [SerializeField]
    public CouponInfo couponInfo;

    [Space]
    [Title("Count")]
    [SerializeField]
    private int adCount = 0;
    [SerializeField]
    private int treasureCount = 0;
    [SerializeField]
    private int questCount = 0;
    [SerializeField]
    private int buffCount = 0;
    [SerializeField]
    private int reincarnationCount = 0;
    [SerializeField]
    private int changeNicknameCount = 0;
    [SerializeField]
    private int useSauce1 = 0;
    [SerializeField]
    private int useSauce2 = 0;
    [SerializeField]
    private int useSauce3 = 0;
    [SerializeField]
    private int useSauce4 = 0;
    [SerializeField]
    private int useSauce5 = 0;
    [SerializeField]
    private int dungeon1Count = 0;
    [SerializeField]
    private int dungeon2Count = 0;
    [SerializeField]
    private int dungeon3Count = 0;
    [SerializeField]
    private int dungeon4Count = 0;
    [SerializeField]
    private int offlineCount = 0;
    [SerializeField]
    private int defDestroyCount = 0;
    [SerializeField]
    private int recoverCount = 0;
    [SerializeField]
    private int equipCount = 0;

    [Space]
    [Title("Quest")]
    [SerializeField]
    private int upgradeCount = 0;
    [SerializeField]
    private int sellCount = 0;
    [SerializeField]
    private int useSauceCount = 0;
    [SerializeField]
    private int openChestBox = 0;
    [SerializeField]
    private int yummyTimeCount = 0;

    [Space]
    [Title("Treasure")]
    [SerializeField]
    private int treasure1 = 0;
    [SerializeField]
    private int treasure2 = 0;
    [SerializeField]
    private int treasure3 = 0;
    [SerializeField]
    private int treasure4 = 0;
    [SerializeField]
    private int treasure5 = 0;
    [SerializeField]
    private int treasure6 = 0;
    [SerializeField]
    private int treasure7 = 0;
    [SerializeField]
    private int treasure8 = 0;
    [SerializeField]
    private int treasure9 = 0;
    [SerializeField]
    private int treasure10 = 0;
    [SerializeField]
    private int treasure11 = 0;
    [SerializeField]
    private int treasure12 = 0;
    [SerializeField]
    private int treasure13 = 0;
    [SerializeField]
    private int treasure14 = 0;
    [SerializeField]
    private int treasure15 = 0;

    [Space]
    [SerializeField]
    private int treasure1Count = 0;
    [SerializeField]
    private int treasure2Count = 0;
    [SerializeField]
    private int treasure3Count = 0;
    [SerializeField]
    private int treasure4Count = 0;
    [SerializeField]
    private int treasure5Count = 0;
    [SerializeField]
    private int treasure6Count = 0;
    [SerializeField]
    private int treasure7Count = 0;
    [SerializeField]
    private int treasure8Count = 0;
    [SerializeField]
    private int treasure9Count = 0;
    [SerializeField]
    private int treasure10Count = 0;
    [SerializeField]
    private int treasure11Count = 0;
    [SerializeField]
    private int treasure12Count = 0;
    [SerializeField]
    private int treasure13Count = 0;
    [SerializeField]
    private int treasure14Count = 0;
    [SerializeField]
    private int treasure15Count = 0;

    [Space]
    [Title("Character")]
    [SerializeField]
    private int character1 = 0;
    [SerializeField]
    private int character2 = 0;
    [SerializeField]
    private int character3 = 0;
    [SerializeField]
    private int character4 = 0;
    [SerializeField]
    private int character5 = 0;
    [SerializeField]
    private int character6 = 0;
    [SerializeField]
    private int character7 = 0;
    [SerializeField]
    private int character8 = 0;
    [SerializeField]
    private int character9 = 0;
    [SerializeField]
    private int character10 = 0;
    [SerializeField]
    private int character11 = 0;
    [SerializeField]
    private int character12 = 0;
    [SerializeField]
    private int character13 = 0;
    [SerializeField]
    private int character14 = 0;
    [SerializeField]
    private int character15 = 0;
    [SerializeField]
    private int character16 = 0;
    [SerializeField]
    private int character17 = 0;
    [SerializeField]
    private int character18 = 0;
    [SerializeField]
    private int character19 = 0;
    [SerializeField]
    private int character20 = 0;
    [SerializeField]
    private int character21 = 0;

    [Space]
    [Title("Truck")]
    [SerializeField]
    private int truck1 = 1;
    [SerializeField]
    private int truck2 = 0;
    [SerializeField]
    private int truck3 = 0;
    [SerializeField]
    private int truck4 = 0;
    [SerializeField]
    private int truck5 = 0;
    [SerializeField]
    private int truck6 = 0;
    [SerializeField]
    private int truck7 = 0;
    [SerializeField]
    private int truck8 = 0;
    [SerializeField]
    private int truck9 = 0;
    [SerializeField]
    private int truck10 = 0;

    [Space]
    [Title("Animal")]
    [SerializeField]
    private int animal1 = 1;
    [SerializeField]
    private int animal2 = 0;
    [SerializeField]
    private int animal3 = 0;
    [SerializeField]
    private int animal4 = 0;
    [SerializeField]
    private int animal5 = 0;
    [SerializeField]
    private int animal6 = 0;
    [SerializeField]
    private int animal7 = 0;
    [SerializeField]
    private int animal8 = 0;

    [Space]
    [Title("Butterfly")]
    [SerializeField]
    private int butterfly1 = 1;
    [SerializeField]
    private int butterfly2 = 0;
    [SerializeField]
    private int butterfly3 = 0;
    [SerializeField]
    private int butterfly4 = 0;
    [SerializeField]
    private int butterfly5 = 0;
    [SerializeField]
    private int butterfly6 = 0;
    [SerializeField]
    private int butterfly7 = 0;
    [SerializeField]
    private int butterfly8 = 0;
    [SerializeField]
    private int butterfly9 = 0;
    [SerializeField]
    private int butterfly10 = 0;
    [SerializeField]
    private int butterfly11 = 0;
    [SerializeField]
    private int butterfly12 = 0;
    [SerializeField]
    private int butterfly13 = 0;
    [SerializeField]
    private int butterfly14 = 0;
    [SerializeField]
    private int butterfly15 = 0;
    [SerializeField]
    private int butterfly16 = 0;
    [SerializeField]
    private int butterfly17 = 0;
    [SerializeField]
    private int butterfly18 = 0;
    [SerializeField]
    private int butterfly19 = 0;
    [SerializeField]
    private int butterfly20 = 0;
    [SerializeField]
    private int butterfly21 = 0;
    [SerializeField]
    private int butterfly22 = 0;
    [SerializeField]
    private int butterfly23 = 0;
    [SerializeField]
    private int butterfly24 = 0;
    [SerializeField]
    private int butterfly25 = 0;
    [SerializeField]
    private int butterfly26 = 0;
    [SerializeField]
    private int butterfly27 = 0;
    [SerializeField]
    private int butterfly28 = 0;

    [Space]
    [Title("Totems")]
    [SerializeField]
    private int totems1 = 1;
    [SerializeField]
    private int totems2 = 0;
    [SerializeField]
    private int totems3 = 0;
    [SerializeField]
    private int totems4 = 0;
    [SerializeField]
    private int totems5 = 0;
    [SerializeField]
    private int totems6 = 0;
    [SerializeField]
    private int totems7 = 0;
    [SerializeField]
    private int totems8 = 0;
    [SerializeField]
    private int totems9 = 0;
    [SerializeField]
    private int totems10 = 0;
    [SerializeField]
    private int totems11 = 0;
    [SerializeField]
    private int totems12 = 0;


    [Space]
    [Title("Flower")]
    [SerializeField]
    private int flower1 = 1;
    [SerializeField]
    private int flower2 = 0;
    [SerializeField]
    private int flower3 = 0;
    [SerializeField]
    private int flower4 = 0;
    [SerializeField]
    private int flower5 = 0;
    [SerializeField]
    private int flower6 = 0;
    [SerializeField]
    private int flower7 = 0;

    [Space]
    [Title("Bucket")]
    [SerializeField]
    private int[] bucket = new int[10];

    [Space]
    [Title("Chair")]
    [SerializeField]
    private int[] chair = new int[10];

    [Space]
    [Title("Tube")]
    [SerializeField]
    private int[] tube = new int[10];

    [Space]
    [Title("Surfboard")]
    [SerializeField]
    private int[] surfboard = new int[10];

    [Space]
    [Title("Umbrella")]
    [SerializeField]
    private int[] umbrella = new int[10];

    [Space]
    [Title("ItemInstance")]
    [SerializeField]
    public List<ShopClass> characterItemInstance = new List<ShopClass>();
    [SerializeField]
    public List<ShopClass> truckItemInstance = new List<ShopClass>();
    [SerializeField]
    public List<ShopClass> animalItemInstance = new List<ShopClass>();
    [SerializeField]
    public List<ShopClass> butterflyItemInstance = new List<ShopClass>();
    [SerializeField]
    public List<ShopClass> totemsItemInstance = new List<ShopClass>();
    [SerializeField]
    public List<ShopClass> bucketItemInstance = new List<ShopClass>();
    [SerializeField]
    public List<ShopClass> chairItemInstance = new List<ShopClass>();
    [SerializeField]
    public List<ShopClass> tubeItemInstance = new List<ShopClass>();
    [SerializeField]
    public List<ShopClass> surfboardItemInstance = new List<ShopClass>();
    [SerializeField]
    public List<ShopClass> umbrellaItemInstance = new List<ShopClass>();


    [Space]
    [Title("Icon")]
    public List<IconClass> iconList = new List<IconClass>();

    [Space]
    [Title("Island")]
    [SerializeField]
    private int island1 = 0;
    [SerializeField]
    private int island2 = 0;
    [SerializeField]
    private int island3 = 0;
    [SerializeField]
    private int island4 = 0;
    [SerializeField]
    private int island5 = 0;
    [SerializeField]
    private int island6 = 0;
    [SerializeField]
    private int island7 = 0;
    [SerializeField]
    private int island8 = 0;
    [SerializeField]
    private int island9 = 0;
    [SerializeField]
    private int island10 = 0;

    [Space]
    [Title("Rank")]
    [SerializeField]
    private int rankLevel1 = 0;
    [SerializeField]
    private int rankLevel2 = 0;
    [SerializeField]
    private int rankLevel3 = 0;
    [SerializeField]
    private int rankLevel4 = 0;
    [SerializeField]
    private int rankLevel5 = 0;
    [SerializeField]
    private int rankLevel6 = 0;
    [SerializeField]
    private int rankLevel7 = 0;
    [SerializeField]
    private int rankLevel8 = 0;
    [SerializeField]
    private int rankLevel9 = 0;
    [SerializeField]
    private int rankLevel10 = 0;
    [SerializeField]
    private int totalLevel = 0;
    [SerializeField]
    private int totalLevel_1 = 0;
    [SerializeField]
    private int totalLevel_2 = 0;
    [SerializeField]
    private int totalLevel_3 = 0;
    [SerializeField]
    private int totalLevel_4 = 0;
    [SerializeField]
    private int totalLevel_5 = 0;
    [SerializeField]
    private int totalLevel_6 = 0;
    [SerializeField]
    private int totalLevel_7 = 0;
    [SerializeField]
    private int totalLevel_8 = 0;
    [SerializeField]
    private int totalLevel_9 = 0;
    [SerializeField]
    private int totalLevel_10 = 0;
    [SerializeField]
    private int totalLevel_11 = 0;
    [SerializeField]
    private int totalLevel_12 = 0;
    [SerializeField]
    private int totalLevel_13 = 0;
    [SerializeField]
    private int totalLevel_14 = 0;
    [SerializeField]
    private int totalLevel_15 = 0;
    [SerializeField]
    private int totalLevel_16 = 0;
    [SerializeField]
    private int totalLevel_17 = 0;
    [SerializeField]
    private int totalLevel_18 = 0;
    [SerializeField]
    private int totalLevel_19 = 0;
    [SerializeField]
    private int totalLevel_20 = 0;

    [Space]
    [Title("Skill")]
    [SerializeField]
    private int skill1 = 0;
    [SerializeField]
    private int skill2 = 0;
    [SerializeField]
    private int skill3 = 0;
    [SerializeField]
    private int skill4 = 0;
    [SerializeField]
    private int skill5 = 0;
    [SerializeField]
    private int skill6 = 0;
    [SerializeField]
    private int skill7 = 0;
    [SerializeField]
    private int skill8 = 0;
    [SerializeField]
    private int skill9 = 0;
    [SerializeField]
    private int skill10 = 0;
    [SerializeField]
    private int skill11 = 0;
    [SerializeField]
    private int skill12 = 0;
    [SerializeField]
    private int skill13 = 0;
    [SerializeField]
    private int skill14 = 0;
    [SerializeField]
    private int skill15 = 0;
    [SerializeField]
    private int skill16 = 0;
    [SerializeField]
    private int skill17 = 0;
    [SerializeField]
    private int skill18 = 0;
    [SerializeField]
    private int skill19 = 0;

    [Space]
    [Title("Portion")]
    [SerializeField]
    private int portion1 = 0;
    [SerializeField]
    private int portion2 = 0;
    [SerializeField]
    private int portion3 = 0;
    [SerializeField]
    private int portion4 = 0;
    [SerializeField]
    private int portion5 = 0;
    [SerializeField]
    private int portion6 = 0;

    [Space]
    [Title("Package")]
    [SerializeField]
    private bool package1 = false;
    [SerializeField]
    private bool package2 = false;
    [SerializeField]
    private bool package3 = false;
    [SerializeField]
    private bool package4 = false;
    [SerializeField]
    private bool package5 = false;
    [SerializeField]
    private bool package6 = false;
    [SerializeField]
    private int package7 = 0;
    [SerializeField]
    private int package8 = 0;
    [SerializeField]
    private int package9 = 0;
    [SerializeField]
    private bool package10 = false;
    [SerializeField]
    private bool package11 = false;
    [SerializeField]
    private bool package12 = false;

    [Space]
    [Title("Reset")]
    [SerializeField]
    private string attendanceDay = "";
    [SerializeField]
    private int attendanceCount = 0;
    [SerializeField]
    private bool attendanceCheck = false;
    [SerializeField]
    private int playTimeCount = 0;
    [SerializeField]
    private int rankEventCount = 0;
    [SerializeField]
    private int recipeEventCount = 0;
    [SerializeField]
    private int levelUpEventCount = 0;
    [SerializeField]
    private int welcomeCount = 0;
    [SerializeField]
    private bool welcomeCheck = false;
    [SerializeField]
    private string nextMonday = "";
    [SerializeField]
    private bool appReview = false;

    [Space]
    [Title("Purchase")]
    [SerializeField]
    private bool removeAds = false;
    [SerializeField]
    private bool goldX2 = false;
    [SerializeField]
    private bool superOffline = false;
    [SerializeField]
    private bool autoUpgrade = false;
    [SerializeField]
    private bool autoPresent = false;
    [SerializeField]
    private bool superExp = false;
    [SerializeField]
    private bool superKitchen = false;


    public long SaveCoin
    {
        get
        {
            return saveCoin;
        }
        set
        {
            saveCoin = value;
        }
    }

    public long Coin
    {
        get
        {
            return coin;
        }
        set
        {
            coin = value;
        }
    }

    public long CoinA
    {
        get
        {
            return coinA;
        }
        set
        {
            coinA = value;
        }
    }

    public long CoinB
    {
        get
        {
            return coinB;
        }
        set
        {
            coinB = value;
        }
    }

    public int Crystal
    {
        get
        {
            return crystal;
        }
        set
        {
            crystal = value;
        }
    }

    public int RankPoint
    {
        get
        {
            return rankPoint;
        }
        set
        {
            rankPoint = value;
        }
    }

    public int DefDestroyTicket
    {
        get
        {
            return defDestroyTicket;
        }
        set
        {
            defDestroyTicket = value;
        }
    }

    public int DefDestroyTicketPiece
    {
        get
        {
            return defDestroyTicketPiece;
        }
        set
        {
            defDestroyTicketPiece = value;
        }
    }

    public int IslandReward
    {
        get
        {
            return islandReward;
        }
        set
        {
            islandReward = value;
        }
    }

    public int EventTicket
    {
        get
        {
            return eventTicket;
        }
        set
        {
            eventTicket = value;
        }
    }

    public int EventTicketCount
    {
        get
        {
            return eventTicketCount;
        }
        set
        {
            eventTicketCount = value;
        }
    }

    public int EventEnter1
    {
        get
        {
            return eventEnter1;
        }
        set
        {
            eventEnter1 = value;
        }
    }

    public int EventEnter2
    {
        get
        {
            return eventEnter2;
        }
        set
        {
            eventEnter2 = value;
        }
    }

    public int EventEnter3
    {
        get
        {
            return eventEnter3;
        }
        set
        {
            eventEnter3 = value;
        }
    }

    public int EventEnter4
    {
        get
        {
            return eventEnter4;
        }
        set
        {
            eventEnter4 = value;
        }
    }

    public int DungeonKey1
    {
        get
        {
            return dungeonKey1;
        }
        set
        {
            dungeonKey1 = value;
        }
    }

    public int DungeonKey2
    {
        get
        {
            return dungeonKey2;
        }
        set
        {
            dungeonKey2 = value;
        }
    }

    public int DungeonKey3
    {
        get
        {
            return dungeonKey3;
        }
        set
        {
            dungeonKey3 = value;
        }
    }

    public int DungeonKey4
    {
        get
        {
            return dungeonKey4;
        }
        set
        {
            dungeonKey4 = value;
        }
    }

    public int Gender
    {
        get
        {
            return gender;
        }
        set
        {
            gender = value;
        }
    }

    public int AbilityPoint
    {
        get { return abilityPoint; }
        set { abilityPoint = value; }
    }

    public int AbilityLevel
    {
        get { return abilityLevel; }
        set { abilityLevel = value; }
    }

    public int Dungeon1Count
    {
        get { return dungeon1Count; }
        set { dungeon1Count = value; }
    }

    public int Dungeon2Count
    {
        get { return dungeon2Count; }
        set { dungeon2Count = value; }
    }

    public int Dungeon3Count
    {
        get { return dungeon3Count; }
        set { dungeon3Count = value; }
    }

    public int Dungeon4Count
    {
        get { return dungeon4Count; }
        set { dungeon4Count = value; }
    }

    public int Dungeon1Level
    {
        get { return dungeon1Level; }
        set { dungeon1Level = value; }
    }

    public int Dungeon2Level
    {
        get { return dungeon2Level; }
        set { dungeon2Level = value; }
    }

    public int Dungeon3Level
    {
        get { return dungeon3Level; }
        set { dungeon3Level = value; }
    }

    public int Dungeon4Level
    {
        get { return dungeon4Level; }
        set { dungeon4Level = value; }
    }

    public int OfflineCount
    {
        get { return offlineCount; }
        set { offlineCount = value; }
    }

    public int DefDestroyCount
    {
        get { return defDestroyCount; }
        set { defDestroyCount = value; }
    }

    public int RecoverCount
    {
        get { return recoverCount; }
        set { recoverCount = value; }
    }

    public int EquipCount
    {
        get { return equipCount; }
        set { equipCount = value; }
    }

    public int LockTutorial
    {
        get
        {
            return lockTutorial;
        }
        set
        {
            lockTutorial = value;
        }
    }

    public int InGameTutorial
    {
        get
        {
            return inGameTutorial;
        }
        set
        {
            inGameTutorial = value;
        }
    }

    public int EventNumber
    {
        get
        {
            return eventNumber;
        }
        set
        {
            eventNumber = value;
        }
    }

    public int UpdateNumber
    {
        get
        {
            return updateNumber;
        }
        set
        {
            updateNumber = value;
        }
    }

    public int FriendsNumber
    {
        get
        {
            return friendsNumber;
        }
        set
        {
            friendsNumber = value;
        }
    }

    public int ReviewNumber
    {
        get
        {
            return reviewNumber;
        }
        set
        {
            reviewNumber = value;
        }
    }

    public int OpenKakao
    {
        get
        {
            return openKakao;
        }
        set
        {
            openKakao = value;
        }
    }

    public int Icon
    {
        get
        {
            return icon;
        }
        set
        {
            icon = value;
        }
    }

    public int FirstReward
    {
        get
        {
            return firstReward;
        }
        set
        {
            firstReward = value;
        }
    }

    public string FirstDate
    {
        get
        {
            return firstDate;
        }
        set
        {
            firstDate = value;
        }
    }

    public string FirstServerDate
    {
        get
        {
            return firstServerDate;
        }
        set
        {
            firstServerDate = value;
        }
    }

    public bool SeasonPass
    {
        get
        {
            return seasonPass;
        }
        set
        {
            seasonPass = value;
        }
    }

    public int SeasonPassLevel
    {
        get
        {
            return seasonPassLevel;
        }
        set
        {
            seasonPassLevel = value;
        }
    }

    public int IslandNumber
    {
        get
        {
            return islandNumber;
        }
        set
        {
            islandNumber = value;
        }
    }

    public int IslandNumber_Ranking
    {
        get
        {
            return islandNumber_Ranking;
        }
        set
        {
            islandNumber_Ranking = value;
        }
    }

    public int TestAccount
    {
        get
        {
            return testAccount;
        }
        set
        {
            testAccount = value;
        }
    }
    public int Update
    {
        get
        {
            return update;
        }
        set
        {
            update = value;
        }
    }


    public int BuffTicket
    {
        get
        {
            return buffTicket;
        }
        set
        {
            buffTicket = value;
        }
    }

    public int SkillTicket
    {
        get
        {
            return skillTicket;
        }
        set
        {
            skillTicket = value;
        }
    }

    public int RecoverTicket
    {
        get
        {
            return recoverTicket;
        }
        set
        {
            recoverTicket = value;
        }
    }

    public int Proficiency
    {
        get
        {
            return proficiency;
        }
        set
        {
            proficiency = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    public int Exp
    {
        get
        {
            return exp;
        }
        set
        {
            exp = value;
        }
    }

    public int PlayTime
    {
        get
        {
            return playTime;
        }
        set
        {
            playTime = value;
        }
    }

    public int Advancement
    {
        get
        {
            return advancement;
        }
        set
        {
            advancement = value;
        }
    }

    public int ChallengePoint
    {
        get
        {
            return challengePoint;
        }
        set
        {
            challengePoint = value;
        }
    }

    public int AccessDate
    {
        get
        {
            return accessDate;
        }
        set
        {
            accessDate = value;
        }
    }

    public int CastleLevel
    {
        get
        {
            return castleLevel;
        }
        set
        {
            castleLevel = value;
        }
    }
    public string CastleDate
    {
        get
        {
            return castleDate;
        }
        set
        {
            castleDate = value;
        }
    }

    public string CastleServerDate
    {
        get
        {
            return castleServerDate;
        }
        set
        {
            castleServerDate = value;
        }
    }

    public int AdCount
    {
        get
        {
            return adCount;
        }
        set
        {
            adCount = value;
        }
    }

    public int TreasureCount
    {
        get
        {
            return treasureCount;
        }
        set
        {
            treasureCount = value;
        }
    }

    public int Treasure1
    {
        get
        {
            return treasure1;
        }
        set
        {
            treasure1 = value;
        }
    }

    public int Treasure2
    {
        get
        {
            return treasure2;
        }
        set
        {
            treasure2 = value;
        }
    }

    public int Treasure3
    {
        get
        {
            return treasure3;
        }
        set
        {
            treasure3 = value;
        }
    }

    public int Treasure4
    {
        get
        {
            return treasure4;
        }
        set
        {
            treasure4 = value;
        }
    }

    public int Treasure5
    {
        get
        {
            return treasure5;
        }
        set
        {
            treasure5 = value;
        }
    }

    public int Treasure6
    {
        get
        {
            return treasure6;
        }
        set
        {
            treasure6 = value;
        }
    }

    public int Treasure7
    {
        get
        {
            return treasure7;
        }
        set
        {
            treasure7 = value;
        }
    }

    public int Treasure8
    {
        get
        {
            return treasure8;
        }
        set
        {
            treasure8 = value;
        }
    }

    public int Treasure9
    {
        get
        {
            return treasure9;
        }
        set
        {
            treasure9 = value;
        }
    }

    public int Treasure10
    {
        get
        {
            return treasure10;
        }
        set
        {
            treasure10 = value;
        }
    }

    public int Treasure11
    {
        get
        {
            return treasure11;
        }
        set
        {
            treasure11 = value;
        }
    }

    public int Treasure12
    {
        get
        {
            return treasure12;
        }
        set
        {
            treasure12 = value;
        }
    }

    public int Treasure13
    {
        get
        {
            return treasure13;
        }
        set
        {
            treasure13 = value;
        }
    }

    public int Treasure14
    {
        get
        {
            return treasure14;
        }
        set
        {
            treasure14 = value;
        }
    }

    public int Treasure15
    {
        get
        {
            return treasure15;
        }
        set
        {
            treasure15 = value;
        }
    }

    public int Treasure1Count
    {
        get
        {
            return treasure1Count;
        }
        set
        {
            treasure1Count = value;
        }
    }

    public int Treasure2Count
    {
        get
        {
            return treasure2Count;
        }
        set
        {
            treasure2Count = value;
        }
    }

    public int Treasure3Count
    {
        get
        {
            return treasure3Count;
        }
        set
        {
            treasure3Count = value;
        }
    }

    public int Treasure4Count
    {
        get
        {
            return treasure4Count;
        }
        set
        {
            treasure4Count = value;
        }
    }

    public int Treasure5Count
    {
        get
        {
            return treasure5Count;
        }
        set
        {
            treasure5Count = value;
        }
    }

    public int Treasure6Count
    {
        get
        {
            return treasure6Count;
        }
        set
        {
            treasure6Count = value;
        }
    }

    public int Treasure7Count
    {
        get
        {
            return treasure7Count;
        }
        set
        {
            treasure7Count = value;
        }
    }

    public int Treasure8Count
    {
        get
        {
            return treasure8Count;
        }
        set
        {
            treasure8Count = value;
        }
    }

    public int Treasure9Count
    {
        get
        {
            return treasure9Count;
        }
        set
        {
            treasure9Count = value;
        }
    }

    public int Treasure10Count
    {
        get
        {
            return treasure10Count;
        }
        set
        {
            treasure10Count = value;
        }
    }

    public int Treasure11Count
    {
        get
        {
            return treasure11Count;
        }
        set
        {
            treasure11Count = value;
        }
    }

    public int Treasure12Count
    {
        get
        {
            return treasure12Count;
        }
        set
        {
            treasure12Count = value;
        }
    }

    public int Treasure13Count
    {
        get
        {
            return treasure13Count;
        }
        set
        {
            treasure13Count = value;
        }
    }

    public int Treasure14Count
    {
        get
        {
            return treasure14Count;
        }
        set
        {
            treasure14Count = value;
        }
    }

    public int Treasure15Count
    {
        get
        {
            return treasure15Count;
        }
        set
        {
            treasure15Count = value;
        }
    }

    public int Character1
    {
        get { return character1; }
        set { character1 = value; }
    }

    public int Character2
    {
        get
        {
            return character2;
        }
        set
        {
            character2 = value;
        }
    }

    public int Character3
    {
        get
        {
            return character3;
        }
        set
        {
            character3 = value;
        }
    }

    public int Character4
    {
        get
        {
            return character4;
        }
        set
        {
            character4 = value;
        }
    }

    public int Character5
    {
        get
        {
            return character5;
        }
        set
        {
            character5 = value;
        }
    }

    public int Character6
    {
        get
        {
            return character6;
        }
        set
        {
            character6 = value;
        }
    }

    public int Character7
    {
        get
        {
            return character7;
        }
        set
        {
            character7 = value;
        }
    }

    public int Character8
    {
        get
        {
            return character8;
        }
        set
        {
            character8 = value;
        }
    }

    public int Character9
    {
        get
        {
            return character9;
        }
        set
        {
            character9 = value;
        }
    }

    public int Character10
    {
        get
        {
            return character10;
        }
        set
        {
            character10 = value;
        }
    }

    public int Character11
    {
        get
        {
            return character11;
        }
        set
        {
            character11 = value;
        }
    }

    public int Character12
    {
        get
        {
            return character12;
        }
        set
        {
            character12 = value;
        }
    }

    public int Character13
    {
        get
        {
            return character13;
        }
        set
        {
            character13 = value;
        }
    }

    public int Character14
    {
        get
        {
            return character14;
        }
        set
        {
            character14 = value;
        }
    }

    public int Character15
    {
        get
        {
            return character15;
        }
        set
        {
            character15 = value;
        }
    }

    public int Character16
    {
        get
        {
            return character16;
        }
        set
        {
            character16 = value;
        }
    }

    public int Character17
    {
        get
        {
            return character17;
        }
        set
        {
            character17 = value;
        }
    }

    public int Character18
    {
        get
        {
            return character18;
        }
        set
        {
            character18 = value;
        }
    }

    public int Character19
    {
        get
        {
            return character19;
        }
        set
        {
            character19 = value;
        }
    }

    public int Character20
    {
        get
        {
            return character20;
        }
        set
        {
            character20 = value;
        }
    }

    public int Character21
    {
        get
        {
            return character21;
        }
        set
        {
            character21 = value;
        }
    }

    public int NextFoodNumber
    {
        get
        {
            return nextFoodNumber;
        }
        set
        {
            nextFoodNumber = value;
        }
    }

    public int UpgradeCount
    {
        get
        {
            return upgradeCount;
        }
        set
        {
            upgradeCount = value;
        }
    }

    public int SellCount
    {
        get
        {
            return sellCount;
        }
        set
        {
            sellCount = value;
        }
    }

    public int UseSauceCount
    {
        get
        {
            return useSauceCount;
        }
        set
        {
            useSauceCount = value;
        }
    }

    public int OpenChestBox
    {
        get
        {
            return openChestBox;
        }
        set
        {
            openChestBox = value;
        }
    }

    public int YummyTimeCount
    {
        get
        {
            return yummyTimeCount;
        }
        set
        {
            yummyTimeCount = value;
        }
    }

    public int GuideIndex
    {
        get
        {
            return guideIndex;
        }
        set
        {
            guideIndex = value;
        }
    }

    public int QuestCount
    {
        get
        {
            return questCount;
        }
        set
        {
            questCount = value;
        }
    }

    public int ReincarnationCount
    {
        get
        {
            return reincarnationCount;
        }
        set
        {
            reincarnationCount = value;
        }
    }

    public int BuffCount
    {
        get
        {
            return buffCount;
        }
        set
        {
            buffCount = value;
        }
    }

    public int ChangeNicknameCount
    {
        get
        {
            return changeNicknameCount;
        }
        set
        {
            changeNicknameCount = value;
        }
    }

    public int UseSauce1
    {
        get{ return useSauce1; }
        set { useSauce1 = value; }
    }

    public int UseSauce2
    {
        get { return useSauce2; }
        set { useSauce2 = value; }
    }

    public int UseSauce3
    {
        get { return useSauce3; }
        set { useSauce3 = value; }
    }

    public int UseSauce4
    {
        get { return useSauce4; }
        set { useSauce4 = value; }
    }

    public int UseSauce5
    {
        get { return useSauce5; }
        set { useSauce5 = value; }
    }

    public int GetGold
    {
        get
        {
            return getGold;
        }
        set
        {
            getGold = value;
        }
    }

    public long ConsumeGold
    {
        get
        {
            return consumeGold;
        }
        set
        {
            consumeGold = value;
        }
    }

    public int BuyCrystal
    {
        get
        {
            return buyCrystal;
        }
        set
        {
            buyCrystal = value;
        }
    }

    public int GourmetLevel
    {
        get
        {
            return gourmetLevel;
        }
        set
        {
            gourmetLevel = value;
        }
    }

    public int RankLevel1
    {
        get
        {
            return rankLevel1;
        }
        set
        {
            rankLevel1 = value;
        }
    }

    public int RankLevel2
    {
        get
        {
            return rankLevel2;
        }
        set
        {
            rankLevel2 = value;
        }
    }

    public int RankLevel3
    {
        get
        {
            return rankLevel3;
        }
        set
        {
            rankLevel3 = value;
        }
    }

    public int RankLevel4
    {
        get
        {
            return rankLevel4;
        }
        set
        {
            rankLevel4 = value;
        }
    }

    public int RankLevel5
    {
        get
        {
            return rankLevel5;
        }
        set
        {
            rankLevel5 = value;
        }
    }

    public int RankLevel6
    {
        get
        {
            return rankLevel6;
        }
        set
        {
            rankLevel6 = value;
        }
    }

    public int RankLevel7
    {
        get
        {
            return rankLevel7;
        }
        set
        {
            rankLevel7 = value;
        }
    }

    public int RankLevel8
    {
        get
        {
            return rankLevel8;
        }
        set
        {
            rankLevel8 = value;
        }
    }

    public int RankLevel9
    {
        get
        {
            return rankLevel9;
        }
        set
        {
            rankLevel9 = value;
        }
    }

    public int RankLevel10
    {
        get
        {
            return rankLevel10;
        }
        set
        {
            rankLevel10 = value;
        }
    }

    public int TotalLevel
    {
        get
        {
            return totalLevel;
        }
        set
        {
            totalLevel = value;
        }
    }

    public int TotalLevel_1
    {
        get
        {
            return totalLevel_1;
        }
        set
        {
            totalLevel_1 = value;
        }
    }

    public int TotalLevel_2
    {
        get
        {
            return totalLevel_2;
        }
        set
        {
            totalLevel_2 = value;
        }
    }

    public int TotalLevel_3
    {
        get
        {
            return totalLevel_3;
        }
        set
        {
            totalLevel_3 = value;
        }
    }

    public int TotalLevel_4
    {
        get
        {
            return totalLevel_4;
        }
        set
        {
            totalLevel_4 = value;
        }
    }

    public int TotalLevel_5
    {
        get
        {
            return totalLevel_5;
        }
        set
        {
            totalLevel_5 = value;
        }
    }

    public int TotalLevel_6
    {
        get
        {
            return totalLevel_6;
        }
        set
        {
            totalLevel_6 = value;
        }
    }

    public int TotalLevel_7
    {
        get
        {
            return totalLevel_7;
        }
        set
        {
            totalLevel_7 = value;
        }
    }

    public int TotalLevel_8
    {
        get
        {
            return totalLevel_8;
        }
        set
        {
            totalLevel_8 = value;
        }
    }

    public int TotalLevel_9
    {
        get
        {
            return totalLevel_9;
        }
        set
        {
            totalLevel_9 = value;
        }
    }

    public int TotalLevel_10
    {
        get
        {
            return totalLevel_10;
        }
        set
        {
            totalLevel_10 = value;
        }
    }

    public int TotalLevel_11
    {
        get
        {
            return totalLevel_11;
        }
        set
        {
            totalLevel_11 = value;
        }
    }

    public int TotalLevel_12
    {
        get
        {
            return totalLevel_12;
        }
        set
        {
            totalLevel_12 = value;
        }
    }

    public int TotalLevel_13
    {
        get
        {
            return totalLevel_13;
        }
        set
        {
            totalLevel_13 = value;
        }
    }

    public int TotalLevel_14
    {
        get
        {
            return totalLevel_14;
        }
        set
        {
            totalLevel_14 = value;
        }
    }

    public int TotalLevel_15
    {
        get
        {
            return totalLevel_15;
        }
        set
        {
            totalLevel_15 = value;
        }
    }

    public int TotalLevel_16
    {
        get
        {
            return totalLevel_16;
        }
        set
        {
            totalLevel_16 = value;
        }
    }

    public int TotalLevel_17
    {
        get
        {
            return totalLevel_17;
        }
        set
        {
            totalLevel_17 = value;
        }
    }

    public int TotalLevel_18
    {
        get
        {
            return totalLevel_18;
        }
        set
        {
            totalLevel_18 = value;
        }
    }

    public int TotalLevel_19
    {
        get
        {
            return totalLevel_19;
        }
        set
        {
            totalLevel_19 = value;
        }
    }

    public int TotalLevel_20
    {
        get
        {
            return totalLevel_20;
        }
        set
        {
            totalLevel_20 = value;
        }
    }

    public int Skill1
    {
        get
        {
            return skill1;
        }
        set
        {
            skill1 = value;
        }
    }

    public int Skill2
    {
        get
        {
            return skill2;
        }
        set
        {
            skill2 = value;
        }
    }

    public int Skill3
    {
        get
        {
            return skill3;
        }
        set
        {
            skill3 = value;
        }
    }

    public int Skill4
    {
        get
        {
            return skill4;
        }
        set
        {
            skill4 = value;
        }
    }

    public int Skill5
    {
        get
        {
            return skill5;
        }
        set
        {
            skill5 = value;
        }
    }

    public int Skill6
    {
        get
        {
            return skill6;
        }
        set
        {
            skill6 = value;
        }
    }

    public int Skill7
    {
        get
        {
            return skill7;
        }
        set
        {
            skill7 = value;
        }
    }

    public int Skill8
    {
        get
        {
            return skill8;
        }
        set
        {
            skill8 = value;
        }
    }
    public int Skill9
    {
        get
        {
            return skill9;
        }
        set
        {
            skill9 = value;
        }
    }

    public int Skill10
    {
        get
        {
            return skill10;
        }
        set
        {
            skill10 = value;
        }
    }

    public int Skill11
    {
        get
        {
            return skill11;
        }
        set
        {
            skill11 = value;
        }
    }

    public int Skill12
    {
        get
        {
            return skill12;
        }
        set
        {
            skill12 = value;
        }
    }

    public int Skill13
    {
        get
        {
            return skill13;
        }
        set
        {
            skill13 = value;
        }
    }

    public int Skill14
    {
        get { return skill14; }
        set { skill14 = value; }
    }

    public int Skill15
    {
        get { return skill15; }
        set { skill15 = value; }
    }

    public int Skill16
    {
        get { return skill16; }
        set { skill16 = value; }
    }

    public int Skill17
    {
        get { return skill17; }
        set { skill17 = value; }
    }

    public int Skill18
    {
        get { return skill18; }
        set { skill18 = value; }
    }

    public int Skill19
    {
        get { return skill19; }
        set { skill19 = value; }
    }

    public int Truck1
    {
        get { return truck1; }
        set { truck1 = value; }
    }

    public int Truck2
    {
        get
        {
            return truck2;
        }
        set
        {
            truck2 = value;
        }
    }

    public int Truck3
    {
        get
        {
            return truck3;
        }
        set
        {
            truck3 = value;
        }
    }

    public int Truck4
    {
        get
        {
            return truck4;
        }
        set
        {
            truck4 = value;
        }
    }

    public int Truck5
    {
        get
        {
            return truck5;
        }
        set
        {
            truck5 = value;
        }
    }

    public int Truck6
    {
        get
        {
            return truck6;
        }
        set
        {
            truck6 = value;
        }
    }

    public int Truck7
    {
        get
        {
            return truck7;
        }
        set
        {
            truck7 = value;
        }
    }

    public int Truck8
    {
        get
        {
            return truck8;
        }
        set
        {
            truck8 = value;
        }
    }

    public int Truck9
    {
        get
        {
            return truck9;
        }
        set
        {
            truck9 = value;
        }
    }

    public int Truck10
    {
        get
        {
            return truck10;
        }
        set
        {
            truck10 = value;
        }
    }

    public int Animal1
    {
        get { return animal1; }
        set { animal1 = value; }
    }

    public int Animal2
    {
        get
        {
            return animal2;
        }
        set
        {
            animal2 = value;
        }
    }

    public int Animal3
    {
        get
        {
            return animal3;
        }
        set
        {
            animal3 = value;
        }
    }

    public int Animal4
    {
        get
        {
            return animal4;
        }
        set
        {
            animal4 = value;
        }
    }

    public int Animal5
    {
        get
        {
            return animal5;
        }
        set
        {
            animal5 = value;
        }
    }

    public int Animal6
    {
        get
        {
            return animal6;
        }
        set
        {
            animal6 = value;
        }
    }

    public int Animal7
    {
        get
        {
            return animal7;
        }
        set
        {
            animal7 = value;
        }
    }

    public int Animal8
    {
        get
        {
            return animal8;
        }
        set
        {
            animal8 = value;
        }
    }

    public int Butterfly1
    {
        get
        {
            return butterfly1;
        }
        set
        {
            butterfly1 = value;
        }
    }

    public int Butterfly2
    {
        get
        {
            return butterfly2;
        }
        set
        {
            butterfly2 = value;
        }
    }

    public int Butterfly3
    {
        get
        {
            return butterfly3;
        }
        set
        {
            butterfly3 = value;
        }
    }

    public int Butterfly4
    {
        get
        {
            return butterfly4;
        }
        set
        {
            butterfly4 = value;
        }
    }

    public int Butterfly5
    {
        get
        {
            return butterfly5;
        }
        set
        {
            butterfly5 = value;
        }
    }

    public int Butterfly6
    {
        get
        {
            return butterfly6;
        }
        set
        {
            butterfly6 = value;
        }
    }

    public int Butterfly7
    {
        get
        {
            return butterfly7;
        }
        set
        {
            butterfly7 = value;
        }
    }

    public int Butterfly8
    {
        get
        {
            return butterfly8;
        }
        set
        {
            butterfly8 = value;
        }
    }

    public int Butterfly9
    {
        get
        {
            return butterfly9;
        }
        set
        {
            butterfly9 = value;
        }
    }

    public int Butterfly10
    {
        get
        {
            return butterfly10;
        }
        set
        {
            butterfly10 = value;
        }
    }

    public int Butterfly11
    {
        get
        {
            return butterfly11;
        }
        set
        {
            butterfly11 = value;
        }
    }

    public int Butterfly12
    {
        get
        {
            return butterfly12;
        }
        set
        {
            butterfly12 = value;
        }
    }

    public int Butterfly13
    {
        get
        {
            return butterfly13;
        }
        set
        {
            butterfly13 = value;
        }
    }

    public int Butterfly14
    {
        get
        {
            return butterfly14;
        }
        set
        {
            butterfly14 = value;
        }
    }

    public int Butterfly15
    {
        get
        {
            return butterfly15;
        }
        set
        {
            butterfly15 = value;
        }
    }

    public int Butterfly16
    {
        get
        {
            return butterfly16;
        }
        set
        {
            butterfly16 = value;
        }
    }

    public int Butterfly17
    {
        get
        {
            return butterfly17;
        }
        set
        {
            butterfly17 = value;
        }
    }

    public int Butterfly18
    {
        get
        {
            return butterfly18;
        }
        set
        {
            butterfly18 = value;
        }
    }

    public int Butterfly19
    {
        get
        {
            return butterfly19;
        }
        set
        {
            butterfly19 = value;
        }
    }

    public int Butterfly20
    {
        get
        {
            return butterfly20;
        }
        set
        {
            butterfly20 = value;
        }
    }

    public int Butterfly21
    {
        get
        {
            return butterfly21;
        }
        set
        {
            butterfly21 = value;
        }
    }

    public int Butterfly22
    {
        get
        {
            return butterfly22;
        }
        set
        {
            butterfly22 = value;
        }
    }

    public int Butterfly23
    {
        get
        {
            return butterfly23;
        }
        set
        {
            butterfly23 = value;
        }
    }

    public int Butterfly24
    {
        get
        {
            return butterfly24;
        }
        set
        {
            butterfly24 = value;
        }
    }

    public int Butterfly25
    {
        get
        {
            return butterfly25;
        }
        set
        {
            butterfly25 = value;
        }
    }

    public int Butterfly26
    {
        get
        {
            return butterfly26;
        }
        set
        {
            butterfly26 = value;
        }
    }

    public int Butterfly27
    {
        get
        {
            return butterfly27;
        }
        set
        {
            butterfly27 = value;
        }
    }

    public int Butterfly28
    {
        get
        {
            return butterfly28;
        }
        set
        {
            butterfly28 = value;
        }
    }

    public int Totems1
    {
        get { return totems1; }
        set { totems1 = value; }
    }

    public int Totems2
    {
        get
        {
            return totems2;
        }
        set
        {
            totems2 = value;
        }
    }

    public int Totems3
    {
        get
        {
            return totems3;
        }
        set
        {
            totems3 = value;
        }
    }

    public int Totems4
    {
        get
        {
            return totems4;
        }
        set
        {
            totems4 = value;
        }
    }

    public int Totems5
    {
        get
        {
            return totems5;
        }
        set
        {
            totems5 = value;
        }
    }

    public int Totems6
    {
        get
        {
            return totems6;
        }
        set
        {
            totems6 = value;
        }
    }

    public int Totems7
    {
        get
        {
            return totems7;
        }
        set
        {
            totems7 = value;
        }
    }

    public int Totems8
    {
        get
        {
            return totems8;
        }
        set
        {
            totems8 = value;
        }
    }

    public int Totems9
    {
        get
        {
            return totems9;
        }
        set
        {
            totems9 = value;
        }
    }

    public int Totems10
    {
        get
        {
            return totems10;
        }
        set
        {
            totems10 = value;
        }
    }

    public int Totems11
    {
        get
        {
            return totems11;
        }
        set
        {
            totems11 = value;
        }
    }

    public int Totems12
    {
        get
        {
            return totems12;
        }
        set
        {
            totems12 = value;
        }
    }
    public int Flower1
    {
        get
        {
            return flower1;
        }
        set
        {
            flower1 = value;
        }
    }

    public int Flower2
    {
        get
        {
            return flower2;
        }
        set
        {
            flower2 = value;
        }
    }

    public int Flower3
    {
        get
        {
            return flower3;
        }
        set
        {
            flower3 = value;
        }
    }

    public int Flower4
    {
        get
        {
            return flower4;
        }
        set
        {
            flower4 = value;
        }
    }

    public int Flower5
    {
        get
        {
            return flower5;
        }
        set
        {
            flower5 = value;
        }
    }

    public int Flower6
    {
        get
        {
            return flower6;
        }
        set
        {
            flower6 = value;
        }
    }

    public int Flower7
    {
        get
        {
            return flower7;
        }
        set
        {
            flower7 = value;
        }
    }

    public int[] Bucket
    {
        get
        {
            return bucket;
        }
        set
        {
            bucket = value;
        }
    }

    public int[] Chair
    {
        get
        {
            return chair;
        }
        set
        {
            chair = value;
        }
    }

    public int[] Tube
    {
        get
        {
            return tube;
        }
        set
        {
            tube = value;
        }
    }

    public int[] Surfboard
    {
        get
        {
            return surfboard;
        }
        set
        {
            surfboard = value;
        }
    }

    public int[] Umbrella
    {
        get
        {
            return umbrella;
        }
        set
        {
            umbrella = value;
        }
    }

    public int Island1
    {
        get
        {
            return island1;
        }
        set
        {
            island1 = value;
        }
    }

    public int Island2
    {
        get
        {
            return island2;
        }
        set
        {
            island2 = value;
        }
    }

    public int Island3
    {
        get
        {
            return island3;
        }
        set
        {
            island3 = value;
        }
    }

    public int Island4
    {
        get
        {
            return island4;
        }
        set
        {
            island4 = value;
        }
    }

    public int Island5
    {
        get
        {
            return island5;
        }
        set
        {
            island5 = value;
        }
    }

    public int Island6
    {
        get
        {
            return island6;
        }
        set
        {
            island6 = value;
        }
    }

    public int Island7
    {
        get
        {
            return island7;
        }
        set
        {
            island7 = value;
        }
    }

    public int Island8
    {
        get
        {
            return island8;
        }
        set
        {
            island8 = value;
        }
    }

    public int Island9
    {
        get
        {
            return island9;
        }
        set
        {
            island9 = value;
        }
    }

    public int Island10
    {
        get
        {
            return island10;
        }
        set
        {
            island10 = value;
        }
    }

    public int Portion1
    {
        get
        {
            return portion1;
        }
        set
        {
            portion1 = value;
        }
    }

    public int Portion2
    {
        get
        {
            return portion2;
        }
        set
        {
            portion2 = value;
        }
    }

    public int Portion3
    {
        get
        {
            return portion3;
        }
        set
        {
            portion3 = value;
        }
    }

    public int Portion4
    {
        get
        {
            return portion4;
        }
        set
        {
            portion4 = value;
        }
    }

    public int Portion5
    {
        get
        {
            return portion5;
        }
        set
        {
            portion5 = value;
        }
    }

    public int Portion6
    {
        get
        {
            return portion6;
        }
        set
        {
            portion6 = value;
        }
    }
    public bool Package1
    {
        get
        {
            return package1;
        }
        set
        {
            package1 = value;
        }
    }

    public bool Package2
    {
        get
        {
            return package2;
        }
        set
        {
            package2 = value;
        }
    }

    public bool Package3
    {
        get
        {
            return package3;
        }
        set
        {
            package3 = value;
        }
    }

    public bool Package4
    {
        get
        {
            return package4;
        }
        set
        {
            package4 = value;
        }
    }

    public bool Package5
    {
        get
        {
            return package5;
        }
        set
        {
            package5 = value;
        }
    }

    public bool Package6
    {
        get
        {
            return package6;
        }
        set
        {
            package6 = value;
        }
    }

    public int Package7
    {
        get
        {
            return package7;
        }
        set
        {
            package7 = value;
        }
    }

    public int Package8
    {
        get
        {
            return package8;
        }
        set
        {
            package8 = value;
        }
    }

    public int Package9
    {
        get
        {
            return package9;
        }
        set
        {
            package9 = value;
        }
    }

    public bool Package10
    {
        get
        {
            return package10;
        }
        set
        {
            package10 = value;
        }
    }

    public bool Package11
    {
        get
        {
            return package11;
        }
        set
        {
            package11 = value;
        }
    }

    public bool Package12
    {
        get
        {
            return package12;
        }
        set
        {
            package12 = value;
        }
    }

    public string SeasonPassDay
    {
        get
        {
            return seasonPassDay;
        }
        set
        {
            seasonPassDay = value;
        }
    }

    public string FreeSeasonPassData
    {
        get
        {
            return freeSeasonPassData;
        }
        set
        {
            freeSeasonPassData = value;
        }
    }

    public string PassSeasonPassData
    {
        get
        {
            return passSeasonPassData;
        }
        set
        {
            passSeasonPassData = value;
        }
    }

    public string AttendanceDay
    {
        get
        {
            return attendanceDay;
        }
        set
        {
            attendanceDay = value;
        }
    }

    public int AttendanceCount
    {
        get
        {
            return attendanceCount;
        }
        set
        {
            attendanceCount = value;
        }
    }

    public int PlayTimeCount
    {
        get
        {
            return playTimeCount;
        }
        set
        {
            playTimeCount = value;
        }
    }

    public int RankEventCount
    {
        get
        {
            return rankEventCount;
        }
        set
        {
            rankEventCount = value;
        }
    }

    public int RecipeEventCount
    {
        get
        {
            return recipeEventCount;
        }
        set
        {
            recipeEventCount = value;
        }
    }

    public int LevelUpEventCount
    {
        get
        {
            return levelUpEventCount;
        }
        set
        {
            levelUpEventCount = value;
        }
    }

    public bool AttendanceCheck
    {
        get
        {
            return attendanceCheck;
        }
        set
        {
            attendanceCheck = value;
        }
    }

    public bool WelcomeCheck
    {
        get
        {
            return welcomeCheck;
        }
        set
        {
            welcomeCheck = value;
        }
    }

    public int WelcomeCount
    {
        get
        {
            return welcomeCount;
        }
        set
        {
            welcomeCount = value;
        }
    }

    public string NextMonday
    {
        get
        {
            return nextMonday;
        }
        set
        {
            nextMonday = value;
        }
    }

    public bool AppReview
    {
        get
        {
            return appReview;
        }
        set
        {
            appReview = value;
        }
    }

    public bool RemoveAds
    {
        get
        {
            return removeAds;
        }
        set
        {
            removeAds = value;
        }
    }

    public bool GoldX2
    {
        get
        {
            return goldX2;
        }
        set
        {
            goldX2 = value;
        }
    }

    public bool SuperOffline
    {
        get
        {
            return superOffline;
        }
        set
        {
            superOffline = value;
        }
    }
    public bool AutoUpgrade
    {
        get
        {
            return autoUpgrade;
        }
        set
        {
            autoUpgrade = value;
        }
    }

    public bool AutoPresent
    {
        get
        {
            return autoPresent;
        }
        set
        {
            autoPresent = value;
        }
    }

    public bool SuperExp
    {
        get
        {
            return superExp;
        }
        set
        {
            superExp = value;
        }
    }

    public bool SuperKitchen
    {
        get
        {
            return superKitchen;
        }
        set
        {
            superKitchen = value;
        }
    }

    public void Initialize()
    {
        removeAds = false;
        goldX2 = false;
        superOffline = false;
        autoUpgrade = false;
        autoPresent = false;
        superExp = false;
        superKitchen = false;
        seasonPass = false;

        //island_Total_Data = new Island_Total_Data();
        island_Total_Data.Initialize();

        rankLevel1 = 0;
        rankLevel2 = 0;
        rankLevel3 = 0;
        rankLevel4 = 0;
        rankLevel5 = 0;
        rankLevel6 = 0;
        rankLevel7 = 0;
        rankLevel8 = 0;
        rankLevel9 = 0;
        rankLevel10 = 0;

        totalLevel = 0;
        totalLevel_1 = 0;
        totalLevel_2 = 0;
        totalLevel_3 = 0;
        totalLevel_4 = 0;
        totalLevel_5 = 0;
        totalLevel_6 = 0;
        totalLevel_7 = 0;
        totalLevel_8 = 0;
        totalLevel_9 = 0;
        totalLevel_10 = 0;
        totalLevel_11 = 0;
        totalLevel_12 = 0;
        totalLevel_13 = 0;
        totalLevel_14 = 0;
        totalLevel_15 = 0;
        totalLevel_16 = 0;
        totalLevel_17 = 0;
        totalLevel_18 = 0;
        totalLevel_19 = 0;
        totalLevel_20 = 0;

        saveCoin = 0;
        coin = 0;
        coinA = 0;
        coinB = 0;
        crystal = 0;
        rankPoint = 0;
        defDestroyTicket = 0;
        defDestroyTicketPiece = 0;
        islandReward = 0;

        eventTicket = 0;
        eventTicketCount = 0;
        eventEnter1 = 0;
        eventEnter2 = 0;
        eventEnter3 = 0;
        eventEnter4 = 0;
        eventNumber = 0;
        updateNumber = 0;
        friendsNumber = 0;
        reviewNumber = 0;
        openKakao = 0;
        lockTutorial = 0;
        inGameTutorial = 0;
        icon = 0;
        firstReward = 0;
        firstDate = "";
        firstServerDate = "";
        islandNumber = 0;
        islandNumber_Ranking = 0;
        testAccount = 0;
        update = 0;
        buffTicket = 0;
        skillTicket = 0;
        recoverTicket = 0;
        proficiency = 0;
        level = 0;
        exp = 0;
        playTime = 0;
        advancement = 0;
        challengePoint = 0;
        adCount = 0;
        treasureCount = 0;
        accessDate = 0;
        castleLevel = 0;
        castleDate = "";
        castleServerDate = "";

        resetInfo = new ResetInfo();
        equip.Initialize();
        seasonRewardInfo.Initialize();
        couponInfo.Initialize();

        nextFoodNumber = 0;

        treasure1 = 0;
        treasure2 = 0;
        treasure3 = 0;
        treasure4 = 0;
        treasure5 = 0;
        treasure6 = 0;
        treasure7 = 0;
        treasure8 = 0;
        treasure9 = 0;
        treasure10 = 0;
        treasure11 = 0;
        treasure12 = 0;
        treasure13 = 0;
        treasure14 = 0;
        treasure15 = 0;

        treasure1Count = 0;
        treasure2Count = 0;
        treasure3Count = 0;
        treasure4Count = 0;
        treasure5Count = 0;
        treasure6Count = 0;
        treasure7Count = 0;
        treasure8Count = 0;
        treasure9Count = 0;
        treasure10Count = 0;
        treasure11Count = 0;
        treasure12Count = 0;
        treasure13Count = 0;
        treasure14Count = 0;
        treasure15Count = 0;

        character1 = 1;
        character2 = 0;
        character3 = 0;
        character4 = 0;
        character5 = 0;
        character6 = 0;
        character7 = 0;
        character8 = 0;
        character9 = 0;
        character10 = 0;
        character11 = 0;
        character12 = 0;
        character13 = 0;
        character14 = 0;
        character15 = 0;
        character16 = 0;
        character17 = 0;
        character18 = 0;
        character19 = 0;
        character20 = 0;
        character21 = 0;

        truck1 = 1;
        truck2 = 0;
        truck3 = 0;
        truck4 = 0;
        truck5 = 0;
        truck6 = 0;
        truck7 = 0;
        truck8 = 0;
        truck9 = 0;
        truck10 = 0;

        animal1 = 1;
        animal2 = 0;
        animal3 = 0;
        animal4 = 0;
        animal5 = 0;
        animal6 = 0;
        animal7 = 0;
        animal8 = 0;

        butterfly1 = 1;
        butterfly2 = 0;
        butterfly3 = 0;
        butterfly4 = 0;
        butterfly5 = 0;
        butterfly6 = 0;
        butterfly7 = 0;
        butterfly8 = 0;
        butterfly9 = 0;
        butterfly10 = 0;
        butterfly11 = 0;
        butterfly12 = 0;
        butterfly13 = 0;
        butterfly14 = 0;
        butterfly15 = 0;
        butterfly16 = 0;
        butterfly17 = 0;
        butterfly18 = 0;
        butterfly19 = 0;
        butterfly20 = 0;
        butterfly21 = 0;
        butterfly22 = 0;
        butterfly23 = 0;
        butterfly24 = 0;
        butterfly25 = 0;
        butterfly26 = 0;
        butterfly27 = 0;
        butterfly28 = 0;

        totems1 = 1;
        totems2 = 0;
        totems3 = 0;
        totems4 = 0;
        totems5 = 0;
        totems6 = 0;
        totems7 = 0;
        totems8 = 0;
        totems9 = 0;
        totems10 = 0;
        totems11 = 0;
        totems12 = 0;

        flower1 = 1;
        flower2 = 0;
        flower3 = 0;
        flower4 = 0;
        flower5 = 0;
        flower6 = 0;
        flower7 = 0;

        for(int i = 0; i < 10; i ++)
        {
            bucket[i] = 0;
            chair[i] = 0;
            tube[i] = 0;
            surfboard[i] = 0;
            umbrella[i] = 0;
        }

        bucket[0] = 1;
        chair[0] = 1;
        tube[0] = 1;
        surfboard[0] = 1;
        umbrella[0] = 1;

        characterItemInstance.Clear();
        truckItemInstance.Clear();
        animalItemInstance.Clear();
        butterflyItemInstance.Clear();
        totemsItemInstance.Clear();
        bucketItemInstance.Clear();
        chairItemInstance.Clear();
        tubeItemInstance.Clear();
        surfboardItemInstance.Clear();
        umbrellaItemInstance.Clear();

        for (int i = 0; i < System.Enum.GetValues(typeof(CharacterType)).Length; i++)
        {
            ShopClass shopClass = new ShopClass();
            characterItemInstance.Add(shopClass);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(TruckType)).Length; i++)
        {
            ShopClass shopClass = new ShopClass();
            truckItemInstance.Add(shopClass);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(AnimalType)).Length; i++)
        {
            ShopClass shopClass = new ShopClass();
            animalItemInstance.Add(shopClass);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(ButterflyType)).Length; i++)
        {
            ShopClass shopClass = new ShopClass();
            butterflyItemInstance.Add(shopClass);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(TotemsType)).Length; i++)
        {
            ShopClass shopClass = new ShopClass();
            totemsItemInstance.Add(shopClass);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(BucketType)).Length; i++)
        {
            ShopClass shopClass = new ShopClass();
            bucketItemInstance.Add(shopClass);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(ChairType)).Length; i++)
        {
            ShopClass shopClass = new ShopClass();
            chairItemInstance.Add(shopClass);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(TubeType)).Length; i++)
        {
            ShopClass shopClass = new ShopClass();
            tubeItemInstance.Add(shopClass);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(SurfboardType)).Length; i++)
        {
            ShopClass shopClass = new ShopClass();
            surfboardItemInstance.Add(shopClass);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(UmbrellaType)).Length; i++)
        {
            ShopClass shopClass = new ShopClass();
            umbrellaItemInstance.Add(shopClass);
        }

        iconList.Clear();

        for (int i = 0; i < System.Enum.GetValues(typeof(IconType)).Length; i++)
        {
            IconClass iconClass = new IconClass();
            IconType iconType = IconType.Icon_1 + i;
            iconClass.iconType = iconType;
            iconClass.count = 0;
            iconList.Add(iconClass);
        }

        island1 = 0;
        island2 = 0;
        island3 = 0;
        island4 = 0;
        island5 = 0;
        island6 = 0;
        island7 = 0;
        island8 = 0;
        island9 = 0;
        island10 = 0;

        questCount = 0;
        reincarnationCount = 0;
        buffCount = 0;
        getGold = 0;
        consumeGold = 0;
        buyCrystal = 0;
        gourmetLevel = 0;
        changeNicknameCount = 0;

        useSauce1 = 0;
        useSauce2 = 0;
        useSauce3 = 0;
        useSauce4 = 0;
        useSauce5 = 0;

        abilityPoint = 0;
        abilityLevel = 0;

        upgradeCount = 0;
        sellCount = 0;
        useSauceCount = 0;
        openChestBox = 0;
        yummyTimeCount = 0;
        dungeon1Count = 0;
        dungeon2Count = 0;
        dungeon3Count = 0;
        dungeon4Count = 0;
        offlineCount = 0;
        defDestroyCount = 0;
        recoverCount = 0;
        equipCount = 0;

        guideIndex = 0;

        gender = 0;

        dungeonKey1 = 0;
        dungeonKey2 = 0;
        dungeonKey3 = 0;
        dungeonKey4 = 0;

        dungeon1Level = 0;
        dungeon2Level = 0;
        dungeon3Level = 0;
        dungeon4Level = 0;

        skill1 = 0;
        skill2 = 0;
        skill3 = 0;
        skill4 = 0;
        skill5 = 0;
        skill6 = 0;
        skill7 = 0;
        skill8 = 0;
        skill9 = 0;
        skill10 = 0;
        skill11 = 0;
        skill12 = 0;
        skill13 = 0;
        skill14 = 0;
        skill15 = 0;
        skill16 = 0;
        skill17 = 0;
        skill18 = 0;
        skill19 = 0;

        portion1 = 0;
        portion2 = 0;
        portion3 = 0;
        portion4 = 0;
        portion5 = 0;
        portion6 = 0;

        package1 = false;
        package2 = false;
        package3 = false;
        package4 = false;
        package5 = false;
        package6 = false;
        package7 = 0;
        package8 = 0;
        package9 = 0;
        package10 = false;
        package11 = false;
        package12 = false;

        seasonPassDay = "";
        seasonPassLevel = 0;
        freeSeasonPassData = "000000000000000000000000000000";
        passSeasonPassData = "000000000000000000000000000000";
        attendanceDay = "";
        attendanceCount = 0;
        attendanceCheck = false;
        playTimeCount = 0;
        rankEventCount = 0;
        recipeEventCount = 0;
        levelUpEventCount = 0;
        welcomeCount = 0;
        welcomeCheck = false;
        nextMonday = "";
    }

    public bool CheckFoodTruck(TruckType type)
    {
        bool check = false;

        switch (type)
        {
            case TruckType.Truck1:
                check = true;
                break;
            case TruckType.Truck2:
                if(Truck2 > 0)
                {
                    check = true;
                }
                break;
            case TruckType.Truck3:
                if (Truck3 > 0)
                {
                    check = true;
                }
                break;
            case TruckType.Truck4:
                if (Truck4 > 0)
                {
                    check = true;
                }
                break;
            case TruckType.Truck5:
                if (Truck6 > 0)
                {
                    check = true;
                }
                break;
            case TruckType.Truck6:
                if (Truck7 > 0)
                {
                    check = true;
                }
                break;
            case TruckType.Truck7:
                if (Truck8 > 0)
                {
                    check = true;
                }
                break;
        }

        return check;
    }

    public int GetTruckNumber()
    {
        int count = 0;

        if(Truck2 > 0)
        {
            count += 1;
        }

        if (Truck3 > 0)
        {
            count += 1;
        }

        if (Truck4 > 0)
        {
            count += 1;
        }

        if (Truck6 > 0)
        {
            count += 1;
        }

        if (Truck7 > 0)
        {
            count += 1;
        }

        if (Truck8 > 0)
        {
            count += 1;
        }

        if (Truck9 > 0)
        {
            count += 1;
        }

        if (Truck10 > 0)
        {
            count += 1;
        }

        return count;
    }

    public int GetAnimalNumber()
    {
        int count = 0;

        if(Animal2 > 0)
        {
            count += 1;
        }

        if(Animal3 > 0)
        {
            count += 1;
        }

        if(Animal4 > 0)
        {
            count += 1;
        }

        if(Animal5 > 0)
        {
            count += 1;
        }

        if(Animal6 > 0)
        {
            count += 1;
        }

        if(Animal7 > 0)
        {
            count += 1;
        }
        
        if(Animal8 > 0)
        {
            count += 1;
        }

        return count;
    }

    public int GetButterflyNumber()
    {
        int count = 0;

        if (butterfly2 > 0)
        {
            count += 1;
        }

        if (butterfly3 > 0)
        {
            count += 1;
        }

        if (butterfly4 > 0)
        {
            count += 1;
        }

        if (butterfly5 > 0)
        {
            count += 1;
        }

        if (butterfly6 > 0)
        {
            count += 1;
        }

        if (butterfly7 > 0)
        {
            count += 1;
        }

        if (butterfly8 > 0)
        {
            count += 1;
        }

        if (butterfly9 > 0)
        {
            count += 1;
        }

        if (butterfly10 > 0)
        {
            count += 1;
        }

        if (butterfly11 > 0)
        {
            count += 1;
        }

        if (butterfly12 > 0)
        {
            count += 1;
        }

        if (butterfly13 > 0)
        {
            count += 1;
        }

        if (butterfly14 > 0)
        {
            count += 1;
        }

        if (butterfly15 > 0)
        {
            count += 1;
        }

        if (butterfly16 > 0)
        {
            count += 1;
        }

        if (butterfly17 > 0)
        {
            count += 1;
        }

        if (butterfly18 > 0)
        {
            count += 1;
        }

        if (butterfly19 > 0)
        {
            count += 1;
        }

        if (butterfly20 > 0)
        {
            count += 1;
        }

        if (butterfly21 > 0)
        {
            count += 1;
        }

        if (butterfly22 > 0)
        {
            count += 1;
        }

        if (butterfly23 > 0)
        {
            count += 1;
        }

        if (butterfly24 > 0)
        {
            count += 1;
        }
        if (butterfly25 > 0)
        {
            count += 1;
        }

        if (butterfly26 > 0)
        {
            count += 1;
        }
        if (butterfly27 > 0)
        {
            count += 1;
        }

        if (butterfly28 > 0)
        {
            count += 1;
        }

        return count;
    }

    public int GetCharacterNumber()
    {
        int count = 0;

        if (character2 > 0)
        {
            count += 1;
        }

        if (character3 > 0)
        {
            count += 1;
        }

        if (character4 > 0)
        {
            count += 1;
        }

        if (character5 > 0)
        {
            count += 1;
        }

        if (character6 > 0)
        {
            count += 1;
        }

        if (character7 > 0)
        {
            count += 1;
        }

        if (character8 > 0)
        {
            count += 1;
        }

        if (character9 > 0)
        {
            count += 1;
        }

        if (character10 > 0)
        {
            count += 1;
        }

        if (character11 > 0)
        {
            count += 1;
        }

        if (character12 > 0)
        {
            count += 1;
        }

        if (character13 > 0)
        {
            count += 1;
        }

        if (character14 > 0)
        {
            count += 1;
        }

        if (character15 > 0)
        {
            count += 1;
        }

        if (character16 > 0)
        {
            count += 1;
        }

        if (character17 > 0)
        {
            count += 1;
        }

        if (character18 > 0)
        {
            count += 1;
        }

        if (character19 > 0)
        {
            count += 1;
        }

        if (character20 > 0)
        {
            count += 1;
        }

        if (character21 > 0)
        {
            count += 1;
        }

        return count;
    }

    public int GetTotemsNumber()
    {
        int count = 0;

        if (totems2 > 0)
        {
            count += 1;
        }

        if (totems3 > 0)
        {
            count += 1;
        }

        if (totems4 > 0)
        {
            count += 1;
        }

        if (totems5 > 0)
        {
            count += 1;
        }

        if (totems6 > 0)
        {
            count += 1;
        }

        if (totems7 > 0)
        {
            count += 1;
        }

        if (totems8 > 0)
        {
            count += 1;
        }

        if (totems9 > 0)
        {
            count += 1;
        }

        if (totems10 > 0)
        {
            count += 1;
        }

        if (totems11 > 0)
        {
            count += 1;
        }

        if (totems12 > 0)
        {
            count += 1;
        }

        return count;
    }

    public int GetBucketNumber()
    {
        int count = 0;

        for(int i = 0; i < bucket.Length; i++)
        {
            count += bucket[i];
        }

        return count - 1;
    }

    public int GetChairNumber()
    {
        int count = 0;

        for (int i = 0; i < chair.Length; i++)
        {
            count += chair[i];
        }

        return count - 1;
    }

    public int GetTubeNumber()
    {
        int count = 0;

        for (int i = 0; i < tube.Length; i++)
        {
            count += tube[i];
        }

        return count - 1;
    }

    public int GetSurfboardNumber()
    {
        int count = 0;

        for (int i = 0; i < surfboard.Length; i++)
        {
            count += surfboard[i];
        }

        return count - 1;
    }

    public int GetUmbrellaNumber()
    {
        int count = 0;

        for (int i = 0; i < umbrella.Length; i++)
        {
            count += umbrella[i];
        }

        return count - 1;
    }



    public int GetTruck_Total_AbilityLevel()
    {
        int number = 0;

        for(int i = 0; i < truckItemInstance.Count; i ++)
        {
            number += truckItemInstance[i].abilityLevel;
        }

        return number;
    }

    public int GetAnimal_Total_AbilityLevel()
    {
        int number = 0;

        for (int i = 0; i < animalItemInstance.Count; i++)
        {
            number += animalItemInstance[i].abilityLevel;
        }

        return number;
    }

    public int GetCharacter_Total_AbilityLevel()
    {
        int number = 0;

        for (int i = 0; i < characterItemInstance.Count; i++)
        {
            number += characterItemInstance[i].abilityLevel;
        }

        return number;
    }

    public int GetButterfly_Total_AbilityLevel()
    {
        int number = 0;

        for (int i = 0; i < butterflyItemInstance.Count; i++)
        {
            number += butterflyItemInstance[i].abilityLevel;
        }

        return number;
    }

    public int GetTotems_Total_AbilityLevel()
    {
        int number = 0;

        for (int i = 0; i < totemsItemInstance.Count; i++)
        {
            number += totemsItemInstance[i].abilityLevel;
        }

        return number;
    }

    public int GetBucket_Total_AbilityLevel()
    {
        int number = 0;

        for (int i = 0; i < bucketItemInstance.Count; i++)
        {
            number += bucketItemInstance[i].abilityLevel;
        }

        return number;
    }

    public int GetChair_Total_AbilityLevel()
    {
        int number = 0;

        for (int i = 0; i < chairItemInstance.Count; i++)
        {
            number += chairItemInstance[i].abilityLevel;
        }

        return number;
    }

    public int GetTube_Total_AbilityLevel()
    {
        int number = 0;

        for (int i = 0; i < tubeItemInstance.Count; i++)
        {
            number += tubeItemInstance[i].abilityLevel;
        }

        return number;
    }

    public int GetSurfboard_Total_AbilityLevel()
    {
        int number = 0;

        for (int i = 0; i < surfboardItemInstance.Count; i++)
        {
            number += surfboardItemInstance[i].abilityLevel;
        }

        return number;
    }

    public int GetUmbrella_Total_AbilityLevel()
    {
        int number = 0;

        for (int i = 0; i < umbrellaItemInstance.Count; i++)
        {
            number += umbrellaItemInstance[i].abilityLevel;
        }

        return number;
    }



    public int GetTruck_AbilityLevel(int number)
    {
        return truckItemInstance[number].abilityLevel;
    }

    public int GetAnimal_AbilityLevel(int number)
    {
        return animalItemInstance[number].abilityLevel;
    }

    public int GetCharacter_AbilityLevel(int number)
    {
        return characterItemInstance[number].abilityLevel;
    }

    public int GetButterfly_AbilityLevel(int number)
    {
        return butterflyItemInstance[number].abilityLevel;
    }

    public int GetTotems_AbilityLevel(int number)
    {
        return totemsItemInstance[number].abilityLevel;
    }

    public int GetBucket_AbilityLevel(int number)
    {
        return bucketItemInstance[number].abilityLevel;
    }

    public int GetChair_AbilityLevel(int number)
    {
        return chairItemInstance[number].abilityLevel;
    }

    public int GetTube_AbilityLevel(int number)
    {
        return tubeItemInstance[number].abilityLevel;
    }

    public int GetSurfboard_AbilityLevel(int number)
    {
        return surfboardItemInstance[number].abilityLevel;
    }

    public int GetUmbrella_AbilityLevel(int number)
    {
        return umbrellaItemInstance[number].abilityLevel;
    }



    public int GetTruck_Rare(int number)
    {
        return truckItemInstance[number].rare;
    }

    public int GetAnimal_Rare(int number)
    {
        return animalItemInstance[number].rare;
    }

    public int GetCharacter_Rare(int number)
    {
        return characterItemInstance[number].rare;
    }

    public int GetButterfly_Rare(int number)
    {
        return butterflyItemInstance[number].rare;
    }

    public int GetTotems_Rare(int number)
    {
        return totemsItemInstance[number].rare;
    }

    public int GetBucket_Rare(int number)
    {
        return bucketItemInstance[number].rare;
    }

    public int GetChair_Rare(int number)
    {
        return chairItemInstance[number].rare;
    }

    public int GetTube_Rare(int number)
    {
        return tubeItemInstance[number].rare;
    }

    public int GetSurfboard_Rare(int number)
    {
        return surfboardItemInstance[number].rare;
    }

    public int GetUmbrella_Rare(int number)
    {
        return umbrellaItemInstance[number].rare;
    }



    public int GetTruckHighNumber()
    {
        int count = 0;

        if (Truck2 > 0)
        {
            count = 1;
        }

        if (Truck3 > 0)
        {
            count = 2;
        }

        if (Truck4 > 0)
        {
            count = 3;
        }

        if (Truck6 > 0)
        {
            count = 4;
        }

        if (Truck7 > 0)
        {
            count = 5;
        }

        if (Truck8 > 0)
        {
            count = 6;
        }

        if (Truck9 > 0)
        {
            count = 7;
        }

        if (Truck10 > 0)
        {
            count = 8;
        }

        return count;
    }

    public int GetAnimalHighNumber()
    {
        int count = 0;

        if (Animal2 > 0)
        {
            count = 1;
        }

        if (Animal3 > 0)
        {
            count = 2;
        }

        if (Animal4 > 0)
        {
            count = 3;
        }

        if (Animal5 > 0)
        {
            count = 4;
        }

        if (Animal6 > 0)
        {
            count = 5;
        }

        if (Animal7 > 0)
        {
            count = 6;
        }

        if (Animal8 > 0)
        {
            count = 7;
        }

        return count;
    }

    public int GetButterflyHighNumber()
    {
        int count = 0;

        if (butterfly2 > 0)
        {
            count = 1;
        }

        if (butterfly3 > 0)
        {
            count = 2;
        }

        if (butterfly4 > 0)
        {
            count = 3;
        }

        if (butterfly5 > 0)
        {
            count = 4;
        }

        if (butterfly6 > 0)
        {
            count = 5;
        }

        if (butterfly7 > 0)
        {
            count = 6;
        }

        if (butterfly8 > 0)
        {
            count = 7;
        }

        if (butterfly9 > 0)
        {
            count = 8;
        }

        if (butterfly10 > 0)
        {
            count = 9;
        }

        if (butterfly11 > 0)
        {
            count = 10;
        }

        if (butterfly12 > 0)
        {
            count = 11;
        }

        if (butterfly13 > 0)
        {
            count = 12;
        }

        if (butterfly14 > 0)
        {
            count = 13;
        }

        if (butterfly15 > 0)
        {
            count = 14;
        }

        if (butterfly16 > 0)
        {
            count = 15;
        }

        if (butterfly17 > 0)
        {
            count = 16;
        }

        if (butterfly18 > 0)
        {
            count = 17;
        }

        if (butterfly19 > 0)
        {
            count = 18;
        }

        if (butterfly20 > 0)
        {
            count = 19;
        }

        if (butterfly21 > 0)
        {
            count = 20;
        }

        if (butterfly22 > 0)
        {
            count = 21;
        }

        if (butterfly23 > 0)
        {
            count = 22;
        }

        if (butterfly24 > 0)
        {
            count = 23;
        }

        if (butterfly25 > 0)
        {
            count = 24;
        }

        if (butterfly26 > 0)
        {
            count = 25;
        }

        if (butterfly27 > 0)
        {
            count = 26;
        }

        if (butterfly28 > 0)
        {
            count = 27;
        }

        return count;
    }

    public int GetCharacterHighNumber()
    {
        int count = 0;

        if (character2 > 0)
        {
            count = 1;
        }

        if (character3 > 0)
        {
            count = 2;
        }

        if (character4 > 0 || character21 > 0)
        {
            count = 3;
        }

        if (character5 > 0)
        {
            count = 4;
        }

        if (character6 > 0)
        {
            count = 5;
        }

        if (character7 > 0)
        {
            count = 6;
        }

        if (character8 > 0)
        {
            count = 7;
        }

        if (character9 > 0)
        {
            count = 8;
        }

        if (character10 > 0)
        {
            count = 9;
        }

        if (character11 > 0)
        {
            count = 10;
        }

        if (character12 > 0)
        {
            count = 11;
        }

        if (character13 > 0)
        {
            count = 12;
        }

        if (character14 > 0)
        {
            count = 13;
        }

        if (character15 > 0)
        {
            count = 14;
        }

        if (character16 > 0)
        {
            count = 15;
        }

        if (character17 > 0)
        {
            count = 16;
        }

        if (character18 > 0)
        {
            count = 17;
        }

        if (character19 > 0)
        {
            count = 18;
        }

        if (character20 > 0)
        {
            count = 19;
        }

        return count;
    }

    public int GetTotemsHighNumber()
    {
        int count = 0;

        if (totems2 > 0)
        {
            count = 1;
        }

        if (totems3 > 0)
        {
            count = 2;
        }

        if (totems4 > 0)
        {
            count = 3;
        }

        if (totems5 > 0)
        {
            count = 4;
        }

        if (totems6 > 0)
        {
            count = 5;
        }

        if (totems7 > 0)
        {
            count = 6;
        }

        if (totems8 > 0)
        {
            count = 7;
        }

        if (totems9 > 0)
        {
            count = 8;
        }

        if (totems10 > 0)
        {
            count = 9;
        }

        if (totems11 > 0)
        {
            count = 10;
        }

        if (totems12 > 0)
        {
            count = 11;
        }

        return count;
    }

    public int GetBucketHighNumber()
    {
        int count = 0;

        for(int i = 0; i < bucket.Length; i ++)
        {
            if(bucket[i] > 0)
            {
                count = i;
            }
        }

        return count;
    }

    public int GetChairHighNumber()
    {
        int count = 0;

        for (int i = 0; i < chair.Length; i++)
        {
            if (chair[i] > 0)
            {
                count = i;
            }
        }

        return count;
    }

    public int GetTubeHighNumber()
    {
        int count = 0;

        for (int i = 0; i < tube.Length; i++)
        {
            if (tube[i] > 0)
            {
                count = i;
            }
        }

        return count;
    }

    public int GetSurfboardHighNumber()
    {
        int count = 0;

        for (int i = 0; i < surfboard.Length; i++)
        {
            if (surfboard[i] > 0)
            {
                count = i;
            }
        }

        return count;
    }

    public int GetUmbrellaHighNumber()
    {
        int count = 0;

        for (int i = 0; i < umbrella.Length; i++)
        {
            if (umbrella[i] > 0)
            {
                count = i;
            }
        }

        return count;
    }


    public int GetRecipeUpgradeCount()
    {
        int number = 0;

        number += Skill1;
        number += Skill2;
        number += Skill3;
        number += Skill4;
        number += Skill5;
        number += Skill6;
        number += Skill7;
        number += Skill8;
        number += Skill9;
        number += Skill10;
        number += Skill11;
        number += Skill12;
        number += Skill13;
        number += Skill14;
        number += Skill15;
        number += Skill16;
        number += Skill17;
        number += Skill18;
        number += Skill19;

        return number;
    }

    public void SetIcon(IconType type, int number)
    {
        for (int i = 0; i < iconList.Count; i++)
        {
            if (iconList[i].iconType.Equals(type))
            {
                iconList[i].count = number;
                break;
            }
        }
    }

    public bool CheckIcon(IconType type)
    {
        bool check = false;

        for (int i = 0; i < iconList.Count; i++)
        {
            if (iconList[i].iconType.Equals(type))
            {
                if(iconList[i].count > 0)
                {
                    check = true;
                    break;
                }
            }
        }

        return check;
    }

    public IconClass GetIconState(IconType type)
    {
        IconClass iconClass = new IconClass();
        for (int i = 0; i < iconList.Count; i++)
        {
            if (iconList[i].iconType.Equals(type))
            {
                iconClass = iconList[i];
            }
        }

        return iconClass;
    }

    public int GetIconHoldNumber()
    {
        int number = 0;

        for (int i = 0; i < iconList.Count; i++)
        {
            if (iconList[i].count >= 1)
            {
                number++;
            }
        }
        return number;
    }

    public int GetNormalBookNumber()
    {
        int number = 0;

        for (int i = 0; i < island_Total_Data.island_Max_Datas.Length - 1; i++)
        {
            if (island_Total_Data.island_Max_Datas[i] != null)
            {
                number += island_Total_Data.island_Max_Datas[i].GetBookValue();
            }
        }

        return number;
    }

    public int GetEpicBookNumber()
    {
        int number = 0;

        for(int i = 0; i < island_Total_Data.island_Rare_Datas.Length - 1; i ++)
        {
            if (island_Total_Data.island_Rare_Datas[i] != null)
            {
                number += island_Total_Data.island_Rare_Datas[i].GetBookValue();
            }
        }

        return number;
    }

    public void SetItemInstance(ItemInstance item, int index, int number)
    {
        ShopClass shopClass = new ShopClass();

        shopClass.instanceId = item.ItemInstanceId;
        shopClass.abilityLevel = 0;
        shopClass.rare = 0;
        shopClass.level = 0;

        if (item.CustomData != null)
        {
            if(item.CustomData.ContainsKey("AbilityLevel"))
            {
                shopClass.abilityLevel = int.Parse(item.CustomData["AbilityLevel"]);
            }

            if (item.CustomData.ContainsKey("Rare"))
            {
                shopClass.rare = int.Parse(item.CustomData["Rare"]);
            }

            if (item.CustomData.ContainsKey("Level"))
            {
                shopClass.level = int.Parse(item.CustomData["Level"]);
            }
        }

        switch(index)
        {
            case 0:
                characterItemInstance[number] = shopClass;
                break;
            case 1:
                truckItemInstance[number] = shopClass;
                break;
            case 2:
                animalItemInstance[number] = shopClass;
                break;
            case 3:
                butterflyItemInstance[number] = shopClass;
                break;
            case 4:
                totemsItemInstance[number] = shopClass;
                break;
            case 6:
                bucketItemInstance[number] = shopClass;
                break;
            case 7:
                chairItemInstance[number] = shopClass;
                break;
            case 8:
                tubeItemInstance[number] = shopClass;
                break;
            case 9:
                surfboardItemInstance[number] = shopClass;
                break;
            case 10:
                umbrellaItemInstance[number] = shopClass;
                break;
        }
    }



    public float GetEquipValue(EquipType equipType)
    {
        float value = 0;

        for (int i = 0; i < equip.equipInfos.Length; i++)
        {
            if (equip.equipInfos[i] != null)
            {
                if (equip.equipInfos[i].option1 == (int)equipType + 1)
                {
                    value += equip.equipInfos[i].option1_Value;
                }

                if (equip.equipInfos[i].option2 == (int)equipType + 1)
                {
                    value += equip.equipInfos[i].option2_Value;
                }

                if (equip.equipInfos[i].option3 == (int)equipType + 1)
                {
                    value += equip.equipInfos[i].option3_Value;
                }

                if (equip.equipInfos[i].option4 == (int)equipType + 1)
                {
                    value += equip.equipInfos[i].option4_Value;
                }
            }
        }
        return value;
    }

    #region Progress
    public void SaveServerToSeasonPass(SeasonPassType type, string str)
    {
        switch (type)
        {
            case SeasonPassType.Free:
                freeSeasonPassData = str;
                break;
            case SeasonPassType.Pass:
                passSeasonPassData = str;
                break;
        }
    }

    public bool GetSeasonPass(SeasonPassType type, int number)
    {
        bool check = false;
        switch (type)
        {
            case SeasonPassType.Free:
                if (freeSeasonPassData.Substring(number, 1).Equals("1")) check = true;
                break;
            case SeasonPassType.Pass:
                if (passSeasonPassData.Substring(number, 1).Equals("1")) check = true;
                break;
        }
        return check;
    }

    public void UpdateSeasonPass(SeasonPassType type, int number)
    {
        switch (type)
        {
            case SeasonPassType.Free:
                freeSeasonPassData = freeSeasonPassData.ReplaceAt(number, char.Parse("1"));
                break;
            case SeasonPassType.Pass:
                passSeasonPassData = passSeasonPassData.ReplaceAt(number, char.Parse("1"));
                break;
        }
    }
    #endregion
}