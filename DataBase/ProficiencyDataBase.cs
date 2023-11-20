using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProficiencyInfo
{
    public int level = 0;
    public int needExp = 0;
}


[CreateAssetMenu(fileName = "ProficiencyDataBase", menuName = "ScriptableObjects/ProficiencyDataBase")]
public class ProficiencyDataBase : ScriptableObject
{


}
