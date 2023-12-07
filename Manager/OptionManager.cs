using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject optionView;
    public GameObject languageView;
    public Text versionText;

    public Sprite[] buttonImgArray;

    [Title("Music")]
    public Image musicButtonImg;
    public LocalizationContent musicText;

    [Space]
    [Title("Sfx")]
    public Image sfxButtonImg;
    public LocalizationContent sfxText;

    [Space]
    [Title("Vibration")]
    public Image vibrationButtonImg;
    public LocalizationContent vibrationText;

    [Space]
    [Title("Effect")]
    public Image effectButtonImg;
    public LocalizationContent effectText;

    [Space]
    public SoundManager soundManager;

    private void Awake()
    {
        optionView.SetActive(false);
        languageView.SetActive(false);

        versionText.text = "v" + Application.version + "  <size=10>(2023/12/03)</size>";
    }

    private void Start()
    {
        GameStateManager.instance.Pause = false;
    }

    public void OpenOptionView()
    {
        if (!optionView.activeInHierarchy)
        {
            optionView.SetActive(true);

            OnBGM();
            OnSFX();
            OnVibration();
            OnEffect();

            GameStateManager.instance.Pause = true;
        }
        else
        {
            optionView.SetActive(false);

            GameStateManager.instance.Pause = false;
        }
    }

    public void OpenLanguageView()
    {
        if(!languageView.activeInHierarchy)
        {
            languageView.SetActive(true);
        }
        else
        {
            languageView.SetActive(false);
        }
    }

    public void MusicOnOff()
    {
        if (GameStateManager.instance.Music)
        {
            GameStateManager.instance.Music = false;
        }
        else
        {
            GameStateManager.instance.Music = true;
        }

        OnBGM();
    }

    public void SfxOnOff()
    {
        if (GameStateManager.instance.Sfx)
        {
            GameStateManager.instance.Sfx = false;
        }
        else
        {
            GameStateManager.instance.Sfx = true;
        }

        OnSFX();
    }

    public void VibrationOnOff()
    {
        if (GameStateManager.instance.Vibration)
        {
            GameStateManager.instance.Vibration = false;
        }
        else
        {
            GameStateManager.instance.Vibration = true;
        }

        OnVibration();
    }

    public void EffectOnOff()
    {
        if (GameStateManager.instance.Effect)
        {
            GameStateManager.instance.Effect = false;
        }
        else
        {
            GameStateManager.instance.Effect = true;
        }

        OnEffect();
    }

    public void OnBGM()
    {
        if (GameStateManager.instance.Music)
        {
            musicText.localizationName = "ON";
            musicText.ReLoad();

            musicButtonImg.sprite = buttonImgArray[0];

            soundManager.PlayBGM();
        }
        else
        {
            musicText.localizationName = "OFF";
            musicText.ReLoad();

            musicButtonImg.sprite = buttonImgArray[1];

            soundManager.StopBGM();
        }
    }

    public void OnSFX()
    {
        if (GameStateManager.instance.Sfx)
        {
            sfxText.localizationName = "ON";
            sfxText.ReLoad();

            sfxButtonImg.sprite = buttonImgArray[0];
        }
        else
        {
            sfxText.localizationName = "OFF";
            sfxText.ReLoad();

            sfxButtonImg.sprite = buttonImgArray[1];
        }
    }

    public void OnVibration()
    {
        if (GameStateManager.instance.Vibration)
        {
            vibrationText.localizationName = "ON";
            vibrationText.ReLoad();

            vibrationButtonImg.sprite = buttonImgArray[0];
        }
        else
        {
            vibrationText.localizationName = "OFF";
            vibrationText.ReLoad();

            vibrationButtonImg.sprite = buttonImgArray[1];
        }
    }

    public void OnEffect()
    {
        if (GameStateManager.instance.Effect)
        {
            effectText.localizationName = "ON";
            effectText.ReLoad();

            effectButtonImg.sprite = buttonImgArray[0];
        }
        else
        {
            effectText.localizationName = "OFF";
            effectText.ReLoad();

            effectButtonImg.sprite = buttonImgArray[1];
        }
    }
}
