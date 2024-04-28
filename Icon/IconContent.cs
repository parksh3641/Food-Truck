using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconContent : MonoBehaviour
{
    public IconType iconType;
    IconClass iconClass;

    public Image icon;

    public GameObject lockedObj;
    public GameObject checkMark;

    IconManager iconManager;

    ImageDataBase imageDataBase;
    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        lockedObj.SetActive(true);
        checkMark.SetActive(false);
    }

    public void Initialize(IconManager manager, IconType type)
    {
        iconManager = manager;
        iconType = type;

        icon.sprite = imageDataBase.GetIconArray(iconType);

        InitState();
    }

    public void InitState()
    {
        iconClass = playerDataBase.GetIconState(iconType);

        if (iconClass.count >= 1)
        {
            UnLock();
        }
    }

    public void UnLock()
    {
        lockedObj.SetActive(false);
    }

    public void CheckMark(bool check)
    {
        checkMark.SetActive(check);
    }

    public void OnClick()
    {
        if(!lockedObj.activeSelf)
        {
            iconManager.UseIcon(iconType);
        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);

            Debug.Log(iconType.ToString());

            switch (iconType)
            {
                case IconType.Icon_2:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked4);
                    break;
                case IconType.Icon_3:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked5);
                    break;
                case IconType.Icon_4:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked6);
                    break;
                case IconType.Icon_5:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked7);
                    break;
                case IconType.Icon_6:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked9);
                    break;
                case IconType.Icon_7:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked10);
                    break;
                case IconType.Icon_8:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked11);
                    break;
                case IconType.Icon_9:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked12);
                    break;
                case IconType.Icon_11:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked13);
                    break;
                case IconType.Icon_12:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked13);
                    break;
                case IconType.Icon_13:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked13);
                    break;
                case IconType.Icon_14:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked13);
                    break;
                case IconType.Icon_15:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked13);
                    break;
                case IconType.Icon_16:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked14);
                    break;
                case IconType.Icon_17:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked15);
                    break;
                case IconType.Icon_18:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked16);
                    break;
                case IconType.Icon_19:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked17);
                    break;
                case IconType.Icon_20:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked18);
                    break;
                case IconType.Icon_21:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked19);
                    break;
                case IconType.Icon_22:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked20);
                    break;
                case IconType.Icon_23:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked21);
                    break;
                case IconType.Icon_24:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked22);
                    break;
                case IconType.Icon_25:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked23);
                    break;
                case IconType.Icon_26:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked24);
                    break;
                case IconType.Icon_27:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked25);
                    break;
                case IconType.Icon_28:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked26);
                    break;
                case IconType.Icon_29:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked27);
                    break;
                case IconType.Icon_30:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked28);
                    break;
                case IconType.Icon_31:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked29);
                    break;
                case IconType.Icon_32:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked29);
                    break;
                case IconType.Icon_33:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked29);
                    break;
                case IconType.Icon_34:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked29);
                    break;
                case IconType.Icon_35:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_36:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_37:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_38:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_39:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_40:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_41:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_42:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_43:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_44:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_45:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_46:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_47:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_48:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                case IconType.Icon_49:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked30);
                    break;
                default:
                    NotionManager.instance.UseNotion(NotionType.Icon_Locked8);
                    break;
            }
        }
    }
}
