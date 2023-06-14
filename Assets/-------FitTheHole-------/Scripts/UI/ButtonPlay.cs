using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : MonoBehaviour
{
    public void ForwardToSceneGameplay()
    {
        SoundManager.Instance.PlaySoundButton();
        //LoadingScene.Instance.Show("Gameplay");
        SceneManager.LoadSceneAsync("Gameplay 1");
    }
}
