using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSound;
    [SerializeField] private AudioClip audioClipOfButton, audioSoundStateChangeCharacter, audioSoundTextPraise;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Button"))
        {
            if (PlayerPrefs.GetInt("Button") == 1)
            {
                audioSound.mute = true;
            }
            else if (PlayerPrefs.GetInt("Button") == 0)
            {
                audioSound.mute = false;
            }
        }
    }

    public void PlaySoundButton()
    {
        audioSound.PlayOneShot(audioClipOfButton);
    }

    public void PlaySoundStateChange()
    {
        audioSound.PlayOneShot(audioSoundStateChangeCharacter);
    }

    public void PlaySoundTextPraise()
    {
        audioSound.PlayOneShot(audioSoundTextPraise);
    }
}
