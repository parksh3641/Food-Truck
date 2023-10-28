using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AnimalInfo
{
    public AnimalType animalType = AnimalType.Colobus;
    public int price = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.DefDestroyPercentUp;
    public float effectNumber = 0;
}


[CreateAssetMenu(fileName = "AnimalDataBase", menuName = "ScriptableObjects/AnimalDataBase")]
public class AnimalDataBase : ScriptableObject
{
    public List<AnimalInfo> animalInfoList = new List<AnimalInfo>();

    public AnimalInfo GetAnimalInfo(AnimalType type)
    {
        AnimalInfo animal = new AnimalInfo();

        for (int i = 0; i < animalInfoList.Count; i++)
        {
            if (animalInfoList[i].animalType.Equals(type))
            {
                animal = animalInfoList[i];
                break;
            }
        }

        return animal;
    }

    public float GetAnimalEffect(AnimalType type)
    {
        float number = 0;

        for (int i = 0; i < animalInfoList.Count; i++)
        {
            if (animalInfoList[i].animalType.Equals(type))
            {
                number = animalInfoList[i].effectNumber;
                break;
            }
        }

        return number;
    }
}
