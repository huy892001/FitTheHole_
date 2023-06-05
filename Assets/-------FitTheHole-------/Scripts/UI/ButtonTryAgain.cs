using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTryAgain : MonoBehaviour
{
    public void HandlerTryAgain()
    {
        SoundManager.Instance.PlaySoundButton();
        SceneManager.LoadSceneAsync("Gameplay");
    }
}
