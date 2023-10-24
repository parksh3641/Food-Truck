using Sirenix.OdinInspector;
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

    private int gold = 0;
    private int max = 0;

    [Space]
    [Title("Prefab")]
    public MoneyContent moneyPrefab;

    public RectTransform moneyTransform;

    List<MoneyContent> moneyPrefabList = new List<MoneyContent>();

    PlayerDataBase playerDataBase;

    WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
    WaitForSeconds waitForSeconds2 = new WaitForSeconds(0.03f);

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        for (int i = 0; i < 10; i++)
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
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 1000);
    }

    public void PlusMoney(int target)
    {
        StopAllCoroutines();

        //gold = playerDataBase.Coin;
        //myMoneyText.text = MoneyUnitString.ToCurrencyString(gold);

        plusMoneyObj.SetActive(false);
        plusMoneyObj.SetActive(true);
        plusMoneyText.text = "+" + MoneyUnitString.ToCurrencyString(target);

        StartCoroutine(PlusMoneyCoroution(target));
    }

    IEnumerator PlusMoneyCoroution(int target)
    {
        for (int i = 0; i < moneyPrefabList.Count; i++)
        {
            moneyPrefabList[i].gameObject.SetActive(true);
            moneyPrefabList[i].GoToTarget(plusMoneyStartTransform.localPosition, plusMoneyEndTransform.localPosition);
        }

        yield return new WaitForSeconds(2.0f);

        //max = 0;

        //while (max < target)
        //{
        //    if (max + 100000000 < target)
        //    {
        //        max += 100000000;
        //    }
        //    else
        //    {
        //        if (max + 10000000 < target)
        //        {
        //            max += 10000000;
        //        }
        //        else
        //        {
        //            if (max + 1000000 < target)
        //            {
        //                max += 1000000;
        //            }
        //            else
        //            {
        //                if (max + 100000 < target)
        //                {
        //                    max += 100000;
        //                }
        //                else
        //                {
        //                    if (max + 10000 < target)
        //                    {
        //                        max += 10000;
        //                    }
        //                    else
        //                    {
        //                        if (max + 10000 < target)
        //                        {
        //                            max += 10000;
        //                        }
        //                        else
        //                        {
        //                            if (max + 1000 < target)
        //                            {
        //                                max += 1000;
        //                            }
        //                            else
        //                            {
        //                                if (max + 100 < target)
        //                                {
        //                                    max += 100;
        //                                }
        //                                else
        //                                {
        //                                    if (max + 10 < target)
        //                                    {
        //                                        max += 10;
        //                                    }
        //                                    else
        //                                    {
        //                                        max += 1;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    myMoneyText.text = MoneyUnitString.ToCurrencyString(gold + max);

        //    yield return waitForSeconds2;
        //}

        //myMoneyText.text = MoneyUnitString.ToCurrencyString(gold + max);

        plusMoneyObj.gameObject.SetActive(false);
    }
}