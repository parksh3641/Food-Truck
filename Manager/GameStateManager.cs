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
        public StoreType storeType = StoreType.None;

        [Space]
        [Title("Login")]
        public string playfabId = "";
        public string customId = "";
        public string region = "";

        public bool autoLogin = false;
        public LoginType loginType = LoginType.None;
        public string nickName = "";

        [Space]
        [Title("Language")]
        public LanguageType language = LanguageType.Default;

        [Space]
        [Title("Game Setting")]
        public bool music = true;
        public bool sfx = true;
        public bool vibration = true;
        public bool effect = true;
        public bool backgroundEffect = true;
        public bool recover = true;

        public bool appReview = false;
        public bool rankingNotice = false;
        public bool hideNotice = false;

        [Space]
        [Title("Auto")]
        public bool autoUpgrade = false;
        public int autoUpgradeLevel = 10;
        public bool autoPresent = false;

        [Space]
        public bool developer = false;
        public bool youtubeVideo = false;


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
        [Title("Tip")]
        public bool firstSuccess;
        public bool firstFail;
        public int destroyCount = 0;

        [Space]
        [Title("Level")]
        public int food1Level = 0;
        public int food2Level = 0;
        public int food3Level = 0;
        public int food4Level = 0;
        public int food5Level = 0;
        public int food6Level = 0;
        public int food7Level = 0;

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

        [Space]
        public int japaneseFood1Level = 0;
        public int japaneseFood2Level = 0;
        public int japaneseFood3Level = 0;
        public int japaneseFood4Level = 0;
        public int japaneseFood5Level = 0;
        public int japaneseFood6Level = 0;
        public int japaneseFood7Level = 0;

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

        [Space]
        [Title("Rank")]
        public int food8Level = 0;
        public int candy10Level = 0;
        public int japaneseFood8Level = 0;
        public int dessert10Level = 0;

        [Space]
        [Title("Save")]
        public float feverCount = 0;
        public int getGold = 0;
        public int consumeGold = 0;
        public int upgradeCount = 0;
        public int sellCount = 0;
        public int useSauce = 0;
        public int openChestBox = 0;
        public int yummyTimeCount = 0;
        public int playTime = 0;
        public long saveGold = 0;
        public int supportCount = 0;
        public int getSellGold = 0; //판매 후 얻은 골드

        [Space]
        [Title("Equip")]
        public AnimalType animalType = AnimalType.Animal1;
        public TruckType truckType = TruckType.Truck1;
        public CharacterType characterType = CharacterType.Character1;
        public ButterflyType butterflyType = ButterflyType.Butterfly1;
        public TotemsType totemsType = TotemsType.Totems1;
        public FlowerType flowerType = FlowerType.Flower1;

        [Space]
        [Title("Ads")]
        public int chestBoxCount = 0;

        [Space]
        [Title("Bankruptcy")]
        public int bankruptcy = 0;

        public bool privacypolicy = false;
        public bool gender = false;
        public bool pause = false;
    }

    #region Data
    public StoreType StoreType
    {
        get
        {
            return gameSettings.storeType;
        }
        set
        {
            gameSettings.storeType = value;
            SaveFile();
        }
    }

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

    public string Region
    {
        get
        {
            return gameSettings.region;
        }
        set
        {
            gameSettings.region = value;
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

    public TotemsType TotemsType
    {
        get
        {
            return gameSettings.totemsType;
        }
        set
        {
            gameSettings.totemsType = value;
            SaveFile();
        }
    }

    public FlowerType FlowerType
    {
        get
        {
            return gameSettings.flowerType;
        }
        set
        {
            gameSettings.flowerType = value;
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

    public bool BackgroundEffect
    {
        get
        {
            return gameSettings.backgroundEffect;
        }
        set
        {
            gameSettings.backgroundEffect = value;
            SaveFile();
        }
    }

    public bool Recover
    {
        get
        {
            return gameSettings.recover;
        }
        set
        {
            gameSettings.recover = value;
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

    public bool HideNotice
    {
        get
        {
            return gameSettings.hideNotice;
        }
        set
        {
            gameSettings.hideNotice = value;
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

    public bool YoutubeVideo
    {
        get
        {
            return gameSettings.youtubeVideo;
        }
        set
        {
            gameSettings.youtubeVideo = value;
            SaveFile();
        }
    }

    public bool AutoUpgrade
    {
        get
        {
            return gameSettings.autoUpgrade;
        }
        set
        {
            gameSettings.autoUpgrade = value;
            SaveFile();
        }
    }

    public int AutoUpgradeLevel
    {
        get
        {
            return gameSettings.autoUpgradeLevel;
        }
        set
        {
            gameSettings.autoUpgradeLevel = value;
            SaveFile();
        }
    }

    public bool AutoPresent
    {
        get
        {
            return gameSettings.autoPresent;
        }
        set
        {
            gameSettings.autoPresent = value;
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

    public bool FirstSuccess
    {
        get
        {
            return gameSettings.firstSuccess;
        }
        set
        {
            gameSettings.firstSuccess = value;
            SaveFile();
        }
    }

    public bool FirstFail
    {
        get
        {
            return gameSettings.firstFail;
        }
        set
        {
            gameSettings.firstFail = value;
            SaveFile();
        }
    }

    public int DestroyCount
    {
        get
        {
            return gameSettings.destroyCount;
        }
        set
        {
            gameSettings.destroyCount = value;
            SaveFile();
        }
    }

    public int Food1Level
    {
        get
        {
            return gameSettings.food1Level;
        }
        set
        {
            gameSettings.food1Level = value;
            SaveFile();
        }
    }

    public int Food2Level
    {
        get
        {
            return gameSettings.food2Level;
        }
        set
        {
            gameSettings.food2Level = value;
            SaveFile();
        }
    }

    public int Food3Level
    {
        get
        {
            return gameSettings.food3Level;
        }
        set
        {
            gameSettings.food3Level = value;
            SaveFile();
        }
    }

    public int Food4Level
    {
        get
        {
            return gameSettings.food4Level;
        }
        set
        {
            gameSettings.food4Level = value;
            SaveFile();
        }
    }

    public int Food5Level
    {
        get
        {
            return gameSettings.food5Level;
        }
        set
        {
            gameSettings.food5Level = value;
            SaveFile();
        }
    }

    public int Food6Level
    {
        get
        {
            return gameSettings.food6Level;
        }
        set
        {
            gameSettings.food6Level = value;
            SaveFile();
        }
    }

    public int Food7Level
    {
        get
        {
            return gameSettings.food7Level;
        }
        set
        {
            gameSettings.food7Level = value;
            SaveFile();
        }
    }

    public int Food8Level
    {
        get
        {
            return gameSettings.food8Level;
        }
        set
        {
            gameSettings.food8Level = value;
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

    public int Candy10Level
    {
        get
        {
            return gameSettings.candy10Level;
        }
        set
        {
            gameSettings.candy10Level = value;
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

    public int JapaneseFood8Level
    {
        get
        {
            return gameSettings.japaneseFood8Level;
        }
        set
        {
            gameSettings.japaneseFood8Level = value;
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

    public int Dessert10Level
    {
        get
        {
            return gameSettings.dessert10Level;
        }
        set
        {
            gameSettings.dessert10Level = value;
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

    public int GetGold
    {
        get
        {
            return gameSettings.getGold;
        }
        set
        {
            gameSettings.getGold = value;
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

    public long SaveGold
    {
        get
        {
            return gameSettings.saveGold;
        }
        set
        {
            gameSettings.saveGold = value;
            SaveFile();
        }
    }

    public int SupportCount
    {
        get
        {
            return gameSettings.supportCount;
        }
        set
        {
            gameSettings.supportCount = value;
            SaveFile();
        }
    }

    public int GetSellGold
    {
        get
        {
            return gameSettings.getSellGold;
        }
        set
        {
            gameSettings.getSellGold = value;
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

    public bool Gender
    {
        get
        {
            return gameSettings.gender;
        }
        set
        {
            gameSettings.gender = value;
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
