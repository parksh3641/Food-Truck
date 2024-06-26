//using Facebook.Unity;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using PlayFab.ProfilesModels;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_IOS
using AppleAuth;
using AppleAuth.Native;
using AppleAuth.Enums;
using AppleAuth.Interfaces;
using AppleAuth.Extensions;
#endif

using EntityKey = PlayFab.ProfilesModels.EntityKey;
using System.Text;
using UnityEngine.SceneManagement;
using Firebase.Analytics;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager instance;

    [ShowInInspector]
    string customId = "";

    public bool isActive = false;
    public bool isLogin = false;
    public bool isUpdate = false;
    public bool isDelay = false;

    private bool playerData = false;
    private bool statisticsData = false;
    private bool inventoryData = false;
    private bool grantItemData = false;
    private bool waitGrantItem = false;

    private long coin = 0;
    private long coinA = 0;
    private long coinB = 0;

    private long consumeGold = 0;
    private long consumeGoldA = 0;
    private long consumeGoldB = 0;

    private int getGoldA = 0;
    private int getGoldB = 0;

    private int saveGold = 0;

    private bool saveDelay = false;

    public NickNameManager nickNameManager;
    public MoneyAnimation moneyAnimation;
    public MoneyAnimation moneyAnimation2;
    public OptionManager optionManager;
    public RankingManager rankingManager;
    public NewsManager newsManager;


    List<string> itemList = new List<string>();

#if UNITY_IOS
    private string AppleUserIdKey = "";
    private IAppleAuthManager _appleAuthManager;

#endif

    WaitForSeconds waitForSeconds = new WaitForSeconds(2f);

    PlayerDataBase playerDataBase;


    [Header("Entity")]
    private string entityId;
    private string entityType;
    private readonly Dictionary<string, string> entityFileJson = new Dictionary<string, string>();

    private List<ItemInstance> inventoryList = new List<ItemInstance>();

    Dictionary<string, string> defaultCustomData = new Dictionary<string, string>() { { "AbilityLevel", "0" }, { "Rare", "0" }, { "Level", "0" } };

    private void Awake()
    {
        Application.targetFrameRate = 60;

#if UNITY_IOS
        Application.targetFrameRate = 90;
#endif

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        isActive = false;
        isLogin = false;
        isUpdate = false;
        isDelay = false;

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        playerDataBase.Initialize();

#if UNITY_ANDROID
        GoogleActivate();
#elif UNITY_IOS
        IOSActivate();
#endif
    }

    private void Start()
    {
        GameStateManager.instance.StoreType = StoreType.None;

#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
        string installerPackageName = packageManager.Call<string>("getInstallerPackageName", Application.identifier);

        // 패키지명으로부터 설치된 스토어 확인

        if (installerPackageName.Equals("com.android.vending"))
        {
            GameStateManager.instance.StoreType = StoreType.Google;

            Debug.Log("앱은 Google Play 스토어에서 설치되었습니다.");
        }
        else if (installerPackageName.Equals("com.amazon.venezia"))
        {
            GameStateManager.instance.StoreType = StoreType.Amazon;

            Debug.Log("앱은 Amazon Appstore에서 설치되었습니다.");
        }
        else if (installerPackageName.Equals("com.skt.skaf.A000Z00040") || installerPackageName.Equals("com.kt.olleh.storefront")
            || installerPackageName.Equals("android.lgt.appstore") || installerPackageName.Equals("com.lguplus.appstore"))
        {
            GameStateManager.instance.StoreType = StoreType.OneStore;

            Debug.Log("앱은 OneStore에서 설치되었습니다.");
        }
        else
        {
            GameStateManager.instance.StoreType = StoreType.None;

            Debug.Log("앱은 알 수 없는 소스에서 설치되었습니다.");
        }
#endif

        //GameStateManager.instance.StoreType = StoreType.OneStore;
    }

    public void Login()
    {
        if (GameStateManager.instance.AutoLogin)
        {
            StateManager.instance.LoginStart();

            switch (GameStateManager.instance.Login)
            {
                case LoginType.None:
                    break;
                case LoginType.Guest:
                    OnClickGuestLogin();
                    break;
                case LoginType.Google:
                    OnClickGoogleLogin();
                    break;
                case LoginType.Facebook:
                    //OnClickFacebookLogin();
                    break;
                case LoginType.Apple:
                    OnClickAppleLogin();
                    break;
            }
        }
    }

    #region Initialize

#if UNITY_ANDROID
    private void GoogleActivate()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .AddOauthScope("profile")
        .RequestServerAuthCode(false)
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Debug.Log("Google Activate Success");
    }
#endif
#if UNITY_IOS
    private void IOSActivate()
    {
        // If the current platform is supported
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            // Creates a default JSON deserializer, to transform JSON Native responses to C# instances
            var deserializer = new PayloadDeserializer();
            // Creates an Apple Authentication manager with the deserializer
            _appleAuthManager = new AppleAuthManager(deserializer);
        }
        StartCoroutine(AppleAuthUpdate());
    }

    IEnumerator AppleAuthUpdate()
    {
        while (true)
        {
            _appleAuthManager?.Update();
            yield return null;
        }
    }
#endif
    #endregion

    public void LogOut()
    {
#if UNITY_EDITOR || UNITY_EDITOR_OSX
        PlayFabClientAPI.ForgetAllCredentials();
#elif UNITY_ANDROID
        ((PlayGamesPlatform)Social.Active).SignOut();
#endif

        SuccessLogOut();
    }

    public void SuccessLogOut()
    {
        GameStateManager.instance.Initialize();

        isActive = false;
        isLogin = false;
        isDelay = false;

        playerDataBase.Initialize();

        PlayerPrefs.SetString("AppleLogin", "");

        Debug.Log("Logout");

        SceneManager.LoadScene("MainScene");
    }


#region Message
    private void SetEditorOnlyMessage(string message, bool error = false)
    {
#if UNITY_EDITOR || UNITY_EDITOR_OSX
        if (error) Debug.LogError("<color=red>" + message + "</color>");
        //else Debug.Log(message);
#endif
    }
    private void DisplayPlayfabError(PlayFabError error)
    {
        SetEditorOnlyMessage("error : " + error.GenerateErrorReport(), true);
    }

#endregion
#region GuestLogin
    public void OnClickGuestLogin()
    {
        GameManager.instance.loginView.SetActive(false);

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            GameManager.instance.OpenLoginView();

            SoundManager.instance.PlaySFX(GameSfxType.Wrong);

            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (isLogin) return;

        isLogin = true;

        customId = GameStateManager.instance.CustomId;

        if (string.IsNullOrEmpty(customId))
            CreateGuestId();
        else
            LoginGuestId();
    }

    private void CreateGuestId()
    {
        Debug.Log("New PlayfabId");

        customId = GetRandomPassword(16);

        GameStateManager.instance.CustomId = customId;

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CustomId = customId,
            CreateAccount = true
        }, result =>
        {
            GameStateManager.instance.AutoLogin = true;
            GameStateManager.instance.Login = LoginType.Guest;
            OnLoginSuccess(result);
        }, error =>
        {
            Debug.LogError("Login Fail - Guest");

            GameManager.instance.OpenLoginView();

            isLogin = false;
        });
    }

    private string GetRandomPassword(int _totLen)
    {
        string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var chars = Enumerable.Range(0, _totLen)
            .Select(x => input[UnityEngine.Random.Range(0, input.Length)]);
        return new string(chars.ToArray());
    }

    private void LoginGuestId()
    {
        Debug.Log("Guest Login");

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CustomId = customId,
            CreateAccount = false
        }, result =>
        {
            GameStateManager.instance.AutoLogin = true;
            GameStateManager.instance.Login = LoginType.Guest;
            OnLoginSuccess(result);
        }, error =>
        {
            Debug.LogError("Login Fail - Guest");

            isLogin = false;

            GameManager.instance.OpenLoginView();
        });
    }

#endregion
#region Google Login
    public void OnClickGoogleLogin()
    {
        GameManager.instance.loginView.SetActive(false);

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            GameManager.instance.OpenLoginView();

            SoundManager.instance.PlaySFX(GameSfxType.Wrong);

            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (isLogin) return;

        isLogin = true;

        Debug.Log("Google Login");

#if UNITY_ANDROID
        LoginGoogleAuthenticate();
#else
        SetEditorOnlyMessage("Only Android Platform");
#endif
    }

    private void LoginGoogleAuthenticate()
    {
#if UNITY_ANDROID
        Social.localUser.Authenticate((bool success) =>
        {
            if (!success)
            {
                return;
            }

            var serverAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();
            PlayFabClientAPI.LoginWithGoogleAccount(new LoginWithGoogleAccountRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                ServerAuthCode = serverAuthCode,
                CreateAccount = true,
            },
            result =>
            {
                GameStateManager.instance.AutoLogin = true;
                GameStateManager.instance.Login = LoginType.Google;

                Debug.Log("Google Login Success");

                OnLoginSuccess(result);
            },
            error =>
            {
                Debug.Log("Google Login Fail");

                DisplayPlayfabError(error);

                isLogin = false;

                GameManager.instance.OpenLoginView();
            });
        });

#endif
    }


    public void OnClickGoogleLink()
    {
#if UNITY_ANDROID
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    var serverAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();

                    LinkGoogleAccountRequest request = new LinkGoogleAccountRequest()
                    {
                        ServerAuthCode = serverAuthCode
                    };

                    PlayFabClientAPI.LinkGoogleAccount(request, result =>
                    {
                        Debug.Log("Link Google Account Success");

                        GameStateManager.instance.AutoLogin = true;
                        GameStateManager.instance.Login = LoginType.Google;

                        optionManager.SuccessGoogleLink();

                        SoundManager.instance.PlaySFX(GameSfxType.Success);
                        NotionManager.instance.UseNotion(NotionType.SuccessLink);
                    }, error =>
                    {
                        if(error.Error == PlayFabErrorCode.AccountAlreadyLinked)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.AlreadyLink);
                        }
                        else
                        {
                            Debug.Log(error.GenerateErrorReport());

                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.FailLink);

                            Debug.Log("Link Google Account Fail");
                        }
                    });
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.FailLink);

                    Debug.Log("Link Google Account Fail");
                }
            });
        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.FailLink);

            Debug.Log("Link Google Account Fail");
        }
#endif
    }
#endregion
#region Apple Login

    public void OnClickAppleLogin()
    {
        GameManager.instance.loginView.SetActive(false);

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            GameManager.instance.OpenLoginView();

            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (isLogin) return;

        isLogin = true;

#if UNITY_IOS
        Debug.Log("Try Apple Login");
        StartCoroutine(AppleLoginCor());
#endif
    }

    public void OnClickAppleLink()
    {
#if UNITY_IOS
        OnClickAppleLink(true);
#endif
    }

#if UNITY_IOS
    void SignInWithApple()
    {
        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

        _appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
                var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
                    OnClickAppleLogin(appleIdCredential.IdentityToken);
                }
            }, error =>
            {
                GameManager.instance.OpenLoginView();

                isLogin = false;

                var authorizationErrorCode = error.GetAuthorizationErrorCode();
            });
    }

    IEnumerator AppleLoginCor()
    {
        if (SaveByte.LoadByteArrayToPlayerPrefs() != null)
        {
            byte[] apple = SaveByte.LoadByteArrayToPlayerPrefs();

            Debug.Log("Apple Auto Login");

            OnClickAppleLogin(apple);
        }
        else
        {
            IOSActivate();

            var _newAppleUser = false;

            while (_appleAuthManager == null) yield return null;

            if (!_newAppleUser)
            {
                var quickLoginArgs = new AppleAuthQuickLoginArgs();

                _appleAuthManager.QuickLogin(
                    quickLoginArgs,
                    credential =>
                    {
                        var appleIdCredential = credential as IAppleIDCredential;
                        if (appleIdCredential != null)
                        {
                            OnClickAppleLogin(appleIdCredential.IdentityToken);
                        }
                    },
                    error =>
                    {
                        _newAppleUser = true;
                        SignInWithApple();
                        var authorizationErrorCode = error.GetAuthorizationErrorCode();
                    });
            }
            else
            {
                SignInWithApple();
            }
            yield return null;
        }
    }

    public void OnClickAppleLogin(byte[] identityToken)
    {
        PlayFabClientAPI.LoginWithApple(new LoginWithAppleRequest
        {
            CreateAccount = true,
            IdentityToken = Encoding.UTF8.GetString(identityToken),
            TitleId = PlayFabSettings.TitleId
        }
        , result =>
        {
            Debug.Log("Apple Login Success");
            
            SaveByte.SaveByteArrayToPlayerPref(identityToken);

            GameStateManager.instance.AutoLogin = true;
            GameStateManager.instance.Login = LoginType.Apple;

            OnLoginSuccess(result);
        }
        , DisplayPlayfabError);
    }

    public void OnClickAppleLink(bool forceLink = false)
    {
        var quickLoginArgs = new AppleAuthQuickLoginArgs();

        _appleAuthManager.QuickLogin(quickLoginArgs, credential =>
        {
            var appleIdCredential = credential as IAppleIDCredential;
            if (appleIdCredential != null)
            {
                TryLinkAppleAccount(appleIdCredential.IdentityToken, forceLink);
            }
        }, error =>
        {
            var authorizationErrorCode = error.GetAuthorizationErrorCode();

            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.FailLink);

            Debug.Log("Link Apple Account Fail");
        });
    }

    public void TryLinkAppleAccount(byte[] identityToken, bool forceLink)
    {
        PlayFabClientAPI.LinkApple(new LinkAppleRequest
        {
            ForceLink = forceLink,
            IdentityToken = Encoding.UTF8.GetString(identityToken)
        }
        , result =>
        {
            Debug.Log("Link Apple Success!!");

            GameStateManager.instance.AutoLogin = true;
            GameStateManager.instance.Login = LoginType.Apple;

            optionManager.SuccessAppleLink();

            SoundManager.instance.PlaySFX(GameSfxType.Success);
            NotionManager.instance.UseNotion(NotionType.SuccessLink);
        }
        , DisplayPlayfabError);
    }
#endif
    #endregion

    public void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
    {
        SetEditorOnlyMessage("Playfab Login Success");

        customId = result.PlayFabId;
        entityId = result.EntityToken.Entity.Id;
        entityType = result.EntityToken.Entity.Type;

        GameStateManager.instance.PlayfabId = result.PlayFabId;

        if (GameStateManager.instance.StoreType == StoreType.OneStore)
        {
            StartCoroutine(LoadDataCoroutine());
            return;
        }

#if UNITY_EDITOR || UNITY_EDITOR_OSX
        StartCoroutine(LoadDataCoroutine());
#elif UNITY_ANDROID
        GetTitleInternalData("CheckAOSVersion", CheckVersion);
#elif UNITY_IOS
        GetTitleInternalData("CheckIOSVersion", CheckVersion);
#endif
    }

    public void CheckVersion(bool check)
    {
        Debug.Log("Checking Version...");

        if (check)
        {
#if UNITY_ANDROID
            GetTitleInternalData("AOSVersion", CheckUpdate);
#elif UNITY_IOS
            GetTitleInternalData("IOSVersion", CheckUpdate);
#endif
        }
        else
        {
            StartCoroutine(LoadDataCoroutine());
        }
    }


    public void CheckUpdate(bool check)
    {
        if (check)
        {
            StartCoroutine(LoadDataCoroutine());
        }
        else
        {
            GameManager.instance.OnNeedUpdate();
        }
    }


    public void SetProfileLanguage(LanguageType type)
    {
        EntityKey entity = new EntityKey();
        entity.Id = entityId;
        entity.Type = entityType;

        var request = new SetProfileLanguageRequest
        {
            Language = type.ToString(),
            ExpectedVersion = 0,
            Entity = entity
        };
        PlayFabProfilesAPI.SetProfileLanguage(request, res =>
        {
            Debug.Log("The language on the entity's profile has been updated.");
        }, FailureCallback);
    }

    public void GetPlayerNickName()
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
        {
            PlayFabId = customId,
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true
            }
        },
        (result) =>
        {
            GameStateManager.instance.NickName = result.PlayerProfile.DisplayName;

            if (GameStateManager.instance.NickName == null)
            {
                UpdateDisplayName(GameStateManager.instance.PlayfabId);
                //nickNameManager.nickNameView.SetActive(true);
            }
        },
        DisplayPlayfabError);
    }

    void FailureCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    private void OnCloudUpdateStats(ExecuteCloudScriptResult result)
    {
        SetEditorOnlyMessage(PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer).SerializeObject(result.FunctionResult));
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        foreach (var json in jsonResult)
        {
            SetEditorOnlyMessage(json.Key + " / " + json.Value);
        }
        object messageValue;
        jsonResult.TryGetValue("OnCloudUpdateStats() messageValue", out messageValue);
        SetEditorOnlyMessage((string)messageValue);

        //GetUserInventory();
    }


    IEnumerator LoadDataCoroutine()
    {
        playerData = false;
        statisticsData = false;
        inventoryData = false;

        StateManager.instance.ServerStart();

        Debug.Log("Load Data...");

        GetPlayerNickName();

        //yield return new WaitForSeconds(0.5f);

        //yield return GetCatalog();

        //yield return new WaitForSeconds(0.5f);

        GetPlayerData();

        while (!playerData)
        {
            yield return null;
        }

        isActive = true;

        GetStatistics();

        while (!statisticsData)
        {
            yield return null;
        }

        GetUserInventory();

        while (!inventoryData)
        {
            yield return null;
        }

        //if (playerDataBase.RemoveAds)
        //{
        //    GoogleAdsManager.instance.admobBanner.DestroyAd();
        //}

        StateManager.instance.Initialize();
    }

    public void GetUserInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result =>
        {
            var Inventory = result.Inventory;
            int coinA = result.VirtualCurrency["GO"];
            int coinB = result.VirtualCurrency["GA"];
            int crystal = result.VirtualCurrency["ST"];

            if(coinA < 0)
            {
                UpdateAddCurrency(MoneyType.CoinA, Mathf.Abs(coinA));

                coinA = 0;
            }

            if (coinB < 0)
            {
                UpdateAddCurrency(MoneyType.CoinB, Mathf.Abs(coinB));

                coinB = 0;
            }

            if (coinA > 2000000000)
            {
                coinA = 2000000000;
            }

            if (coinB > 2000000000)
            {
                coinB = 2000000000;
            }

            if (crystal > 2000000000)
            {
                crystal = 2000000000;
            }

            playerDataBase.CoinA = coinA;
            playerDataBase.CoinB = coinB;
            playerDataBase.Crystal = crystal;

            if (Inventory != null)
            {
                for (int i = 0; i < Inventory.Count; i++)
                {
                    inventoryList.Add(Inventory[i]);
                }

                foreach (ItemInstance list in inventoryList)
                {
                    if (list.ItemId.Contains("Icon_"))
                    {
                        try
                        {
                            IconType icon = (IconType)Enum.Parse(typeof(IconType), list.ItemId);

                            playerDataBase.SetIcon(icon, (int)list.RemainingUses);
                        }
                        catch (ArgumentException e)
                        {

                        }
                    }

                    if (list.ItemId.Contains("Character") || list.ItemId.Contains("Butterfly") || list.ItemId.Contains("Totems") || list.ItemId.Contains("Bucket")
                    || list.ItemId.Contains("Chair") || list.ItemId.Contains("Tube") || list.ItemId.Contains("Surfboard") || list.ItemId.Contains("Umbrella"))
                    {
                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }
                    }

                    if (list.ItemId.Equals("RemoveAds"))
                    {
                        playerDataBase.RemoveAds = true;
                    }

                    if (list.ItemId.Equals("GoldX2"))
                    {
                        playerDataBase.GoldX2 = true;
                    }

                    if (list.ItemId.Equals("SuperOffline"))
                    {
                        playerDataBase.SuperOffline = true;
                    }

                    if (list.ItemId.Equals("AutoUpgrade"))
                    {
                        playerDataBase.AutoUpgrade = true;
                    }

                    if (list.ItemId.Equals("AutoPresent"))
                    {
                        playerDataBase.AutoPresent = true;
                    }

                    if (list.ItemId.Equals("SuperExp"))
                    {
                        playerDataBase.SuperExp = true;
                    }

                    if (list.ItemId.Equals("SuperKitchen"))
                    {
                        playerDataBase.SuperKitchen = true;
                    }

                    if (list.ItemId.Equals("Character1"))
                    {
                        playerDataBase.Character1 = 1;

                        playerDataBase.SetItemInstance(list, 0, 0);
                    }

                    if (list.ItemId.Equals("Character2"))
                    {
                        playerDataBase.Character2 = 1;

                        playerDataBase.SetItemInstance(list, 0, 1);
                    }

                    if (list.ItemId.Equals("Character3"))
                    {
                        playerDataBase.Character3 = 1;

                        playerDataBase.SetItemInstance(list, 0, 2);
                    }

                    if (list.ItemId.Equals("Character4"))
                    {
                        playerDataBase.Character4 = 1;

                        playerDataBase.SetItemInstance(list, 0, 3);
                    }

                    if (list.ItemId.Equals("Character5"))
                    {
                        playerDataBase.Character5 = 1;

                        playerDataBase.SetItemInstance(list, 0, 4);
                    }

                    if (list.ItemId.Equals("Character6"))
                    {
                        playerDataBase.Character6 = 1;

                        playerDataBase.SetItemInstance(list, 0, 5);
                    }

                    if (list.ItemId.Equals("Character7"))
                    {
                        playerDataBase.Character7 = 1;

                        playerDataBase.SetItemInstance(list, 0, 6);
                    }

                    if (list.ItemId.Equals("Character8"))
                    {
                        playerDataBase.Character8 = 1;

                        playerDataBase.SetItemInstance(list, 0, 7);
                    }

                    if (list.ItemId.Equals("Character9"))
                    {
                        playerDataBase.Character9 = 1;

                        playerDataBase.SetItemInstance(list, 0, 8);
                    }

                    if (list.ItemId.Equals("Character10"))
                    {
                        playerDataBase.Character10 = 1;

                        playerDataBase.SetItemInstance(list, 0, 9);
                    }

                    if (list.ItemId.Equals("Character11"))
                    {
                        playerDataBase.Character11 = 1;

                        playerDataBase.SetItemInstance(list, 0, 10);
                    }

                    if (list.ItemId.Equals("Character12"))
                    {
                        playerDataBase.Character12 = 1;

                        playerDataBase.SetItemInstance(list, 0, 11);
                    }

                    if (list.ItemId.Equals("Character13"))
                    {
                        playerDataBase.Character13 = 1;

                        playerDataBase.SetItemInstance(list, 0, 12);
                    }

                    if (list.ItemId.Equals("Character14"))
                    {
                        playerDataBase.Character14 = 1;

                        playerDataBase.SetItemInstance(list, 0, 13);
                    }

                    if (list.ItemId.Equals("Character15"))
                    {
                        playerDataBase.Character15 = 1;

                        playerDataBase.SetItemInstance(list, 0, 14);
                    }

                    if (list.ItemId.Equals("Character16"))
                    {
                        playerDataBase.Character16 = 1;

                        playerDataBase.SetItemInstance(list, 0, 15);
                    }

                    if (list.ItemId.Equals("Character17"))
                    {
                        playerDataBase.Character17 = 1;

                        playerDataBase.SetItemInstance(list, 0, 16);
                    }

                    if (list.ItemId.Equals("Character18"))
                    {
                        playerDataBase.Character18 = 1;

                        playerDataBase.SetItemInstance(list, 0, 17);
                    }

                    if (list.ItemId.Equals("Character19"))
                    {
                        playerDataBase.Character19 = 1;

                        playerDataBase.SetItemInstance(list, 0, 18);
                    }

                    if (list.ItemId.Equals("Character20"))
                    {
                        playerDataBase.Character20 = 1;

                        playerDataBase.SetItemInstance(list, 0, 19);
                    }

                    if (list.ItemId.Equals("Character21"))
                    {
                        playerDataBase.Character21 = 1;

                        playerDataBase.SetItemInstance(list, 0, 20);
                    }

                    if (list.ItemId.Equals("Truck1"))
                    {
                        playerDataBase.Truck1 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 1, 0);
                    }

                    if (list.ItemId.Equals("Truck2"))
                    {
                        playerDataBase.Truck2 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 1, 1);
                    }

                    if (list.ItemId.Equals("Truck3"))
                    {
                        playerDataBase.Truck3 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 1, 2);
                    }

                    if (list.ItemId.Equals("Truck4"))
                    {
                        playerDataBase.Truck4 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 1, 3);
                    }

                    if (list.ItemId.Equals("Truck5"))
                    {
                        playerDataBase.Truck5 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 1, 4);
                    }

                    if (list.ItemId.Equals("Truck6"))
                    {
                        playerDataBase.Truck6 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 1, 5);
                    }

                    if (list.ItemId.Equals("Truck7"))
                    {
                        playerDataBase.Truck7 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 1, 6);
                    }

                    if (list.ItemId.Equals("Truck8"))
                    {
                        playerDataBase.Truck8 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 1, 7);
                    }

                    if (list.ItemId.Equals("Truck9"))
                    {
                        playerDataBase.Truck9 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 1, 8);
                    }

                    if (list.ItemId.Equals("Truck10"))
                    {
                        playerDataBase.Truck10 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 1, 9);
                    }

                    if (list.ItemId.Equals("Animal1"))
                    {
                        playerDataBase.Animal1 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 2, 0);
                    }

                    if (list.ItemId.Equals("Animal2"))
                    {
                        playerDataBase.Animal2 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 2, 1);
                    }

                    if (list.ItemId.Equals("Animal3"))
                    {
                        playerDataBase.Animal3 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 2, 2);
                    }

                    if (list.ItemId.Equals("Animal4"))
                    {
                        playerDataBase.Animal4 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 2, 3);
                    }

                    if (list.ItemId.Equals("Animal5"))
                    {
                        playerDataBase.Animal5 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 2, 4);
                    }

                    if (list.ItemId.Equals("Animal6"))
                    {
                        playerDataBase.Animal6 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 2, 5);
                    }

                    if (list.ItemId.Equals("Animal7"))
                    {
                        playerDataBase.Animal7 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 2, 6);
                    }

                    if (list.ItemId.Equals("Animal8"))
                    {
                        playerDataBase.Animal8 = 1;

                        if (list.CustomData == null)
                        {
                            SetInventoryCustomData(list.ItemInstanceId, defaultCustomData);
                        }

                        playerDataBase.SetItemInstance(list, 2, 7);
                    }

                    if (list.ItemId.Equals("Butterfly1"))
                    {
                        playerDataBase.Butterfly1 = 1;

                        playerDataBase.SetItemInstance(list, 3, 0);
                    }

                    if (list.ItemId.Equals("Butterfly2"))
                    {
                        playerDataBase.Butterfly2 = 1;

                        playerDataBase.SetItemInstance(list, 3, 1);
                    }

                    if (list.ItemId.Equals("Butterfly3"))
                    {
                        playerDataBase.Butterfly3 = 1;

                        playerDataBase.SetItemInstance(list, 3, 2);
                    }

                    if (list.ItemId.Equals("Butterfly4"))
                    {
                        playerDataBase.Butterfly4 = 1;

                        playerDataBase.SetItemInstance(list, 3, 3);
                    }

                    if (list.ItemId.Equals("Butterfly5"))
                    {
                        playerDataBase.Butterfly5 = 1;

                        playerDataBase.SetItemInstance(list, 3, 4);
                    }

                    if (list.ItemId.Equals("Butterfly6"))
                    {
                        playerDataBase.Butterfly6 = 1;

                        playerDataBase.SetItemInstance(list, 3, 5);
                    }

                    if (list.ItemId.Equals("Butterfly7"))
                    {
                        playerDataBase.Butterfly7 = 1;

                        playerDataBase.SetItemInstance(list, 3, 6);
                    }

                    if (list.ItemId.Equals("Butterfly8"))
                    {
                        playerDataBase.Butterfly8 = 1;

                        playerDataBase.SetItemInstance(list, 3, 7);
                    }

                    if (list.ItemId.Equals("Butterfly9"))
                    {
                        playerDataBase.Butterfly9 = 1;

                        playerDataBase.SetItemInstance(list, 3, 8);
                    }

                    if (list.ItemId.Equals("Butterfly10"))
                    {
                        playerDataBase.Butterfly10 = 1;

                        playerDataBase.SetItemInstance(list, 3, 9);
                    }

                    if (list.ItemId.Equals("Butterfly11"))
                    {
                        playerDataBase.Butterfly11 = 1;

                        playerDataBase.SetItemInstance(list, 3, 10);
                    }

                    if (list.ItemId.Equals("Butterfly12"))
                    {
                        playerDataBase.Butterfly12 = 1;

                        playerDataBase.SetItemInstance(list, 3, 11);
                    }

                    if (list.ItemId.Equals("Butterfly13"))
                    {
                        playerDataBase.Butterfly13 = 1;

                        playerDataBase.SetItemInstance(list, 3, 12);
                    }

                    if (list.ItemId.Equals("Butterfly14"))
                    {
                        playerDataBase.Butterfly14 = 1;

                        playerDataBase.SetItemInstance(list, 3, 13);
                    }

                    if (list.ItemId.Equals("Butterfly15"))
                    {
                        playerDataBase.Butterfly15 = 1;

                        playerDataBase.SetItemInstance(list, 3, 14);
                    }

                    if (list.ItemId.Equals("Butterfly16"))
                    {
                        playerDataBase.Butterfly16 = 1;

                        playerDataBase.SetItemInstance(list, 3, 15);
                    }

                    if (list.ItemId.Equals("Butterfly17"))
                    {
                        playerDataBase.Butterfly17 = 1;

                        playerDataBase.SetItemInstance(list, 3, 16);
                    }

                    if (list.ItemId.Equals("Butterfly18"))
                    {
                        playerDataBase.Butterfly18 = 1;

                        playerDataBase.SetItemInstance(list, 3, 17);
                    }

                    if (list.ItemId.Equals("Butterfly19"))
                    {
                        playerDataBase.Butterfly19 = 1;

                        playerDataBase.SetItemInstance(list, 3, 18);
                    }

                    if (list.ItemId.Equals("Butterfly20"))
                    {
                        playerDataBase.Butterfly20 = 1;

                        playerDataBase.SetItemInstance(list, 3, 19);
                    }

                    if (list.ItemId.Equals("Butterfly21"))
                    {
                        playerDataBase.Butterfly21 = 1;

                        playerDataBase.SetItemInstance(list, 3, 20);
                    }

                    if (list.ItemId.Equals("Butterfly22"))
                    {
                        playerDataBase.Butterfly22 = 1;

                        playerDataBase.SetItemInstance(list, 3, 21);
                    }

                    if (list.ItemId.Equals("Butterfly23"))
                    {
                        playerDataBase.Butterfly23 = 1;

                        playerDataBase.SetItemInstance(list, 3, 22);
                    }

                    if (list.ItemId.Equals("Butterfly24"))
                    {
                        playerDataBase.Butterfly24 = 1;

                        playerDataBase.SetItemInstance(list, 3, 23);
                    }

                    if (list.ItemId.Equals("Butterfly25"))
                    {
                        playerDataBase.Butterfly25 = 1;

                        playerDataBase.SetItemInstance(list, 3, 24);
                    }

                    if (list.ItemId.Equals("Butterfly26"))
                    {
                        playerDataBase.Butterfly26 = 1;

                        playerDataBase.SetItemInstance(list, 3, 25);
                    }

                    if (list.ItemId.Equals("Butterfly27"))
                    {
                        playerDataBase.Butterfly27 = 1;

                        playerDataBase.SetItemInstance(list, 3, 26);
                    }

                    if (list.ItemId.Equals("Butterfly28"))
                    {
                        playerDataBase.Butterfly28 = 1;

                        playerDataBase.SetItemInstance(list, 3, 27);
                    }

                    if (list.ItemId.Equals("Totems1"))
                    {
                        playerDataBase.Totems1 = 1;

                        playerDataBase.SetItemInstance(list, 4, 0);
                    }

                    if (list.ItemId.Equals("Totems2"))
                    {
                        playerDataBase.Totems2 = 1;

                        playerDataBase.SetItemInstance(list, 4, 1);
                    }

                    if (list.ItemId.Equals("Totems3"))
                    {
                        playerDataBase.Totems3 = 1;

                        playerDataBase.SetItemInstance(list, 4, 2);
                    }

                    if (list.ItemId.Equals("Totems4"))
                    {
                        playerDataBase.Totems4 = 1;

                        playerDataBase.SetItemInstance(list, 4, 3);
                    }

                    if (list.ItemId.Equals("Totems5"))
                    {
                        playerDataBase.Totems5 = 1;

                        playerDataBase.SetItemInstance(list, 4, 4);
                    }

                    if (list.ItemId.Equals("Totems6"))
                    {
                        playerDataBase.Totems6 = 1;

                        playerDataBase.SetItemInstance(list, 4, 5);
                    }

                    if (list.ItemId.Equals("Totems7"))
                    {
                        playerDataBase.Totems7 = 1;

                        playerDataBase.SetItemInstance(list, 4, 6);
                    }

                    if (list.ItemId.Equals("Totems8"))
                    {
                        playerDataBase.Totems8 = 1;

                        playerDataBase.SetItemInstance(list, 4, 7);
                    }

                    if (list.ItemId.Equals("Totems9"))
                    {
                        playerDataBase.Totems9 = 1;

                        playerDataBase.SetItemInstance(list, 4, 8);
                    }

                    if (list.ItemId.Equals("Totems10"))
                    {
                        playerDataBase.Totems10 = 1;

                        playerDataBase.SetItemInstance(list, 4, 9);
                    }

                    if (list.ItemId.Equals("Totems11"))
                    {
                        playerDataBase.Totems11 = 1;

                        playerDataBase.SetItemInstance(list, 4, 10);
                    }

                    if (list.ItemId.Equals("Totems12"))
                    {
                        playerDataBase.Totems12 = 1;

                        playerDataBase.SetItemInstance(list, 4, 11);
                    }

                    if (list.ItemId.Equals("Flower1"))
                    {
                        playerDataBase.Flower1 = 1;

                        playerDataBase.SetItemInstance(list, 5, 0);
                    }

                    if (list.ItemId.Equals("Flower2"))
                    {
                        playerDataBase.Flower2 = 1;

                        playerDataBase.SetItemInstance(list, 5, 1);
                    }

                    if (list.ItemId.Equals("Flower3"))
                    {
                        playerDataBase.Flower3 = 1;

                        playerDataBase.SetItemInstance(list, 5, 2);
                    }

                    if (list.ItemId.Equals("Flower4"))
                    {
                        playerDataBase.Flower4 = 1;

                        playerDataBase.SetItemInstance(list, 5, 3);
                    }

                    if (list.ItemId.Equals("Flower5"))
                    {
                        playerDataBase.Flower5 = 1;

                        playerDataBase.SetItemInstance(list, 5, 4);
                    }

                    if (list.ItemId.Equals("Flower6"))
                    {
                        playerDataBase.Flower6 = 1;

                        playerDataBase.SetItemInstance(list, 5, 5);
                    }

                    if (list.ItemId.Equals("Flower7"))
                    {
                        playerDataBase.Flower7 = 1;

                        playerDataBase.SetItemInstance(list, 5, 6);
                    }

                    if (list.ItemId.Equals("Bucket1"))
                    {
                        playerDataBase.Bucket[0] = 1;
                    }

                    if (list.ItemId.Equals("Bucket2"))
                    {
                        playerDataBase.Bucket[1] = 1;
                    }

                    if (list.ItemId.Equals("Bucket3"))
                    {
                        playerDataBase.Bucket[2] = 1;
                    }

                    if (list.ItemId.Equals("Bucket4"))
                    {
                        playerDataBase.Bucket[3] = 1;
                    }

                    if (list.ItemId.Equals("Bucket5"))
                    {
                        playerDataBase.Bucket[4] = 1;
                    }

                    if (list.ItemId.Equals("Bucket6"))
                    {
                        playerDataBase.Bucket[5] = 1;
                    }

                    if (list.ItemId.Equals("Bucket7"))
                    {
                        playerDataBase.Bucket[6] = 1;
                    }

                    if (list.ItemId.Equals("Bucket8"))
                    {
                        playerDataBase.Bucket[7] = 1;
                    }

                    if (list.ItemId.Equals("Chair1"))
                    {
                        playerDataBase.Chair[0] = 1;
                    }

                    if (list.ItemId.Equals("Chair2"))
                    {
                        playerDataBase.Chair[1] = 1;
                    }

                    if (list.ItemId.Equals("Chair3"))
                    {
                        playerDataBase.Chair[2] = 1;
                    }

                    if (list.ItemId.Equals("Chair4"))
                    {
                        playerDataBase.Chair[3] = 1;
                    }

                    if (list.ItemId.Equals("Chair5"))
                    {
                        playerDataBase.Chair[4] = 1;
                    }

                    if (list.ItemId.Equals("Chair6"))
                    {
                        playerDataBase.Chair[5] = 1;
                    }

                    if (list.ItemId.Equals("Chair7"))
                    {
                        playerDataBase.Chair[6] = 1;
                    }

                    if (list.ItemId.Equals("Chair8"))
                    {
                        playerDataBase.Chair[7] = 1;
                    }

                    if (list.ItemId.Equals("Tube1"))
                    {
                        playerDataBase.Tube[0] = 1;
                    }

                    if (list.ItemId.Equals("Tube2"))
                    {
                        playerDataBase.Tube[1] = 1;
                    }

                    if (list.ItemId.Equals("Tube3"))
                    {
                        playerDataBase.Tube[2] = 1;
                    }

                    if (list.ItemId.Equals("Tube4"))
                    {
                        playerDataBase.Tube[3] = 1;
                    }

                    if (list.ItemId.Equals("Tube5"))
                    {
                        playerDataBase.Tube[4] = 1;
                    }

                    if (list.ItemId.Equals("Tube6"))
                    {
                        playerDataBase.Tube[5] = 1;
                    }

                    if (list.ItemId.Equals("Tube7"))
                    {
                        playerDataBase.Tube[6] = 1;
                    }

                    if (list.ItemId.Equals("Tube8"))
                    {
                        playerDataBase.Tube[7] = 1;
                    }

                    if (list.ItemId.Equals("Surfboard1"))
                    {
                        playerDataBase.Surfboard[0] = 1;
                    }

                    if (list.ItemId.Equals("Surfboard2"))
                    {
                        playerDataBase.Surfboard[1] = 1;
                    }

                    if (list.ItemId.Equals("Surfboard3"))
                    {
                        playerDataBase.Surfboard[2] = 1;
                    }

                    if (list.ItemId.Equals("Surfboard4"))
                    {
                        playerDataBase.Surfboard[3] = 1;
                    }

                    if (list.ItemId.Equals("Surfboard5"))
                    {
                        playerDataBase.Surfboard[4] = 1;
                    }

                    if (list.ItemId.Equals("Surfboard6"))
                    {
                        playerDataBase.Surfboard[5] = 1;
                    }

                    if (list.ItemId.Equals("Surfboard7"))
                    {
                        playerDataBase.Surfboard[6] = 1;
                    }

                    if (list.ItemId.Equals("Surfboard8"))
                    {
                        playerDataBase.Surfboard[7] = 1;
                    }

                    if (list.ItemId.Equals("Umbrella1"))
                    {
                        playerDataBase.Umbrella[0] = 1;
                    }

                    if (list.ItemId.Equals("Umbrella2"))
                    {
                        playerDataBase.Umbrella[1] = 1;
                    }

                    if (list.ItemId.Equals("Umbrella3"))
                    {
                        playerDataBase.Umbrella[2] = 1;
                    }

                    if (list.ItemId.Equals("Umbrella4"))
                    {
                        playerDataBase.Umbrella[3] = 1;
                    }

                    if (list.ItemId.Equals("Umbrella5"))
                    {
                        playerDataBase.Umbrella[4] = 1;
                    }

                    if (list.ItemId.Equals("Umbrella6"))
                    {
                        playerDataBase.Umbrella[5] = 1;
                    }

                    if (list.ItemId.Equals("Umbrella7"))
                    {
                        playerDataBase.Umbrella[6] = 1;
                    }

                    if (list.ItemId.Equals("Umbrella8"))
                    {
                        playerDataBase.Umbrella[7] = 1;
                    }

                    if (list.ItemId.Equals("Umbrella9"))
                    {
                        playerDataBase.Umbrella[8] = 1;
                    }

                    if (list.ItemId.Equals("Umbrella10"))
                    {
                        playerDataBase.Umbrella[9] = 1;
                    }
                }
            }

            inventoryData = true;

        }, DisplayPlayfabError);
    }

    public bool GetCatalog()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Shop" }, shop =>
        {
            for (int i = 0; i < shop.Catalog.Count; i++)
            {
                var catalog = shop.Catalog[i];
            }
        }, (error) =>
        {

        });

        return true;
    }

    public void GetStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
           new GetPlayerStatisticsRequest(),
           (Action<GetPlayerStatisticsResult>)((result) =>
           {
               foreach (var statistics in result.Statistics)
               {
                   switch (statistics.StatisticName)
                   {
                       //case "":
                       //    string text = statistics.Value.ToString();
                       //    break;
                       case "RankPoint":
                           playerDataBase.RankPoint = statistics.Value;
                           break;
                       case "Icon":
                           playerDataBase.Icon = statistics.Value;

                           if (playerDataBase.Icon > System.Enum.GetValues(typeof(IconType)).Length - 1)
                           {
                               playerDataBase.Icon = 0;
                           }
                           break;
                       case "FirstReward":
                           playerDataBase.FirstReward = statistics.Value;
                           break;
                       case "FirstDate":
                           playerDataBase.FirstDate = statistics.Value.ToString();
                           break;
                       case "FirstServerDate":
                           playerDataBase.FirstServerDate = statistics.Value.ToString();
                           break;
                       case "IslandNumber":
                           playerDataBase.IslandNumber = statistics.Value;

                           if((int)GameStateManager.instance.IslandType > playerDataBase.IslandNumber)
                           {
                               playerDataBase.IslandNumber = (int)GameStateManager.instance.IslandType;
                           }

                           break;
                       case "IslandNumber_Ranking":
                           playerDataBase.IslandNumber_Ranking = statistics.Value;
                           break;
                       case "TestAccount":
                           playerDataBase.TestAccount = statistics.Value;
                           break;
                       case "Update":
                           playerDataBase.Update = statistics.Value;
                           break;
                       case "BuffTickets":
                           playerDataBase.BuffTicket = statistics.Value;
                           break;
                       case "SkillTickets":
                           playerDataBase.SkillTicket = statistics.Value;
                           break;
                       case "RecoverTicket":
                           playerDataBase.RecoverTicket = statistics.Value;
                           break;
                       case "Proficiency":
                           playerDataBase.Proficiency = statistics.Value;
                           break;
                       case "Level":
                           playerDataBase.Level = statistics.Value;
                           break;
                       case "Exp":
                           playerDataBase.Exp = statistics.Value;
                           break;
                       case "PlayTime":
                           playerDataBase.PlayTime = statistics.Value;
                           break;
                       case "Advancement":
                           playerDataBase.Advancement = statistics.Value;
                           break;
                       case "ChallengePoint":
                           playerDataBase.ChallengePoint = statistics.Value;
                           break;
                       case "AdCount":
                           playerDataBase.AdCount = statistics.Value;
                           break;
                       case "TreasureCount":
                           playerDataBase.TreasureCount = statistics.Value;
                           break;
                       case "GuideIndex":
                           playerDataBase.GuideIndex = statistics.Value;
                           break;
                       case "ChangeNicknameCount":
                           playerDataBase.ChangeNicknameCount = statistics.Value;
                           break;
                       case "UseSauce1":
                           playerDataBase.UseSauce1 = statistics.Value;
                           break;
                       case "UseSauce2":
                           playerDataBase.UseSauce2 = statistics.Value;
                           break;
                       case "UseSauce3":
                           playerDataBase.UseSauce3 = statistics.Value;
                           break;
                       case "UseSauce4":
                           playerDataBase.UseSauce4 = statistics.Value;
                           break;
                       case "UseSauce5":
                           playerDataBase.UseSauce5 = statistics.Value;
                           break;
                       case "Dungeon1Count":
                           playerDataBase.Dungeon1Count = statistics.Value;
                           break;
                       case "Dungeon2Count":
                           playerDataBase.Dungeon2Count = statistics.Value;
                           break;
                       case "Dungeon3Count":
                           playerDataBase.Dungeon3Count = statistics.Value;
                           break;
                       case "Dungeon4Count":
                           playerDataBase.Dungeon4Count = statistics.Value;
                           break;
                       case "OfflineCount":
                           playerDataBase.OfflineCount = statistics.Value;
                           break;
                       case "DefDestroyCount":
                           playerDataBase.DefDestroyCount = statistics.Value;
                           break;
                       case "RecoverCount":
                           playerDataBase.RecoverCount = statistics.Value;
                           break;
                       case "EquipCount":
                           playerDataBase.EquipCount = statistics.Value;
                           break;
                       case "AccessDate":
                           playerDataBase.AccessDate = statistics.Value;
                           break;
                       case "CastleLevel":
                           playerDataBase.CastleLevel = statistics.Value;
                           break;
                       case "CastleDate":
                           playerDataBase.CastleDate = statistics.Value.ToString();
                           break;
                       case "CastleServerDate":
                           playerDataBase.CastleServerDate = statistics.Value.ToString();
                           break;
                       case "GetGold":
                           playerDataBase.GetGold = statistics.Value;
                           break;
                       case "ConsumeGold":
                           playerDataBase.ConsumeGold = statistics.Value;
                           break;
                       case "BuyCrystal":
                           playerDataBase.BuyCrystal = statistics.Value;
                           break;
                       case "GourmetLevel":
                           playerDataBase.GourmetLevel = statistics.Value;
                           break;
                       case "EventTicket":
                           playerDataBase.EventTicket = statistics.Value;
                           break;
                       case "EventTicketCount":
                           playerDataBase.EventTicketCount = statistics.Value;
                           break;
                       case "EventEnter1":
                           playerDataBase.EventEnter1 = statistics.Value;
                           break;
                       case "EventEnter2":
                           playerDataBase.EventEnter2 = statistics.Value;
                           break;
                       case "EventEnter3":
                           playerDataBase.EventEnter3 = statistics.Value;
                           break;
                       case "EventEnter4":
                           playerDataBase.EventEnter4 = statistics.Value;
                           break;
                       case "Package1":
                           if (statistics.Value == 1)
                           {
                               playerDataBase.Package1 = true;
                           }
                           break;
                       case "Package2":
                           if (statistics.Value == 1)
                           {
                               playerDataBase.Package2 = true;
                           }
                           break;
                       case "Package3":
                           if (statistics.Value == 1)
                           {
                               playerDataBase.Package3 = true;
                           }
                           break;
                       case "Package4":
                           if (statistics.Value == 1)
                           {
                               playerDataBase.Package4 = true;
                           }
                           break;
                       case "Package5":
                           if (statistics.Value == 1)
                           {
                               playerDataBase.Package5 = true;
                           }
                           break;
                       case "Package6":
                           if (statistics.Value == 1)
                           {
                               playerDataBase.Package6 = true;
                           }
                           break;
                       case "Package10":
                           if (statistics.Value == 1)
                           {
                               playerDataBase.Package10 = true;
                           }
                           break;
                       case "Package11":
                           if (statistics.Value == 1)
                           {
                               playerDataBase.Package11 = true;
                           }
                           break;
                       case "Package12":
                           if (statistics.Value == 1)
                           {
                               playerDataBase.Package12 = true;
                           }
                           break;
                       case "SeasonPass":
                           if (statistics.Value == 1)
                           {
                               playerDataBase.SeasonPass = true;
                           }
                           break;
                       case "SeasonPassLevel":
                           playerDataBase.SeasonPassLevel = statistics.Value;
                           break;
                       case "SeasonPassDay":
                           playerDataBase.SeasonPassDay = statistics.Value.ToString();
                           break;
                       case "AttendanceDay":
                           playerDataBase.AttendanceDay = statistics.Value.ToString();
                           break;
                       case "NextMonday":
                           playerDataBase.NextMonday = statistics.Value.ToString();
                           break;
                       case "AttendanceCount":
                           playerDataBase.AttendanceCount = statistics.Value;
                           break;
                       case "AttendanceCheck":
                           if (statistics.Value == 0)
                           {
                               playerDataBase.AttendanceCheck = false;
                           }
                           else
                           {
                               playerDataBase.AttendanceCheck = true;
                           }
                           break;
                       case "WelcomeCount":
                           playerDataBase.WelcomeCount = statistics.Value;
                           break;
                       case "WelcomeCheck":
                           if (statistics.Value == 0)
                           {
                               playerDataBase.WelcomeCheck = false;
                           }
                           else
                           {
                               playerDataBase.WelcomeCheck = true;
                           }
                           break;
                       case "AppReview":
                           if (statistics.Value == 0)
                           {
                               playerDataBase.AppReview = false;
                           }
                           else
                           {
                               playerDataBase.AppReview = true;
                           }
                           break;
                       case "PlayTimeCount":
                           playerDataBase.PlayTimeCount = statistics.Value;
                           break;
                       case "RankEventCount":
                           playerDataBase.RankEventCount = statistics.Value;
                           break;
                       case "RecipeEventCount":
                           playerDataBase.RecipeEventCount = statistics.Value;
                           break;
                       case "LevelUpEventCount":
                           playerDataBase.LevelUpEventCount = statistics.Value;
                           break;
                       case "DefDestroyTicket":
                           playerDataBase.DefDestroyTicket = statistics.Value;
                           break;
                       case "DefDestroyTicketPiece":
                           playerDataBase.DefDestroyTicketPiece = statistics.Value;
                           break;
                       case "EventNumber":
                           playerDataBase.EventNumber = statistics.Value;
                           break;
                       case "UpdateNumber":
                           playerDataBase.UpdateNumber = statistics.Value;
                           break;
                       case "FriendsNumber":
                           playerDataBase.FriendsNumber = statistics.Value;
                           break;
                       case "ReviewNumber":
                           playerDataBase.ReviewNumber = statistics.Value;
                           break;
                       case "OpenKakao":
                           playerDataBase.OpenKakao = statistics.Value;
                           break;
                       case "LockTutorial":
                           playerDataBase.LockTutorial = statistics.Value;

                           if(playerDataBase.LockTutorial == 7)
                           {
                               playerDataBase.LockTutorial = 8;
                               UpdatePlayerStatisticsInsert("LockTutorial", 8);
                           }
                           break;
                       case "AbilityPoint":
                           playerDataBase.AbilityPoint = statistics.Value;
                           break;
                       case "AbilityLevel":
                           playerDataBase.AbilityLevel = statistics.Value;
                           break;
                       case "DungeonKey1":
                           playerDataBase.DungeonKey1 = statistics.Value;
                           break;
                       case "DungeonKey2":
                           playerDataBase.DungeonKey2 = statistics.Value;
                           break;
                       case "DungeonKey3":
                           playerDataBase.DungeonKey3 = statistics.Value;
                           break;
                       case "DungeonKey4":
                           playerDataBase.DungeonKey4 = statistics.Value;
                           break;
                       case "Dungeon1Level":
                           playerDataBase.Dungeon1Level = statistics.Value;
                           break;
                       case "Dungeon2Level":
                           playerDataBase.Dungeon2Level = statistics.Value;
                           break;
                       case "Dungeon3Level":
                           playerDataBase.Dungeon3Level = statistics.Value;
                           break;
                       case "Dungeon4Level":
                           playerDataBase.Dungeon4Level = statistics.Value;
                           break;
                       case "Gender":
                           playerDataBase.Gender = statistics.Value;
                           break;
                       case "InGameTutorial":
                           playerDataBase.InGameTutorial = statistics.Value;
                           break;
                       case "Treasure1":
                           playerDataBase.Treasure1 = statistics.Value;

                           if(playerDataBase.Treasure1 > 199)
                           {
                               playerDataBase.Treasure1 = 199;
                           }
                           break;
                       case "Treasure2":
                           playerDataBase.Treasure2 = statistics.Value;

                           if (playerDataBase.Treasure2 > 199)
                           {
                               playerDataBase.Treasure2 = 199;
                           }
                           break;
                       case "Treasure3":
                           playerDataBase.Treasure3 = statistics.Value;

                           if (playerDataBase.Treasure3 > 199)
                           {
                               playerDataBase.Treasure3 = 199;
                           }
                           break;
                       case "Treasure4":
                           playerDataBase.Treasure4 = statistics.Value;

                           if (playerDataBase.Treasure4 > 199)
                           {
                               playerDataBase.Treasure4 = 199;
                           }
                           break;
                       case "Treasure5":
                           playerDataBase.Treasure5 = statistics.Value;

                           if (playerDataBase.Treasure5 > 199)
                           {
                               playerDataBase.Treasure5 = 199;
                           }
                           break;
                       case "Treasure6":
                           playerDataBase.Treasure6 = statistics.Value;

                           if (playerDataBase.Treasure6 > 199)
                           {
                               playerDataBase.Treasure6 = 199;
                           }
                           break;
                       case "Treasure7":
                           playerDataBase.Treasure7 = statistics.Value;

                           if (playerDataBase.Treasure7 > 199)
                           {
                               playerDataBase.Treasure7 = 199;
                           }
                           break;
                       case "Treasure8":
                           playerDataBase.Treasure8 = statistics.Value;

                           if (playerDataBase.Treasure8 > 199)
                           {
                               playerDataBase.Treasure8 = 199;
                           }
                           break;
                       case "Treasure9":
                           playerDataBase.Treasure9 = statistics.Value;

                           if (playerDataBase.Treasure9 > 199)
                           {
                               playerDataBase.Treasure9 = 199;
                           }
                           break;
                       case "Treasure10":
                           playerDataBase.Treasure10 = statistics.Value;

                           if (playerDataBase.Treasure10 > 199)
                           {
                               playerDataBase.Treasure10 = 199;
                           }
                           break;
                       case "Treasure11":
                           playerDataBase.Treasure11 = statistics.Value;

                           if (playerDataBase.Treasure11 > 199)
                           {
                               playerDataBase.Treasure11 = 199;
                           }
                           break;
                       case "Treasure12":
                           playerDataBase.Treasure12 = statistics.Value;

                           if (playerDataBase.Treasure12 > 199)
                           {
                               playerDataBase.Treasure12 = 199;
                           }
                           break;
                       case "Treasure13":
                           playerDataBase.Treasure13 = statistics.Value;

                           if (playerDataBase.Treasure13 > 199)
                           {
                               playerDataBase.Treasure13 = 199;
                           }
                           break;
                       case "Treasure14":
                           playerDataBase.Treasure14 = statistics.Value;

                           if (playerDataBase.Treasure14 > 199)
                           {
                               playerDataBase.Treasure14 = 199;
                           }
                           break;
                       case "Treasure15":
                           playerDataBase.Treasure15 = statistics.Value;

                           if (playerDataBase.Treasure15 > 199)
                           {
                               playerDataBase.Treasure15 = 199;
                           }
                           break;
                       case "Treasure1Count":
                           playerDataBase.Treasure1Count = statistics.Value;
                           break;
                       case "Treasure2Count":
                           playerDataBase.Treasure2Count = statistics.Value;
                           break;
                       case "Treasure3Count":
                           playerDataBase.Treasure3Count = statistics.Value;
                           break;
                       case "Treasure4Count":
                           playerDataBase.Treasure4Count = statistics.Value;
                           break;
                       case "Treasure5Count":
                           playerDataBase.Treasure5Count = statistics.Value;
                           break;
                       case "Treasure6Count":
                           playerDataBase.Treasure6Count = statistics.Value;
                           break;
                       case "Treasure7Count":
                           playerDataBase.Treasure7Count = statistics.Value;
                           break;
                       case "Treasure8Count":
                           playerDataBase.Treasure8Count = statistics.Value;
                           break;
                       case "Treasure9Count":
                           playerDataBase.Treasure9Count = statistics.Value;
                           break;
                       case "Treasure10Count":
                           playerDataBase.Treasure10Count = statistics.Value;
                           break;
                       case "Treasure11Count":
                           playerDataBase.Treasure11Count = statistics.Value;
                           break;
                       case "Treasure12Count":
                           playerDataBase.Treasure12Count = statistics.Value;
                           break;
                       case "Treasure13Count":
                           playerDataBase.Treasure13Count = statistics.Value;
                           break;
                       case "Treasure14Count":
                           playerDataBase.Treasure14Count = statistics.Value;
                           break;
                       case "Treasure15Count":
                           playerDataBase.Treasure15Count = statistics.Value;
                           break;
                       case "NextFoodNumber":
                           playerDataBase.NextFoodNumber = statistics.Value;
                           break;
                       case "SellCount":
                           playerDataBase.SellCount = statistics.Value;
                           break;
                       case "UseSources":
                           playerDataBase.UseSauceCount = statistics.Value;
                           break;
                       case "OpenChestBox":
                           playerDataBase.OpenChestBox = statistics.Value;
                           break;
                       case "YummyTimeCount":
                           playerDataBase.YummyTimeCount = statistics.Value;
                           break;
                       case "QuestCount":
                           playerDataBase.QuestCount = statistics.Value;
                           break;
                       case "Skill1":
                           playerDataBase.Skill1 = statistics.Value;

                           if(playerDataBase.Skill1 > 499)
                           {
                               playerDataBase.Skill1 = 499;
                           }
                           break;
                       case "Skill2":
                           playerDataBase.Skill2 = statistics.Value;

                           if (playerDataBase.Skill2 > 499)
                           {
                               playerDataBase.Skill2 = 499;
                           }
                           break;
                       case "Skill3":
                           playerDataBase.Skill3 = statistics.Value;

                           if (playerDataBase.Skill3 > 499)
                           {
                               playerDataBase.Skill3 = 499;
                           }
                           break;
                       case "Skill4":
                           playerDataBase.Skill4 = statistics.Value;

                           if (playerDataBase.Skill4 > 499)
                           {
                               playerDataBase.Skill4 = 499;
                           }
                           break;
                       case "Skill5":
                           playerDataBase.Skill5 = statistics.Value;

                           if (playerDataBase.Skill5 > 499)
                           {
                               playerDataBase.Skill5 = 499;
                           }
                           break;
                       case "Skill6":
                           playerDataBase.Skill6 = statistics.Value;

                           if (playerDataBase.Skill6 > 499)
                           {
                               playerDataBase.Skill6 = 499;
                           }
                           break;
                       case "Skill7":
                           playerDataBase.Skill7 = statistics.Value;

                           if (playerDataBase.Skill7 > 499)
                           {
                               playerDataBase.Skill7 = 499;
                           }
                           break;
                       case "Skill8":
                           playerDataBase.Skill8 = statistics.Value;

                           if (playerDataBase.Skill8 > 499)
                           {
                               playerDataBase.Skill8 = 499;
                           }
                           break;
                       case "Skill9":
                           playerDataBase.Skill9 = statistics.Value;

                           if (playerDataBase.Skill9 > 499)
                           {
                               playerDataBase.Skill9 = 499;
                           }
                           break;
                       case "Skill10":
                           playerDataBase.Skill10 = statistics.Value;

                           if (playerDataBase.Skill10 > 499)
                           {
                               playerDataBase.Skill10 = 499;
                           }
                           break;
                       case "Skill11":
                           playerDataBase.Skill11 = statistics.Value;

                           if (playerDataBase.Skill11 > 499)
                           {
                               playerDataBase.Skill11 = 499;
                           }
                           break;
                       case "Skill12":
                           playerDataBase.Skill12 = statistics.Value;

                           if (playerDataBase.Skill12 > 499)
                           {
                               playerDataBase.Skill12 = 499;
                           }
                           break;
                       case "Skill13":
                           playerDataBase.Skill13 = statistics.Value;

                           if (playerDataBase.Skill13 > 499)
                           {
                               playerDataBase.Skill13 = 499;
                           }
                           break;
                       case "Skill14":
                           playerDataBase.Skill14 = statistics.Value;

                           if (playerDataBase.Skill14 > 99)
                           {
                               playerDataBase.Skill14 = 99;
                           }
                           break;
                       case "Skill15":
                           playerDataBase.Skill15 = statistics.Value;

                           if (playerDataBase.Skill15 > 99)
                           {
                               playerDataBase.Skill15 = 99;
                           }
                           break;
                       case "Skill16":
                           playerDataBase.Skill16 = statistics.Value;

                           if (playerDataBase.Skill16 > 99)
                           {
                               playerDataBase.Skill16 = 99;
                           }
                           break;
                       case "Skill17":
                           playerDataBase.Skill17 = statistics.Value;

                           if (playerDataBase.Skill17 > 99)
                           {
                               playerDataBase.Skill17 = 99;
                           }
                           break;
                       case "Skill18":
                           playerDataBase.Skill18 = statistics.Value;

                           if (playerDataBase.Skill18 > 99)
                           {
                               playerDataBase.Skill18 = 99;
                           }
                           break;
                       case "Skill19":
                           playerDataBase.Skill19 = statistics.Value;

                           if (playerDataBase.Skill19 > 99)
                           {
                               playerDataBase.Skill19 = 99;
                           }
                           break;
                       case "Portion1":
                           playerDataBase.Portion1 = statistics.Value;
                           break;
                       case "Portion2":
                           playerDataBase.Portion2 = statistics.Value;
                           break;
                       case "Portion3":
                           playerDataBase.Portion3 = statistics.Value;
                           break;
                       case "Portion4":
                           playerDataBase.Portion4 = statistics.Value;
                           break;
                       case "Portion5":
                           playerDataBase.Portion5 = statistics.Value;
                           break;
                       case "Portion6":
                           playerDataBase.Portion6 = statistics.Value;
                           break;
                       case "Package7":
                           playerDataBase.Package7 = statistics.Value;
                           break;
                       case "Package8":
                           playerDataBase.Package8 = statistics.Value;
                           break;
                       case "Package9":
                           playerDataBase.Package9 = statistics.Value;
                           break;
                       case "UpgradeCount":
                           playerDataBase.UpgradeCount = statistics.Value;
                           break;
                       case "ReincarnationCount":
                           playerDataBase.ReincarnationCount = statistics.Value;
                           break;
                       case "BuffCount":
                           playerDataBase.BuffCount = statistics.Value;
                           break;
                       case "RankLevel1":
                           playerDataBase.RankLevel1 = statistics.Value;

                           if(GameStateManager.instance.RankFoodLevel[0] > playerDataBase.RankLevel1)
                           {
                               GameStateManager.instance.RankFoodLevel[0] = playerDataBase.RankLevel1;
                           }
                           break;
                       case "RankLevel2":
                           playerDataBase.RankLevel2 = statistics.Value;

                           if (GameStateManager.instance.RankFoodLevel[1] > playerDataBase.RankLevel2)
                           {
                               GameStateManager.instance.RankFoodLevel[1] = playerDataBase.RankLevel2;
                           }
                           break;
                       case "RankLevel3":
                           playerDataBase.RankLevel3 = statistics.Value;

                           if (GameStateManager.instance.RankFoodLevel[2] > playerDataBase.RankLevel3)
                           {
                               GameStateManager.instance.RankFoodLevel[2] = playerDataBase.RankLevel3;
                           }
                           break;
                       case "RankLevel4":
                           playerDataBase.RankLevel4 = statistics.Value;

                           if (GameStateManager.instance.RankFoodLevel[3] > playerDataBase.RankLevel4)
                           {
                               GameStateManager.instance.RankFoodLevel[3] = playerDataBase.RankLevel4;
                           }
                           break;
                       case "RankLevel5":
                           playerDataBase.RankLevel5 = statistics.Value;

                           if (GameStateManager.instance.RankFoodLevel[4] > playerDataBase.RankLevel5)
                           {
                               GameStateManager.instance.RankFoodLevel[4] = playerDataBase.RankLevel5;
                           }
                           break;
                       case "RankLevel6":
                           playerDataBase.RankLevel6 = statistics.Value;

                           if (GameStateManager.instance.RankFoodLevel[5] > playerDataBase.RankLevel6)
                           {
                               GameStateManager.instance.RankFoodLevel[5] = playerDataBase.RankLevel6;
                           }
                           break;
                       case "RankLevel7":
                           playerDataBase.RankLevel7 = statistics.Value;

                           if (GameStateManager.instance.RankFoodLevel[6] > playerDataBase.RankLevel7)
                           {
                               GameStateManager.instance.RankFoodLevel[6] = playerDataBase.RankLevel7;
                           }
                           break;
                       case "RankLevel8":
                           playerDataBase.RankLevel8 = statistics.Value;

                           if (GameStateManager.instance.RankFoodLevel[7] > playerDataBase.RankLevel8)
                           {
                               GameStateManager.instance.RankFoodLevel[7] = playerDataBase.RankLevel8;
                           }
                           break;
                       case "RankLevel9":
                           playerDataBase.RankLevel9 = statistics.Value;

                           if (GameStateManager.instance.RankFoodLevel[8] > playerDataBase.RankLevel9)
                           {
                               GameStateManager.instance.RankFoodLevel[8] = playerDataBase.RankLevel9;
                           }
                           break;
                       case "RankLevel10":
                           playerDataBase.RankLevel10 = statistics.Value;

                           if (GameStateManager.instance.RankFoodLevel[9] > playerDataBase.RankLevel10)
                           {
                               GameStateManager.instance.RankFoodLevel[9] = playerDataBase.RankLevel10;
                           }
                           break;
                       case "TotalLevel":
                           playerDataBase.TotalLevel = statistics.Value;
                           break;
                       case "TotalLevel_1":
                           playerDataBase.TotalLevel_1 = statistics.Value;
                           break;
                       case "TotalLevel_2":
                           playerDataBase.TotalLevel_2 = statistics.Value;
                           break;
                       case "TotalLevel_3":
                           playerDataBase.TotalLevel_3 = statistics.Value;
                           break;
                       case "TotalLevel_4":
                           playerDataBase.TotalLevel_4 = statistics.Value;
                           break;
                       case "TotalLevel_5":
                           playerDataBase.TotalLevel_5 = statistics.Value;
                           break;
                       case "TotalLevel_6":
                           playerDataBase.TotalLevel_6 = statistics.Value;
                           break;
                       case "TotalLevel_7":
                           playerDataBase.TotalLevel_7 = statistics.Value;
                           break;
                       case "TotalLevel_8":
                           playerDataBase.TotalLevel_8 = statistics.Value;
                           break;
                       case "TotalLevel_9":
                           playerDataBase.TotalLevel_9 = statistics.Value;
                           break;
                       case "TotalLevel_10":
                           playerDataBase.TotalLevel_10 = statistics.Value;
                           break;
                       case "TotalLevel_11":
                           playerDataBase.TotalLevel_11 = statistics.Value;
                           break;
                       case "TotalLevel_12":
                           playerDataBase.TotalLevel_12 = statistics.Value;
                           break;
                       case "TotalLevel_13":
                           playerDataBase.TotalLevel_13 = statistics.Value;
                           break;
                       case "TotalLevel_14":
                           playerDataBase.TotalLevel_14 = statistics.Value;
                           break;
                       case "TotalLevel_15":
                           playerDataBase.TotalLevel_15 = statistics.Value;
                           break;
                       case "TotalLevel_16":
                           playerDataBase.TotalLevel_16 = statistics.Value;
                           break;
                       case "TotalLevel_17":
                           playerDataBase.TotalLevel_17 = statistics.Value;
                           break;
                       case "TotalLevel_18":
                           playerDataBase.TotalLevel_18 = statistics.Value;
                           break;
                       case "TotalLevel_19":
                           playerDataBase.TotalLevel_19 = statistics.Value;
                           break;
                       case "TotalLevel_20":
                           playerDataBase.TotalLevel_20 = statistics.Value;
                           break;
                       case "IslandReward":
                           playerDataBase.IslandReward = statistics.Value;
                           break;
                   }
               }

               statisticsData = true;
           })
           , (error) =>
           {

           });
    }

    public void SetPlayerData(Dictionary<string, string> data)
    {
        var request = new UpdateUserDataRequest() { Data = data, Permission = UserDataPermission.Public };
        try
        {
            PlayFabClientAPI.UpdateUserData(request, (result) =>
            {
                Debug.Log("Update Player Data!");

            }, DisplayPlayfabError);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void GetPlayerData()
    {
        var request = new GetUserDataRequest() { PlayFabId = GameStateManager.instance.PlayfabId };
        PlayFabClientAPI.GetUserData(request, (result) =>
        {
            Island_Total_Data island_Total_Data = new Island_Total_Data();
            ResetInfo resetInfo = new ResetInfo();
            Equip equip = new Equip();
            SeasonRewardInfo seasonRewardInfo = new SeasonRewardInfo();
            CouponInfo couponInfo = new CouponInfo();

            foreach (var eachData in result.Data)
            {
                string key = eachData.Key;
                if (key.Contains("ResetInfo"))
                {
                    resetInfo = JsonUtility.FromJson<ResetInfo>(eachData.Value.Value);
                    playerDataBase.resetInfo = resetInfo;
                }
                else if (key.Contains("Equip"))
                {
                    equip = JsonUtility.FromJson<Equip>(eachData.Value.Value);
                    playerDataBase.equip.SaveServerData(equip);
                }
                else if (key.Contains("Island_Total_Data"))
                {
                    island_Total_Data = JsonUtility.FromJson<Island_Total_Data>(eachData.Value.Value);
                    playerDataBase.island_Total_Data.SaveServerData(island_Total_Data);
                }
                else if (key.Contains("SeasonRewardInfo"))
                {
                    seasonRewardInfo = JsonUtility.FromJson<SeasonRewardInfo>(eachData.Value.Value);
                    playerDataBase.seasonRewardInfo.SaveServerData(seasonRewardInfo);
                }
                else if (key.Contains("CouponInfo"))
                {
                    couponInfo = JsonUtility.FromJson<CouponInfo>(eachData.Value.Value);
                    playerDataBase.couponInfo.SaveServerData(couponInfo);
                }
                else if (key.Contains("SeasonPass_Free"))
                {
                    playerDataBase.SaveServerToSeasonPass(SeasonPassType.Free, eachData.Value.Value);
                }
                else if (key.Contains("SeasonPass_Pass"))
                {
                    playerDataBase.SaveServerToSeasonPass(SeasonPassType.Pass, eachData.Value.Value);
                }
            }

            playerData = true;

        }, DisplayPlayfabError);
    }

    public void GetPlayerProfile(string playFabId, Action<string> action)
    {
        string countryCode = "";

        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
        {
            PlayFabId = playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowLocations = true
            }
        }, result =>
        {
            countryCode = result.PlayerProfile.Locations[0].CountryCode.Value.ToString();
            action?.Invoke(countryCode);

        }, error =>
        {
            action?.Invoke("");
        });
    }

    public void UpdatePlayerStatistics(List<StatisticUpdate> data)
    {
        if (NetworkConnect.instance.CheckConnectInternet())
        {
            try
            {
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "UpdatePlayerStatistics",
                    FunctionParameter = new
                    {
                        Statistics = data
                    },
                    GeneratePlayStreamEvent = true,
                }, OnCloudUpdateStats
                , DisplayPlayfabError);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        else
        {
            Debug.LogError("Error : Internet Disconnected\nCheck Internet State");
        }
    }

    public void UpdatePlayerStatisticsInsert(string name, int value)
    {
        try
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "UpdatePlayerStatistics",
                FunctionParameter = new
                {
                    Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate {StatisticName = name, Value = value}
                }
                },
                GeneratePlayStreamEvent = true,
            },
            result =>
            {
                OnCloudUpdateStats(result);
            }
            , DisplayPlayfabError);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void UpdateSellPriceGold(int number)
    {
        playerDataBase.SaveCoin += number;
        saveGold += number;

        GameStateManager.instance.SaveGold = saveGold;

        if(!saveDelay)
        {
            saveDelay = true;
            StartCoroutine(SaveGoldCoroution());
        }

        GameManager.instance.RenewalVC();
    }

    IEnumerator SaveGoldCoroution()
    {
        yield return waitForSeconds;

        playerDataBase.SaveCoin = 0;
        GameStateManager.instance.SaveGold = 0;
        GameManager.instance.RenewalVC();

        if (saveGold > 0)
        {
            UpdateAddGold(saveGold);

            //Debug.LogError(saveGold + "만큼 골드 증가");
        }
        else
        {
            UpdateSubtractGold(-saveGold);

            //Debug.LogError(saveGold + "만큼 골드 감소");
        }

        saveGold = 0;
        saveDelay = false;
    }


    public void UpdateAddGold(int number)
    {
        if(!saveDelay)
        {
            moneyAnimation.PlusMoney(number);
        }

        coin = playerDataBase.Coin;
        coinA = playerDataBase.CoinA;
        coinB = playerDataBase.CoinB;

        coin += number;

        coinB = coin / 100000000;
        coinA = coin - (coinB * 100000000);

        if (coinA > playerDataBase.CoinA)
        {
            UpdateAddCurrency(MoneyType.CoinA, (int)(coinA - playerDataBase.CoinA));
        }
        else if (coinA < playerDataBase.CoinA)
        {
            UpdateSubtractCurrency(MoneyType.CoinA, (int)(playerDataBase.CoinA - coinA));
        }

        if (coinB > playerDataBase.CoinB)
        {
            UpdateAddCurrency(MoneyType.CoinB, (int)(coinB - playerDataBase.CoinB));
        }
        else if(coinB < playerDataBase.CoinB)
        {
            UpdateSubtractCurrency(MoneyType.CoinB, (int)(playerDataBase.CoinB - coinB));
        }

        getGoldA = GameStateManager.instance.GetGold;

        getGoldA += number;

        if (getGoldA >= 1000000)
        {
            getGoldB = getGoldA / 1000000;

            getGoldA /= 1000000;

            playerDataBase.GetGold += getGoldB;

            GameStateManager.instance.GetGold = getGoldA;
            UpdatePlayerStatisticsInsert("GetGold", playerDataBase.GetGold);
        }

        GameManager.instance.RenewalVC();
    }

    public void UpdateSubtractGold(long number)
    {
        coin = playerDataBase.Coin;
        coinA = playerDataBase.CoinA;
        coinB = playerDataBase.CoinB;

        coin -= number;

        coinB = coin / 100000000;
        coinA = coin - (coinB * 100000000);

        if (coinA > playerDataBase.CoinA)
        {
            UpdateAddCurrency(MoneyType.CoinA, (int)(coinA - playerDataBase.CoinA));
        }
        else
        {
            UpdateSubtractCurrency(MoneyType.CoinA, (int)(playerDataBase.CoinA - coinA));
        }

        if (coinB > playerDataBase.CoinB)
        {
            UpdateAddCurrency(MoneyType.CoinB, (int)(coinB - playerDataBase.CoinB));
        }
        else
        {
            UpdateSubtractCurrency(MoneyType.CoinB, (int)(playerDataBase.CoinB - coinB));
        }


        consumeGoldA = GameStateManager.instance.ConsumeGold;

        consumeGoldA += number;

        if(consumeGoldA >= 1000000)
        {
            consumeGoldB = consumeGoldA / 1000000;

            consumeGoldA /= 1000000;

            playerDataBase.ConsumeGold += consumeGoldB;

            GameStateManager.instance.ConsumeGold = consumeGoldA;
            UpdatePlayerStatisticsInsert("ConsumeGold", (int)playerDataBase.ConsumeGold);
        }

        GameManager.instance.RenewalVC();
    }

    public void UpdateAddCurrency(MoneyType type, int number)
    {
        string currentType = "";

        switch (type)
        {
            case MoneyType.CoinA:
                currentType = "GO";
                break;
            case MoneyType.Crystal:
                currentType = "ST";
                break;
            case MoneyType.CoinB:
                currentType = "GA";
                break;
        }

        if (NetworkConnect.instance.CheckConnectInternet())
        {
            try
            {
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "AddMoney",
                    FunctionParameter = new { currencyType = currentType, currencyAmount = number },
                    GeneratePlayStreamEvent = true,
                }, OnCloudUpdateStats, DisplayPlayfabError);

                switch (type)
                {
                    case MoneyType.CoinA:
                        playerDataBase.CoinA += number;
                        break;
                    case MoneyType.Crystal:
                        playerDataBase.Crystal += number;
                        moneyAnimation2.PlusMoney(number);
                        break;
                    case MoneyType.CoinB:
                        playerDataBase.CoinB += number;
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        else
        {
            Debug.LogError("Error : Internet Disconnected\nCheck Internet State");
        }

        GameManager.instance.RenewalVC();
    }

    public void UpdateSubtractCurrency(MoneyType type, int number)
    {
        string currentType = "";

        switch (type)
        {
            case MoneyType.CoinA:
                currentType = "GO";
                break;
            case MoneyType.Crystal:
                currentType = "ST";

                //playerDataBase.ConsumeGold += number;
                //UpdatePlayerStatisticsInsert("ConsumeGold", playerDataBase.ConsumeGold);

                if(playerDataBase.Crystal - number < 0)
                {
                    number = playerDataBase.Crystal;
                }

                break;
            case MoneyType.CoinB:
                currentType = "GA";
                break;
        }

        if (NetworkConnect.instance.CheckConnectInternet())
        {
            try
            {
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "SubtractMoney",
                    FunctionParameter = new { currencyType = currentType, currencyAmount = number },
                    GeneratePlayStreamEvent = true,
                }, OnCloudUpdateStats, DisplayPlayfabError);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            switch (type)
            {
                case MoneyType.CoinA:
                    playerDataBase.CoinA -= number;
                    break;
                case MoneyType.Crystal:
                    playerDataBase.Crystal -= number;
                    break;
                case MoneyType.CoinB:
                    playerDataBase.CoinB -= number;
                    break;
            }
        }
        else
        {
            Debug.LogError("Error : Internet Disconnected\nCheck Internet State");
        }

        GameManager.instance.RenewalVC();
    }



    public void UpdateDisplayName(string nickname, Action successAction, Action failAction)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nickname
        },
        result =>
        {
            Debug.Log("Update NickName : " + result.DisplayName);

            GameStateManager.instance.NickName = result.DisplayName;

            //GameManager.instance.Initialize();

            successAction?.Invoke();
        }
        , error =>
        {
            string report = error.GenerateErrorReport();
            if (report.Contains("Name not available"))
            {
                failAction?.Invoke();
            }
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    public void UpdateDisplayName(string nickname)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nickname
        },
        result =>
        {
            Debug.Log("Update First NickName : " + result.DisplayName);

            GameStateManager.instance.NickName = result.DisplayName;
        }
        , error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    public void GetTitleInternalData(string name, Action<bool> action)
    {
        PlayFabServerAPI.GetTitleInternalData(new PlayFab.ServerModels.GetTitleDataRequest(),
            result =>
            {
                if (name.Equals("CheckAOSVersion"))
                {
                    if (result.Data[name].Equals("ON"))
                    {
                        action?.Invoke(true);
                    }
                    else
                    {
                        action?.Invoke(false);
                    }
                }
                else if (name.Equals("CheckIOSVersion"))
                {
                    if (result.Data[name].Equals("ON"))
                    {
                        action?.Invoke(true);
                    }
                    else
                    {
                        action?.Invoke(false);
                    }
                }
                else if (name.Equals("AOSVersion") || name.Equals("IOSVersion"))
                {
                    if (result.Data[name].Equals(Application.version))
                    {
                        action?.Invoke(true);
                    }
                    else
                    {
                        action?.Invoke(false);
                    }
                }
                else if(name.Equals("Coupon"))
                {
                    if (result.Data[name].Equals("ON"))
                    {
                        action?.Invoke(true);
                    }
                    else
                    {
                        action?.Invoke(false);
                    }
                }
                else if(name.Equals("Gifticon"))
                {
                    if (result.Data[name].Equals("ON"))
                    {
                        action?.Invoke(true);
                    }
                    else
                    {
                        action?.Invoke(false);
                    }
                }
            },
            error =>
            {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());

                action?.Invoke(false);
            }
        );
    }

    public void GetTitleInternalData(string name, Action<string> action)
    {
        PlayFabServerAPI.GetTitleInternalData(new PlayFab.ServerModels.GetTitleDataRequest(),
            result =>
            {
                if(result.Data.ContainsKey(name))
                {
                    action.Invoke(result.Data[name]);
                }
            },
            error =>
            {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }

    public void GetLeaderboarder(string name, int min, Action<GetLeaderboardResult> successCalback)
    {
        var requestLeaderboard = new GetLeaderboardRequest
        {
            StartPosition = min,
            StatisticName = name,
            MaxResultsCount = 100,

            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowLocations = true,
                ShowDisplayName = true,
                ShowStatistics = true
            }
        };

        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, successCalback, error =>
        {
            rankingManager.isDelay = false;
            rankingManager.isDelay2 = false;

            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
        });
    }

    public void GetLeaderboardMyRank(string name, Action<GetLeaderboardAroundPlayerResult> successCalback)
    {
        var request = new GetLeaderboardAroundPlayerRequest()
        {
            StatisticName = name,
            MaxResultsCount = 1,
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, successCalback, DisplayPlayfabError);
    }

    public void SetProfileLanguage(string language)
    {
        EntityKey entity = new EntityKey();
        entity.Id = entityId;
        entity.Type = entityType;

        var request = new SetProfileLanguageRequest
        {
            Language = language,
            ExpectedVersion = 0,
            Entity = entity
        };
        PlayFabProfilesAPI.SetProfileLanguage(request, res =>
        {
            Debug.Log("The language on the entity's profile has been updated.");
        }, FailureCallback);
    }

    public void GetServerTime(Action<DateTime> action)
    {
        if (NetworkConnect.instance.CheckConnectInternet())
        {
            try
            {
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "GetServerTime",
                    GeneratePlayStreamEvent = true,
                }, result =>
                {
                    string date = PlayFabSimpleJson.SerializeObject(result.FunctionResult);

                    string year = date.Substring(1, 4);
                    string month = date.Substring(6, 2);
                    string day = date.Substring(9, 2);
                    string hour = date.Substring(12, 2);
                    string minute = date.Substring(15, 2);
                    string second = date.Substring(18, 2);

                    DateTime serverTime = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), 0, 0, 0);

                    serverTime = serverTime.AddDays(1);

                    DateTime time = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), int.Parse(second));

                    TimeSpan span = serverTime - time;

                    action?.Invoke(DateTime.Parse(span.ToString()));
                }, DisplayPlayfabError);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        else
        {
            Debug.LogError("Error : Internet Disconnected\nCheck Internet State");
        }
    }

    //"2022-04-24T22:17:04.548Z"

    public void ReadTitleNews(Action<List<TitleNewsItem>> action)
    {
        List<TitleNewsItem> item = new List<TitleNewsItem>();

        PlayFabClientAPI.GetTitleNews(new GetTitleNewsRequest(), result =>
        {
            foreach (var list in result.News)
            {
                item.Add(list);
            }

            action.Invoke(item);

        }, error =>
        {
            newsManager.isDelay = false;

            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
        });
    }

    public void ConsumeItem(string itemInstanceID)
    {
        try
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "ConsumeItem",
                FunctionParameter = new { ConsumeCount = 1, ItemInstanceId = itemInstanceID },
                GeneratePlayStreamEvent = true,
            }, OnCloudUpdateStats, DisplayPlayfabError);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void GrantItemsToUser(string itemIds, string catalogVersion)
    {
        grantItemData = false;

        if(!waitGrantItem)
        {
            waitGrantItem = true;
            StartCoroutine(WaitGrantItemCoroution());
        }

        try
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "GrantItemsToUser",
                FunctionParameter = new { ItemIds = itemIds, CatalogVersion = catalogVersion },
                GeneratePlayStreamEvent = true,
            }, result =>
            {
                grantItemData = true;

            }, DisplayPlayfabError);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    IEnumerator WaitGrantItemCoroution()
    {
        while (!grantItemData)
        {
            yield return null;
        }

        GetUserInventory();

        waitGrantItem = false;
    }

    public void GrantItemToUser(string catalogversion, List<string> itemIds)
    {
        try
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "GrantItemToUser",
                FunctionParameter = new { CatalogVersion = catalogversion, ItemIds = itemIds },
                GeneratePlayStreamEvent = true,
            }, OnCloudUpdateStats, DisplayPlayfabError);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void PurchaseRemoveAd()
    {
        if (playerDataBase.RemoveAds) return;

        itemList.Clear();
        itemList.Add("RemoveAds");

        GrantItemToUser("Shop", itemList);

        playerDataBase.RemoveAds = true;

        UpdatePlayerStatisticsInsert("RemoveAds", 1);

        GameManager.instance.CheckPurchase();

        FirebaseAnalytics.LogEvent("Buy_Purchase : RemoveAds");
    }

    public void PurchaseGoldX2()
    {
        if (playerDataBase.GoldX2) return;

        itemList.Clear();
        itemList.Add("GoldX2");

        GrantItemToUser("Shop", itemList);

        UpdatePlayerStatisticsInsert("GoldX2", 1);

        playerDataBase.GoldX2 = true;

        GameManager.instance.CheckPurchase();

        FirebaseAnalytics.LogEvent("Buy_Purchase : GoldX2");
    }

    public void PurchaseSuperOffline()
    {
        if (playerDataBase.SuperOffline) return;

        itemList.Clear();
        itemList.Add("SuperOffline");

        GrantItemToUser("Shop", itemList);

        UpdatePlayerStatisticsInsert("SuperOffline", 1);

        playerDataBase.CastleServerDate = DateTime.Now.AddDays(1).ToString("MMddHHmm");
        UpdatePlayerStatisticsInsert("CastleServerDate", int.Parse("1" + playerDataBase.CastleServerDate));

        playerDataBase.SuperOffline = true;

        GameManager.instance.CheckPurchase();

        FirebaseAnalytics.LogEvent("Buy_Purchase : SuperOffline");
    }

    public void PurchaseAutoUpgrade()
    {
        if (playerDataBase.AutoUpgrade) return;

        itemList.Clear();
        itemList.Add("AutoUpgrade");

        GrantItemToUser("Shop", itemList);

        UpdatePlayerStatisticsInsert("AutoUpgrade", 1);

        GameStateManager.instance.AutoUpgrade = true;
        GameStateManager.instance.AutoUpgradeLevel = 10;

        playerDataBase.AutoUpgrade = true;

        GameManager.instance.CheckPurchase();

        FirebaseAnalytics.LogEvent("Buy_Purchase : AutoUpgrade");
    }

    public void PurchaseAutoPresent()
    {
        if (playerDataBase.AutoPresent) return;

        itemList.Clear();
        itemList.Add("AutoPresent");

        GrantItemToUser("Shop", itemList);

        UpdatePlayerStatisticsInsert("AutoPresent", 1);

        GameStateManager.instance.AutoPresent = true;

        playerDataBase.AutoPresent = true;

        GameManager.instance.CheckPurchase();

        FirebaseAnalytics.LogEvent("Buy_Purchase : AutoPresent");
    }

    public void PurchaseSuperExp()
    {
        if (playerDataBase.SuperExp) return;

        itemList.Clear();
        itemList.Add("SuperExp");

        GrantItemToUser("Shop", itemList);

        UpdatePlayerStatisticsInsert("SuperExp", 1);

        playerDataBase.SuperExp = true;

        GameManager.instance.CheckPurchase();

        FirebaseAnalytics.LogEvent("Buy_Purchase : SuperExp");
    }

    public void PurchaseSuperKitchen()
    {
        if (playerDataBase.SuperKitchen) return;

        itemList.Clear();
        itemList.Add("SuperKitchen");

        GrantItemToUser("Shop", itemList);

        UpdatePlayerStatisticsInsert("SuperKitchen", 1);

        playerDataBase.SuperKitchen = true;

        GameManager.instance.CheckPurchase();

        FirebaseAnalytics.LogEvent("Buy_Purchase : SuperKitchen");
    }

    public void PurchaseSeasonPass()
    {
        if (playerDataBase.SeasonPass) return;

        playerDataBase.SeasonPass = true;

        DateTime now = DateTime.Now;
        DateTime endOfMonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

        playerDataBase.SeasonPassDay = endOfMonth.ToString("yyyyMMdd");

        UpdatePlayerStatisticsInsert("SeasonPass", 1);
        UpdatePlayerStatisticsInsert("SeasonPassDay", int.Parse(playerDataBase.SeasonPassDay));

        FirebaseAnalytics.LogEvent("Buy_Purchase : SeasonPass");
    }

    public void RestorePurchases()
    {
        if (isDelay) return;

        GetUserInventory();

        NotionManager.instance.UseNotion(NotionType.RestorePurchasesNotion);

        isDelay = true;
        Invoke("WaitDelay", 2f);
    }

    void WaitDelay()
    {
        isDelay = false;
    }

    [Button]
    public void AddMoney()
    {
        UpdateAddGold(50000000);
    }

    [Button]
    public void MinusMoney()
    {
        GameManager.instance.RenewalVC();

        UpdateSubtractGold(50000000);
    }

    public void SetInventoryCustomData(string itemInstanceID, Dictionary<string, string> datas)
    {
        try
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "UpdateUserInventoryItemCustomData",
                FunctionParameter = new { Data = datas, ItemInstanceId = itemInstanceID },
                GeneratePlayStreamEvent = true,
            }, OnCloudUpdateStats, DisplayPlayfabError);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
