using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReincarnationManager : MonoBehaviour
{
    public GameObject reincarnationView;

    public Text crystalText;
    public Text countText;
    public Text adText;

    private float crystal = 0;


    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        reincarnationView.SetActive(false);


    }

    public void OpenReincarnationView()
    {
        if(!reincarnationView.activeInHierarchy)
        {
            reincarnationView.SetActive(true);

            Initialize();
        }
        else
        {
            reincarnationView.SetActive(false);
        }
    }

    void Initialize()
    {
        crystal = 0;

        if (playerDataBase.FriesMaxValue > 0)
        {
            crystal += 300;
        }

        if(playerDataBase.Candy1MaxValue > 0)
        {
            crystal += 10;
        }

        if (playerDataBase.Candy2MaxValue > 0)
        {
            crystal += 20;
        }

        if (playerDataBase.Candy3MaxValue > 0)
        {
            crystal += 30;
        }

        if (playerDataBase.Candy4MaxValue > 0)
        {
            crystal += 50;
        }

        if (playerDataBase.Candy5MaxValue > 0)
        {
            crystal += 70;
        }

        if (playerDataBase.Candy6MaxValue > 0)
        {
            crystal += 100;
        }

        if (playerDataBase.Candy7MaxValue > 0)
        {
            crystal += 150;
        }

        if (playerDataBase.Candy8MaxValue > 0)
        {
            crystal += 200;
        }

        if (playerDataBase.Candy9MaxValue > 0)
        {
            crystal += 300;
        }

        crystal = crystal + (crystal * (0.01f * (playerDataBase.Skill11 * 1)));

        crystalText.text = MoneyUnitString.ToCurrencyString((int)crystal).ToString();

        countText.text = LocalizationManager.instance.GetString("Reincarnation_Count") + " : " + playerDataBase.ReincarnationCount;

        adText.text = LocalizationManager.instance.GetString("Reincarnation_Ad") + "\n+" + MoneyUnitString.ToCurrencyString((int)crystal).ToString();
    }

    public void Free()
    {

    }

    public void Ad()
    {

    }


}
