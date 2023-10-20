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

    [Title("Music")]
    public LocalizationContent musicText;

    [Space]
    [Title("Sfx")]
    public LocalizationContent sfxText;

    [Space]
    [Title("Vibration")]
    public LocalizationContent vibrationText;

    [Space]
    [Title("Effect")]
    public LocalizationContent effectText;

    [Space]
    public SoundManager soundManager;

    private void Awake()
    {
        optionView.SetActive(false);
        languageView.SetActive(false);

        versionText.text = "v" + Application.version;
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
        }
        else
        {
            optionView.SetActive(false);
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

            soundManager.PlayBGM();
        }
        else
        {
            musicText.localizationName = "OFF";
            musicText.ReLoad();

            soundManager.StopBGM();
        }
    }

    public void OnSFX()
    {
        if (GameStateManager.instance.Sfx)
        {
            sfxText.localizationName = "ON";
            sfxText.ReLoad();
        }
        else
        {
            sfxText.localizationName = "OFF";
            sfxText.ReLoad();
        }
    }

    public void OnVibration()
    {
        if (GameStateManager.instance.Vibration)
        {
            vibrationText.localizationName = "ON";
            vibrationText.ReLoad();
        }
        else
        {
            vibrationText.localizationName = "OFF";
            vibrationText.ReLoad();
        }
    }

    public void OnEffect()
    {
        if (GameStateManager.instance.Effect)
        {
            effectText.localizationName = "ON";
            effectText.ReLoad();
        }
        else
        {
            effectText.localizationName = "OFF";
            effectText.ReLoad();
        }
    }
}
