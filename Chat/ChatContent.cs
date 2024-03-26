using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatContent : MonoBehaviour
{
    public Text chatText;

    public void Initialize(string title)
    {
        chatText.text = title;
    }
}
