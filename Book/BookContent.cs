using Firebase.Analytics;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookContent : MonoBehaviour
{
    public FoodType foodType = FoodType.Food1;

    private int index = 0;

    public Image icon;

    private bool isStart = false;

    public GameObject lockedObj;
    public GameObject checkMark;

    Color speicalColor = new Color(1, 50f / 255f, 1);

    BookManager bookManager;

    ImageDataBase imageDataBase;

    private void Awake()
    {
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        lockedObj.SetActive(true);
    }

    public void Initialize(FoodType type, int number, BookManager manager)
    {
        foodType = type;

        index = number;

        bookManager = manager;

        icon.sprite = imageDataBase.GetFoodIconType(type);

        if(!isStart)
        {
            isStart = true;

            if (number == 1)
            {
                icon.color = speicalColor;
            }
        }
    }

    public void UnLock()
    {
        lockedObj.SetActive(false);
    }

    public void OnClick()
    {
        if(index == 0)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.OpenFood);
        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.OpenRareFood);
        }
    }

    public void ChangeFood()
    {
        bookManager.CheckFood(foodType, index);
    }

    public void SetCheckMark(bool check)
    {
        checkMark.SetActive(check);
    }
}
