using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class IconClass
{
    public IconType iconType = IconType.Icon_1;
    public int count = 0;
}

public class IconManager : MonoBehaviour
{
    IconType iconType = IconType.Icon_1;

    public GameObject iconView;

    public GameObject alarm;

    public Text titleText;

    public IconContent iconPrefab;

    public RectTransform iconContentTransform;

    public Image icon;
    public Image mainIcon; //간판 아이콘
    public Image profileIcon;

    public Text plusText;

    public GameObject saveLockObject;

    private int number = 0;
    private int maxProficiency = 4;
    private float reward = 0.5f;


    public List<IconContent> iconContentList = new List<IconContent>();

    public GuideMissionManager guideMissionManager;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;
    ProficiencyDataBase proficiencyDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (proficiencyDataBase == null) proficiencyDataBase = Resources.Load("ProficiencyDataBase") as ProficiencyDataBase;

        for (int i = 0; i < System.Enum.GetValues(typeof(IconType)).Length; i ++)
        {
            IconContent monster = Instantiate(iconPrefab);
            monster.transform.SetParent(iconContentTransform);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;

            monster.gameObject.SetActive(true);

            iconContentList.Add(monster);
        }

        iconView.SetActive(false);
        saveLockObject.SetActive(true);

        alarm.SetActive(true);

        iconContentTransform.anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenIcon()
    {
        if (!iconView.activeSelf)
        {
            iconView.SetActive(true);

            alarm.SetActive(false);

            CheckMyIcon();

            FirebaseAnalytics.LogEvent("Open_Icon");
        }
        else
        {
            if (guideMissionManager.guideMissonView.gameObject.activeInHierarchy)
            {
                guideMissionManager.Initialize();
            }

            iconView.SetActive(false);
        }
    }

    public void Initialize()
    {
        icon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);
        mainIcon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);
        profileIcon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);
    }

    public void CheckMyIcon()
    {
        titleText.text = LocalizationManager.instance.GetString("ChangeIcon") + " ( " + (playerDataBase.GetIconHoldNumber() + 1) +" / " + System.Enum.GetValues(typeof(IconType)).Length + " )"
            + "\n<size=11>" + LocalizationManager.instance.GetString("Collect") + " : " + (((playerDataBase.GetIconHoldNumber() * 1.0f) / System.Enum.GetValues(typeof(IconType)).Length) * 100f).ToString("N1") + "%</size>";

        for (int i = 0; i < iconContentList.Count; i++)
        {
            iconContentList[i].CheckMark(false);
        }

        iconContentList[playerDataBase.Icon].CheckMark(true);

        plusText.text = LocalizationManager.instance.GetString("NowPrice") + " +" + (playerDataBase.GetIconHoldNumber() * reward).ToString() + "%  (+" + reward + "%)";

        CheckInitialize();
    }

    public void CheckInitialize()
    {
        IconType iconType = IconType.Icon_1;

        for (int i = 0; i < iconContentList.Count; i++)
        {
            if (imageDataBase.GetIconArray(iconType))
            {
                iconContentList[i].Initialize(this, iconType);

                if (i == 0)
                {
                    iconContentList[i].UnLock();
                }
            }

            iconType++;
        }

        CheckIconUnLock();

        iconType = IconType.Icon_1;

        icon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);
        mainIcon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);
        profileIcon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);
    }

    public void CheckIconUnLock() //아이콘 언락 체크
    {
        number = 1;
        if (playerDataBase.Candy9MaxValue > 0 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.JapaneseFood7MaxValue > 0 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Dessert9MaxValue > 0 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Food2MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Food3MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Food4MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Food5MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Food6MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Candy1MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Candy2MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Candy3MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Candy4MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Candy5MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Candy6MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Candy7MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Candy8MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Candy9MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.JapaneseFood1MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.JapaneseFood2MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.JapaneseFood3MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.JapaneseFood4MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.JapaneseFood5MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.JapaneseFood6MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.JapaneseFood7MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Dessert1MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Dessert2MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Dessert3MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Dessert4MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Dessert5MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Dessert6MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Dessert7MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Dessert8MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.Dessert9MaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.RankLevel1) > 199 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.RankLevel2) > 199 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.RankLevel3) > 199 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.RankLevel4) > 199 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.GetAnimalNumber() > 6 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.GetTruckNumber() > 8 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.GetCharacterNumber() > 18 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.GetButterflyNumber() > 26 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.UseSauce1 > 99 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.UseSauce2 > 99 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.UseSauce3 > 99 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.UseSauce4 > 99 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.UseSauce5 > 99 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }


        int sauce = playerDataBase.UseSauce1 + playerDataBase.UseSauce2 + playerDataBase.UseSauce3 + playerDataBase.UseSauce4 + playerDataBase.UseSauce5;
        number++;
        if (sauce > 999 ||playerDataBase.CheckIcon(IconType.Icon_1 + number)) //아무 소스 1000번
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.WelcomeCount >= 7 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.UpgradeCount > 999 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.UpgradeCount > 9999 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (GameStateManager.instance.Bankruptcy > 0 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.GetEpicBookNumber() == 32 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.RecipeEventCount > 999 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.OfflineCount > 99 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.AccessDate > 6 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.AccessDate > 29 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.CheckIcon(IconType.Icon_1 + number)) //버그 발견 이벤트 참여
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.CheckIcon(IconType.Icon_1 + number)) //리뷰 작성 이벤트 참여
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.DefDestroyCount > 299 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //파괴 방어 300회
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.YummyTimeCount > 99 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.RecoverCount > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //복구 50회
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Dungeon1Count > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Dungeon2Count > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Dungeon3Count > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Dungeon4Count > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure1 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure2 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure3 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure4 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure5 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure6 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure7 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure8 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure9 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure10 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure11 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure12 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure13 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure14 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.Treasure15 > 49 || playerDataBase.CheckIcon(IconType.Icon_1 + number)) //보물
        {
            GetIcon(number);
        }

        Debug.Log((number + 1));
    }

    void GetIcon(int number)
    {
        iconContentList[number].UnLock();

        if (!playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            playerDataBase.SetIcon(IconType.Icon_1 + number, 1);
            PlayfabManager.instance.GrantItemsToUser((IconType.Icon_1 + number).ToString(), "Icon");
        }
    }

    public void UseIcon(IconType type)
    {
        if ((int)type == playerDataBase.Icon)
        {
            saveLockObject.SetActive(true);
        }
        else
        {
            saveLockObject.SetActive(false);
        }

        for (int i = 0; i < iconContentList.Count; i++)
        {
            iconContentList[i].CheckMark(false);
        }

        iconContentList[(int)type].CheckMark(true);

        iconType = type;
    }

    public void SaveIcon()
    {
        playerDataBase.Icon = (int)iconType;

        icon.sprite = imageDataBase.GetIconArray(IconType.Icon_1 + playerDataBase.Icon);
        mainIcon.sprite = imageDataBase.GetIconArray(IconType.Icon_1 + playerDataBase.Icon);
        profileIcon.sprite = imageDataBase.GetIconArray(IconType.Icon_1 + playerDataBase.Icon);

        for (int i = 0; i < iconContentList.Count; i++)
        {
            iconContentList[i].CheckMark(false);
        }

        iconContentList[(int)iconType].CheckMark(true);

        saveLockObject.SetActive(true);

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Icon", (int)iconType);

        FirebaseAnalytics.LogEvent("Change_Icon");

        NotionManager.instance.UseNotion(NotionType.SaveNotion);
        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
    }
}
