using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInHome : MonoBehaviour
{
    [SerializeField] private AudioClip musicInHome;
    void Start()
    {
        MusicManager.Instance.audioMusic.clip = musicInHome;
        MusicManager.Instance.audioMusic.Play();
    }
}
