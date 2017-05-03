using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private static AudioPlayer instance = null;
    public static AudioPlayer Instance
    {
        get { return instance; }
    }

    #region audio
    public AudioSource backgroundMusic;
    public AudioSource soundEffect;

    public List<AudioClip> music;

    public AudioClip optionSelectSFX;
    public AudioClip ButtonClickSFX;
    public AudioClip newmonthSFX;
    #endregion

    System.Random rnd;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        else
        {
            rnd = new System.Random();
            music = new List<AudioClip>();
            AudioSource[] audioSources = GetComponents<AudioSource>();
            backgroundMusic = audioSources[0];
            soundEffect = audioSources[1];

            music.Add(Resources.Load("Sounds/music/LugiaTheme", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/AvatarTheme", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/MindHeist", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/SearchForTheGirlTheme", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/TheLastOfUsTheme", typeof(AudioClip)) as AudioClip);
            optionSelectSFX = Resources.Load("Sounds/sfx/btnhoverSFX", typeof(AudioClip)) as AudioClip;
            ButtonClickSFX = Resources.Load("Sounds/sfx/btnclickSFX", typeof(AudioClip)) as AudioClip;
            newmonthSFX = Resources.Load("Sounds/sfx/newmonthSFX", typeof(AudioClip)) as AudioClip;
            instance = this;

            EventManager.PlayBackgroundMusic += StartBackgroundMusic;
            EventManager.PlayButtonClickSFX += PlayButtonClickSFX;
            EventManager.PlayOptionSelectSFX += PlayOptionSelectSFX;
            EventManager.PlayNewTurnStartSFX += PlayNewMonthSFX;
            
            DontDestroyOnLoad(this.gameObject);
        }

    }

    public void StartBackgroundMusic()
    {
        //can't be removed >>> playerprefs error
    }

    void Update()
    {
        if (!backgroundMusic.isPlaying)
        {
            AudioClip newSong = music[rnd.Next(0, music.Count)];
            if (newSong != backgroundMusic.clip)
            {
                backgroundMusic.clip = newSong;
                backgroundMusic.Play();
            }
        }
    }

    public void PlayButtonClickSFX()
    {
        soundEffect.PlayOneShot(ButtonClickSFX);
    }

    public void PlayOptionSelectSFX()
    {
        soundEffect.PlayOneShot(optionSelectSFX);
    }

    public void PlayNewMonthSFX()
    {
        soundEffect.PlayOneShot(newmonthSFX);
    }

    public void changeVolumeMusic(float value)
    {
        backgroundMusic.volume = value;
    }

    public void changeVolumeEffects(float value)
    {
        soundEffect.volume = value;
    }
}
