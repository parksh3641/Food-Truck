using Firebase.Analytics;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    public GameObject bookView;

    public GameObject bookInfoView;

    public GameObject bookCamera;

    public GameObject alarm;

    private int index = -1;

    [Space]
    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;

    public Text titleText;
    public Text infoText;

    public Text plusText;

    private float reward = 0.3f; //°­È­ È®·ü
    private float reward2 = 0.5f; //ÆÄ±« ¹æ¾î È®·ü

    [Space]
    [Title("ScrollView")]
    public GameObject[] bookArray;

    public RectTransform[] bookContentTransform;

    private int number = 0;

    private int zoomNumber = 0;
    private bool zoom = false;

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
        bookInfoView.SetActive(false);

        alarm.SetActive(true);

        bookContentTransform[0].anchoredPosition = new Vector2(0, -9999);
        bookContentTransform[1].anchoredPosition = new Vector2(0, -9999);

        bookCamera.SetActive(false);

        bookCamera.transform.position = new Vector3(0, 0.88f, 2.5f);
    }

    public void OpenBook()
    {
        if (!bookView.activeSelf)
        {
            bookView.SetActive(true);

            alarm.SetActive(false);

            bookCamera.SetActive(true);

            if (index == -1)
            {
                ChangeTopToggle(0);
            }

            CheckFood(FoodType.Food1, 0);

            FirebaseAnalytics.LogEvent("Open_Book");
        }
        else
        {
            bookView.SetActive(false);

            bookCamera.SetActive(false);

            GameManager.instance.ChangeFood(GameStateManager.instance.FoodType);
        }
    }

    public void OpenBookInfo()
    {
        if (!bookInfoView.activeSelf)
        {
            bookInfoView.SetActive(true);

            FirebaseAnalytics.LogEvent("Open_BookInfo");
        }
        else
        {
            bookInfoView.SetActive(false);
        }
    }

    public void ChangeTopToggle(int number)
    {
        if(number == 2)
        {
            NotionManager.instance.UseNotion(NotionType.ComingSoon);
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            return;
        }

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
                bookNormalContentList[i].Initialize(FoodType.Food1 + i, 0, this);
            }

            titleText.text = LocalizationManager.instance.GetString("Book") + " ( " + playerDataBase.GetNormalBookNumber() + " / " + System.Enum.GetValues(typeof(FoodType)).Length + " )"
    + "\n<size=11>" + LocalizationManager.instance.GetString("Collect") + " : " + (((playerDataBase.GetNormalBookNumber() * 1.0f) / System.Enum.GetValues(typeof(FoodType)).Length) * 100f).ToString("N1") + "%</size>";

            plusText.text = LocalizationManager.instance.GetString("SuccessPercent") + " +" + (playerDataBase.GetNormalBookNumber() * reward).ToString() + "%  (+" + reward + "%)";

            CheckNormalInitialize();
        }
        else
        {
            for (int i = 0; i < bookEpicContentList.Count; i++)
            {
                bookEpicContentList[i].Initialize(FoodType.Food1 + i, 1, this);
            }

            titleText.text = LocalizationManager.instance.GetString("Book") + " ( " + playerDataBase.GetEpicBookNumber() + " / " + System.Enum.GetValues(typeof(FoodType)).Length + " )"
+ "\n<size=11>" + LocalizationManager.instance.GetString("Collect") + " : " + (((playerDataBase.GetEpicBookNumber() * 1.0f) / System.Enum.GetValues(typeof(FoodType)).Length) * 100f).ToString("N1") + "%</size>";

            plusText.text = LocalizationManager.instance.GetString("DefDestroyPercent") + " +" + (playerDataBase.GetEpicBookNumber() * reward2).ToString() + "%  (+" + reward2 + "%)";

            CheckEpicInitialize();
        }
    }

    void CheckNormalInitialize()
    {
        number = 0;

        for (int i = 0; i < System.Enum.GetValues(typeof(IslandType)).Length; i++)
        {
            for(int j = 0; j < GameStateManager.instance.Island; j ++)
            {
                if (playerDataBase.island_Total_Data.island_Max_Datas[i].GetValue(FoodType.Food1 + (i * GameStateManager.instance.Island) + j) > 0 ||
                    playerDataBase.NextFoodNumber >= i)
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
            for (int j = 0; j < GameStateManager.instance.Island; j++)
            {
                if (playerDataBase.island_Total_Data.island_Rare_Datas[i].GetValue(FoodType.Food1 + (i * GameStateManager.instance.Island) + j) > 0)
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

    public void CheckFood(FoodType type, int index)
    {
        GameManager.instance.ChangeFood_Book(type, index);

        infoText.text = LocalizationManager.instance.GetString(type.ToString() + "_Info");
    }

    public void Zoom()
    {
        if (zoomNumber == 0)
        {
            if(!zoom)
            {
                zoom = true;

                bookCamera.transform.position = new Vector3(0, 0.88f, 2.7f);
                bookCamera.transform.rotation = Quaternion.identity;
            }
            else
            {
                zoom = false;

                bookCamera.transform.position = new Vector3(0, 0.88f, 2.5f);
                bookCamera.transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            if (!zoom)
            {
                zoom = true;

                bookCamera.transform.position = new Vector3(0, 1.25f, 2.7f);
                bookCamera.transform.rotation = Quaternion.Euler(40, 0, 0);
            }
            else
            {
                zoom = false;

                bookCamera.transform.position = new Vector3(0, 1.45f, 2.5f);
                bookCamera.transform.rotation = Quaternion.Euler(40, 0, 0);
            }
        }
    }

    public void ChangeCamera(int number)
    {
        zoomNumber = number;

        if(zoomNumber == 0)
        {
            if(!zoom)
            {
                bookCamera.transform.position = new Vector3(0, 0.88f, 2.5f);
                bookCamera.transform.rotation = Quaternion.identity;
            }
            else
            {
                bookCamera.transform.position = new Vector3(0, 0.88f, 2.7f);
                bookCamera.transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            if (!zoom)
            {
                bookCamera.transform.position = new Vector3(0, 1.45f, 2.5f);
                bookCamera.transform.rotation = Quaternion.Euler(40, 0, 0);
            }
            else
            {
                bookCamera.transform.position = new Vector3(0, 1.25f, 2.7f);
                bookCamera.transform.rotation = Quaternion.Euler(40, 0, 0);
            }
        }
    }

    public void Information()
    {
        if (infoText.enabled)
        {
            infoText.enabled = false;
        }
        else
        {
            infoText.enabled = true;
        }
    }
}
