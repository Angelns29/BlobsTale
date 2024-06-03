using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManagerScript : MonoBehaviour
{
    [NonSerialized] public static AudioManagerScript instance;
    [Header("------------Audio Source --------------")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    [Header("------------Audio Clips -------------")]
    public AudioClip menuTheme;
    public AudioClip gameTheme;
    public AudioClip gameOverTheme;
    public AudioClip attack;
    public AudioClip jump;
    public AudioClip collectCoin;
    public AudioClip killEnemy;

    private void OnEnable()
    {
        StartCoroutine(InitializeAudio());
    }

    private IEnumerator InitializeAudio()
    {
        
        yield return null;

        if (GameManager.Instance.isThereSaveData)
        {
           
            musicSource.volume = GameManager.Instance.musicSliderValue;
            sfxSource.volume = GameManager.Instance.SFXSliderValue;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else Destroy(gameObject);
    }

    //public void SetMixerOnStart(float volume)
    //{
    //    musicMixer.SetFloat("MusicVolume", volume);
    //}

    void Start()
    {
        musicSource.clip = menuTheme;
        musicSource.Play();
    }

    public void StartGame()
    {
        musicSource.Stop();
        musicSource.clip = gameTheme;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void StartMenuTheme()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
        musicSource.clip = menuTheme;
        musicSource.Play();
    }

    public void StartGameOverTheme()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
        musicSource.clip = gameOverTheme;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip audio)
    {
        sfxSource.PlayOneShot(audio);
    }
}