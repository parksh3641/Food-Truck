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

    public float GetCharacterEffect(CharacterType type)
    {
        float number = 0;

        for (int i = 0; i < characterInfoList.Count; i++)
        {
            if (characterInfoList[i].characterType.Equals(type))
            {
                number = characterInfoList[i].effectNumber;
                break;
            }
        }

        return number;
    }
}
