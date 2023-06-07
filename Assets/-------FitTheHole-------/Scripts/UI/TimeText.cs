using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeText : MonoBehaviour
{
    private float timeLeftToPlay = 31;
    private TimeSpan time;
    [SerializeField] private TMP_Text timeRemainingText, textLevelNumber;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            textLevelNumber.text = "Level " + PlayerPrefs.GetInt("Level");
        }
        else
        {
            textLevelNumber.text = "Level 1";
        }
    }
    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.Start)
        {
            if (timeLeftToPlay > 0)
            {
                timeLeftToPlay -= UnityEngine.Time.deltaTime;
                time = TimeSpan.FromSeconds(timeLeftToPlay);
                timeRemainingText.text = time.ToString(@"mm\:ss");
            }
            else if (timeLeftToPlay <= 0)
            {
                GameManager.Instance.UpdateState(GameState.Lose);
            }
        }
    }
}
