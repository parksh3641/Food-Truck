using Firebase.Analytics;
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

    public GameObject[] checkMarkArray;

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
    [Title("Background Effect")]
    public Image backgroundEffectButtonImg;
    public LocalizationContent backgroundEffectText;

    [Space]
    [Title("Recover")]
    public Image recoverButtonImg;
    public LocalizationContent recoverText;

    public GameObject googleLink;
    public GameObject appleLink;


    private void Awake()
    {
        optionView.SetActive(false);
        languageView.SetActive(false);

        versionText.text = "v" + Application.version + "  <size=10>(2024/03/17)</size>";
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
            OnBackgroundEffect();
            OnRecover();

            googleLink.SetActive(false);
            appleLink.SetActive(false);

            if(GameStateManager.instance.Login == LoginType.Guest)
            {
#if UNITY_ANDROID
                googleLink.SetActive(true);
#elif UNITY_IOS
                appleLink.SetActive(true);
#endif
            }
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

            for(int i = 0; i < checkMarkArray.Length; i ++)
            {
                checkMarkArray[i].SetActive(false);
            }

            checkMarkArray[(int)GameStateManager.instance.Language - 1].SetActive(true);

            FirebaseAnalytics.LogEvent("Open_Language");
        }
        else
        {
            languageView.SetActive(false);
        }
    }

    public void ChangeLanguage()
    {
        for (int i = 0; i < checkMarkArray.Length; i++)
        {
            checkMarkArray[i].SetActive(false);
        }

        checkMarkArray[(int)GameStateManager.instance.Language - 1].SetActive(true);
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

    public void BackgroundEffectOnOff()
    {
        if (GameStateManager.instance.BackgroundEffect)
        {
            GameStateManager.instance.BackgroundEffect = false;
        }
        else
        {
            GameStateManager.instance.BackgroundEffect = true;
        }

        OnBackgroundEffect();
    }

    public void RecoverOnOff()
    {
        if (GameStateManager.instance.Recover)
        {
            GameStateManager.instance.Recover = false;
        }
        else
        {
            GameStateManager.instance.Recover = true;
        }

        OnRecover();
    }

    public void OnBGM()
    {
        if (GameStateManager.instance.Music)
        {
            musicText.localizationName = "ON";
            musicText.ReLoad();

            musicButtonImg.sprite = buttonImgArray[0];

            SoundManager.instance.PlayBGM();
        }
        else
        {
            musicText.localizationName = "OFF";
            musicText.ReLoad();

            musicButtonImg.sprite = buttonImgArray[1];

            SoundManager.instance.StopBGM();
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

            GameManager.instance.SetParticle(true);
        }
        else
        {
            effectText.localizationName = "OFF";
            effectText.ReLoad();

            effectButtonImg.sprite = buttonImgArray[1];

            GameManager.instance.SetParticle(false);
        }
    }

    public void OnBackgroundEffect()
    {
        if (GameStateManager.instance.BackgroundEffect)
        {
            backgroundEffectText.localizationName = "ON";
            backgroundEffectText.ReLoad();

            backgroundEffectButtonImg.sprite = buttonImgArray[0];

            GameManager.instance.BackgroundEffect(true);
        }
        else
        {
            backgroundEffectText.localizationName = "OFF";
            backgroundEffectText.ReLoad();

            backgroundEffectButtonImg.sprite = buttonImgArray[1];

            GameManager.instance.BackgroundEffect(false);
        }
    }

    public void OnRecover()
    {
        if (GameStateManager.instance.Recover)
        {
            recoverText.localizationName = "ON";
            recoverText.ReLoad();

            recoverButtonImg.sprite = buttonImgArray[0];
        }
        else
        {
            recoverText.localizationName = "OFF";
            recoverText.ReLoad();

            recoverButtonImg.sprite = buttonImgArray[1];
        }
    }

    public void SuccessGoogleLink()
    {
        googleLink.SetActive(false);
    }

    public void SuccessAppleLink()
    {
        appleLink.SetActive(false);
    }
}
