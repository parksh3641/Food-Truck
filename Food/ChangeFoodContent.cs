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

    Color food1Color = new Color(209 / 255f, 243 / 255f, 224 / 255f);
    Color food2Color = new Color(254 / 255f, 185 / 255f, 200 / 255f);
    Color food3Color = new Color(246 / 255f, 167 / 255f, 186 / 255f);
    Color food4Color = new Color(245 / 255f, 250 / 255f, 241 / 255f);

    Color food5Color = new Color(228 / 255f, 23 / 255f, 73 / 255f);
    Color food6Color = new Color(245 / 255f, 88 / 255f, 123 / 255f);
    Color food7Color = new Color(255 / 255f, 138 / 255f, 92 / 255f);
    Color food8Color = new Color(254 / 255f, 245 / 255f, 145 / 255f);

    Color food9Color = new Color(190 / 255f, 238 / 255f, 246 / 255f);
    Color food10Color = new Color(111 / 255f, 194 / 255f, 208 / 255f);
    Color food11Color = new Color(55 / 255f, 58 / 255f, 109 / 255f);
    Color food12Color = new Color(255 / 255f, 130 / 255f, 70 / 255f);

    Color food13Color = new Color(178 / 255f, 6 / 255f, 176 / 255f);
    Color food14Color = new Color(228 / 255f, 23 / 255f, 73 / 255f);
    Color food15Color = new Color(69 / 255f, 146 / 255f, 175 / 255f);
    Color food16Color = new Color(226 / 255f, 196 / 255f, 168 / 255f);

    Color food17Color = new Color(240 / 255f, 245 / 255f, 159 / 255f);
    Color food18Color = new Color(176 / 255f, 224 / 255f, 168 / 255f);
    Color food19Color = new Color(216 / 255f, 239 / 255f, 240 / 255f);
    Color food20Color = new Color(191 / 255f, 204 / 255f, 126 / 255f);

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

        switch ((int)type / GameStateManager.instance.Island)
        {
            case 0:
                background.color = food1Color;
                break;
            case 1:
                background.color = food2Color;
                break;
            case 2:
                background.color = food3Color;
                break;
            case 3:
                background.color = food4Color;
                break;
            case 4:
                background.color = food4Color;
                break;
            case 5:
                background.color = food5Color;
                break;
            case 6:
                background.color = food6Color;
                break;
            case 7:
                background.color = food7Color;
                break;
            case 8:
                background.color = food8Color;
                break;
            case 9:
                background.color = food9Color;
                break;
            case 10:
                background.color = food10Color;
                break;
            case 11:
                background.color = food11Color;
                break;
            case 12:
                background.color = food12Color;
                break;
            case 13:
                background.color = food13Color;
                break;
            case 14:
                background.color = food14Color;
                break;
            case 15:
                background.color = food15Color;
                break;
            case 16:
                background.color = food16Color;
                break;
            case 17:
                background.color = food17Color;
                break;
            case 18:
                background.color = food18Color;
                break;
            case 19:
                background.color = food19Color;
                break;
            case 20:
                background.color = food20Color;
                break;
            case 21:
                background.color = food1Color;
                break;
            case 22:
                background.color = food2Color;
                break;
            case 23:
                background.color = food3Color;
                break;
            case 24:
                background.color = food4Color;
                break;
            case 25:
                background.color = food5Color;
                break;
            case 26:
                background.color = food6Color;
                break;
            case 27:
                background.color = food7Color;
                break;
            case 28:
                background.color = food8Color;
                break;
            case 29:
                background.color = food9Color;
                break;
            default:
                background.color = food1Color;
                break;
        }

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

        bestText.text = LocalizationManager.instance.GetString("Best");

        switch (type)
        {
            case RankFoodType.RankFood1:
                background.color = food1Color;

                bestText.text += " : " + playerDataBase.RankLevel1;
                break;
            case RankFoodType.RankFood2:
                background.color = food2Color;

                bestText.text += " : " + playerDataBase.RankLevel2;
                break;
            case RankFoodType.RankFood3:
                background.color = food3Color;

                bestText.text += " : " + playerDataBase.RankLevel3;
                break;
            case RankFoodType.RankFood4:
                background.color = food4Color;

                bestText.text += " : " + playerDataBase.RankLevel4;
                break;
            case RankFoodType.RankFood5:
                background.color = food5Color;

                bestText.text += " : " + playerDataBase.RankLevel5;
                break;
            case RankFoodType.RankFood6:
                background.color = food6Color;

                bestText.text += " : " + playerDataBase.RankLevel6;
                break;
            case RankFoodType.RankFood7:
                background.color = food7Color;

                bestText.text += " : " + playerDataBase.RankLevel7;
                break;
            case RankFoodType.RankFood8:
                background.color = food8Color;

                bestText.text += " : " + playerDataBase.RankLevel8;
                break;
            case RankFoodType.RankFood9:
                background.color = food9Color;

                bestText.text += " : " + playerDataBase.RankLevel9;
                break;
            case RankFoodType.RankFood10:
                background.color = food10Color;

                bestText.text += " : " + playerDataBase.RankLevel10;
                break;
        }

        changeFoodManager = manager;

        titleText.localizationName = type.ToString();
        titleText.ReLoad();

        lockedText.localizationName = "IslandLocked";
        lockedText.ReLoad();

        proficiency.SetActive(false);
    }

    public void CheckFoodProficiency()
    {
        exp = 0;

        if (playerDataBase.island_Total_Data.island_Max_Datas[(int)GameStateManager.instance.IslandType] != null)
        {
            exp = playerDataBase.island_Total_Data.island_Max_Datas[(int)GameStateManager.instance.IslandType].GetValue((int)foodType % GameStateManager.instance.Island);
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
