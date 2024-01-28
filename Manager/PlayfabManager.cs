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

    private long coin = 0;
    private long coinA = 0;
    private long coinB = 0;

    private int consumeGold = 0;
    private int consumeGoldA = 0;
    private int consumeGoldB = 0;

    private int getGoldA = 0;
    private int getGoldB = 0;

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

    PlayerDataBase playerDataBase;


    [Header("Entity")]
    private string entityId;
    private string entityType;
    private readonly Dictionary<string, string> entityFileJson = new Dictionary<string, string>();

    private List<ItemInstance> inventoryList = new List<ItemInstance>();

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

    public void Login()
    {
        if (GameStateManager.instance.AutoLogin)
        {
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
#if UNITY_EDITOR
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
#if UNITY_EDITOR
        if (error) Debug.LogError("<color=red>" + message + "</color>");
        else Debug.Log(message);
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
                        ForceLink = true,
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
                        Debug.Log(error.GenerateErrorReport());

                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.FailLink);

                        Debug.Log("Link Google Account Fail");
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

                    if (list.ItemId.Equals("Character1"))
                    {
                        playerDataBase.Character1 = 1;
                    }

                    if (list.ItemId.Equals("Character2"))
                    {
                        playerDataBase.Character2 = 1;
                    }

                    if (list.ItemId.Equals("Character3"))
                    {
                        playerDataBase.Character3 = 1;
                    }

                    if (list.ItemId.Equals("Character4"))
                    {
                        playerDataBase.Character4 = 1;
                    }

                    if (list.ItemId.Equals("Character5"))
                    {
                        playerDataBase.Character5 = 1;
                    }

                    if (list.ItemId.Equals("Character6"))
                    {
                        playerDataBase.Character6 = 1;
                    }

                    if (list.ItemId.Equals("Character7"))
                    {
                        playerDataBase.Character7 = 1;
                    }

                    if (list.ItemId.Equals("Character8"))
                    {
                        playerDataBase.Character8 = 1;
                    }

                    if (list.ItemId.Equals("Character9"))
                    {
                        playerDataBase.Character9 = 1;
                    }

                    if (list.ItemId.Equals("Character10"))
                    {
                        playerDataBase.Character10 = 1;
                    }

                    if (list.ItemId.Equals("Character11"))
                    {
                        playerDataBase.Character11 = 1;
                    }

                    if (list.ItemId.Equals("Character12"))
                    {
                        playerDataBase.Character12 = 1;
                    }

                    if (list.ItemId.Equals("Character13"))
                    {
                        playerDataBase.Character13 = 1;
                    }

                    if (list.ItemId.Equals("Character14"))
                    {
                        playerDataBase.Character14 = 1;
                    }

                    if (list.ItemId.Equals("Character15"))
                    {
                        playerDataBase.Character15 = 1;
                    }

                    if (list.ItemId.Equals("Character16"))
                    {
                        playerDataBase.Character16 = 1;
                    }

                    if (list.ItemId.Equals("Character17"))
                    {
                        playerDataBase.Character17 = 1;
                    }

                    if (list.ItemId.Equals("Character18"))
                    {
                        playerDataBase.Character18 = 1;
                    }

                    if (list.ItemId.Equals("Character19"))
                    {
                        playerDataBase.Character19 = 1;
                    }

                    if (list.ItemId.Equals("Character20"))
                    {
                        playerDataBase.Character20 = 1;
                    }

                    if (list.ItemId.Equals("Character21"))
                    {
                        playerDataBase.Character21 = 1;
                    }

                    if (list.ItemId.Equals("Chips"))
                    {
                        playerDataBase.ChipsTruck = 1;
                    }

                    if (list.ItemId.Equals("Donut"))
                    {
                        playerDataBase.DonutTruck = 1;
                    }

                    if (list.ItemId.Equals("Hamburger"))
                    {
                        playerDataBase.HamburgerTruck = 1;
                    }

                    if (list.ItemId.Equals("Hotdog"))
                    {
                        playerDataBase.HotdogTruck = 1;
                    }

                    if (list.ItemId.Equals("Icecream"))
                    {
                        playerDataBase.IcecreamTruck = 1;
                    }

                    if (list.ItemId.Equals("Lemonade"))
                    {
                        playerDataBase.LemonadeTruck = 1;
                    }

                    if (list.ItemId.Equals("Noodles"))
                    {
                        playerDataBase.NoodlesTruck = 1;
                    }

                    if (list.ItemId.Equals("Pizza"))
                    {
                        playerDataBase.PizzaTruck = 1;
                    }

                    if (list.ItemId.Equals("Sushi"))
                    {
                        playerDataBase.SushiTruck = 1;
                    }

                    if (list.ItemId.Equals("Gecko"))
                    {
                        playerDataBase.GeckoAnimal = 1;
                    }

                    if (list.ItemId.Equals("Herring"))
                    {
                        playerDataBase.HerringAnimal = 1;
                    }

                    if (list.ItemId.Equals("Muskrat"))
                    {
                        playerDataBase.MuskratAnimal = 1;
                    }

                    if (list.ItemId.Equals("Pudu"))
                    {
                        playerDataBase.PuduAnimal = 1;
                    }

                    if (list.ItemId.Equals("Sparrow"))
                    {
                        playerDataBase.SparrowAnimal = 1;
                    }

                    if (list.ItemId.Equals("Squid"))
                    {
                        playerDataBase.SquidAnimal = 1;
                    }

                    if (list.ItemId.Equals("Taipan"))
                    {
                        playerDataBase.TaipanAnimal = 1;
                    }

                    if (list.ItemId.Equals("Butterfly1"))
                    {
                        playerDataBase.Butterfly1 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly2"))
                    {
                        playerDataBase.Butterfly2 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly3"))
                    {
                        playerDataBase.Butterfly3 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly4"))
                    {
                        playerDataBase.Butterfly4 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly5"))
                    {
                        playerDataBase.Butterfly5 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly6"))
                    {
                        playerDataBase.Butterfly6 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly7"))
                    {
                        playerDataBase.Butterfly7 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly8"))
                    {
                        playerDataBase.Butterfly8 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly9"))
                    {
                        playerDataBase.Butterfly9 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly10"))
                    {
                        playerDataBase.Butterfly10 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly11"))
                    {
                        playerDataBase.Butterfly11 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly12"))
                    {
                        playerDataBase.Butterfly12 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly13"))
                    {
                        playerDataBase.Butterfly13 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly14"))
                    {
                        playerDataBase.Butterfly14 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly15"))
                    {
                        playerDataBase.Butterfly15 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly16"))
                    {
                        playerDataBase.Butterfly16 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly17"))
                    {
                        playerDataBase.Butterfly17 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly18"))
                    {
                        playerDataBase.Butterfly18 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly19"))
                    {
                        playerDataBase.Butterfly19 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly20"))
                    {
                        playerDataBase.Butterfly20 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly21"))
                    {
                        playerDataBase.Butterfly21 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly22"))
                    {
                        playerDataBase.Butterfly22 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly23"))
                    {
                        playerDataBase.Butterfly23 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly24"))
                    {
                        playerDataBase.Butterfly24 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly25"))
                    {
                        playerDataBase.Butterfly25 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly26"))
                    {
                        playerDataBase.Butterfly26 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly27"))
                    {
                        playerDataBase.Butterfly27 = 1;
                    }

                    if (list.ItemId.Equals("Butterfly28"))
                    {
                        playerDataBase.Butterfly28 = 1;
                    }

                    if (list.ItemId.Equals("Totems1"))
                    {
                        playerDataBase.Totems1 = 1;
                    }

                    if (list.ItemId.Equals("Totems2"))
                    {
                        playerDataBase.Totems2 = 1;
                    }

                    if (list.ItemId.Equals("Totems3"))
                    {
                        playerDataBase.Totems3 = 1;
                    }

                    if (list.ItemId.Equals("Totems4"))
                    {
                        playerDataBase.Totems4 = 1;
                    }

                    if (list.ItemId.Equals("Totems5"))
                    {
                        playerDataBase.Totems5 = 1;
                    }

                    if (list.ItemId.Equals("Totems6"))
                    {
                        playerDataBase.Totems6 = 1;
                    }

                    if (list.ItemId.Equals("Totems7"))
                    {
                        playerDataBase.Totems7 = 1;
                    }

                    if (list.ItemId.Equals("Totems8"))
                    {
                        playerDataBase.Totems8 = 1;
                    }

                    if (list.ItemId.Equals("Totems9"))
                    {
                        playerDataBase.Totems9 = 1;
                    }

                    if (list.ItemId.Equals("Totems10"))
                    {
                        playerDataBase.Totems10 = 1;
                    }

                    if (list.ItemId.Equals("Totems11"))
                    {
                        playerDataBase.Totems11 = 1;
                    }

                    if (list.ItemId.Equals("Totems12"))
                    {
                        playerDataBase.Totems12 = 1;
                    }

                    if (list.ItemId.Equals("Flower1"))
                    {
                        playerDataBase.Flower1 = 1;
                    }

                    if (list.ItemId.Equals("Flower2"))
                    {
                        playerDataBase.Flower2 = 1;
                    }

                    if (list.ItemId.Equals("Flower3"))
                    {
                        playerDataBase.Flower3 = 1;
                    }

                    if (list.ItemId.Equals("Flower4"))
                    {
                        playerDataBase.Flower4 = 1;
                    }

                    if (list.ItemId.Equals("Flower5"))
                    {
                        playerDataBase.Flower5 = 1;
                    }

                    if (list.ItemId.Equals("Flower6"))
                    {
                        playerDataBase.Flower6 = 1;
                    }

                    if (list.ItemId.Equals("Flower7"))
                    {
                        playerDataBase.Flower7 = 1;
                    }

                    if (list.ItemId.Equals("Island1"))
                    {
                        playerDataBase.Island1 = 1;
                    }

                    if (list.ItemId.Equals("Island2"))
                    {
                        playerDataBase.Island2 = 1;
                    }

                    if (list.ItemId.Equals("Island3"))
                    {
                        playerDataBase.Island3 = 1;
                    }

                    if (list.ItemId.Equals("Island4"))
                    {
                        playerDataBase.Island4 = 1;
                    }

                    if (list.ItemId.Equals("Island5"))
                    {
                        playerDataBase.Island5 = 1;
                    }

                    if (list.ItemId.Equals("Island6"))
                    {
                        playerDataBase.Island6 = 1;
                    }

                    if (list.ItemId.Equals("Island7"))
                    {
                        playerDataBase.Island7 = 1;
                    }

                    if (list.ItemId.Equals("Island8"))
                    {
                        playerDataBase.Island8 = 1;
                    }

                    if (list.ItemId.Equals("Island9"))
                    {
                        playerDataBase.Island9 = 1;
                    }

                    if (list.ItemId.Equals("Island10"))
                    {
                        playerDataBase.Island10 = 1;
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
                       case "Michelin":
                           playerDataBase.Michelin = statistics.Value;
                           break;
                       case "AdCount":
                           playerDataBase.AdCount = statistics.Value;
                           break;
                       case "TreasureCount":
                           playerDataBase.TreasureCount = statistics.Value;
                           break;
                       case "ChangeNicknameCount":
                           playerDataBase.ChangeNicknameCount = statistics.Value;
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
                       case "DailyReward":
                           playerDataBase.DailyReward = statistics.Value;
                           break;
                       case "DailyReward_Portion":
                           playerDataBase.DailyReward_Portion = statistics.Value;
                           break;
                       case "DailyReward_DefTicket":
                           playerDataBase.DailyReward_DefTicket = statistics.Value;
                           break;
                       case "DailyReward_Crystal":
                           playerDataBase.DailyReward_Crystal = statistics.Value;
                           break;
                       case "DailyAdsReward":
                           playerDataBase.DailyAdsReward = statistics.Value;
                           break;
                       case "DailyAdsReward2":
                           playerDataBase.DailyAdsReward2 = statistics.Value;
                           break;
                       case "DailyCastleReward":
                           playerDataBase.DailyCastleReward = statistics.Value;
                           break;
                       case "DailyQuestReward":
                           playerDataBase.DailyQuestReward = statistics.Value;
                           break;
                       case "DailyTreasureReward":
                           playerDataBase.DailyTreasureReward = statistics.Value;
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
                       case "LockTutorial":
                           playerDataBase.LockTutorial = statistics.Value;
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
                       case "InGameTutorial":
                           playerDataBase.InGameTutorial = statistics.Value;
                           break;
                       case "Treasure1":
                           playerDataBase.Treasure1 = statistics.Value;
                           break;
                       case "Treasure2":
                           playerDataBase.Treasure2 = statistics.Value;
                           break;
                       case "Treasure3":
                           playerDataBase.Treasure3 = statistics.Value;
                           break;
                       case "Treasure4":
                           playerDataBase.Treasure4 = statistics.Value;
                           break;
                       case "Treasure5":
                           playerDataBase.Treasure5 = statistics.Value;
                           break;
                       case "Treasure6":
                           playerDataBase.Treasure6 = statistics.Value;
                           break;
                       case "Treasure7":
                           playerDataBase.Treasure7 = statistics.Value;
                           break;
                       case "Treasure8":
                           playerDataBase.Treasure8 = statistics.Value;
                           break;
                       case "Treasure9":
                           playerDataBase.Treasure9 = statistics.Value;
                           break;
                       case "Treasure10":
                           playerDataBase.Treasure10 = statistics.Value;
                           break;
                       case "Treasure11":
                           playerDataBase.Treasure11 = statistics.Value;
                           break;
                       case "Treasure12":
                           playerDataBase.Treasure12 = statistics.Value;
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
                       case "NextFoodNumber":
                           playerDataBase.NextFoodNumber = statistics.Value;
                           break;
                       case "NextFoodNumber2":
                           playerDataBase.NextFoodNumber2 = statistics.Value;
                           break;
                       case "NextFoodNumber3":
                           playerDataBase.NextFoodNumber3 = statistics.Value;
                           break;
                       case "NextFoodNumber4":
                           playerDataBase.NextFoodNumber4 = statistics.Value;
                           break;
                       case "HamburgerMaxValue":
                           playerDataBase.HamburgerMaxValue = statistics.Value;
                           break;
                       case "SandwichMaxValue":
                           playerDataBase.SandwichMaxValue = statistics.Value;
                           break;
                       case "SnackLabMaxValue":
                           playerDataBase.SnackLabMaxValue = statistics.Value;
                           break;
                       case "DrinkMaxValue":
                           playerDataBase.DrinkMaxValue = statistics.Value;
                           break;
                       case "PizzaMaxValue":
                           playerDataBase.PizzaMaxValue = statistics.Value;
                           break;
                       case "DonutMaxValue":
                           playerDataBase.DonutMaxValue = statistics.Value;
                           break;
                       case "FriesMaxValue":
                           playerDataBase.FriesMaxValue = statistics.Value;
                           break;
                       case "Candy1MaxValue":
                           playerDataBase.Candy1MaxValue = statistics.Value;
                           break;
                       case "Candy2MaxValue":
                           playerDataBase.Candy2MaxValue = statistics.Value;
                           break;
                       case "Candy3MaxValue":
                           playerDataBase.Candy3MaxValue = statistics.Value;
                           break;
                       case "Candy4MaxValue":
                           playerDataBase.Candy4MaxValue = statistics.Value;
                           break;
                       case "Candy5MaxValue":
                           playerDataBase.Candy5MaxValue = statistics.Value;
                           break;
                       case "Candy6MaxValue":
                           playerDataBase.Candy6MaxValue = statistics.Value;
                           break;
                       case "Candy7MaxValue":
                           playerDataBase.Candy7MaxValue = statistics.Value;
                           break;
                       case "Candy8MaxValue":
                           playerDataBase.Candy8MaxValue = statistics.Value;
                           break;
                       case "Candy9MaxValue":
                           playerDataBase.Candy9MaxValue = statistics.Value;
                           break;
                       case "JapaneseFood1MaxValue":
                           playerDataBase.JapaneseFood1MaxValue = statistics.Value;
                           break;
                       case "JapaneseFood2MaxValue":
                           playerDataBase.JapaneseFood2MaxValue = statistics.Value;
                           break;
                       case "JapaneseFood3MaxValue":
                           playerDataBase.JapaneseFood3MaxValue = statistics.Value;
                           break;
                       case "JapaneseFood4MaxValue":
                           playerDataBase.JapaneseFood4MaxValue = statistics.Value;
                           break;
                       case "JapaneseFood5MaxValue":
                           playerDataBase.JapaneseFood5MaxValue = statistics.Value;
                           break;
                       case "JapaneseFood6MaxValue":
                           playerDataBase.JapaneseFood6MaxValue = statistics.Value;
                           break;
                       case "JapaneseFood7MaxValue":
                           playerDataBase.JapaneseFood7MaxValue = statistics.Value;
                           break;
                       case "Dessert1MaxValue":
                           playerDataBase.Dessert1MaxValue = statistics.Value;
                           break;
                       case "Dessert2MaxValue":
                           playerDataBase.Dessert2MaxValue = statistics.Value;
                           break;
                       case "Dessert3MaxValue":
                           playerDataBase.Dessert3MaxValue = statistics.Value;
                           break;
                       case "Dessert4MaxValue":
                           playerDataBase.Dessert4MaxValue = statistics.Value;
                           break;
                       case "Dessert5MaxValue":
                           playerDataBase.Dessert5MaxValue = statistics.Value;
                           break;
                       case "Dessert6MaxValue":
                           playerDataBase.Dessert6MaxValue = statistics.Value;
                           break;
                       case "Dessert7MaxValue":
                           playerDataBase.Dessert7MaxValue = statistics.Value;
                           break;
                       case "Dessert8MaxValue":
                           playerDataBase.Dessert8MaxValue = statistics.Value;
                           break;
                       case "Dessert9MaxValue":
                           playerDataBase.Dessert9MaxValue = statistics.Value;
                           break;
                       case "SellCount":
                           playerDataBase.SellCount = statistics.Value;
                           break;
                       case "UseSources":
                           playerDataBase.UseSources = statistics.Value;
                           break;
                       case "OpenChestBox":
                           playerDataBase.OpenChestBox = statistics.Value;
                           break;
                       case "YummyTimeCount":
                           playerDataBase.YummyTimeCount = statistics.Value;
                           break;
                       case "Season1Reward":
                           playerDataBase.Season1Reward = statistics.Value;
                           break;
                       case "Season2Reward":
                           playerDataBase.Season2Reward = statistics.Value;
                           break;
                       case "Season3Reward":
                           playerDataBase.Season3Reward = statistics.Value;
                           break;
                       case "Season4Reward":
                           playerDataBase.Season4Reward = statistics.Value;
                           break;
                       case "Season5Reward":
                           playerDataBase.Season5Reward = statistics.Value;
                           break;
                       case "Season6Reward":
                           playerDataBase.Season6Reward = statistics.Value;
                           break;
                       case "Season7Reward":
                           playerDataBase.Season7Reward = statistics.Value;
                           break;
                       case "Season8Reward":
                           playerDataBase.Season8Reward = statistics.Value;
                           break;
                       case "Season9Reward":
                           playerDataBase.Season9Reward = statistics.Value;
                           break;
                       case "Season10Reward":
                           playerDataBase.Season10Reward = statistics.Value;
                           break;
                       case "Season11Reward":
                           playerDataBase.Season11Reward = statistics.Value;
                           break;
                       case "QuestCount":
                           playerDataBase.QuestCount = statistics.Value;
                           break;
                       case "Skill1":
                           playerDataBase.Skill1 = statistics.Value;
                           break;
                       case "Skill2":
                           playerDataBase.Skill2 = statistics.Value;
                           break;
                       case "Skill3":
                           playerDataBase.Skill3 = statistics.Value;
                           break;
                       case "Skill4":
                           playerDataBase.Skill4 = statistics.Value;
                           break;
                       case "Skill5":
                           playerDataBase.Skill5 = statistics.Value;
                           break;
                       case "Skill6":
                           playerDataBase.Skill6 = statistics.Value;
                           break;
                       case "Skill7":
                           playerDataBase.Skill7 = statistics.Value;
                           break;
                       case "Skill8":
                           playerDataBase.Skill8 = statistics.Value;
                           break;
                       case "Skill9":
                           playerDataBase.Skill9 = statistics.Value;
                           break;
                       case "Skill10":
                           playerDataBase.Skill10 = statistics.Value;
                           break;
                       case "Skill11":
                           playerDataBase.Skill11 = statistics.Value;
                           break;
                       case "Skill12":
                           playerDataBase.Skill12 = statistics.Value;
                           break;
                       case "Skill13":
                           playerDataBase.Skill13 = statistics.Value;
                           break;
                       case "Skill14":
                           playerDataBase.Skill14 = statistics.Value;
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
                       case "Coupon1":
                           playerDataBase.Coupon1 = statistics.Value;
                           break;
                       case "Coupon2":
                           playerDataBase.Coupon2 = statistics.Value;
                           break;
                       case "Coupon3":
                           playerDataBase.Coupon3 = statistics.Value;
                           break;
                       case "Coupon4":
                           playerDataBase.Coupon4 = statistics.Value;
                           break;
                       case "Coupon5":
                           playerDataBase.Coupon5 = statistics.Value;
                           break;
                       case "Coupon6":
                           playerDataBase.Coupon6 = statistics.Value;
                           break;
                       case "Coupon7":
                           playerDataBase.Coupon7 = statistics.Value;
                           break;
                       case "Coupon8":
                           playerDataBase.Coupon8 = statistics.Value;
                           break;
                       case "Coupon9":
                           playerDataBase.Coupon9 = statistics.Value;
                           break;
                       case "Coupon10":
                           playerDataBase.Coupon10 = statistics.Value;
                           break;
                       case "Coupon11":
                           playerDataBase.Coupon11 = statistics.Value;
                           break;
                       case "Coupon12":
                           playerDataBase.Coupon12 = statistics.Value;
                           break;
                       case "Coupon13":
                           playerDataBase.Coupon13 = statistics.Value;
                           break;
                       case "SpCoupon1":
                           playerDataBase.SpCoupon1 = statistics.Value;
                           break;
                       case "SpCoupon2":
                           playerDataBase.SpCoupon2 = statistics.Value;
                           break;
                       case "SpCoupon3":
                           playerDataBase.SpCoupon3 = statistics.Value;
                           break;
                       case "SpCoupon4":
                           playerDataBase.SpCoupon4 = statistics.Value;
                           break;
                       case "SpCoupon5":
                           playerDataBase.SpCoupon5 = statistics.Value;
                           break;
                       case "SpCoupon6":
                           playerDataBase.SpCoupon6 = statistics.Value;
                           break;
                       case "SpCoupon7":
                           playerDataBase.SpCoupon7 = statistics.Value;
                           break;
                       case "SpCoupon8":
                           playerDataBase.SpCoupon8 = statistics.Value;
                           break;
                       case "SpCoupon9":
                           playerDataBase.SpCoupon9 = statistics.Value;
                           break;
                       case "SpCoupon10":
                           playerDataBase.SpCoupon10 = statistics.Value;
                           break;
                       case "SpCoupon11":
                           playerDataBase.SpCoupon11 = statistics.Value;
                           break;
                       case "SpCoupon12":
                           playerDataBase.SpCoupon12 = statistics.Value;
                           break;
                       case "SpCoupon13":
                           playerDataBase.SpCoupon13 = statistics.Value;
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

                           if(GameStateManager.instance.Food8Level > playerDataBase.RankLevel1)
                           {
                               GameStateManager.instance.Food8Level = playerDataBase.RankLevel1;
                           }
                           break;
                       case "RankLevel2":
                           playerDataBase.RankLevel2 = statistics.Value;

                           if (GameStateManager.instance.Candy10Level > playerDataBase.RankLevel2)
                           {
                               GameStateManager.instance.Candy10Level = playerDataBase.RankLevel2;
                           }
                           break;
                       case "RankLevel3":
                           playerDataBase.RankLevel3 = statistics.Value;

                           if (GameStateManager.instance.JapaneseFood8Level > playerDataBase.RankLevel3)
                           {
                               GameStateManager.instance.JapaneseFood8Level = playerDataBase.RankLevel3;
                           }
                           break;
                       case "RankLevel4":
                           playerDataBase.RankLevel4 = statistics.Value;

                           if (GameStateManager.instance.Dessert10Level > playerDataBase.RankLevel4)
                           {
                               GameStateManager.instance.Dessert10Level = playerDataBase.RankLevel4;
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
                           playerDataBase.TotalLevel_6 = statistics.Value;
                           break;
                       case "TotalLevel_6":
                           playerDataBase.TotalLevel_7 = statistics.Value;
                           break;
                       case "TotalLevel_7":
                           playerDataBase.TotalLevel_8 = statistics.Value;
                           break;
                       case "TotalLevel_8":
                           playerDataBase.TotalLevel_9 = statistics.Value;
                           break;
                       case "TotalLevel_10":
                           playerDataBase.TotalLevel_10 = statistics.Value;
                           break;
                       case "Island1Level":
                           playerDataBase.Island1Level = statistics.Value;
                           break;
                       case "Island2Level":
                           playerDataBase.Island2Level = statistics.Value;
                           break;
                       case "Island3Level":
                           playerDataBase.Island3Level = statistics.Value;
                           break;
                       case "Island4Level":
                           playerDataBase.Island4Level = statistics.Value;
                           break;
                       case "Island1Count":
                           playerDataBase.Island1Count = statistics.Value;
                           break;
                       case "Island2Count":
                           playerDataBase.Island2Count = statistics.Value;
                           break;
                       case "Island3Count":
                           playerDataBase.Island3Count = statistics.Value;
                           break;
                       case "Island4Count":
                           playerDataBase.Island4Count = statistics.Value;
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
        }, DisplayPlayfabError);

        playerData = true;
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

    public void UpdateAddGold(int number)
    {
        moneyAnimation.PlusMoney(number);

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

    public void UpdateSubtractGold(int number)
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
            UpdatePlayerStatisticsInsert("ConsumeGold", playerDataBase.ConsumeGold);
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

                playerDataBase.ConsumeGold += number;
                UpdatePlayerStatisticsInsert("ConsumeGold", playerDataBase.ConsumeGold);

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
                if (name.Equals("CheckIOSVersion"))
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
                else
                {
                    Debug.Log(name + " ?? ???????? ???? ???????? ?????? ?? ????????");
                }
            },
            error =>
            {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }

    public void GetLeaderboarder(string name, Action<GetLeaderboardResult> successCalback)
    {
        var requestLeaderboard = new GetLeaderboardRequest
        {
            StartPosition = 0,
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
        try
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "GrantItemsToUser",
                FunctionParameter = new { ItemIds = itemIds, CatalogVersion = catalogVersion },
                GeneratePlayStreamEvent = true,
            }, OnCloudUpdateStats, DisplayPlayfabError);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
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
}
