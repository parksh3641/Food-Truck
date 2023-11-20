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
    public Text levelText;
    public Text proficiencyText;
    public GameObject lockedObj;
    public GameObject selectedObj;

    public LocalizationContent lockedText;

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

        lockedText.localizationName = "FoodLocked";
        lockedText.ReLoad();
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

        if(type == CandyType.Chocolate)
        {
            lockedText.localizationName = "RankLocked1";
        }
        else
        {
            lockedText.localizationName = "FoodLocked";
        }
        lockedText.ReLoad();

        proficiencyText.text = LocalizationManager.instance.GetString("Proficiency") + "Lv. 1";
    }

    public void SetLevel(int level, int max)
    {
        levelText.text = "( " + (level + 1).ToString() + " / " + max.ToString() + " )";
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
            if(foodType == FoodType.Ribs)
            {
                changeFoodManager.ChangeRankFood(foodType);
            }
            else
            {
                changeFoodManager.ChangeFood(foodType);
            }
        }
        else
        {
            if(candyType == CandyType.Chocolate)
            {
                changeFoodManager.ChangeRankCandy(candyType);
            }
            else
            {
                changeFoodManager.ChangeCandy(candyType);
            }
        }
    }
}
