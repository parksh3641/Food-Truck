using Firebase.Analytics;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    public GameObject bookView;

    public GameObject alarm;

    private int index = -1;

    [Space]
    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;

    public Text titleText;

    public Text plusText;

    private float reward = 0.2f; //판매 수익
    private float reward2 = 0.2f; //2단 강화 확률

    [Space]
    [Title("ScrollView")]
    public GameObject[] bookArray;

    public RectTransform[] bookContentTransform;

    private int number = 0;

    public BookContent bookPrefab;
    public List<BookContent> bookNormalContentList = new List<BookContent>();
    public List<BookContent> bookEpicContentList = new List<BookContent>();

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        for (int i = 0; i < System.Enum.GetValues(typeof(FoodType)).Length; i++)
        {
            BookContent monster = Instantiate(bookPrefab);
            monster.transform.SetParent(bookContentTransform[0]);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;

            monster.gameObject.SetActive(true);

            bookNormalContentList.Add(monster);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(FoodType)).Length; i++)
        {
            BookContent monster = Instantiate(bookPrefab);
            monster.transform.SetParent(bookContentTransform[1]);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;

            monster.gameObject.SetActive(true);

            bookEpicContentList.Add(monster);
        }

        bookView.SetActive(false);

        alarm.SetActive(true);

        bookContentTransform[0].anchoredPosition = new Vector2(0, -9999);
        bookContentTransform[1].anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenBook()
    {
        if (!bookView.activeSelf)
        {
            bookView.SetActive(true);

            alarm.SetActive(false);

            if(index == -1)
            {
                ChangeTopToggle(0);
            }

            FirebaseAnalytics.LogEvent("Open_Book");
        }
        else
        {
            bookView.SetActive(false);
        }
    }

    public void ChangeTopToggle(int number)
    {
        if (index == number) return;

        index = number;

        for (int i = 0; i < topMenuImgArray.Length; i++)
        {
            topMenuImgArray[i].sprite = topMenuSpriteArray[0];
            bookArray[i].gameObject.SetActive(false);
        }

        topMenuImgArray[number].sprite = topMenuSpriteArray[1];
        bookArray[number].gameObject.SetActive(true);

        if(number == 0)
        {
            for(int i = 0; i < bookNormalContentList.Count; i ++)
            {
                bookNormalContentList[i].Initialize(FoodType.Food1 + i, 0);
            }

            titleText.text = LocalizationManager.instance.GetString("Book") + " ( " + playerDataBase.GetNormalBookNumber() + " / " + System.Enum.GetValues(typeof(FoodType)).Length + " )"
    + "\n<size=11>" + LocalizationManager.instance.GetString("Collect") + " : " + (((playerDataBase.GetNormalBookNumber() * 1.0f) / System.Enum.GetValues(typeof(FoodType)).Length) * 100f).ToString("N1") + "%</size>";

            plusText.text = LocalizationManager.instance.GetString("NowPrice") + " +" + (playerDataBase.GetNormalBookNumber() * reward).ToString() + "%  (+" + reward + "%)";

            CheckNormalInitialize();
        }
        else
        {
            for (int i = 0; i < bookEpicContentList.Count; i++)
            {
                bookEpicContentList[i].Initialize(FoodType.Food1 + i, 1);
            }

            titleText.text = LocalizationManager.instance.GetString("Book") + " ( " + playerDataBase.GetEpicBookNumber() + " / " + System.Enum.GetValues(typeof(FoodType)).Length + " )"
+ "\n<size=11>" + LocalizationManager.instance.GetString("Collect") + " : " + (((playerDataBase.GetEpicBookNumber() * 1.0f) / System.Enum.GetValues(typeof(FoodType)).Length) * 100f).ToString("N1") + "%</size>";

            plusText.text = LocalizationManager.instance.GetString("SuccessX2Percent") + " +" + (playerDataBase.GetEpicBookNumber() * reward2).ToString() + "%  (+" + reward2 + "%)";

            CheckEpicInitialize();
        }
    }

    void CheckNormalInitialize()
    {
        number = 0;

        for (int i = 0; i < System.Enum.GetValues(typeof(IslandType)).Length; i++)
        {
            for(int j = 0; j < 9; j ++)
            {
                if (playerDataBase.island_Total_Data.island_Max_Datas[i].GetValue(FoodType.Food1 + (i * 9) + j) > 0)
                {
                    NormalUnLocked(number);
                }

                number++;
            }
        }
    }

    void NormalUnLocked(int number)
    {
        bookNormalContentList[number].UnLock();
    }

    void CheckEpicInitialize()
    {
        number = 0;

        for (int i = 0; i < System.Enum.GetValues(typeof(IslandType)).Length; i ++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (playerDataBase.island_Total_Data.island_Rare_Datas[i].GetValue(FoodType.Food1 + (i * 9) + j) > 0)
                {
                    EpicUnLocked(number);
                }

                number++;
            }
        }
    }

    void EpicUnLocked(int number)
    {
        bookEpicContentList[number].UnLock();
    }
}
