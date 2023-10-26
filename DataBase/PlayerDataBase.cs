using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataBase", menuName = "ScriptableObjects/PlayerDataBase")]
public class PlayerDataBase : ScriptableObject
{
    [Title("Player")]
    [SerializeField]
    private int coin = 0;
    [SerializeField]
    private int crystal = 0;
    [SerializeField]
    private int defDestroyTicket = 0;
    [SerializeField]
    private int lockTutorial = 0;
    [SerializeField]
    private int firstReward = 0;

    [Space]
    [SerializeField]
    private int nextFoodNumber = 0;
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
    private int gourmetLevel = 0;
    [SerializeField]
    private int donutLevel = 0;
    [SerializeField]
    private int upgradeCount = 0;
    [SerializeField]
    private int sellCount = 0;
    [SerializeField]
    private int useSources = 0;
    [SerializeField]
    private int openChestBox = 0;
    [SerializeField]
    private int questCount = 0;

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
    [Title("Portion")]
    [SerializeField]
    private int portion1 = 0;
    [SerializeField]
    private int portion2 = 0;
    [SerializeField]
    private int portion3 = 0;
    [SerializeField]
    private int portion4 = 0;

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


    [Space]
    [Title("Best Record Offline")]
    [SerializeField]
    private int easy_Offline = 0;

    [SerializeField]
    private int normal_Offline = 0;

    [SerializeField]
    private int hard_Offline = 0;

    [SerializeField]
    private int crazy_Offline = 0;

    [SerializeField]
    private int insane_Offline = 0;

    [Title("Best Record _Online")]
    [SerializeField]
    private int easy = 0;

    [SerializeField]
    private int normal = 0;

    [SerializeField]
    private int hard = 0;

    [SerializeField]
    private int crazy = 0;

    [SerializeField]
    private int insane = 0;

    [SerializeField]
    private bool removeAds = false;
    [SerializeField]
    private bool goldX2 = false;

    public int Coin
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

    public int DonutLevel
    {
        get
        {
            return donutLevel;
        }
        set
        {
            donutLevel = value;
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

    public int Easy
    {
        get
        {
            return easy;
        }
        set
        {
            easy = value;
        }
    }

    public int Normal
    {
        get
        {
            return normal;
        }
        set
        {
            normal = value;
        }
    }

    public int Hard
    {
        get
        {
            return hard;
        }
        set
        {
            hard = value;
        }
    }

    public int Crazy
    {
        get
        {
            return crazy;
        }
        set
        {
            crazy = value;
        }
    }

    public int Insane
    {
        get
        {
            return insane;
        }
        set
        {
            insane = value;
        }
    }

    public int Easy_Offline
    {
        get
        {
            return easy_Offline;
        }
        set
        {
            easy_Offline = value;
        }
    }

    public int Normal_Offline
    {
        get
        {
            return normal_Offline;
        }
        set
        {
            normal_Offline = value;
        }
    }

    public int Hard_Offline
    {
        get
        {
            return hard_Offline;
        }
        set
        {
            hard_Offline = value;
        }
    }

    public int Crazy_Offline
    {
        get
        {
            return crazy_Offline;
        }
        set
        {
            crazy_Offline = value;
        }
    }

    public int Insane_Offline
    {
        get
        {
            return insane_Offline;
        }
        set
        {
            insane_Offline = value;
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
        crystal = 0;
        defDestroyTicket = 0;
        lockTutorial = 0;
        nextFoodNumber = 0;
        FirstReward = 0;

        hamburgerMaxValue = 0;
        sandwichMaxValue = 0;
        snackLabMaxValue = 0;
        drinkMaxValue = 0;
        pizzaMaxValue = 0;
        donutMaxValue = 0;
        gourmetLevel = 0;
        donutLevel = 0;
        upgradeCount = 0;
        sellCount = 0;
        useSources = 0;
        openChestBox = 0;
        questCount = 0;

        skill1 = 0;
        skill2 = 0;
        skill3 = 0;
        skill4 = 0;
        skill5 = 0;
        skill6 = 0;

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

        portion1 = 0;
        portion2 = 0;
        portion3 = 0;
        portion4 = 0;

        coupon1 = 0;
        coupon2 = 0;
        coupon3 = 0;
        coupon4 = 0;

        attendanceDay = "";
        attendanceCount = 0;
        attendanceCheck = false;
        nextMonday = "";

        easy_Offline = 0;
        normal_Offline = 0;
        hard_Offline = 0;
        crazy_Offline = 0;
        insane_Offline = 0;

        easy = 0;
        normal = 0;
        hard = 0;
        crazy = 0;
        insane = 0;

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
}