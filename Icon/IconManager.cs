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

    public IconContent iconPrefab;

    public RectTransform iconContentTransform;

    public Image mainIcon; //간판 아이콘
    public Image profileIcon;

    public Text plusText;

    public GameObject saveLockObject;


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

        iconContentTransform.anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenIcon()
    {
        if (!iconView.activeSelf)
        {
            iconView.SetActive(true);

            CheckMyIcon();
        }
        else
        {
            iconView.SetActive(false);
        }
    }

    public void Initialize()
    {
        mainIcon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);
        profileIcon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);
    }

    public void CheckMyIcon()
    {
        for (int i = 0; i < iconContentList.Count; i++)
        {
            iconContentList[i].CheckMark(false);
        }

        iconContentList[playerDataBase.Icon].CheckMark(true);

        int plusScore = playerDataBase.GetIconHoldNumber();

        plusText.text = LocalizationManager.instance.GetString("NowPrice") + " +" + (0.1f * plusScore).ToString() + "%  (+0.1%)";

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

        mainIcon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);
        profileIcon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);
    }

    public void CheckIconUnLock() //아이콘 언락 체크
    {
        if(playerDataBase.Candy9MaxValue > 0 || playerDataBase.CheckIcon(IconType.Icon_2))
        {
            iconContentList[1].UnLock();

            if(!playerDataBase.CheckIcon(IconType.Icon_2))
            {
                playerDataBase.SetIcon(IconType.Icon_2, 1);
                PlayfabManager.instance.GrantItemsToUser("Icon_2", "Icon");
            }
        }

        if (playerDataBase.JapaneseFood7MaxValue > 0 || playerDataBase.CheckIcon(IconType.Icon_3))
        {
            iconContentList[2].UnLock();

            if (!playerDataBase.CheckIcon(IconType.Icon_3))
            {
                playerDataBase.SetIcon(IconType.Icon_3, 1);
                PlayfabManager.instance.GrantItemsToUser("Icon_3", "Icon");
            }
        }

        if (playerDataBase.Dessert9MaxValue > 0 || playerDataBase.CheckIcon(IconType.Icon_4))
        {
            iconContentList[3].UnLock();

            if (!playerDataBase.CheckIcon(IconType.Icon_4))
            {
                playerDataBase.SetIcon(IconType.Icon_4, 1);
                PlayfabManager.instance.GrantItemsToUser("Icon_4", "Icon");
            }
        }

        if(proficiencyDataBase.GetLevel(playerDataBase.SandwichMaxValue) > 9 || playerDataBase.CheckIcon(IconType.Icon_5))
        {
            iconContentList[4].UnLock();

            if (!playerDataBase.CheckIcon(IconType.Icon_5))
            {
                playerDataBase.SetIcon(IconType.Icon_5, 1);
                PlayfabManager.instance.GrantItemsToUser("Icon_5", "Icon");
            }
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

        mainIcon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);
        profileIcon.sprite = imageDataBase.GetIconArray(iconType + playerDataBase.Icon);

        for (int i = 0; i < iconContentList.Count; i++)
        {
            iconContentList[i].CheckMark(false);
        }

        iconContentList[(int)iconType].CheckMark(true);

        saveLockObject.SetActive(true);

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Icon", (int)iconType);

        NotionManager.instance.UseNotion(NotionType.SaveNotion);
    }
}
