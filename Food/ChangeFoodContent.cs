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

    public Text proficiencyValueText;
    public Image proficiencyFillamount;

    public LocalizationContent lockedText;

    public Image background;

    Color foodColor = new Color(206 / 255f, 141 / 255f, 1);
    Color candyColor = new Color(247 / 255f, 160 / 255f, 0);

    private int index = 0;

    private int exp = 0;
    private int level = 0;
    private int nowExp = 0;
    private int nextExp = 0;

    ChangeFoodManager changeFoodManager;

    PlayerDataBase playerDataBase;
    ProficiencyDataBase proficiencyDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (proficiencyDataBase == null) proficiencyDataBase = Resources.Load("ProficiencyDataBase") as ProficiencyDataBase;
    }


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

    public void CheckFoodProficiency()
    {
        switch (foodType)
        {
            case FoodType.Hamburger:
                exp = playerDataBase.HamburgerMaxValue;
                break;
            case FoodType.Sandwich:
                exp = playerDataBase.SandwichMaxValue;
                break;
            case FoodType.SnackLab:
                exp = playerDataBase.SnackLabMaxValue;
                break;
            case FoodType.Drink:
                exp = playerDataBase.DrinkMaxValue;
                break;
            case FoodType.Pizza:
                exp = playerDataBase.PizzaMaxValue;
                break;
            case FoodType.Donut:
                exp = playerDataBase.DonutMaxValue;
                break;
            case FoodType.Fries:
                exp = playerDataBase.FriesMaxValue;
                break;
            case FoodType.Ribs:
                exp = 0;
                break;
        }

        level = proficiencyDataBase.GetLevel(exp);

        proficiencyText.text = LocalizationManager.instance.GetString("Proficiency") + "  Lv." + (level + 1);

        nowExp = proficiencyDataBase.GetNowExp(exp);
        nextExp = proficiencyDataBase.GetNextExp(level);

        proficiencyValueText.text = nowExp + " / " + nextExp;

        proficiencyFillamount.fillAmount = (nowExp * 1.0f) / (nextExp * 1.0f);
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
    }

    public void CheckCandyProficiency()
    {
        switch (candyType)
        {
            case CandyType.Candy1:
                exp = playerDataBase.Candy1MaxValue;
                break;
            case CandyType.Candy2:
                exp = playerDataBase.Candy2MaxValue;
                break;
            case CandyType.Candy3:
                exp = playerDataBase.Candy3MaxValue;
                break;
            case CandyType.Candy4:
                exp = playerDataBase.Candy4MaxValue;
                break;
            case CandyType.Candy5:
                exp = playerDataBase.Candy5MaxValue;
                break;
            case CandyType.Candy6:
                exp = playerDataBase.Candy6MaxValue;
                break;
            case CandyType.Candy7:
                exp = playerDataBase.Candy7MaxValue;
                break;
            case CandyType.Candy8:
                exp = playerDataBase.Candy8MaxValue;
                break;
            case CandyType.Candy9:
                exp = playerDataBase.Candy9MaxValue;
                break;
            case CandyType.Chocolate:
                exp = 0;
                break;
        }

        level = proficiencyDataBase.GetLevel(exp);

        proficiencyText.text = LocalizationManager.instance.GetString("Proficiency") + " Lv." + (level + 1);

        nowExp = proficiencyDataBase.GetNowExp(exp);
        nextExp = proficiencyDataBase.GetNextExp(level);

        proficiencyValueText.text = nowExp + " / " + nextExp;

        proficiencyFillamount.fillAmount = (nowExp * 1.0f) / (nextExp * 1.0f);
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
