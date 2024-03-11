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


    public BookContent bookPrefab;
    public List<BookContent> bookContentList = new List<BookContent>();

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

            bookContentList.Add(monster);
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

            Initialize();

            FirebaseAnalytics.LogEvent("Open_Book");
        }
        else
        {
            bookView.SetActive(false);
        }
    }

    void Initialize()
    {

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
    }
}
