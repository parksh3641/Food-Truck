using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankContent : MonoBehaviour
{
    ChefType chefType;

    public GameObject frame;

    public Image iconImg;
    public Image banner;
    public Text indexText;
    public Image indexRankImg;
    public Sprite[] rankIconList;
    public Image countryImg;
    public Text nickNameText;
    public Text titleText;
    public Text scoreText;

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
        }
        else
        {
            indexRankImg.enabled = false;
            indexText.text = index.ToString();
        }
    }

    public void InitState(int index, string country, string nickName, string score, bool checkMy)
    {
        if(index <= 3)
        {
            indexRankImg.enabled = true;
            indexRankImg.sprite = rankIconList[index - 1];
        }
        else
        {
            indexRankImg.enabled = false;
            indexText.text = index.ToString();
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

        if(chefType.ToString().Length == 7)
        {
            titleText.text = LocalizationManager.instance.GetString(chefType.ToString().Substring(0, 5)) + " <color=#FF0000>" +
chefType.ToString().Substring(6, 1) + "</color>";
        }
        else
        {
            titleText.text = LocalizationManager.instance.GetString(chefType.ToString().Substring(0, 6)) + " <color=#FF0000>" +
chefType.ToString().Substring(7, 1) + "</color>";
        }
    }

    public void IconState(IconType type)
    {
        iconImg.sprite = imageDataBase.GetIconArray(type);
    }
}
