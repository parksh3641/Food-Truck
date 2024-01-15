using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataBase", menuName = "ScriptableObjects/PlayerDataBase")]
public class PlayerDataBase : ScriptableObject
{
    [Title("Player")]
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
    private int firstReward = 0;
    [SerializeField]
    private string firstDate = "";
    [SerializeField]
    private string firstServerDate = "";
    [SerializeField]
    private int lockTutorial = 0;
    [SerializeField]
    private int inGameTutorial = 0;
    [SerializeField]
    private int eventNumber = 0;
    [SerializeField]
    private int testAccount = 0;

    [Title("Info")]
    [SerializeField]
    private int level = 0;
    [SerializeField]
    private int exp = 0;
    [SerializeField]
    private int playTime = 0;
    [SerializeField]
    private int getGold = 0;
    [SerializeField]
    private int consumeGold = 0;
    [SerializeField]
    private int buyCrystal = 0;
    [SerializeField]
    private int gourmetLevel = 0;
    [SerializeField]
    private int islandNumber = 0;
    [SerializeField]
    private int buffTicket = 0;
    [SerializeField]
    private int skillTicket = 0;
    [SerializeField]
    private int recoverTicket = 0;
    [SerializeField]
    private int proficiency = 0;
    [SerializeField]
    private int michelin = 0;
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

    [Space]
    [Title("Daily")]
    [SerializeField]
    private int dailyReward = 0;
    [SerializeField]
    private int dailyReward_Portion = 0;
    [SerializeField]
    private int dailyReward_DefTicket = 0;
    [SerializeField]
    private int dailyAdsReward = 0;
    [SerializeField]
    private int dailyAdsReward2 = 0;
    [SerializeField]
    private int dailyCastleReward = 0;
    [SerializeField]
    private int dailyQuestReward = 0;
    [SerializeField]
    private int dailyTreasureReward = 0;
    [SerializeField]
    private int dailyReward_Crystal = 0;

    [Space]
    [Title("Count")]
    [SerializeField]
    private int adCount = 0;
    [SerializeField]
    private int treasureCount = 0;
    [SerializeField]
    private int questCount = 0;
    [SerializeField]
    private int reincarnationCount = 0;
    [SerializeField]
    private int buffCount = 0;
    [SerializeField]
    private int changeNicknameCount = 0;

    [Space]
    [Title("Quest")]
    [SerializeField]
    private int upgradeCount = 0;
    [SerializeField]
    private int sellCount = 0;
    [SerializeField]
    private int useSources = 0;
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
    [Title("Food Number")]
    [SerializeField]
    private int nextFoodNumber = 0;
    [SerializeField]
    private int nextFoodNumber2 = 0;
    [SerializeField]
    private int nextFoodNumber3 = 0;
    [SerializeField]
    private int nextFoodNumber4 = 0;

    [Space]
    [Title("Fast Food")]
    [SerializeField]
    private int hamburgerMaxValue = 0;
    [SerializeField]
    private int sandwichMaxValue = 0;
    [SerializeField]
    private int snackLabMaxValue = 0;
    [SerializeField]
    private int drinkMaxValue = 0;
    [SerializeField]
    private int pizzaMaxValue = 0;
    [SerializeField]
    private int donutMaxValue = 0;
    [SerializeField]
    private int friesMaxValue = 0;

    [Space]
    [Title("Candy")]
    [SerializeField]
    private int candy1MaxValue = 0;
    [SerializeField]
    private int candy2MaxValue = 0;
    [SerializeField]
    private int candy3MaxValue = 0;
    [SerializeField]
    private int candy4MaxValue = 0;
    [SerializeField]
    private int candy5MaxValue = 0;
    [SerializeField]
    private int candy6MaxValue = 0;
    [SerializeField]
    private int candy7MaxValue = 0;
    [SerializeField]
    private int candy8MaxValue = 0;
    [SerializeField]
    private int candy9MaxValue = 0;

    [Space]
    [Title("Japanese Food")]
    [SerializeField]
    private int japaneseFood1MaxValue = 0;
    [SerializeField]
    private int japaneseFood2MaxValue = 0;
    [SerializeField]
    private int japaneseFood3MaxValue = 0;
    [SerializeField]
    private int japaneseFood4MaxValue = 0;
    [SerializeField]
    private int japaneseFood5MaxValue = 0;
    [SerializeField]
    private int japaneseFood6MaxValue = 0;
    [SerializeField]
    private int japaneseFood7MaxValue = 0;

    [Space]
    [Title("Dessert")]
    [SerializeField]
    private int dessert1MaxValue = 0;
    [SerializeField]
    private int dessert2MaxValue = 0;
    [SerializeField]
    private int dessert3MaxValue = 0;
    [SerializeField]
    private int dessert4MaxValue = 0;
    [SerializeField]
    private int dessert5MaxValue = 0;
    [SerializeField]
    private int dessert6MaxValue = 0;
    [SerializeField]
    private int dessert7MaxValue = 0;
    [SerializeField]
    private int dessert8MaxValue = 0;
    [SerializeField]
    private int dessert9MaxValue = 0;

    [Space]
    [Title("Truck")]
    [SerializeField]
    private int chips = 0;
    [SerializeField]
    private int donut = 0;
    [SerializeField]
    private int hamburger = 0;
    [SerializeField]
    private int hotdog = 0;
    [SerializeField]
    private int icecream = 0;
    [SerializeField]
    private int lemonade = 0;
    [SerializeField]
    private int noodles = 0;
    [SerializeField]
    private int pizza = 0;
    [SerializeField]
    private int sushi = 0;

    [Space]
    [Title("Animal")]
    [SerializeField]
    private int colobus = 0;
    [SerializeField]
    private int gecko = 0;
    [SerializeField]
    private int herring = 0;
    [SerializeField]
    private int muskrat = 0;
    [SerializeField]
    private int pudu = 0;
    [SerializeField]
    private int sparrow = 0;
    [SerializeField]
    private int squid = 0;
    [SerializeField]
    private int taipan = 0;

    [Space]
    [Title("Butterfly")]
    [SerializeField]
    private int butterfly1 = 0;
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
    private int totems1 = 0;
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
    private int flower1 = 0;
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
    private int totalLevel = 0;

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
    [Title("Coupon")]
    [SerializeField]
    private int coupon1 = 0;
    [SerializeField]
    private int coupon2 = 0;
    [SerializeField]
    private int coupon3 = 0;
    [SerializeField]
    private int coupon4 = 0;
    [SerializeField]
    private int coupon5 = 0;
    [SerializeField]
    private int coupon6 = 0;
    [SerializeField]
    private int coupon7 = 0;
    [SerializeField]
    private int coupon8 = 0;
    [SerializeField]
    private int coupon9 = 0;
    [SerializeField]
    private int coupon10 = 0;
    [SerializeField]
    private int coupon11 = 0;
    [SerializeField]
    private int coupon12 = 0;
    [SerializeField]
    private int coupon13 = 0;

    [Space]
    [Title("Speical Coupon")]
    [SerializeField]
    private int spCoupon1 = 0;
    [SerializeField]
    private int spCoupon2 = 0;
    [SerializeField]
    private int spCoupon3 = 0;
    [SerializeField]
    private int spCoupon4 = 0;
    [SerializeField]
    private int spCoupon5 = 0;
    [SerializeField]
    private int spCoupon6 = 0;
    [SerializeField]
    private int spCoupon7 = 0;
    [SerializeField]
    private int spCoupon8 = 0;
    [SerializeField]
    private int spCoupon9 = 0;
    [SerializeField]
    private int spCoupon10 = 0;
    [SerializeField]
    private int spCoupon11 = 0;
    [SerializeField]
    private int spCoupon12 = 0;
    [SerializeField]
    private int spCoupon13 = 0;

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

    public int Michelin
    {
        get
        {
            return michelin;
        }
        set
        {
            michelin = value;
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

    public int DailyReward
    {
        get
        {
            return dailyReward;
        }
        set
        {
            dailyReward = value;
        }
    }

    public int DailyReward_Portion
    {
        get
        {
            return dailyReward_Portion;
        }
        set
        {
            dailyReward_Portion = value;
        }
    }

    public int DailyReward_DefTicket
    {
        get
        {
            return dailyReward_DefTicket;
        }
        set
        {
            dailyReward_DefTicket = value;
        }
    }

    public int DailyReward_Crystal
    {
        get
        {
            return dailyReward_Crystal;
        }
        set
        {
            dailyReward_Crystal = value;
        }
    }

    public int DailyAdsReward
    {
        get
        {
            return dailyAdsReward;
        }
        set
        {
            dailyAdsReward = value;
        }
    }

    public int DailyAdsReward2
    {
        get
        {
            return dailyAdsReward2;
        }
        set
        {
            dailyAdsReward2 = value;
        }
    }

    public int DailyCastleReward
    {
        get
        {
            return dailyCastleReward;
        }
        set
        {
            dailyCastleReward = value;
        }
    }

    public int DailyQuestReward
    {
        get
        {
            return dailyQuestReward;
        }
        set
        {
            dailyQuestReward = value;
        }
    }

    public int DailyTreasureReward
    {
        get
        {
            return dailyTreasureReward;
        }
        set
        {
            dailyTreasureReward = value;
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

    public int Character1
    {
        get
        {
            return character1;
        }
        set
        {
            character1 = value;
        }
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

    public int NextFoodNumber2
    {
        get
        {
            return nextFoodNumber2;
        }
        set
        {
            nextFoodNumber2 = value;
        }
    }

    public int NextFoodNumber3
    {
        get
        {
            return nextFoodNumber3;
        }
        set
        {
            nextFoodNumber3 = value;
        }
    }

    public int NextFoodNumber4
    {
        get
        {
            return nextFoodNumber4;
        }
        set
        {
            nextFoodNumber4 = value;
        }
    }

    public int HamburgerMaxValue
    {
        get
        {
            return hamburgerMaxValue;
        }
        set
        {
            hamburgerMaxValue = value;
        }
    }

    public int SandwichMaxValue
    {
        get
        {
            return sandwichMaxValue;
        }
        set
        {
            sandwichMaxValue = value;
        }
    }

    public int SnackLabMaxValue
    {
        get
        {
            return snackLabMaxValue;
        }
        set
        {
            snackLabMaxValue = value;
        }
    }

    public int DrinkMaxValue
    {
        get
        {
            return drinkMaxValue;
        }
        set
        {
            drinkMaxValue = value;
        }
    }

    public int PizzaMaxValue
    {
        get
        {
            return pizzaMaxValue;
        }
        set
        {
            pizzaMaxValue = value;
        }
    }

    public int DonutMaxValue
    {
        get
        {
            return donutMaxValue;
        }
        set
        {
            donutMaxValue = value;
        }
    }

    public int FriesMaxValue
    {
        get
        {
            return friesMaxValue;
        }
        set
        {
            friesMaxValue = value;
        }
    }

    public int Candy1MaxValue
    {
        get
        {
            return candy1MaxValue;
        }
        set
        {
            candy1MaxValue = value;
        }
    }

    public int Candy2MaxValue
    {
        get
        {
            return candy2MaxValue;
        }
        set
        {
            candy2MaxValue = value;
        }
    }

    public int Candy3MaxValue
    {
        get
        {
            return candy3MaxValue;
        }
        set
        {
            candy3MaxValue = value;
        }
    }

    public int Candy4MaxValue
    {
        get
        {
            return candy4MaxValue;
        }
        set
        {
            candy4MaxValue = value;
        }
    }

    public int Candy5MaxValue
    {
        get
        {
            return candy5MaxValue;
        }
        set
        {
            candy5MaxValue = value;
        }
    }

    public int Candy6MaxValue
    {
        get
        {
            return candy6MaxValue;
        }
        set
        {
            candy6MaxValue = value;
        }
    }

    public int Candy7MaxValue
    {
        get
        {
            return candy7MaxValue;
        }
        set
        {
            candy7MaxValue = value;
        }
    }

    public int Candy8MaxValue
    {
        get
        {
            return candy8MaxValue;
        }
        set
        {
            candy8MaxValue = value;
        }
    }

    public int Candy9MaxValue
    {
        get
        {
            return candy9MaxValue;
        }
        set
        {
            candy9MaxValue = value;
        }
    }

    public int JapaneseFood1MaxValue
    {
        get
        {
            return japaneseFood1MaxValue;
        }
        set
        {
            japaneseFood1MaxValue = value;
        }
    }

    public int JapaneseFood2MaxValue
    {
        get
        {
            return japaneseFood2MaxValue;
        }
        set
        {
            japaneseFood2MaxValue = value;
        }
    }

    public int JapaneseFood3MaxValue
    {
        get
        {
            return japaneseFood3MaxValue;
        }
        set
        {
            japaneseFood3MaxValue = value;
        }
    }

    public int JapaneseFood4MaxValue
    {
        get
        {
            return japaneseFood4MaxValue;
        }
        set
        {
            japaneseFood4MaxValue = value;
        }
    }

    public int JapaneseFood5MaxValue
    {
        get
        {
            return japaneseFood5MaxValue;
        }
        set
        {
            japaneseFood5MaxValue = value;
        }
    }

    public int JapaneseFood6MaxValue
    {
        get
        {
            return japaneseFood6MaxValue;
        }
        set
        {
            japaneseFood6MaxValue = value;
        }
    }

    public int JapaneseFood7MaxValue
    {
        get
        {
            return japaneseFood7MaxValue;
        }
        set
        {
            japaneseFood7MaxValue = value;
        }
    }

    public int Dessert1MaxValue
    {
        get
        {
            return dessert1MaxValue;
        }
        set
        {
            dessert1MaxValue = value;
        }
    }

    public int Dessert2MaxValue
    {
        get
        {
            return dessert2MaxValue;
        }
        set
        {
            dessert2MaxValue = value;
        }
    }

    public int Dessert3MaxValue
    {
        get
        {
            return dessert3MaxValue;
        }
        set
        {
            dessert3MaxValue = value;
        }
    }

    public int Dessert4MaxValue
    {
        get
        {
            return dessert4MaxValue;
        }
        set
        {
            dessert4MaxValue = value;
        }
    }

    public int Dessert5MaxValue
    {
        get
        {
            return dessert5MaxValue;
        }
        set
        {
            dessert5MaxValue = value;
        }
    }

    public int Dessert6MaxValue
    {
        get
        {
            return dessert6MaxValue;
        }
        set
        {
            dessert6MaxValue = value;
        }
    }

    public int Dessert7MaxValue
    {
        get
        {
            return dessert7MaxValue;
        }
        set
        {
            dessert7MaxValue = value;
        }
    }

    public int Dessert8MaxValue
    {
        get
        {
            return dessert8MaxValue;
        }
        set
        {
            dessert8MaxValue = value;
        }
    }

    public int Dessert9MaxValue
    {
        get
        {
            return dessert9MaxValue;
        }
        set
        {
            dessert9MaxValue = value;
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

    public int UseSources
    {
        get
        {
            return useSources;
        }
        set
        {
            useSources = value;
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

    public int ConsumeGold
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
        get
        {
            return skill14;
        }
        set
        {
            skill14 = value;
        }
    }

    public int ChipsTruck
    {
        get
        {
            return chips;
        }
        set
        {
            chips = value;
        }
    }

    public int DonutTruck
    {
        get
        {
            return donut;
        }
        set
        {
            donut = value;
        }
    }

    public int HamburgerTruck
    {
        get
        {
            return hamburger;
        }
        set
        {
            hamburger = value;
        }
    }

    public int HotdogTruck
    {
        get
        {
            return hotdog;
        }
        set
        {
            hotdog = value;
        }
    }

    public int IcecreamTruck
    {
        get
        {
            return icecream;
        }
        set
        {
            icecream = value;
        }
    }

    public int LemonadeTruck
    {
        get
        {
            return lemonade;
        }
        set
        {
            lemonade = value;
        }
    }

    public int NoodlesTruck
    {
        get
        {
            return noodles;
        }
        set
        {
            noodles = value;
        }
    }

    public int PizzaTruck
    {
        get
        {
            return pizza;
        }
        set
        {
            pizza = value;
        }
    }

    public int SushiTruck
    {
        get
        {
            return sushi;
        }
        set
        {
            sushi = value;
        }
    }

    public int ColobusAnimal
    {
        get
        {
            return colobus;
        }
        set
        {
            colobus = value;
        }
    }

    public int GeckoAnimal
    {
        get
        {
            return gecko;
        }
        set
        {
            gecko = value;
        }
    }

    public int HerringAnimal
    {
        get
        {
            return herring;
        }
        set
        {
            herring = value;
        }
    }

    public int MuskratAnimal
    {
        get
        {
            return muskrat;
        }
        set
        {
            muskrat = value;
        }
    }

    public int PuduAnimal
    {
        get
        {
            return pudu;
        }
        set
        {
            pudu = value;
        }
    }

    public int SparrowAnimal
    {
        get
        {
            return sparrow;
        }
        set
        {
            sparrow = value;
        }
    }

    public int SquidAnimal
    {
        get
        {
            return squid;
        }
        set
        {
            squid = value;
        }
    }

    public int TaipanAnimal
    {
        get
        {
            return taipan;
        }
        set
        {
            taipan = value;
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
        get
        {
            return totems1;
        }
        set
        {
            totems1 = value;
        }
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

    public int Coupon1
    {
        get
        {
            return coupon1;
        }
        set
        {
            coupon1 = value;
        }
    }

    public int Coupon2
    {
        get
        {
            return coupon2;
        }
        set
        {
            coupon2 = value;
        }
    }

    public int Coupon3
    {
        get
        {
            return coupon3;
        }
        set
        {
            coupon3 = value;
        }
    }

    public int Coupon4
    {
        get
        {
            return coupon4;
        }
        set
        {
            coupon4 = value;
        }
    }

    public int Coupon5
    {
        get
        {
            return coupon5;
        }
        set
        {
            coupon5 = value;
        }
    }

    public int Coupon6
    {
        get
        {
            return coupon6;
        }
        set
        {
            coupon6 = value;
        }
    }

    public int Coupon7
    {
        get
        {
            return coupon7;
        }
        set
        {
            coupon7 = value;
        }
    }

    public int Coupon8
    {
        get
        {
            return coupon8;
        }
        set
        {
            coupon8 = value;
        }
    }

    public int Coupon9
    {
        get
        {
            return coupon9;
        }
        set
        {
            coupon9 = value;
        }
    }

    public int Coupon10
    {
        get
        {
            return coupon10;
        }
        set
        {
            coupon10 = value;
        }
    }

    public int Coupon11
    {
        get
        {
            return coupon11;
        }
        set
        {
            coupon12 = value;
        }
    }

    public int Coupon12
    {
        get
        {
            return coupon12;
        }
        set
        {
            coupon12 = value;
        }
    }

    public int Coupon13
    {
        get
        {
            return coupon13;
        }
        set
        {
            coupon13 = value;
        }
    }

    public int SpCoupon1
    {
        get
        {
            return spCoupon1;
        }
        set
        {
            spCoupon1 = value;
        }
    }

    public int SpCoupon2
    {
        get
        {
            return spCoupon2;
        }
        set
        {
            spCoupon2 = value;
        }
    }

    public int SpCoupon3
    {
        get
        {
            return spCoupon3;
        }
        set
        {
            spCoupon3 = value;
        }
    }

    public int SpCoupon4
    {
        get
        {
            return spCoupon4;
        }
        set
        {
            spCoupon4 = value;
        }
    }

    public int SpCoupon5
    {
        get
        {
            return spCoupon5;
        }
        set
        {
            spCoupon5 = value;
        }
    }

    public int SpCoupon6
    {
        get
        {
            return spCoupon6;
        }
        set
        {
            spCoupon6 = value;
        }
    }

    public int SpCoupon7
    {
        get
        {
            return spCoupon7;
        }
        set
        {
            spCoupon7 = value;
        }
    }

    public int SpCoupon8
    {
        get
        {
            return spCoupon8;
        }
        set
        {
            spCoupon8 = value;
        }
    }

    public int SpCoupon9
    {
        get
        {
            return spCoupon9;
        }
        set
        {
            spCoupon9 = value;
        }
    }

    public int SpCoupon10
    {
        get
        {
            return spCoupon10;
        }
        set
        {
            spCoupon10 = value;
        }
    }

    public int SpCoupon11
    {
        get
        {
            return spCoupon11;
        }
        set
        {
            spCoupon11 = value;
        }
    }

    public int SpCoupon12
    {
        get
        {
            return spCoupon12;
        }
        set
        {
            spCoupon12 = value;
        }
    }

    public int SpCoupon13
    {
        get
        {
            return spCoupon13;
        }
        set
        {
            spCoupon13 = value;
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

    public void Initialize()
    {
        removeAds = false;
        goldX2 = false;
        superOffline = false;
        autoUpgrade = false;
        autoPresent = false;

        rankLevel1 = 0;
        rankLevel2 = 0;
        rankLevel3 = 0;
        rankLevel4 = 0;
        totalLevel = 0;

        coin = 0;
        coinA = 0;
        coinB = 0;
        crystal = 0;
        rankPoint = 0;
        defDestroyTicket = 0;
        defDestroyTicketPiece = 0;
        eventNumber = 0;
        lockTutorial = 0;
        inGameTutorial = 0;
        firstReward = 0;
        firstDate = "";
        firstServerDate = "";
        islandNumber = 0;
        testAccount = 0;
        buffTicket = 0;
        skillTicket = 0;
        recoverTicket = 0;
        proficiency = 0;
        level = 0;
        exp = 0;
        playTime = 0;
        michelin = 0;
        adCount = 0;
        treasureCount = 0;
        accessDate = 0;
        castleLevel = 0;
        castleDate = "";
        castleServerDate = "";

        dailyReward = 0;
        dailyReward_Portion = 0;
        dailyReward_DefTicket = 0;
        dailyReward_Crystal = 0;
        dailyAdsReward = 0;
        dailyAdsReward2 = 0;
        dailyCastleReward = 0;
        dailyQuestReward = 0;
        dailyTreasureReward = 0;

        nextFoodNumber = 0;
        nextFoodNumber2 = 0;
        nextFoodNumber3 = 0;
        nextFoodNumber4 = 0;

        hamburgerMaxValue = 0;
        sandwichMaxValue = 0;
        snackLabMaxValue = 0;
        drinkMaxValue = 0;
        pizzaMaxValue = 0;
        donutMaxValue = 0;
        friesMaxValue = 0;

        candy1MaxValue = 0;
        candy2MaxValue = 0;
        candy3MaxValue = 0;
        candy4MaxValue = 0;
        candy5MaxValue = 0;
        candy6MaxValue = 0;
        candy7MaxValue = 0;
        candy8MaxValue = 0;
        candy9MaxValue = 0;

        japaneseFood1MaxValue = 0;
        japaneseFood2MaxValue = 0;
        japaneseFood3MaxValue = 0;
        japaneseFood4MaxValue = 0;
        japaneseFood5MaxValue = 0;
        japaneseFood6MaxValue = 0;
        japaneseFood7MaxValue = 0;

        dessert1MaxValue = 0;
        dessert2MaxValue = 0;
        dessert3MaxValue = 0;
        dessert4MaxValue = 0;
        dessert5MaxValue = 0;
        dessert6MaxValue = 0;
        dessert7MaxValue = 0;
        dessert8MaxValue = 0;
        dessert9MaxValue = 0;

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

        character1 = 0;
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

        chips = 0;
        donut = 0;
        hamburger = 0;
        hotdog = 0;
        icecream = 0;
        lemonade = 0;
        noodles = 0;
        pizza = 0;
        sushi = 0;

        colobus = 0;
        gecko = 0;
        herring = 0;
        muskrat = 0;
        pudu = 0;
        sparrow = 0;
        squid = 0;
        taipan = 0;

        butterfly1 = 0;
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

        totems1 = 0;
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

        flower1 = 0;
        flower2 = 0;
        flower3 = 0;
        flower4 = 0;
        flower5 = 0;
        flower6 = 0;
        flower7 = 0;

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
        ChangeNicknameCount = 0;

        upgradeCount = 0;
        sellCount = 0;
        useSources = 0;
        openChestBox = 0;
        yummyTimeCount = 0;

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

        portion1 = 0;
        portion2 = 0;
        portion3 = 0;
        portion4 = 0;
        portion5 = 0;
        portion6 = 0;

        coupon1 = 0;
        coupon2 = 0;
        coupon3 = 0;
        coupon4 = 0;
        coupon5 = 0;
        coupon6 = 0;
        coupon7 = 0;
        coupon8 = 0;
        coupon9 = 0;
        coupon10 = 0;
        coupon11 = 0;
        coupon12 = 0;
        coupon13 = 0;

        spCoupon1 = 0;
        spCoupon2 = 0;
        spCoupon3 = 0;
        spCoupon4 = 0;
        spCoupon5 = 0;
        spCoupon6 = 0;
        spCoupon7 = 0;
        spCoupon8 = 0;
        spCoupon9 = 0;
        spCoupon10 = 0;
        spCoupon11 = 0;
        spCoupon12 = 0;
        SpCoupon13 = 0;

        package1 = false;
        package2 = false;
        package3 = false;
        package4 = false;
        package5 = false;
        package6 = false;

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

    public bool CheckCharacter(TruckType type)
    {
        bool check = false;

        switch (type)
        {
            case TruckType.Bread:
                check = true;
                break;
            case TruckType.Chips:
                if(ChipsTruck > 0)
                {
                    check = true;
                }
                break;
            case TruckType.Donut:
                if (DonutTruck > 0)
                {
                    check = true;
                }
                break;
            case TruckType.Hamburger:
                if (HamburgerTruck > 0)
                {
                    check = true;
                }
                break;
            case TruckType.Hotdog:
                if (IcecreamTruck > 0)
                {
                    check = true;
                }
                break;
            case TruckType.Icecream:
                if (LemonadeTruck > 0)
                {
                    check = true;
                }
                break;
            case TruckType.Lemonade:
                if (NoodlesTruck > 0)
                {
                    check = true;
                }
                break;
        }

        return check;
    }

    public void BuyCharacter(TruckType type)
    {
        switch (type)
        {
            case TruckType.Bread:
                break;
            case TruckType.Chips:
                ChipsTruck = 1;
                break;
            case TruckType.Donut:
                DonutTruck = 1;
                break;
            case TruckType.Hamburger:
                HamburgerTruck = 1;
                break;
            case TruckType.Hotdog:
                IcecreamTruck = 1;
                break;
            case TruckType.Icecream:
                LemonadeTruck = 1;
                break;
            case TruckType.Lemonade:
                NoodlesTruck = 1;
                break;
        }
    }

    public int GetTruckNumber()
    {
        int count = 0;

        if(ChipsTruck > 0)
        {
            count += 1;
        }

        if (DonutTruck > 0)
        {
            count += 1;
        }

        if (HamburgerTruck > 0)
        {
            count += 1;
        }

        if (IcecreamTruck > 0)
        {
            count += 1;
        }

        if (LemonadeTruck > 0)
        {
            count += 1;
        }

        if (NoodlesTruck > 0)
        {
            count += 1;
        }

        if (PizzaTruck > 0)
        {
            count += 1;
        }

        if (SushiTruck > 0)
        {
            count += 1;
        }

        return count;
    }

    public int GetAnimalNumber()
    {
        int count = 0;

        if(GeckoAnimal > 0)
        {
            count += 1;
        }

        if(HerringAnimal > 0)
        {
            count += 1;
        }

        if(MuskratAnimal > 0)
        {
            count += 1;
        }

        if(PuduAnimal > 0)
        {
            count += 1;
        }

        if(SparrowAnimal > 0)
        {
            count += 1;
        }

        if(SquidAnimal > 0)
        {
            count += 1;
        }
        
        if(TaipanAnimal > 0)
        {
            count += 1;
        }

        return count;
    }

    public int GetButterflyNumber()
    {
        int count = 0;

        if (butterfly1 > 0)
        {
            count += 1;
        }

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

        if (character1 > 0)
        {
            count += 1;
        }

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

        if (totems1 > 0)
        {
            count += 1;
        }

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

    public int GetFlowerNumber()
    {
        int count = 0;

        if (flower1 > 0)
        {
            count += 1;
        }

        if (flower2 > 0)
        {
            count += 1;
        }

        if (flower3 > 0)
        {
            count += 1;
        }

        if (flower4 > 0)
        {
            count += 1;
        }

        if (flower5 > 0)
        {
            count += 1;
        }

        if (flower6 > 0)
        {
            count += 1;
        }

        if (flower7 > 0)
        {
            count += 1;
        }

        return count;
    }

    public int GetTruckHighNumber()
    {
        int count = 0;

        if (ChipsTruck > 0)
        {
            count = 1;
        }

        if (DonutTruck > 0)
        {
            count = 2;
        }

        if (HamburgerTruck > 0)
        {
            count = 3;
        }

        if (IcecreamTruck > 0)
        {
            count = 4;
        }

        if (LemonadeTruck > 0)
        {
            count = 5;
        }

        if (NoodlesTruck > 0)
        {
            count = 6;
        }

        if (PizzaTruck > 0)
        {
            count = 7;
        }

        if (SushiTruck > 0)
        {
            count = 8;
        }

        return count;
    }

    public int GetAnimalHighNumber()
    {
        int count = 0;

        if (GeckoAnimal > 0)
        {
            count = 1;
        }

        if (HerringAnimal > 0)
        {
            count = 2;
        }

        if (MuskratAnimal > 0)
        {
            count = 3;
        }

        if (PuduAnimal > 0)
        {
            count = 4;
        }

        if (SparrowAnimal > 0)
        {
            count = 5;
        }

        if (SquidAnimal > 0)
        {
            count = 6;
        }

        if (TaipanAnimal > 0)
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

        return number;
    }
}