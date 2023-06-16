using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionEffect : Singleton<TransitionEffect>
{
    [SerializeField] private Image _blackBg;
    private string scenes;
    [SerializeField] private GameObject textLoading;
    [SerializeField] private float _durationOffScreen = 0.7f;
    [SerializeField] private float _durationOnScreen = 0.3f;

    public void Show(string nameScene)
    {
        scenes = nameScene;
        Loading();
    }

    void LoadingAnimOpen()
    {
        PlayAnim();
    }

    void LoadingAnimClose()
    {
        // anim.enabled = true;
        _blackBg.color = Color.black;
        _blackBg.DOColor(Color.clear, _durationOnScreen);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        // gameObject.SetActive(true);
        // anim.Play("LoadingAnim_Close");
    }

    private void PlayAnim()
    {
        //  anim.enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        _blackBg.color = Color.clear;
        _blackBg.DOColor(Color.black, _durationOffScreen);
        // gameObject.SetActive(true);
        //   anim.Play("LoadingAnim");
    }

    void Loading()
    {
        //Debug.Log(2);
        StartCoroutine(DelayLoadingGame());
    }

    IEnumerator DelayLoadingGame()
    {  
        LoadingAnimOpen();
        Debug.Log("truoc khi delay");
        textLoading.SetActive(true);
        textLoading.transform.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(_durationOffScreen);


        //   anim.Play("IdleLoad");
        /*    AsyncOperation aop = SceneManager.LoadSceneAsync(scenes);
    
            while (!aop.isDone)
            {
                yield return null;
            }*/
        //Debug.Log(3);

        Debug.Log("bat dau load scene");
        SceneManager.LoadSceneAsync(scenes);
        

        yield return new WaitForSeconds(0.1f);
        //  aop.allowSceneActivation = true;
#if UNITY_ANDROID
        Resources.UnloadUnusedAssets();
#endif
        textLoading.SetActive(false);
        LoadingAnimClose();
    }
}