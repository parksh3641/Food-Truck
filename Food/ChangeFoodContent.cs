using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFoodContent : MonoBehaviour
{
    public FoodType foodType = FoodType.Hamburger;
    public CandyType candyType = CandyType.Candy1;

    public Image icon;
    public LocalizationContent titleText;
    public GameObject lockedObj;
    public GameObject selectedObj;

    public Image background;

    Color foodColor = new Color(206 / 255f, 141 / 255f, 1);
    Color candyColor = new Color(247 / 255f, 160 / 255f, 0);

    private int index = 0;

    ChangeFoodManager changeFoodManager;


    public void InitializeFood(FoodType type, Sprite sp, ChangeFoodManager manager)
    {
        index = 0;

        foodType = type;

        icon.sprite = sp;

        background.color = foodColor;

        changeFoodManager = manager;

        titleText.localizationName = type.ToString();
        titleText.ReLoad();
    }

    public void InitializeCandy(CandyType type, Sprite sp, ChangeFoodManager manager)
    {
        index = 1;

        candyType = type;

        icon.sprite = sp;

        background.color = candyColor;

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

    public void Selected()
    {
        selectedObj.SetActive(true);
    }

    public void UnSelected()
    {
        selectedObj.SetActive(false);
    }

    public void OnClick()
    {
        if (index == 0)
        {
            changeFoodManager.ChangeFood(foodType);
        }
        else
        {
            changeFoodManager.ChangeCandy(candyType);
        }
    }
}
