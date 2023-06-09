using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInSelectLevel : MonoBehaviour
{
    [SerializeField] private AudioClip musicInSelectLevel;
    void Start()
    {
        MusicManager.Instance.audioMusic.clip = musicInSelectLevel;
        MusicManager.Instance.audioMusic.Play();
    }
}
