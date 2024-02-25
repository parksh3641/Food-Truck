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
    }
}
