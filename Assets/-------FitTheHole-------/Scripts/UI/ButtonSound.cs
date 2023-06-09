using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private GameObject buttonSoundOn, buttonSoundOff;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Button"))
        {
            if (PlayerPrefs.GetInt("Button") == 0)
            {
                buttonSoundOn.SetActive(true);
                buttonSoundOff.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Button") == 1)
            {
                buttonSoundOn.SetActive(false);
                buttonSoundOff.SetActive(true);
            }
        }
    }
    public void SetButtonSoundOn()
    {
        SoundManager.Instance.PlaySoundButton();
        buttonSoundOn.SetActive(true);
        buttonSoundOff.SetActive(false);
        PlayerPrefs.SetInt("Button",0);
        SoundManager.Instance.muteAudioSoureOfFireWork = false;
        SoundManager.Instance.audioSound.mute = false;
    }
    public void SetButtonSoundOff()
    {
        SoundManager.Instance.PlaySoundButton();
        buttonSoundOn.SetActive(false);
        buttonSoundOff.SetActive(true);
        PlayerPrefs.SetInt("Button", 1);
        SoundManager.Instance.audioSound.mute = true;
        SoundManager.Instance.muteAudioSoureOfFireWork = true;
    }
}
