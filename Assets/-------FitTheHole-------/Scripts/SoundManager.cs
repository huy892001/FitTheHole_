using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSound;
    public int indexOfAudioTextPraise;
    [SerializeField] private List<audioSoundTextPraiseOfCharacter> audioSoundTextPraise = new List<audioSoundTextPraiseOfCharacter>();
    [SerializeField] private AudioClip audioClipOfButton, audioSoundStateChangeCharacter;
    public AudioClip audioClipAnimationRunning,
                                       audioClipAnimationHit,
                                       audioClipWinPanel,
                                       audioClipLosePanel,
                                       audioClipHunterAngry,
                                       audioClipHunterStomping;
    public bool muteAudioSoureOfFireWork = true;
    private void Start()
    {
        SetMuteOnSoundManager();
    }

    public void SetMuteOnSoundManager()
    {
        if (PlayerPrefs.HasKey("Button"))
        {
            if (PlayerPrefs.GetInt("Button") == 1)
            {
                audioSound.mute = true;
                muteAudioSoureOfFireWork = true;
            }
            else if (PlayerPrefs.GetInt("Button") == 0)
            {
                audioSound.mute = false;
                muteAudioSoureOfFireWork = false;
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
        audioSound.PlayOneShot(audioSoundTextPraise[indexOfAudioTextPraise].audioSoundTextPraise);
    }
    public void PlaySoundHunterAngry()
    {
        audioSound.PlayOneShot(audioClipHunterAngry);
    }
    public void PlaySoundHunterStomping()
    {
        audioSound.PlayOneShot(audioClipHunterStomping);
    }
}

[Serializable]
public class audioSoundTextPraiseOfCharacter
{
    [HorizontalGroup("Background Chapter", 75)]
    [PreviewField(50)]
    [HideLabel]
    public Sprite Icon;
    [VerticalGroup("Background Chapter/Chapter")]
    [LabelWidth(100)]
    public AudioClip audioSoundTextPraise;
}

