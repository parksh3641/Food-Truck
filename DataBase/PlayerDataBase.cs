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

    public void Initialize()
    {
        coin = 0;
        crystal = 0;
        defDestroyTicket = 0;

        nextFoodNumber = 0;
        hamburgerMaxValue = 0;
        sandwichMaxValue = 0;
        snackLabMaxValue = 0;
        drinkMaxValue = 0;
        pizzaMaxValue = 0;
        donutMaxValue = 0;

        chips = 0;
        donut = 0;
        hamburger = 0;
        hotdog = 0;
        icecream = 0;
        lemonade = 0;
        noodles = 0;
        pizza = 0;
        sushi = 0;

        portion1 = 0;
        portion2 = 0;
        portion3 = 0;
        portion4 = 0;

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

        removeAds = false;
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
}