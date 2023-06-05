using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInGameplay : MonoBehaviour
{
    [SerializeField] private AudioClip musicInGameplay;
    void Start()
    {
        MusicManager.Instance.audioMusic.clip = musicInGameplay;
        MusicManager.Instance.audioMusic.Play();
    }

}
