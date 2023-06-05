using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNoThanks : MonoBehaviour
{
    public void ForwardToSceneGameplayNextLevel()
    {
        SoundManager.Instance.PlaySoundButton();
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadSceneAsync("Gameplay");
    }
}
