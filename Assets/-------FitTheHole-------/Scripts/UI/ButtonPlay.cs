using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ButtonPlay : MonoBehaviour
{
    public void ForwardToSceneGameplay()
    {
        SoundManager.Instance.PlaySoundButton();
        DataManager.instance.ShowInter();
        StartCoroutine(WaitToChangeScene());
        //SceneManager.LoadSceneAsync("Gameplay 1");
    }
    
    IEnumerator WaitToChangeScene()
    {
        ButtonInHome.Instance.MoveButtonToOutScene();
        yield return new WaitForSeconds(0.7f);
        TransitionEffect.Instance.Show("Gameplay");
    }
}
