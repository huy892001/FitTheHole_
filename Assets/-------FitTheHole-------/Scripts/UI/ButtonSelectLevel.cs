using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSelectLevel : MonoBehaviour
{
    public void ForwardToSceneSelectLevel()
    {
        SoundManager.Instance.PlaySoundButton();
        SceneManager.LoadSceneAsync("SelectLevel");
    }
}
