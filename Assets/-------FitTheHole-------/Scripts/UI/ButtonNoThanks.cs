using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNoThanks : MonoBehaviour
{
    public void ForwardToSceneGameplayNextLevel()
    {
        SoundManager.Instance.PlaySoundButton();
        DataManager.instance.ShowInter();
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        if (PlayerPrefs.GetInt("Level") < 25)
        {
            TransitionEffect.Instance.Show("Gameplay");
            //SceneManager.LoadSceneAsync("Gameplay");
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("Chapter", 1);
            TransitionEffect.Instance.Show("Gameplay");
            //SceneManager.LoadSceneAsync("Home");            
        }
    }
}
