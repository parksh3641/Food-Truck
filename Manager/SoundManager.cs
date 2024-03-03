using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicAudio;
    public AudioSource musicAudio2;

    public AudioClip musicFever;
    public AudioClip boss;

    public AudioClip[] musicArray;

    public AudioSource[] sfxAudio;

    private bool fever = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (GameStateManager.instance.Music)
        {
            PlayGameBGM();

            musicAudio2.Play();
        }
    }

    public void PlayGameBGM()
    {
        StartCoroutine(PlayList());
    }

    IEnumerator PlayList()
    {
        while (true)
        {
            if (!musicAudio.isPlaying)
            {
                musicAudio.Stop();
                musicAudio.clip = musicArray[Random.Range(0, musicArray.Length)];
                musicAudio.Play();
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void PlayBGM()
    {
        musicAudio.volume = 1;

        if(!fever)
        {
            if (!musicAudio.isPlaying)
            {
                StartCoroutine(PlayList());
            }

            musicAudio2.Play();
        }
    }

    public void StopBGM()
    {
        musicAudio.volume = 0;

        musicAudio2.Stop();
    }

    public void PlaySFX(GameSfxType type)
    {
        if (!GameStateManager.instance.Sfx) return;

        for (int i = 0; i < sfxAudio.Length; i++)
        {
            if (sfxAudio[i].name.Equals(type.ToString()))
            {
                //if (!sfxAudio[i].isPlaying) sfxAudio[i].Play();
                sfxAudio[i].Play();
            }
        }
    }

    public void PlayFever()
    {
        fever = true;

        if (!GameStateManager.instance.Music) return;

        musicAudio.Stop();
        musicAudio.clip = musicFever;
        musicAudio.Play();
    }

    public void StopFever()
    {
        fever = false;

        ResetBGM();
    }

    public void PlayBoss()
    {
        if (!GameStateManager.instance.Music) return;

        musicAudio.Stop();
        musicAudio.clip = boss;
        musicAudio.Play();
    }

    public void StopBoss()
    {
        ResetBGM();
    }

    public void ResetBGM()
    {
        musicAudio.Stop();
        StopAllCoroutines();
        StartCoroutine(PlayList());
    }
}
