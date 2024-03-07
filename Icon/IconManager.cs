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

    public GameObject alarm;

    public GameObject iconView;

    public Text titleText;

    public IconContent iconPrefab;

    public RectTransform iconContentTransform;

    public Image icon;
    public Image mainIcon; //간판 아이콘
    public Image profileIcon;

    public Text plusText;

    public GameObject saveLockObject;

    private int number = 0;
    private int maxProficiency = 9;
    private float reward = 0.5f;


    public List<IconContent> iconContentList = new List<IconContent>();

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

            FirebaseAnalytics.LogEvent("OpenIcon");
        }
        else
        {
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
        titleText.text = LocalizationManager.instance.GetString("ChangeIcon") + " ( " + (playerDataBase.GetIconHoldNumber() + 1) +" / " + System.Enum.GetValues(typeof(IconType)).Length + " )";

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
        if (proficiencyDataBase.GetLevel(playerDataBase.SandwichMaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.HotdogTruck) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.DrinkMaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.PizzaMaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (proficiencyDataBase.GetLevel(playerDataBase.DonutMaxValue) > maxProficiency || playerDataBase.CheckIcon(IconType.Icon_1 + number))
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
        if (playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.GetPetNumber() > 6 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.GetFoodTruckNumber() > 8 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
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
        if (playerDataBase.UseSauce1 > 199 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.UseSauce2 > 199 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.UseSauce3 > 199 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.UseSauce4 > 199 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }

        number++;
        if (playerDataBase.UseSauce5 > 199 || playerDataBase.CheckIcon(IconType.Icon_1 + number))
        {
            GetIcon(number);
        }
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

        FirebaseAnalytics.LogEvent("ChangeIcon");

        NotionManager.instance.UseNotion(NotionType.SaveNotion);
        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
    }
}
