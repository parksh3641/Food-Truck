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

    private float reward = 0.1f; //판매 수익
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

        for (int i = 0; i < System.Enum.GetValues(typeof(BookType)).Length; i++)
        {
            BookContent monster = Instantiate(bookPrefab);
            monster.transform.SetParent(bookContentTransform[0]);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;

            monster.gameObject.SetActive(true);

            bookNormalContentList.Add(monster);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(BookType)).Length; i++)
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
                bookNormalContentList[i].Initialize(BookType.Food1 + i, 0);
            }

            titleText.text = LocalizationManager.instance.GetString("Book") + " ( " + playerDataBase.GetNormalBookNumber() + " / " + System.Enum.GetValues(typeof(BookType)).Length + " )"
    + "\n<size=11>" + LocalizationManager.instance.GetString("Collect") + " : " + (((playerDataBase.GetNormalBookNumber() * 1.0f) / System.Enum.GetValues(typeof(BookType)).Length) * 100f).ToString("N1") + "%</size>";

            plusText.text = LocalizationManager.instance.GetString("NowPrice") + " +" + (playerDataBase.GetNormalBookNumber() * reward).ToString() + "%  (+" + reward + "%)";

            CheckNormalInitialize();
        }
        else
        {
            for (int i = 0; i < bookEpicContentList.Count; i++)
            {
                bookEpicContentList[i].Initialize(BookType.Food1 + i, 1);
            }

            titleText.text = LocalizationManager.instance.GetString("Book") + " ( " + playerDataBase.GetEpicBookNumber() + " / " + System.Enum.GetValues(typeof(BookType)).Length + " )"
+ "\n<size=11>" + LocalizationManager.instance.GetString("Collect") + " : " + (((playerDataBase.GetEpicBookNumber() * 1.0f) / System.Enum.GetValues(typeof(BookType)).Length) * 100f).ToString("N1") + "%</size>";

            plusText.text = LocalizationManager.instance.GetString("SuccessX2Percent") + " +" + (playerDataBase.GetEpicBookNumber() * reward2).ToString() + "%  (+" + reward2 + "%)";

            CheckEpicInitialize();
        }
    }

    void CheckNormalInitialize()
    {
        number = 0;
        if(playerDataBase.Food1MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Food2MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Food3MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Food4MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Food5MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Food6MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Food7MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy1MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy2MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy3MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy4MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy5MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy6MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy7MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy8MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy9MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood1MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood2MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood3MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood4MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood5MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood6MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood7MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert1MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert2MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert3MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert4MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert5MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert6MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert7MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert8MaxValue > 0)
        {
            NormalUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert9MaxValue > 0)
        {
            NormalUnLocked(number);
        }
    }

    void NormalUnLocked(int number)
    {
        bookNormalContentList[number].UnLock();
    }

    void CheckEpicInitialize()
    {
        number = 0;
        if (playerDataBase.island1RareData.index1 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island1RareData.index2 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island1RareData.index3 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island1RareData.index4 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island1RareData.index5 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island1RareData.index6 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island1RareData.index7 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island2RareData.index1 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island2RareData.index2 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island2RareData.index3 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island2RareData.index4 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island2RareData.index5 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island2RareData.index6 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island2RareData.index7 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island2RareData.index8 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island2RareData.index9 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island3RareData.index1 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island3RareData.index2 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island3RareData.index3 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island3RareData.index4 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island3RareData.index5 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island3RareData.index6 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island3RareData.index7 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island4RareData.index1 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island4RareData.index2 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island4RareData.index3 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island4RareData.index4 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island4RareData.index5 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island4RareData.index6 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island4RareData.index7 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island4RareData.index8 > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.island4RareData.index9 > 0)
        {
            EpicUnLocked(number);
        }
    }

    void EpicUnLocked(int number)
    {
        bookEpicContentList[number].UnLock();
    }
}
