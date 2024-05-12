using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AnimalInfo
{
    public AnimalType animalType = AnimalType.Animal1;
    public long price = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.DefDestroyPercentUp;
    public float effectNumber = 0;
}


[CreateAssetMenu(fileName = "AnimalDataBase", menuName = "ScriptableObjects/AnimalDataBase")]
public class AnimalDataBase : ScriptableObject
{
    public List<AnimalInfo> animalInfoList = new List<AnimalInfo>();

    [Space]
    public PassiveEffect retentionEffect = PassiveEffect.None;
    public float retentionValue = 0f;
    public int[] retentionPrice = new int[10];

    AnimalInfo info = new AnimalInfo();

    public AnimalInfo GetAnimalInfo(AnimalType type)
    {
        for (int i = 0; i < animalInfoList.Count; i++)
        {
            if (animalInfoList[i].animalType.Equals(type))
            {
                info = animalInfoList[i];
                break;
            }
        }

        return info;
    }

    public float GetAnimalEffect(int number)
    {
        if (animalInfoList[number] == null)
        {
            number = 0;
        }

        return animalInfoList[number].effectNumber;
    }

    public int GetRetentionPrice(int number)
    {
        return retentionPrice[number];
    }
}
