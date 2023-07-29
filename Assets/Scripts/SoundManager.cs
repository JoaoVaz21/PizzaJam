using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager _instance;
    public static SoundManager Instance => _instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    #endregion Singleton

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioClip[] musicsList;

    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioClip[] sfxList;

    protected virtual void Start()
    {
        // If the game starts in a menu scene, play the appropriate music
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolumeValue", 1);
        sfxSource.volume = PlayerPrefs.GetFloat("SfxVolumeValue", 1);

        // PlayMusic(0);
    }

    public static void PlayMusic(int index)
    {
        if (_instance != null)
        {
            if (_instance.musicSource != null)
            {
                _instance.musicSource.Stop();
                _instance.musicSource.clip = _instance.musicsList[index];
                _instance.musicSource.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    public static void PlaySFX(int index)
    {
        if (_instance != null)
        {
            if (_instance.sfxSource != null)
            {
                _instance.sfxSource.Stop();
                _instance.sfxSource.clip = _instance.sfxList[index];
                _instance.sfxSource.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable SFXPlayer component");
        }
    }

    public void UpdateMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolumeValue", musicSource.volume);
    }

    public void UpdateSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SfxVolumeValue", sfxSource.volume);
    }
}