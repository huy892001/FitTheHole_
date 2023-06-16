using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonPause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public void OpenThePausePanel()
    {
        SoundManager.Instance.PlaySoundButton();
        DataManager.instance.ShowInter();
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
}
