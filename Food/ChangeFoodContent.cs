using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFoodContent : MonoBehaviour
{
    public FoodType foodType = FoodType.Hamburger;

    public Image icon;

    public LocalizationContent titleText;

    public GameObject lockedObj;

    public ChangeFoodManager changeFoodManager;


    public void Initialize(FoodType type, Sprite sp, ChangeFoodManager manager)
    {
        foodType = type;

        icon.sprite = sp;

        changeFoodManager = manager;

        titleText.localizationName = type.ToString();
        titleText.ReLoad();
    }

    public void SetLevel(int level, int max)
    {
        titleText.plusText = "\n( +" + (level + 1).ToString() + " / " + max.ToString() + " )";

        titleText.ReLoad();
    }

    public void Locked()
    {
        lockedObj.SetActive(true);
    }

    public void UnLock()
    {
        lockedObj.SetActive(false);
    }

    public void OnClick()
    {
        changeFoodManager.ChangeFood(foodType);
    }
}
