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
    private int defDestroyTicket = 0;
    [SerializeField]
    private int lockTutorial = 0;
    [SerializeField]
    private int firstReward = 0;
    [SerializeField]
    private int islandNumber = 0;

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

    [Space]
    [Title("Food")]
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
    [SerializeField]
    private int nextFoodNumber = 0;

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
    [SerializeField]
    private int nextFoodNumber2 = 0;


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
    [Title("Count")]
    [SerializeField]
    private int gourmetLevel = 0;
    [SerializeField]
    private int upgradeCount = 0;
    [SerializeField]
    private int sellCount = 0;
    [SerializeField]
    private int useSources = 0;
    [SerializeField]
    private int openChestBox = 0;
    [SerializeField]
    private int feverModeCount = 0;
    [SerializeField]
    private int questCount = 0;
    [SerializeField]
    private int reincarnationCount = 0;
    [SerializeField]
    private int buffCount = 0;

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


    [Space]
    [Title("Reset")]
    public string attendanceDay = "";
    public int attendanceCount = 0;
    public bool attendanceCheck = false;
    [Space]
    public string nextMonday = "";

    [SerializeField]
    private bool removeAds = false;
    [SerializeField]
    private bool goldX2 = false;

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

    public int FeverModeCount
    {
        get
        {
            return feverModeCount;
        }
        set
        {
            feverModeCount = value;
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

    public void Initialize()
    {
        removeAds = false;
        goldX2 = false;

        coin = 0;
        coinA = 0;
        coinB = 0;
        crystal = 0;
        defDestroyTicket = 0;
        lockTutorial = 0;
        firstReward = 0;
        islandNumber = 0;

        nextFoodNumber = 0;
        hamburgerMaxValue = 0;
        sandwichMaxValue = 0;
        snackLabMaxValue = 0;
        drinkMaxValue = 0;
        pizzaMaxValue = 0;
        donutMaxValue = 0;
        friesMaxValue = 0;

        nextFoodNumber2 = 0;
        candy1MaxValue = 0;
        candy2MaxValue = 0;
        candy3MaxValue = 0;
        candy4MaxValue = 0;
        candy5MaxValue = 0;
        candy6MaxValue = 0;
        candy7MaxValue = 0;
        candy8MaxValue = 0;
        candy9MaxValue = 0;

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

        gourmetLevel = 0;
        upgradeCount = 0;
        sellCount = 0;
        useSources = 0;
        openChestBox = 0;
        feverModeCount = 0;
        questCount = 0;
        reincarnationCount = 0;
        buffCount = 0;

        rankLevel1 = 0;
        rankLevel2 = 0;
        rankLevel3 = 0;
        rankLevel4 = 0;
        totalLevel = 0;

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

        portion1 = 0;
        portion2 = 0;
        portion3 = 0;
        portion4 = 0;
        portion5 = 0;

        coupon1 = 0;
        coupon2 = 0;
        coupon3 = 0;
        coupon4 = 0;

        attendanceDay = "";
        attendanceCount = 0;
        attendanceCheck = false;
        nextMonday = "";

        //easy_Offline = PlayerPrefs.GetInt("Easy_Offline");
        //normal_Offline = PlayerPrefs.GetInt("Normal_Offline");
        //hard_Offline = PlayerPrefs.GetInt("Hard_Offline");
        //crazy_Offline = PlayerPrefs.GetInt("Crazy_Offline");
        //insane_Offline = PlayerPrefs.GetInt("Insane_Offline");
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

        if (GeckoAnimal > 0)
        {
            count += 1;
        }

        if (HerringAnimal > 0)
        {
            count += 1;
        }

        if (MuskratAnimal > 0)
        {
            count += 1;
        }

        if (PuduAnimal > 0)
        {
            count += 1;
        }

        if (SparrowAnimal > 0)
        {
            count += 1;
        }

        if (SquidAnimal > 0)
        {
            count += 1;
        }

        if (TaipanAnimal > 0)
        {
            count += 1;
        }

        return count;
    }
}