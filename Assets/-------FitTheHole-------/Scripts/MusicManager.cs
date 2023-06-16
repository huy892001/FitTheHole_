using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public AudioSource audioMusic;
    private void Start()
    {
        SetMuteOnMusicManager();
    }

    public void SetMuteOnMusicManager()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            if (PlayerPrefs.GetInt("Music") == 1)
            {
                audioMusic.mute = true;
            }
            else if (PlayerPrefs.GetInt("Music") == 0)
            {
                audioMusic.mute = false;
            }
        }
    }
}
