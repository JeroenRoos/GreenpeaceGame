using System;
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

    public AudioClip backgroundSong1;

    public AudioClip buttonHoverSFX;
    public AudioClip ButtonClickSFX;
    public AudioClip newmonthSFX;
    #endregion

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        else
        {

            AudioSource[] audioSources = GetComponents<AudioSource>();
            backgroundMusic = audioSources[0];
            soundEffect = audioSources[1];

            backgroundSong1 = Resources.Load("Sounds/music/LugiaTheme", typeof(AudioClip)) as AudioClip;
            buttonHoverSFX = Resources.Load("Sounds/sfx/btnhoverSFX", typeof(AudioClip)) as AudioClip;
            ButtonClickSFX = Resources.Load("Sounds/sfx/btnclickSFX", typeof(AudioClip)) as AudioClip;
            newmonthSFX = Resources.Load("Sounds/sfx/newmonthSFX", typeof(AudioClip)) as AudioClip;
            instance = this;

            EventManager.PlayBackgroundMusic += PlayBackgroundMusic;
            EventManager.PlayButtonClickSFX += PlayButtonClickSFX;
            EventManager.PlayButtonHoverSFX += PlayButtonHoverSFX;
            EventManager.ChangeMonth += PlayNewMonthSFX;

            backgroundMusic.loop = true;
            backgroundMusic.clip = backgroundSong1;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayBackgroundMusic()
    {
        if (!backgroundMusic.isPlaying)
            backgroundMusic.Play();
    }

    public void PlayButtonClickSFX()
    {
        soundEffect.PlayOneShot(ButtonClickSFX);
    }

    public void PlayButtonHoverSFX()
    {
        soundEffect.PlayOneShot(buttonHoverSFX);
    }

    public void PlayNewMonthSFX()
    {
        soundEffect.PlayOneShot(newmonthSFX);
    }
}
