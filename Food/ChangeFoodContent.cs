using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFoodContent : MonoBehaviour
{
    public FoodType foodType = FoodType.Food1;
    public CandyType candyType = CandyType.Candy1;
    public JapaneseFoodType japaneseFoodType = JapaneseFoodType.JapaneseFood1;
    public DessertType dessertType = DessertType.Dessert1;

    public Image icon;
    public LocalizationContent titleText;
    public Text levelText;
    public Text proficiencyText;
    public GameObject lockedObj;
    public GameObject selectedObj;
    public GameObject proficiency;
    public Text bestText;

    public Text proficiencyValueText;
    public Image proficiencyFillamount;

    public LocalizationContent lockedText;

    public Image background;

    public GameObject moveArrow;

    Color foodColor = new Color(206 / 255f, 141 / 255f, 1);
    Color candyColor = new Color(247 / 255f, 160 / 255f, 0);
    Color jpColor = new Color(94 / 255f, 102 / 255f, 220 / 255f);
    Color dessertColor = new Color(242 / 255f, 138 / 255f, 222 / 255f);

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

        moveArrow.SetActive(false);
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

        proficiency.SetActive(true);
        bestText.text = "";

        if (type == FoodType.Ribs)
        {
            proficiency.SetActive(false);

            bestText.text = LocalizationManager.instance.GetString("Best") + " : " + playerDataBase.RankLevel1;
        }
    }

    public void CheckFoodProficiency()
    {
        switch (foodType)
        {
            case FoodType.Food1:
                exp = playerDataBase.HamburgerMaxValue;
                break;
            case FoodType.Food2:
                exp = playerDataBase.SandwichMaxValue;
                break;
            case FoodType.Food3:
                exp = playerDataBase.SnackLabMaxValue;
                break;
            case FoodType.Food4:
                exp = playerDataBase.DrinkMaxValue;
                break;
            case FoodType.Food5:
                exp = playerDataBase.PizzaMaxValue;
                break;
            case FoodType.Food6:
                exp = playerDataBase.DonutMaxValue;
                break;
            case FoodType.Food7:
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

        proficiency.SetActive(true);
        bestText.text = "";

        if (type == CandyType.Chocolate)
        {
            proficiency.SetActive(false);
            lockedText.localizationName = "RankLocked1";

            bestText.text = LocalizationManager.instance.GetString("Best") + " : " + playerDataBase.RankLevel2;
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

    public void InitializeJapaneseFood(JapaneseFoodType type, Sprite sp, ChangeFoodManager manager)
    {
        index = 2;

        japaneseFoodType = type;

        icon.sprite = sp;

        background.color = jpColor;

        changeFoodManager = manager;

        titleText.localizationName = type.ToString();
        titleText.ReLoad();

        proficiency.SetActive(true);
        bestText.text = "";

        if (type == JapaneseFoodType.Ramen)
        {
            proficiency.SetActive(false);
            lockedText.localizationName = "RankLocked2";

            bestText.text = LocalizationManager.instance.GetString("Best") + " : " + playerDataBase.RankLevel3;
        }
        else
        {
            lockedText.localizationName = "FoodLocked";
        }

        lockedText.ReLoad();
    }

    public void CheckJapaneseFoodProficiency()
    {
        switch (japaneseFoodType)
        {
            case JapaneseFoodType.JapaneseFood1:
                exp = playerDataBase.JapaneseFood1MaxValue;
                break;
            case JapaneseFoodType.JapaneseFood2:
                exp = playerDataBase.JapaneseFood2MaxValue;
                break;
            case JapaneseFoodType.JapaneseFood3:
                exp = playerDataBase.JapaneseFood3MaxValue;
                break;
            case JapaneseFoodType.JapaneseFood4:
                exp = playerDataBase.JapaneseFood4MaxValue;
                break;
            case JapaneseFoodType.JapaneseFood5:
                exp = playerDataBase.JapaneseFood5MaxValue;
                break;
            case JapaneseFoodType.JapaneseFood6:
                exp = playerDataBase.JapaneseFood6MaxValue;
                break;
            case JapaneseFoodType.JapaneseFood7:
                exp = playerDataBase.JapaneseFood7MaxValue;
                break;
            case JapaneseFoodType.Ramen:
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

    public void InitializeDessert(DessertType type, Sprite sp, ChangeFoodManager manager)
    {
        index = 3;

        dessertType = type;

        icon.sprite = sp;

        background.color = dessertColor;

        changeFoodManager = manager;

        titleText.localizationName = type.ToString();
        titleText.ReLoad();

        proficiency.SetActive(true);
        bestText.text = "";

        if (type == DessertType.FruitSkewers)
        {
            proficiency.SetActive(false);
            lockedText.localizationName = "RankLocked3";

            bestText.text = LocalizationManager.instance.GetString("Best") + " : " + playerDataBase.RankLevel4;
        }
        else
        {
            lockedText.localizationName = "FoodLocked";
        }

        lockedText.ReLoad();
    }

    public void CheckDessertProficiency()
    {
        switch (dessertType)
        {
            case DessertType.Dessert1:
                exp = playerDataBase.Dessert1MaxValue;
                break;
            case DessertType.Dessert2:
                exp = playerDataBase.Dessert2MaxValue;
                break;
            case DessertType.Dessert3:
                exp = playerDataBase.Dessert3MaxValue;
                break;
            case DessertType.Dessert4:
                exp = playerDataBase.Dessert4MaxValue;
                break;
            case DessertType.Dessert5:
                exp = playerDataBase.Dessert5MaxValue;
                break;
            case DessertType.Dessert6:
                exp = playerDataBase.Dessert6MaxValue;
                break;
            case DessertType.Dessert7:
                exp = playerDataBase.Dessert7MaxValue;
                break;
            case DessertType.Dessert8:
                exp = playerDataBase.Dessert8MaxValue;
                break;
            case DessertType.Dessert9:
                exp = playerDataBase.Dessert9MaxValue;
                break;
            case DessertType.FruitSkewers:
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

    public void SetMoveArrow()
    {
        moveArrow.SetActive(true);
    }




    public void SetLevel(int level, int max)
    {
        if(level + 1 > max)
        {
            level = max - 1;
        }

        levelText.text = "Lv. ( " + (level + 1).ToString() + " / " + max.ToString() + " )";
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
        switch (index)
        {
            case 0:
                if (foodType == FoodType.Ribs)
                {
                    changeFoodManager.ChangeRankFood(foodType);
                }
                else
                {
                    changeFoodManager.ChangeFood(foodType);
                }
                break;
            case 1:
                if (candyType == CandyType.Chocolate)
                {
                    changeFoodManager.ChangeRankCandy(candyType);
                }
                else
                {
                    changeFoodManager.ChangeCandy(candyType);
                }
                break;
            case 2:
                if (japaneseFoodType == JapaneseFoodType.Ramen)
                {
                    changeFoodManager.ChangeRankJapaneseFood(japaneseFoodType);
                }
                else
                {
                    changeFoodManager.ChangeJapaneseFood(japaneseFoodType);
                }
                break;
            case 3:
                if (dessertType == DessertType.FruitSkewers)
                {
                    changeFoodManager.ChangeRankDessert(dessertType);
                }
                else
                {
                    changeFoodManager.ChangeDessert(dessertType);
                }
                break;
        }

        moveArrow.SetActive(false);
    }
}
