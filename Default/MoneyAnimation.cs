﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyAnimation : MonoBehaviour
{
    public Text myMoneyText;

    public GameObject plusMoneyObj;
    public Text plusMoneyText;

    public Transform plusMoneyStartTransform;
    public Transform plusMoneyEndTransform;

    private int number = 0;

    [Space]
    [Title("Prefab")]
    public MoneyContent moneyPrefab;

    public RectTransform moneyTransform;

    List<MoneyContent> moneyPrefabList = new List<MoneyContent>();

    PlayerDataBase playerDataBase;

    WaitForSeconds waitForSeconds = new WaitForSeconds(2.0f);

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        for (int i = 0; i < 40; i++)
        {
            MoneyContent monster = Instantiate(moneyPrefab);
            monster.transform.SetParent(moneyTransform);
            monster.transform.localPosition = Vector3.zero;
            monster.transform.localScale = new Vector3(1, 1, 1);
            monster.gameObject.SetActive(false);
            moneyPrefabList.Add(monster);
        }

        plusMoneyObj.SetActive(false);
    }

    [Button]
    void PlusMoney()
    {
        PlayfabManager.instance.UpdateAddGold(1000);
    }

    [Button]
    void PlusCrystal()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 10);
    }

    public void PlusMoney(int target)
    {
        number = target;

        if(number >= 10000000)
        {
            number = 40;
        }
        else if (number >= 5000000)
        {
            number = 30;
        }
        else if (number >= 1000000)
        {
            number = 20;
        }
        else if(number >= 100000)
        {
            number = 15;
        }
        else
        {
            if(number >= 10)
            {
                number = 10;
            }
        }

        StopAllCoroutines();

        plusMoneyObj.SetActive(false);
        plusMoneyObj.SetActive(true);
        if (plusMoneyText != null)
        {
            plusMoneyText.text = "+" + MoneyUnitString.ToCurrencyString(target);
        }

        StartCoroutine(PlusMoneyCoroution(target));
    }

    IEnumerator PlusMoneyCoroution(int target)
    {
        for (int i = 0; i < number; i++)
        {
            moneyPrefabList[i].gameObject.SetActive(true);
            moneyPrefabList[i].GoToTarget(plusMoneyStartTransform.localPosition, plusMoneyEndTransform.localPosition);
        }

        yield return waitForSeconds;

        plusMoneyObj.gameObject.SetActive(false);
    }
}