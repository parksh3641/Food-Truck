using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFoodContent : MonoBehaviour
{
    public FoodType foodType = FoodType.Food1;
    public RankFoodType rankFoodType = RankFoodType.RankFood1;

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


    public void Initialize_Food(FoodType type, Sprite sp, ChangeFoodManager manager)
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
    }

    public void Initialize_RankFood(RankFoodType type, Sprite sp, ChangeFoodManager manager)
    {
        index = 1;

        rankFoodType = type;

        icon.sprite = sp;

        background.color = foodColor;

        changeFoodManager = manager;

        titleText.localizationName = type.ToString();
        titleText.ReLoad();

        lockedText.localizationName = "FoodLocked";
        lockedText.ReLoad();

        proficiency.SetActive(false);

        bestText.text = LocalizationManager.instance.GetString("Best") + " : " + playerDataBase.RankLevel1;
    }

    public void CheckFoodProficiency()
    {
        exp = playerDataBase.island_Total_Data.island_Max_Datas[(int)GameStateManager.instance.IslandType].GetValue(foodType);

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
        if (index == 0)
        {
            changeFoodManager.ChangeFood(foodType);
        }
        else
        {
            changeFoodManager.ChangeRankFood(rankFoodType);
        }

        moveArrow.SetActive(false);
    }
}
