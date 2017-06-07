using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//singleton AudioPlayer gameobject
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

            music.Add(Resources.Load("Sounds/music/funnysong", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/clearday", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/anewbeginning", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/acousticbreeze", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/goinghigher", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/jazzcomedy", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/jazzyfrenchy", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/love", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/retrosoul", typeof(AudioClip)) as AudioClip);
            music.Add(Resources.Load("Sounds/music/slowmotion", typeof(AudioClip)) as AudioClip);
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

        backgroundMusic.clip = music[0];
        backgroundMusic.Play();
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

    #region PlaySoundEffectsMethods
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
    #endregion

    #region ChangeVolumeMethods
    public void changeVolumeMusic(float value)
    {
        backgroundMusic.volume = value;
    }

    public void changeVolumeEffects(float value)
    {
        soundEffect.volume = value;
    }
    #endregion
}
