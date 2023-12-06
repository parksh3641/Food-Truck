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
        public bool music = true;
        public bool sfx = true;
        public bool vibration = true;
        public bool effect = true;
        public bool appReview = false;
        public bool developer = false;
        public bool rankingNotice = false;

        [Space]
        [Title("InGame")]
        public GameType gameType = GameType.Story;
        public IslandType islandType = IslandType.Island1;
        public FoodType foodType = FoodType.Food1;
        public CandyType candyType = CandyType.Candy1;
        public JapaneseFoodType japaneseFoodType = JapaneseFoodType.JapaneseFood1;
        public DessertType dessertType = DessertType.Dessert1;

        public int level = 0;

        [Space]
        [Title("Level")]
        public int hamburgerLevel = 0;
        public int sandwichLevel = 0;
        public int snackLabLevel = 0;
        public int drinkLevel = 0;
        public int pizzaLevel = 0;
        public int donutLevel = 0;
        public int friesLevel = 0;
        public int ribsLevel = 0;

        [Space]
        public int candy1Level = 0;
        public int candy2Level = 0;
        public int candy3Level = 0;
        public int candy4Level = 0;
        public int candy5Level = 0;
        public int candy6Level = 0;
        public int candy7Level = 0;
        public int candy8Level = 0;
        public int candy9Level = 0;
        public int chocolateLevel = 0;

        [Space]
        public int japaneseFood1Level = 0;
        public int japaneseFood2Level = 0;
        public int japaneseFood3Level = 0;
        public int japaneseFood4Level = 0;
        public int japaneseFood5Level = 0;
        public int japaneseFood6Level = 0;
        public int japaneseFood7Level = 0;
        public int ramenLevel = 0;

        [Space]
        public int dessert1Level = 0;
        public int dessert2Level = 0;
        public int dessert3Level = 0;
        public int dessert4Level = 0;
        public int dessert5Level = 0;
        public int dessert6Level = 0;
        public int dessert7Level = 0;
        public int dessert8Level = 0;
        public int dessert9Level = 0;
        public int fruitSkewersLevel = 0;

        [Space]
        [Title("Save")]
        public float feverCount = 0;
        public int consumeGold = 0;
        public int upgradeCount = 0;
        public int sellCount = 0;
        public int useSauce = 0;
        public int openChestBox = 0;
        public int yummyTimeCount = 0;
        public int playTime = 0;

        [Space]
        [Title("Equip")]
        public CharacterType characterType = CharacterType.Character1;
        public TruckType truckType = TruckType.Bread;
        public AnimalType animalType = AnimalType.Colobus;
        public ButterflyType butterflyType = ButterflyType.Butterfly1;

        [Space]
        [Title("Ads")]
        public bool removeAds = false;
        public bool adActive = false;
        public int adCount = 200;
        public int chestBoxCount = 0;
        public int chestBoxCoolTime = 120;

        [Space]
        [Title("Bankruptcy")]
        public int bankruptcy = 0;
        public bool tutorial = false;

        public bool privacypolicy = false;
        public bool pause = false;
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

    public GameType GameType
    {
        get
        {
            return gameSettings.gameType;
        }
        set
        {
            gameSettings.gameType = value;
            SaveFile();
        }
    }

    public IslandType IslandType
    {
        get
        {
            return gameSettings.islandType;
        }
        set
        {
            gameSettings.islandType = value;
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

    public CandyType CandyType
    {
        get
        {
            return gameSettings.candyType;
        }
        set
        {
            gameSettings.candyType = value;
            SaveFile();
        }
    }

    public JapaneseFoodType JapaneseFoodType
    {
        get
        {
            return gameSettings.japaneseFoodType;
        }
        set
        {
            gameSettings.japaneseFoodType = value;
            SaveFile();
        }
    }

    public DessertType DessertType
    {
        get
        {
            return gameSettings.dessertType;
        }
        set
        {
            gameSettings.dessertType = value;
            SaveFile();
        }
    }

    public CharacterType CharacterType
    {
        get
        {
            return gameSettings.characterType;
        }
        set
        {
            gameSettings.characterType = value;
            SaveFile();
        }
    }

    public ButterflyType ButterflyType
    {
        get
        {
            return gameSettings.butterflyType;
        }
        set
        {
            gameSettings.butterflyType = value;
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

    public AnimalType AnimalType
    {
        get
        {
            return gameSettings.animalType;
        }
        set
        {
            gameSettings.animalType = value;
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

    public bool Effect
    {
        get
        {
            return gameSettings.effect;
        }
        set
        {
            gameSettings.effect = value;
            SaveFile();
        }
    }

    public bool RankingNotice
    {
        get
        {
            return gameSettings.rankingNotice;
        }
        set
        {
            gameSettings.rankingNotice = value;
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

    public int Level
    {
        get
        {
            return gameSettings.level;
        }
        set
        {
            gameSettings.level = value;
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

    public int FriesLevel
    {
        get
        {
            return gameSettings.friesLevel;
        }
        set
        {
            gameSettings.friesLevel = value;
            SaveFile();
        }
    }

    public int RibsLevel
    {
        get
        {
            return gameSettings.ribsLevel;
        }
        set
        {
            gameSettings.ribsLevel = value;
            SaveFile();
        }
    }

    public int Candy1Level
    {
        get
        {
            return gameSettings.candy1Level;
        }
        set
        {
            gameSettings.candy1Level = value;
            SaveFile();
        }
    }

    public int Candy2Level
    {
        get
        {
            return gameSettings.candy2Level;
        }
        set
        {
            gameSettings.candy2Level = value;
            SaveFile();
        }
    }

    public int Candy3Level
    {
        get
        {
            return gameSettings.candy3Level;
        }
        set
        {
            gameSettings.candy3Level = value;
            SaveFile();
        }
    }

    public int Candy4Level
    {
        get
        {
            return gameSettings.candy4Level;
        }
        set
        {
            gameSettings.candy4Level = value;
            SaveFile();
        }
    }

    public int Candy5Level
    {
        get
        {
            return gameSettings.candy5Level;
        }
        set
        {
            gameSettings.candy5Level = value;
            SaveFile();
        }
    }

    public int Candy6Level
    {
        get
        {
            return gameSettings.candy6Level;
        }
        set
        {
            gameSettings.candy6Level = value;
            SaveFile();
        }
    }

    public int Candy7Level
    {
        get
        {
            return gameSettings.candy7Level;
        }
        set
        {
            gameSettings.candy7Level = value;
            SaveFile();
        }
    }

    public int Candy8Level
    {
        get
        {
            return gameSettings.candy8Level;
        }
        set
        {
            gameSettings.candy8Level = value;
            SaveFile();
        }
    }

    public int Candy9Level
    {
        get
        {
            return gameSettings.candy9Level;
        }
        set
        {
            gameSettings.candy9Level = value;
            SaveFile();
        }
    }

    public int ChocolateLevel
    {
        get
        {
            return gameSettings.chocolateLevel;
        }
        set
        {
            gameSettings.chocolateLevel = value;
            SaveFile();
        }
    }

    public int JapaneseFood1Level
    {
        get
        {
            return gameSettings.japaneseFood1Level;
        }
        set
        {
            gameSettings.japaneseFood1Level = value;
            SaveFile();
        }
    }

    public int JapaneseFood2Level
    {
        get
        {
            return gameSettings.japaneseFood2Level;
        }
        set
        {
            gameSettings.japaneseFood2Level = value;
            SaveFile();
        }
    }

    public int JapaneseFood3Level
    {
        get
        {
            return gameSettings.japaneseFood3Level;
        }
        set
        {
            gameSettings.japaneseFood3Level = value;
            SaveFile();
        }
    }

    public int JapaneseFood4Level
    {
        get
        {
            return gameSettings.japaneseFood4Level;
        }
        set
        {
            gameSettings.japaneseFood4Level = value;
            SaveFile();
        }
    }

    public int JapaneseFood5Level
    {
        get
        {
            return gameSettings.japaneseFood5Level;
        }
        set
        {
            gameSettings.japaneseFood5Level = value;
            SaveFile();
        }
    }

    public int JapaneseFood6Level
    {
        get
        {
            return gameSettings.japaneseFood6Level;
        }
        set
        {
            gameSettings.japaneseFood6Level = value;
            SaveFile();
        }
    }

    public int JapaneseFood7Level
    {
        get
        {
            return gameSettings.japaneseFood7Level;
        }
        set
        {
            gameSettings.japaneseFood7Level = value;
            SaveFile();
        }
    }

    public int RamenLevel
    {
        get
        {
            return gameSettings.ramenLevel;
        }
        set
        {
            gameSettings.ramenLevel = value;
            SaveFile();
        }
    }

    public int Dessert1Level
    {
        get
        {
            return gameSettings.dessert1Level;
        }
        set
        {
            gameSettings.dessert1Level = value;
            SaveFile();
        }
    }

    public int Dessert2Level
    {
        get
        {
            return gameSettings.dessert2Level;
        }
        set
        {
            gameSettings.dessert2Level = value;
            SaveFile();
        }
    }

    public int Dessert3Level
    {
        get
        {
            return gameSettings.dessert3Level;
        }
        set
        {
            gameSettings.dessert3Level = value;
            SaveFile();
        }
    }

    public int Dessert4Level
    {
        get
        {
            return gameSettings.dessert4Level;
        }
        set
        {
            gameSettings.dessert4Level = value;
            SaveFile();
        }
    }

    public int Dessert5Level
    {
        get
        {
            return gameSettings.dessert5Level;
        }
        set
        {
            gameSettings.dessert5Level = value;
            SaveFile();
        }
    }

    public int Dessert6Level
    {
        get
        {
            return gameSettings.dessert6Level;
        }
        set
        {
            gameSettings.dessert6Level = value;
            SaveFile();
        }
    }

    public int Dessert7Level
    {
        get
        {
            return gameSettings.dessert7Level;
        }
        set
        {
            gameSettings.dessert7Level = value;
            SaveFile();
        }
    }

    public int Dessert8Level
    {
        get
        {
            return gameSettings.dessert8Level;
        }
        set
        {
            gameSettings.dessert8Level = value;
            SaveFile();
        }
    }

    public int Dessert9Level
    {
        get
        {
            return gameSettings.dessert9Level;
        }
        set
        {
            gameSettings.dessert9Level = value;
            SaveFile();
        }
    }

    public int FruitSkewersLevel
    {
        get
        {
            return gameSettings.fruitSkewersLevel;
        }
        set
        {
            gameSettings.fruitSkewersLevel = value;
            SaveFile();
        }
    }

    public float FeverCount
    {
        get
        {
            return gameSettings.feverCount;
        }
        set
        {
            gameSettings.feverCount = value;
            SaveFile();
        }
    }

    public int ConsumeGold
    {
        get
        {
            return gameSettings.consumeGold;
        }
        set
        {
            gameSettings.consumeGold = value;
            SaveFile();
        }
    }

    public int UpgradeCount
    {
        get
        {
            return gameSettings.upgradeCount;
        }
        set
        {
            gameSettings.upgradeCount = value;
            SaveFile();
        }
    }

    public int SellCount
    {
        get
        {
            return gameSettings.sellCount;
        }
        set
        {
            gameSettings.sellCount = value;
            SaveFile();
        }
    }

    public int UseSauce
    {
        get
        {
            return gameSettings.useSauce;
        }
        set
        {
            gameSettings.useSauce = value;
            SaveFile();
        }
    }

    public int OpenChestBox
    {
        get
        {
            return gameSettings.openChestBox;
        }
        set
        {
            gameSettings.openChestBox = value;
            SaveFile();
        }
    }

    public int YummyTimeCount
    {
        get
        {
            return gameSettings.yummyTimeCount;
        }
        set
        {
            gameSettings.yummyTimeCount = value;
            SaveFile();
        }
    }

    public int PlayTime
    {
        get
        {
            return gameSettings.playTime;
        }
        set
        {
            gameSettings.playTime = value;
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

    public int ChestBoxCount
    {
        get
        {
            return gameSettings.chestBoxCount;
        }
        set
        {
            gameSettings.chestBoxCount = value;
            SaveFile();
        }
    }

    public int ChestBoxCoolTime
    {
        get
        {
            return gameSettings.chestBoxCoolTime;
        }
        set
        {
            gameSettings.chestBoxCoolTime = value;
            SaveFile();
        }
    }

    public int Bankruptcy
    {
        get
        {
            return gameSettings.bankruptcy;
        }
        set
        {
            gameSettings.bankruptcy = value;
            SaveFile();
        }
    }

    public bool Tutorial
    {
        get
        {
            return gameSettings.tutorial;
        }
        set
        {
            gameSettings.tutorial = value;
            SaveFile();
        }
    }

    public bool Privacypolicy
    {
        get
        {
            return gameSettings.privacypolicy;
        }
        set
        {
            gameSettings.privacypolicy = value;
            SaveFile();
        }
    }

    public bool Pause
    {
        get
        {
            return gameSettings.pause;
        }
        set
        {
            gameSettings.pause = value;
            SaveFile();
        }
    }

    #endregion

    private void Awake()
    {
        instance = this;

        LoadData();
    }

    public void Initialize()
    {
        gameSettings = new GameSettings();

        string str = JsonUtility.ToJson(gameSettings);
        FileIO.SaveData(DEVICESETTINGFILENAME, str, true);
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
