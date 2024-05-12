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
        public bool todayQuiz = false;

        [Space]
        [Title("Auto")]
        public bool autoUpgrade = false;
        public int autoUpgradeLevel = 10;
        public bool autoPresent = false;

        [Space]
        public bool developer = false;
        public bool youtubeVideo = false;
        public int island = 5;


        [Space]
        [Title("InGame")]
        public GameType gameType = GameType.Story;
        public IslandType islandType = IslandType.Island1;
        public FoodType foodType = FoodType.Food1;
        public RankFoodType rankFoodType = RankFoodType.RankFood1;
        public int[] foodLevel = new int[Enum.GetValues(typeof(FoodType)).Length];
        public int[] rankFoodLevel = new int[Enum.GetValues(typeof(RankFoodType)).Length];
        public ParticleType particleType = ParticleType.Effect1;

        public int level = 0;

        [Space]
        [Title("Tip")]
        public bool firstSuccess;
        public bool firstFail;
        public bool firstDestory;
        public int destroyCount = 0;

        [Space]
        [Title("Save")]
        public float feverCount = 0;
        public int getGold = 0;
        public long consumeGold = 0; //계산 용
        public int upgradeCount = 0;
        public int sellCount = 0;
        public int useSauce = 0;
        public int openChestBox = 0;
        public int yummyTimeCount = 0;
        public int playTime = 0;
        public long saveGold = 0; //서버 저장 전에 나가버릴 경우
        public int supportCount = 0;
        public long getSellGold = 0; //판매 후 얻은 골드
        public long todayGold = 0;
        public long yesterdayGold = 0;

        [Space]
        [Title("Reset")]
        public bool portion1Ad = false;
        public bool portion2Ad = false;
        public bool portion3Ad = false;
        public bool portion4Ad = false;
        public bool portion5Ad = false;

        [Space]
        [Title("Equip")]
        public AnimalType animalType = AnimalType.Animal1;
        public TruckType truckType = TruckType.Truck1;
        public CharacterType characterType = CharacterType.Character1;
        public ButterflyType butterflyType = ButterflyType.Butterfly1;
        public TotemsType totemsType = TotemsType.Totems1;
        public FlowerType flowerType = FlowerType.Flower1;
        public BucketType bucketType = BucketType.Bucket1;
        public ChairType chairType = ChairType.Chair1;
        public TubeType tubeType = TubeType.Tube1;
        public SurfboardType surfboardType = SurfboardType.Surfboard1;
        public UmbrellaType umbrellaType = UmbrellaType.Umbrella1;

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

    public RankFoodType RankFoodType
    {
        get
        {
            return gameSettings.rankFoodType;
        }
        set
        {
            gameSettings.rankFoodType = value;
            SaveFile();
        }
    }

    public ParticleType ParticleType
    {
        get
        {
            return gameSettings.particleType;
        }
        set
        {
            gameSettings.particleType = value;
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

    public BucketType BucketType
    {
        get
        {
            return gameSettings.bucketType;
        }
        set
        {
            gameSettings.bucketType = value;
            SaveFile();
        }
    }

    public ChairType ChairType
    {
        get
        {
            return gameSettings.chairType;
        }
        set
        {
            gameSettings.chairType = value;
            SaveFile();
        }
    }

    public TubeType TubeType
    {
        get
        {
            return gameSettings.tubeType;
        }
        set
        {
            gameSettings.tubeType = value;
            SaveFile();
        }
    }

    public SurfboardType SurfboardType
    {
        get
        {
            return gameSettings.surfboardType;
        }
        set
        {
            gameSettings.surfboardType = value;
            SaveFile();
        }
    }

    public UmbrellaType UmbrellaType
    {
        get
        {
            return gameSettings.umbrellaType;
        }
        set
        {
            gameSettings.umbrellaType = value;
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

    public bool TodayQuiz
    {
        get
        {
            return gameSettings.todayQuiz;
        }
        set
        {
            gameSettings.todayQuiz = value;
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

    public int Island
    {
        get
        {
            return gameSettings.island;
        }
        set
        {
            gameSettings.island = value;
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

    public bool FirstDestory
    {
        get
        {
            return gameSettings.firstDestory;
        }
        set
        {
            gameSettings.firstDestory = value;
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
    public int[] FoodLevel
    {
        get
        {
            return gameSettings.foodLevel;
        }
        set
        {
            gameSettings.foodLevel = value;
            SaveFile();
        }
    }

    public int[] RankFoodLevel
    {
        get
        {
            return gameSettings.rankFoodLevel;
        }
        set
        {
            gameSettings.rankFoodLevel = value;
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

    public long ConsumeGold
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

    public long GetSellGold
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

    public long TodayGold
    {
        get
        {
            return gameSettings.todayGold;
        }
        set
        {
            gameSettings.todayGold = value;
            SaveFile();
        }
    }

    public long YesterdayGold
    {
        get
        {
            return gameSettings.yesterdayGold;
        }
        set
        {
            gameSettings.yesterdayGold = value;
            SaveFile();
        }
    }

    public bool Portion1Ad
    {
        get
        {
            return gameSettings.portion1Ad;
        }
        set
        {
            gameSettings.portion1Ad = value;
            SaveFile();
        }
    }

    public bool Portion2Ad
    {
        get
        {
            return gameSettings.portion2Ad;
        }
        set
        {
            gameSettings.portion2Ad = value;
            SaveFile();
        }
    }

    public bool Portion3Ad
    {
        get
        {
            return gameSettings.portion3Ad;
        }
        set
        {
            gameSettings.portion3Ad = value;
            SaveFile();
        }
    }

    public bool Portion4Ad
    {
        get
        {
            return gameSettings.portion4Ad;
        }
        set
        {
            gameSettings.portion4Ad = value;
            SaveFile();
        }
    }

    public bool Portion5Ad
    {
        get
        {
            return gameSettings.portion5Ad;
        }
        set
        {
            gameSettings.portion5Ad = value;
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

                if(FoodLevel.Length < Enum.GetValues(typeof(FoodType)).Length) //새로운 음식이 추가됬을 경우
                {
                    int[] foodArray = new int[Enum.GetValues(typeof(FoodType)).Length];

                    for(int i = 0; i < FoodLevel.Length; i ++)
                    {
                        foodArray[i] = FoodLevel[i];
                    }

                    FoodLevel = new int[Enum.GetValues(typeof(FoodType)).Length];

                    for (int i = 0; i < foodArray.Length; i++)
                    {
                        FoodLevel[i] = foodArray[i];
                    }
                }

                if (RankFoodLevel.Length < Enum.GetValues(typeof(RankFoodType)).Length) //새로운 음식이 추가됬을 경우
                {
                    int[] foodArray = new int[Enum.GetValues(typeof(RankFoodType)).Length];

                    for (int i = 0; i < RankFoodLevel.Length; i++)
                    {
                        foodArray[i] = RankFoodLevel[i];
                    }

                    RankFoodLevel = new int[Enum.GetValues(typeof(RankFoodType)).Length];

                    for (int i = 0; i < foodArray.Length; i++)
                    {
                        RankFoodLevel[i] = foodArray[i];
                    }
                }
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
