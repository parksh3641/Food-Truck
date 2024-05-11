using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EquipContent : MonoBehaviour
{
    public int index = 0;

    public EquipInfo equipInfo = new EquipInfo();

    public Image icon;
    public Image background;

    public Text titleText;
    public Text infoText;
    public Text coinText;

    public GameObject lockedObj;
    public Text lockedText;

    private int coin = 0;
    private float rankUp = 0;
    private bool isDelay = false;
    private bool first = false;

    string str = "";

    private List<int> rangeNumbers = new List<int>();
    private List<int> numbers = new List<int>();
    private List<int> extractedNumbers = new List<int>();

    private Dictionary<string, string> playerData = new Dictionary<string, string>();

    EquipmentManager equipmentManager;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;
    EquipDataBase equipDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (equipDataBase == null) equipDataBase = Resources.Load("EquipDataBase") as EquipDataBase;

        lockedObj.SetActive(false);
    }


    public void Initialize(int number, EquipmentManager manager)
    {
        index = number;
        equipmentManager = manager;

        lockedObj.SetActive(true);

        switch(number)
        {
            case 0:
                lockedObj.SetActive(false);
                break;
            case 1:
                if(playerDataBase.Level >= 10)
                {
                    lockedObj.SetActive(false);
                }
                lockedText.text = "Lv. 10";
                break;
            case 2:
                if (playerDataBase.Level >= 20)
                {
                    lockedObj.SetActive(false);
                }
                lockedText.text = "Lv. 20";
                break;
            case 3:
                if (playerDataBase.Level >= 30)
                {
                    lockedObj.SetActive(false);
                }
                lockedText.text = "Lv. 30";
                break;
            case 4:
                if (playerDataBase.Level >= 40)
                {
                    lockedObj.SetActive(false);
                }
                lockedText.text = "Lv. 40";
                break;
            case 5:
                if (playerDataBase.Level >= 50)
                {
                    lockedObj.SetActive(false);
                }
                lockedText.text = "Lv. 50";
                break;
            case 6:
                if (playerDataBase.Level >= 75)
                {
                    lockedObj.SetActive(false);
                }
                lockedText.text = "Lv. 75";
                break;
            case 7:
                if (playerDataBase.Level >= 100)
                {
                    lockedObj.SetActive(false);
                }
                lockedText.text = "Lv. 100";
                break;
            case 8:
                if (playerDataBase.Level >= 125)
                {
                    lockedObj.SetActive(false);
                }
                lockedText.text = "Lv. 125";
                break;
            case 9:
                if (playerDataBase.Level >= 150)
                {
                    lockedObj.SetActive(false);
                }
                lockedText.text = "Lv. 150";
                break;
        }

        if(!first)
        {
            first = true;
            equipInfo = playerDataBase.equip.equipInfos[index];
        }

        icon.sprite = imageDataBase.GetEquipArray(index);

        background.sprite = imageDataBase.GetRankBackgroundArray(equipInfo.rank);

        titleText.text = LocalizationManager.instance.GetString("Equip_" + (index + 1));

        infoText.text = GetAbility(equipInfo.option1, equipInfo.option1_Value);

        if(equipInfo.option2 > 0)
        {
            infoText.text += "\n" + GetAbility(equipInfo.option2, equipInfo.option2_Value);
        }

        if (equipInfo.option3 > 0)
        {
            infoText.text += "\n" + GetAbility(equipInfo.option3, equipInfo.option3_Value);
        }

        if (equipInfo.option4 > 0)
        {
            infoText.text += "\n" + GetAbility(equipInfo.option4, equipInfo.option4_Value);
        }

        switch(equipInfo.rank)
        {
            case 0:
                coin = 1000000;
                break;
            case 1:
                coin = 5000000;
                break;
            case 2:
                coin = 10000000;
                break;
            case 3:
                coin = 50000000;
                break;
        }

        rankUp = equipDataBase.GetRankUpPercent(equipInfo.rank);

        coinText.text = MoneyUnitString.ToCurrencyString(coin);
    }

    public string GetAbility(int index, float value)
    {
        str = "";

        if(index == 0)
        {
            str = LocalizationManager.instance.GetString("None");
        }
        else
        {     
            str = LocalizationManager.instance.GetString("Equip_Index_" + index)
                + equipDataBase.CheckValue(EquipType.Equip_Index_1 + index - 1, RankType.Normal + equipInfo.rank, value) + "  +" + value + "%</color>";
        }

        return str;
    }

    public void ChangeOptions()
    {
        if (isDelay) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (playerDataBase.Coin < coin)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion2(NotionType.LowCoin);

            ShopManager.instance.LowCoin(coin);
            return;
        }

        PlayfabManager.instance.UpdateSubtractGold(coin);

        RandomOption();

        playerDataBase.EquipCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("EquipCount", playerDataBase.EquipCount);

        SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
        NotionManager.instance.UseNotion2(NotionType.ChangeOptionNotion);

        isDelay = true;
        Invoke("Delay", 0.5f);
    }

    void Delay()
    {
        isDelay = false;
    }

    void RandomOption()
    {
        if (equipInfo.rank < 3)
        {
            if (Random.Range(0f, 100f) >= 100f - rankUp) //·©Å© ¾÷ È®·ü
            {
                equipInfo.rank += 1;

                equipmentManager.LevelIUpAnimation();

                SoundManager.instance.PlaySFX(GameSfxType.Upgrade5);
                NotionManager.instance.UseNotion3(NotionType.LevelUpOption);
            }
        }

        rangeNumbers = new List<int>();
        rangeNumbers = ExtractNumbers(1, System.Enum.GetValues(typeof(EquipType)).Length, equipInfo.rank + 1);

        FirebaseAnalytics.LogEvent("Change_Options : " + equipInfo.rank);

        if (rangeNumbers.Count >= 1)
        {
            equipInfo.option1 = rangeNumbers[0];
            equipInfo.option1_Value = equipDataBase.GetRange(EquipType.Equip_Index_1 + equipInfo.option1 - 1, RankType.Normal + equipInfo.rank);
        }

        if(rangeNumbers.Count >= 2)
        {
            equipInfo.option2 = rangeNumbers[1];
            equipInfo.option2_Value = equipDataBase.GetRange(EquipType.Equip_Index_1 + equipInfo.option2 - 1, RankType.Normal + equipInfo.rank);
        }

        if(rangeNumbers.Count >= 3)
        {
            equipInfo.option3 = rangeNumbers[2];
            equipInfo.option3_Value = equipDataBase.GetRange(EquipType.Equip_Index_1 + equipInfo.option3 - 1, RankType.Normal + equipInfo.rank);
        }

        if(rangeNumbers.Count >= 4)
        {
            equipInfo.option4 = rangeNumbers[3];
            equipInfo.option4_Value = equipDataBase.GetRange(EquipType.Equip_Index_1 + equipInfo.option4 - 1, RankType.Normal + equipInfo.rank);
        }

        SaveOption();

        Initialize(index, equipmentManager);
    }

    private List<int> ExtractNumbers(int start, int end, int count)
    {
        numbers = new List<int>();
        extractedNumbers = new List<int>();

        if (count > (end - start) + 1)
        {
            count = (end - start) + 1;
        }

        for (int i = start; i <= end; i++)
        {
            numbers.Add(i);
        }

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, numbers.Count);
            extractedNumbers.Add(numbers[index]);
            numbers.RemoveAt(index);
        }

        return extractedNumbers;
    }

    private void SaveOption()
    {
        playerDataBase.equip.equipInfos[index] = equipInfo;

        playerData.Clear();
        playerData.Add("Equip", JsonUtility.ToJson(playerDataBase.equip));
        PlayfabManager.instance.SetPlayerData(playerData);
    }
}
