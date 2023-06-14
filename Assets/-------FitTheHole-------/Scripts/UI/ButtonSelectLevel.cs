using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSelectLevel : MonoBehaviour
{
    public void ForwardToSceneSelectLevel()
    {
        SoundManager.Instance.PlaySoundButton();
        StartCoroutine(WaitToChangeScene());
        //SceneManager.LoadSceneAsync("SelectLevel");
    }

    IEnumerator WaitToChangeScene()
    {
        ButtonInHome.Instance.MoveButtonToOutScene();
        yield return new WaitForSeconds(0.7f);
        TransitionEffect.Instance.Show("SelectLevel");
    }
}
