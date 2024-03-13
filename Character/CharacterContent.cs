using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContent : MonoBehaviour
{
    public int index = 0;

    public GameObject[] objArray;

    private void Awake()
    {
        for (int i = 0; i < objArray.Length; i++)
        {
            objArray[i].SetActive(false);
        }

        objArray[0].SetActive(true);
    }


    public void Initialize(int number)
    {
        for(int i = 0; i < objArray.Length; i ++)
        {
            objArray[i].SetActive(false);
        }

        objArray[number].SetActive(true);
    }
}
