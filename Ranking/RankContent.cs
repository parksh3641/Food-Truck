using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankContent : MonoBehaviour
{
    ChefType chefType;

    public GameObject frame;
    public GameObject effect;

    public Image classImg;

    public Image iconImg;
    public Image banner;
    public Text indexText;
    public Image indexRankImg;
    public Sprite[] rankIconList;
    public Image countryImg;
    public Text nickNameText;
    public Text titleText;
    public Text scoreText;

    Sprite sp;

    ImageDataBase imageDataBase;


    private void Awake()
    {
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        indexText.text = "";
        nickNameText.text = "";
    }

    public void SetIndex(int index)
    {
        if (index <= 3)
        {
            indexRankImg.enabled = true;
            indexRankImg.sprite = rankIconList[index - 1];
            effect.SetActive(true);
        }
        else
        {
            indexRankImg.enabled = false;
            indexText.text = index.ToString();
            effect.SetActive(false);
        }
    }

    public void InitState(int index, string country, string nickName, string score, bool checkMy)
    {
        if(index <= 3)
        {
            indexRankImg.enabled = true;
            indexRankImg.sprite = rankIconList[index - 1];
            effect.SetActive(true);
        }
        else
        {
            indexRankImg.enabled = false;
            indexText.text = index.ToString();
            effect.SetActive(false);
        }

        nickNameText.text = nickName;
        countryImg.sprite = Resources.Load<Sprite>("Country/" + country);
        scoreText.text = MoneyUnitString.ToCurrencyString((int.Parse(score)));


        if (index == 999)
        {
            indexText.text = "-";
        }

        frame.SetActive(checkMy);
    }

    public void TitleState(int number)
    {
        chefType = ChefType.Cook1_1 + number;

        classImg.sprite = GetAdvencementImg(chefType);

        if (chefType.ToString().Length == 7)
        {
            titleText.text = LocalizationManager.instance.GetString(chefType.ToString().Substring(0, 5)) + "  <color=#FFFF00>" +
chefType.ToString().Substring(6, 1) + "</color>";
        }
        else
        {
            titleText.text = LocalizationManager.instance.GetString(chefType.ToString().Substring(0, 6)) + "  <color=#FFFF00>" +
chefType.ToString().Substring(7, 1) + "</color>";
        }
    }

    public void IconState(IconType type)
    {
        iconImg.sprite = imageDataBase.GetIconArray(type);
    }

    public Sprite GetAdvencementImg(ChefType chefType)
    {
        switch (chefType)
        {
            case ChefType.Cook1_1:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook1_2:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook1_3:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook1_4:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook2_1:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook2_2:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook2_3:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook2_4:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook3_1:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook3_2:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook3_3:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook3_4:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook4_1:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook4_2:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook4_3:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook4_4:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook5_1:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook5_2:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook5_3:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook5_4:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook6_1:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook6_2:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook6_3:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook6_4:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook7_1:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook7_2:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook7_3:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook7_4:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook8_1:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook8_2:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook8_3:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook8_4:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook9_1:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook9_2:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook9_3:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook9_4:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook10_1:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook10_2:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook10_3:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook10_4:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook11_1:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook11_2:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook11_3:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook11_4:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook12_1:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook12_2:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook12_3:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook12_4:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook13_1:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook13_2:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook13_3:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook13_4:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook14_1:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook14_2:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook14_3:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook14_4:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook15_1:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook15_2:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook15_3:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook15_4:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
        }

        return sp;
    }
}
