using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInfo
{
    public CharacterType characterType = CharacterType.Character1;
    public int price = 0;
    public int crystal = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.UpgradePercentUp;
    public float effectNumber = 0;
}

[CreateAssetMenu(fileName = "CharacterDataBase", menuName = "ScriptableObjects/CharacterDataBase")]
public class CharacterDataBase : ScriptableObject
{
    public List<CharacterInfo> characterInfoList = new List<CharacterInfo>();

    [Space]
    public PassiveEffect retentionEffect = PassiveEffect.None;
    public float retentionValue = 0f;
    public int retentionPrice = 0;

    public CharacterInfo GetCharacterInfo(CharacterType type)
    {
        CharacterInfo character = new CharacterInfo();

        for (int i = 0; i < characterInfoList.Count; i++)
        {
            if (characterInfoList[i].characterType.Equals(type))
            {
                character = characterInfoList[i];
                break;
            }
        }

        return character;
    }

    public float GetCharacterEffect(int number)
    {
        if (characterInfoList[number] == null)
        {
            number = 0;
        }

        return characterInfoList[number].effectNumber;
    }

    public int GetRetentionPrice(int number)
    {
        return retentionPrice * (number + 1);
    }
}
