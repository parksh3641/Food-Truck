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

            CheckNormalInitialize();
        }
        else
        {
            for (int i = 0; i < bookEpicContentList.Count; i++)
            {
                bookEpicContentList[i].Initialize(BookType.Food1 + i, 1);
            }

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
        if (playerDataBase.Food1Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Food2Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Food3Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Food4Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Food5Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Food6Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Food7Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy1Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy2Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy3Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy4Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy5Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy6Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy7Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy8Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Candy9Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood1Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood2Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood3Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood4Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood5Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood6Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.JapaneseFood7Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert1Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert2Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert3Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert4Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert5Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert6Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert7Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert8Rare > 0)
        {
            EpicUnLocked(number);
        }

        number++;
        if (playerDataBase.Dessert9Rare > 0)
        {
            EpicUnLocked(number);
        }
    }

    void EpicUnLocked(int number)
    {
        bookEpicContentList[number].UnLock();
    }
}
