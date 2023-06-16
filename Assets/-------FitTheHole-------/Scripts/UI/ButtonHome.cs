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
        DataManager.instance.ShowInter();
        TransitionEffect.Instance.Show("Home");
        //SceneManager.LoadSceneAsync("Home");
    }
}
