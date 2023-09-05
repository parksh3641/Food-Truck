using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public GameSettings gameSettings;

    [NonSerialized]
    public const string DEVICESETTINGFILENAME = "DeviceSetting.bin";

    [Serializable]
    public class GameSettings
    {
        [Space]
        [Title("Login")]
        public string playfabId = "";
        public string customId = "";

        public bool autoLogin = false;
        public LoginType loginType = LoginType.None;
        public string nickName = "";

        [Space]
        [Title("Language")]
        public LanguageType language = LanguageType.Default;

        [Space]
        [Title("Game Setting")]
        public LevelType levelType = LevelType.Easy;
        public TruckType truckType = TruckType.Bread;
        public bool music = true;
        public bool sfx = true;
        public bool vibration = true;
        public bool record = false;
        public bool appReview = false;
        public bool developer = false;

        [Space]
        [Title("InGame")]
        public FoodType foodType = FoodType.Hamburger;
        public int hamburgerLevel = 0;
        public int sandwichLevel = 0;
        public int snackLabLevel = 0;
        public int drinkLevel = 0;
        public int pizzaLevel = 0;
        public int donutLevel = 0;

        [Space]
        [Title("Ads")]
        public bool removeAds = false;
        public bool adActive = false;
        public int adCount = 0;
        public bool dailyReward = false;
        public bool dailyAdsReward = false;
    }

    #region Data
    public string PlayfabId
    {
        get
        {
            return gameSettings.playfabId;
        }
        set
        {
            gameSettings.playfabId = value;
            SaveFile();
        }
    }

    public string CustomId
    {
        get
        {
            return gameSettings.customId;
        }
        set
        {
            gameSettings.customId = value;
            SaveFile();
        }
    }

    public string NickName
    {
        get
        {
            return gameSettings.nickName;
        }
        set
        {
            gameSettings.nickName = value;
            SaveFile();
        }
    }

    public LanguageType Language
    {
        get
        {
            return gameSettings.language;
        }
        set
        {
            gameSettings.language = value;
            SaveFile();
        }
    }
    public bool AutoLogin
    {
        get
        {
            return gameSettings.autoLogin;
        }
        set
        {
            gameSettings.autoLogin = value;
            SaveFile();
        }
    }

    public LoginType Login
    {
        get
        {
            return gameSettings.loginType;
        }
        set
        {
            gameSettings.loginType = value;
            SaveFile();
        }
    }

    public LevelType LevelType
    {
        get
        {
            return gameSettings.levelType;
        }
        set
        {
            gameSettings.levelType = value;
            SaveFile();
        }
    }

    public TruckType TruckType
    {
        get
        {
            return gameSettings.truckType;
        }
        set
        {
            gameSettings.truckType = value;
            SaveFile();
        }
    }

    public bool Music
    {
        get
        {
            return gameSettings.music;
        }
        set
        {
            gameSettings.music = value;
            SaveFile();
        }
    }

    public bool Sfx
    {
        get
        {
            return gameSettings.sfx;
        }
        set
        {
            gameSettings.sfx = value;
            SaveFile();
        }
    }

    public bool Vibration
    {
        get
        {
            return gameSettings.vibration;
        }
        set
        {
            gameSettings.vibration = value;
            SaveFile();
        }
    }

    public bool Record
    {
        get
        {
            return gameSettings.record;
        }
        set
        {
            gameSettings.record = value;
            SaveFile();
        }
    }

    public bool AppReview
    {
        get
        {
            return gameSettings.appReview;
        }
        set
        {
            gameSettings.appReview = value;
            SaveFile();
        }
    }

    public bool Developer
    {
        get
        {
            return gameSettings.developer;
        }
        set
        {
            gameSettings.developer = value;
            SaveFile();
        }
    }

    public FoodType FoodType
    {
        get
        {
            return gameSettings.foodType;
        }
        set
        {
            gameSettings.foodType = value;
            SaveFile();
        }
    }

    public int HamburgerLevel
    {
        get
        {
            return gameSettings.hamburgerLevel;
        }
        set
        {
            gameSettings.hamburgerLevel = value;
            SaveFile();
        }
    }

    public int SandwichLevel
    {
        get
        {
            return gameSettings.sandwichLevel;
        }
        set
        {
            gameSettings.sandwichLevel = value;
            SaveFile();
        }
    }

    public int SnackLabLevel
    {
        get
        {
            return gameSettings.snackLabLevel;
        }
        set
        {
            gameSettings.snackLabLevel = value;
            SaveFile();
        }
    }

    public int DrinkLevel
    {
        get
        {
            return gameSettings.drinkLevel;
        }
        set
        {
            gameSettings.drinkLevel = value;
            SaveFile();
        }
    }

    public int PizzaLevel
    {
        get
        {
            return gameSettings.pizzaLevel;
        }
        set
        {
            gameSettings.pizzaLevel = value;
            SaveFile();
        }
    }

    public int DonutLevel
    {
        get
        {
            return gameSettings.donutLevel;
        }
        set
        {
            gameSettings.donutLevel = value;
            SaveFile();
        }
    }

    public bool RemoveAds
    {
        get
        {
            return gameSettings.removeAds;
        }
        set
        {
            gameSettings.removeAds = value;
            SaveFile();
        }
    }

    public bool AdActive
    {
        get
        {
            return gameSettings.adActive;
        }
        set
        {
            gameSettings.adActive = value;
            SaveFile();
        }
    }

    public int AdCount
    {
        get
        {
            return gameSettings.adCount;
        }
        set
        {
            gameSettings.adCount = value;
            SaveFile();
        }
    }

    public bool DailyReward
    {
        get
        {
            return gameSettings.dailyReward;
        }
        set
        {
            gameSettings.dailyReward = value;
            SaveFile();
        }
    }

    public bool DailyAdsReward
    {
        get
        {
            return gameSettings.dailyAdsReward;
        }
        set
        {
            gameSettings.dailyAdsReward = value;
            SaveFile();
        }
    }

    #endregion

    private void Awake()
    {
        instance = this;

        LoadData();
    }
    private void LoadData()
    {
        try
        {
            string stjs = FileIO.LoadData(DEVICESETTINGFILENAME, true);

            if (!string.IsNullOrEmpty(stjs))
            {
                gameSettings = JsonUtility.FromJson<GameSettings>(stjs);
            }
            else
            {
                gameSettings = new GameSettings();
                gameSettings.dailyReward = false;
                gameSettings.dailyAdsReward = false;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Load Error \n" + e.Message);
        }
    }

    public void SaveFile()
    {
        try
        {
            string str = JsonUtility.ToJson(gameSettings);
            FileIO.SaveData(DEVICESETTINGFILENAME, str, true);
        }
        catch (Exception e)
        {
            Debug.LogError("Save Error \n" + e.Message);
        }
    }
}
