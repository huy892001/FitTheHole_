using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoundAnimationOfHunter : MonoBehaviour
{

    [SerializeField] private AudioSource audioSourceOfHunter;
    [SerializeField]
    private AudioClip audioClipAnimationRunning,
                                       audioClipAnimationHit,
                                       audioClipWinPanel,
                                       audioClipLosePanel;
    void Start()
    {
        GameManager.Instance.animationOfHuntCharacter.AnimationState.Start += HandleEventStart;
        GameManager.Instance.animationOfHuntCharacter.AnimationState.Event += HandleEventStop;
        GameManager.Instance.animationOfHuntCharacter.AnimationState.Event += HandleEventRun;
        GameManager.Instance.animationOfHuntCharacter.AnimationState.Event += HandleEventHit;
        GameManager.Instance.animationOfHuntCharacter.AnimationState.Complete += HandleEventEnd;
    }

    void HandleEventStart(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "Win" || trackEntry.Animation.Name == "Lose")
        {
            audioSourceOfHunter.PlayOneShot(audioClipAnimationRunning);
        }
    }

    void HandleEventStop(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "dung" && (trackEntry.Animation.Name == "Win" || trackEntry.Animation.Name == "Lose"))
        {
            audioSourceOfHunter.Stop();
        }
    }
    void HandleEventRun(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "di" && (trackEntry.Animation.Name == "Win" || trackEntry.Animation.Name == "Lose"))
        {
            audioSourceOfHunter.PlayOneShot(audioClipAnimationRunning);
        }
    }

    void HandleEventHit(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "dap" && (trackEntry.Animation.Name == "Lose"))
        {
            audioSourceOfHunter.PlayOneShot(audioClipAnimationHit);
        }
    }

    void HandleEventEnd(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "Win")
        {
            audioSourceOfHunter.PlayOneShot(audioClipWinPanel);
            GameManager.Instance.WinPanel.SetActive(true);
        }
        else if (trackEntry.Animation.Name == "Lose")
        {
            audioSourceOfHunter.PlayOneShot(audioClipLosePanel);
            GameManager.Instance.LosePanel.SetActive(true);
        }
    }
}
