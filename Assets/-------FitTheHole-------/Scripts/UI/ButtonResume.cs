using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonResume : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public void CloseThePausePanel()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }
}
