using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizationContent : MonoBehaviour
{
    public bool purchase = false;

    private Text localizationText;
    public string localizationName = "";
    public string plusText = "";


    private void Awake()
    {
        localizationText = GetComponent<Text>();
    }

    private void Start()
    {
        ReLoad();

        if (LocalizationManager.instance != null) LocalizationManager.instance.AddContent(this);
    }

    public void ReLoad()
    {
        if (localizationName.Length > 0)
        {
            localizationText.text = "";

            if (!purchase)
            {
                localizationText.text = LocalizationManager.instance.GetString(localizationName);
            }
            else
            {
#if UNITY_IOS || UNITY_EDITOR_OSX
                localizationText.text += LocalizationManager.instance.GetString(localizationName + "_IOS");
#else
                localizationText.text += LocalizationManager.instance.GetString(localizationName + "_AOS");
#endif
            }

            if (plusText.Length > 0)
            {
                localizationText.text += plusText;
            }
        }
    }
}