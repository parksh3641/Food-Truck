using Firebase.Analytics;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookContent : MonoBehaviour
{
    public BookType bookType;

    private int index = 0;

    public Image icon;

    private bool isStart = false;

    public GameObject lockedObj;

    ImageDataBase imageDataBase;

    private void Awake()
    {
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        lockedObj.SetActive(true);
    }

    public void Initialize(BookType type, int number)
    {
        bookType = type;

        index = number;

        icon.sprite = imageDataBase.GetFoodIconType(type);

        if(!isStart)
        {
            isStart = true;

            if (number == 1)
            {
                icon.color = new Color(Random.Range(0, 200f) / 255f, Random.Range(0, 200f) / 255f, Random.Range(0, 200f) / 255f);
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
}
