using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHome : MonoBehaviour
{
    public void ForwardToSceneHome()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.PlaySoundButton();
        SceneManager.LoadSceneAsync("Home");
    }
}
