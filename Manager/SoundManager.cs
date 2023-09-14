using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicAudio;

    public AudioClip musicFever;

    public AudioClip[] musicArray;

    public AudioSource[] sfxAudio;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (GameStateManager.instance.Music)
        {
            PlayGameBGM();
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
    }

    public void StopBGM()
    {
        musicAudio.volume = 0;
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
        musicAudio.Stop();
        musicAudio.clip = musicFever;
        musicAudio.Play();
    }

    public void StopFever()
    {
        musicAudio.Stop();
        musicAudio.clip = musicArray[Random.Range(0, musicArray.Length)];
        musicAudio.Play();
    }
}
