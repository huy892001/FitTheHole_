using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMusic : MonoBehaviour
{
    [SerializeField] private GameObject buttonMusicOn, buttonMusicOff;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            if (PlayerPrefs.GetInt("Music") == 0)
            {
                buttonMusicOn.SetActive(true);
                buttonMusicOff.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Music") == 1)
            {
                buttonMusicOn.SetActive(false);
                buttonMusicOff.SetActive(true);
            }
        }
    }
    public void SetButtonSoundOn()
    {
        SoundManager.Instance.PlaySoundButton();
        buttonMusicOn.SetActive(true);
        buttonMusicOff.SetActive(false);
        PlayerPrefs.SetInt("Music", 0);
        MusicManager.Instance.audioMusic.mute = false;
    }
    public void SetButtonSoundOff()
    {
        SoundManager.Instance.PlaySoundButton();
        buttonMusicOn.SetActive(false);
        buttonMusicOff.SetActive(true);
        PlayerPrefs.SetInt("Music", 1);
        MusicManager.Instance.audioMusic.mute = true;
    }
}
